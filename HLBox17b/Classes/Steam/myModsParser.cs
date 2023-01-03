using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;
using HLBox17b.Classes.Files;


namespace HLBox17b
	{
	class myModsParser
		{
		private string szFullPath;
		private Dictionary<string, Dictionary<string, string>> ModsCfg;
		private readonly Regex _sectionRegex = new Regex(@"(?<=\[)(?<SectionName>[^\]]+)(?=\])");
		private readonly Regex _keyValueRegex = new Regex(@"(?<Key>[^=]+)=(?<Value>.+)");
		public bool bIsOk;

		public myModsParser(string szpath)
			{
			ModsCfg = new Dictionary<string, Dictionary<string, string>>();
			szFullPath = szpath;
			bIsOk = true;
			if (szFullPath != null)
				{
				if (!Load(szFullPath))
					bIsOk = false;
				}
			else
				bIsOk = false;
			}


		public string GetValue(string sectionName, string key)
			{
			if (ModsCfg.ContainsKey(sectionName) && ModsCfg[sectionName].ContainsKey(key))
				return ModsCfg[sectionName][key];
			else
				return null;
			}

		public void SetValue(string sectionName, string key, string value)
			{
			if (!ModsCfg.ContainsKey(sectionName))
				ModsCfg[sectionName] = new Dictionary<string, string>();
			ModsCfg[sectionName][key] = value;
			}

		public Dictionary<string, string> GetSection(string sectionName)
			{
			if (ModsCfg.ContainsKey(sectionName))
				return new Dictionary<string, string>(ModsCfg[sectionName]);
			else
				return new Dictionary<string, string>();
			}

		public void SetSection(string sectionName, IDictionary<string, string> sectionValues)
			{
			if (sectionValues == null)
				return;
			ModsCfg[sectionName] = new Dictionary<string, string>(sectionValues);
			}

		protected bool LineContainsAComment(string line)
			{
			Regex CommentRegex = new Regex("#.*");
			return !string.IsNullOrEmpty(line) && CommentRegex.Match(line).Success;
			}

		protected string ExtractComment(string line)
			{
			Regex CommentRegex = new Regex("#.*");
			string comment = CommentRegex.Match(line).Value.Trim();
			return line.Replace(comment, "").Trim();
			}

		public bool Load(string filename)
			{
			string cleanline = "";
			if (File.Exists(filename))
				{
				try
					{
					var content = File.ReadAllLines(filename);
					string currentSectionName = string.Empty;
					string realSectionName = string.Empty;
					foreach (var line in content)
						{
						cleanline = line;
						if (LineContainsAComment(line))
							cleanline = ExtractComment(line);
						Match m = _sectionRegex.Match(cleanline);
						if (m.Success)
							{
							currentSectionName = ((m.Groups["SectionName"].Value).ToLower()).Trim();
							realSectionName = (m.Groups["SectionName"].Value).Trim();
							}
						else
							{
							m = _keyValueRegex.Match(cleanline);
							if (m.Success)
								{
								string key = ((m.Groups["Key"].Value).ToLower()).Trim(); ;
								string value = ((m.Groups["Value"].Value).ToLower()).Trim(); ;

								Dictionary<string, string> kvpList;
								if (ModsCfg.ContainsKey(currentSectionName))
									{
									kvpList = ModsCfg[currentSectionName];
									}
								else
									{
									kvpList = new Dictionary<string, string>();
									kvpList["modname"] = realSectionName;
									}
								kvpList[key] = value;
								ModsCfg[currentSectionName] = kvpList;
								}
							}
						}
					return true;
					}
				catch
					{
					return false;
					}

				}
			else
				{
				return false;
				}
			}

		public List<Dictionary<string, string>> GetModFromIdent(string ident)
			{
			var LstMods = new List<Dictionary<string, string>>();
			Dictionary<string, string> ModTmp = new Dictionary<string, string>();
			foreach (var sectionName in ModsCfg)
				{
				foreach (var keyValue in sectionName.Value)
					{
					if (keyValue.Key == "ident" && keyValue.Value == ident.ToLower())
						{
						LstMods.Add(GetSection(sectionName.Key));
						}
					}
				}
			return LstMods;
			}


		public List<Dictionary<string, string>> GetModFromPath(string szPath)
			{
			var LstMods = new List<Dictionary<string, string>>();
			Regex OldPath0;
			Regex NewPath0;
			Regex StmPath0;
			Dictionary<string, string> ModTmp = new Dictionary<string, string>();

			szPath = szPath.Replace("\\", "/");

			foreach (var sectionName in ModsCfg)
				{
				if (!sectionName.Value.ContainsKey("ident"))
					continue;
				if (!Program.mRc.bGens_NoSteam && sectionName.Value.ContainsKey("oldpath"))
					{
					OldPath0 = new Regex(sectionName.Value["oldpath"] + "/" + sectionName.Value["ident"] + "(_[^/]*)/{0,1}", RegexOptions.IgnoreCase);

					Match mO = OldPath0.Match(szPath);
					if (mO.Success)
						{
						LstMods.Add(GetSection(sectionName.Key));
						continue;
						}
					}
				if (!Program.mRc.bGens_NoSteam && sectionName.Value.ContainsKey("newpath"))
					{
					NewPath0 = new Regex(sectionName.Value["newpath"] + "/" + sectionName.Value["ident"] + "(_[^/]*)?/", RegexOptions.IgnoreCase);
					Match mN = NewPath0.Match(szPath);
					if (mN.Success)
						{
						LstMods.Add(GetSection(sectionName.Key));
						continue;
						}
					}
				if (Program.mRc.bGens_NoSteam && sectionName.Value.ContainsKey("ident"))
					{
					StmPath0 = new Regex("/" + sectionName.Value["ident"] + "(_[^/]*)?/", RegexOptions.IgnoreCase);
					Match mL = StmPath0.Match(szPath);
					if (mL.Success)
						{
						LstMods.Add(GetSection(sectionName.Key));
						continue;
						}
					StmPath0 = new Regex("/" + sectionName.Value["ident"] + "$", RegexOptions.IgnoreCase);
					Match mE = StmPath0.Match(szPath);
					if (mE.Success)
						{
						LstMods.Add(GetSection(sectionName.Key));
						continue;
						}
					}
				}
			if (LstMods.Count == 0)
				{
				foreach (var sectionName in ModsCfg)
					{
					if (!sectionName.Value.ContainsKey("ident"))
						continue;
					if (sectionName.Value.ContainsKey("ident"))
						{
						StmPath0 = new Regex("/" + sectionName.Value["ident"] + "/", RegexOptions.IgnoreCase);
						Match mS = StmPath0.Match(szPath);
						if (mS.Success)
							{
							LstMods.Add(GetSection(sectionName.Key));
							continue;
							}
						StmPath0 = new Regex("/" + sectionName.Value["ident"] + "(_[^/]*)/{0,1}", RegexOptions.IgnoreCase);
						Match mL = StmPath0.Match(szPath);
						if (mL.Success)
							{
							LstMods.Add(GetSection(sectionName.Key));
							continue;
							}
						StmPath0 = new Regex("/" + sectionName.Value["ident"] + "$", RegexOptions.IgnoreCase);
						Match mE = StmPath0.Match(szPath);
						if (mE.Success)
							{
							LstMods.Add(GetSection(sectionName.Key));
							continue;
							}
						}
					}
				}
			return LstMods;
			}


		public Dictionary<string, string> GetModFromAppID(uint appID)
			{
			return GetModFromAppID(appID.ToString());
			}

		public Dictionary<string, string> GetModFromAppID(string appID)
			{
			Dictionary<string, string> ModTmp = new Dictionary<string, string>();

			foreach (var sectionName in ModsCfg)
				{
				foreach (var keyValue in sectionName.Value)
					{
					if (keyValue.Key.ToLower() == "appid" && keyValue.Value == appID.ToLower())
						{
						return GetSection(sectionName.Key);
						}
					}
				}
			return ModTmp;
			}

		public List<ModName> GetAllMods(uint Game, bool bMustHaveFolder)
			{
			List<ModName> arLstMods = new List<ModName>();
			ModName ModTmp;
			foreach (var sectionName in ModsCfg)
				{
				foreach (var keyValue in sectionName.Value)
					{
					if (keyValue.Key == "modname")
						{
						int nGame = Convert.ToInt32(GetValue(sectionName.Key.ToString(), "hlversion"));
						if (nGame == Game)
							{
							int appID = Convert.ToInt32(GetValue(sectionName.Key.ToString(), "appid"));
							if (bMustHaveFolder)
								{
								myMods objMod = new myMods((uint)appID, false);
								if (objMod.bIsOk)
									{
									try
										{
										if (objMod.sTargetPath == string.Empty)
											continue;
										DirectoryInfo cdi = new DirectoryInfo(objMod.sTargetPath);
										if (!cdi.Exists)
											continue;
										}
									catch (Exception)
										{
										continue;
										}
									}
								}
							ModTmp = new ModName(keyValue.Value, appID);
							if (!arLstMods.Contains(ModTmp))
								arLstMods.Add(ModTmp);
							}
						}
					}
				}
			return arLstMods;
			}

		public List<ModName> GetAllMods()
			{
			List<ModName> arLstMods = new List<ModName>();
			ModName ModTmp;
			foreach (var sectionName in ModsCfg)
				{
				foreach (var keyValue in sectionName.Value)
					{
					if (keyValue.Key == "modname")
						{
						int appID = Convert.ToInt32(GetValue(sectionName.Key.ToString(), "appid"));
						ModTmp = new ModName(keyValue.Value, appID);
						if (!arLstMods.Contains(ModTmp))
							arLstMods.Add(ModTmp);
						}
					}
				}
			return arLstMods;
			}
		}

	}
