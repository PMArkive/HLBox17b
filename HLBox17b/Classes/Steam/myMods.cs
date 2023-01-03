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
	class myMods
		{
		public string sProvidedPath;
		public string sModName;
		public uint nHLVersion;
		public string sHLVersion;
		public string sIdent;
		public uint nAppID;
		public string sAppID;
		public List<string> sAllIds;
		public List<uint> nAllIds;
		public string sOldPath;
		public string sNewPath;
		public bool bIsOk;
		public bool bHasAppManifest;
		public string sAppManifestFile;
		public string sModInstallDir;
		public string sCfgInstallDir;
		public List<string> sDepotsFiles;
		public double dlastManifestUpdate;
		public double dlastCacheUpdate;
		public string sCacheFls;
		public string sCacheTxs;
		public bool bHasCacheFile;
		public bool bMustUpdateCache;
		public string sModRootPath;
		public string sModMainRoot;
		public List<string> SystemFiles;
		public List<WadFileInfo> WadsFiles;
		public string sTargetPath;
		public string sLanguage;
		public bool bUseSteamPipe;
		public uint nCustomType;

		public myMods(uint AppID, bool bLoadFiles)
			{
			sProvidedPath = string.Empty;
			GetFromAppID(AppID.ToString(), bLoadFiles);
			}

		public myMods(string myString, bool bLoadFiles)
			{
			int number;
			bool result = Int32.TryParse(myString, out number);
			if (result) //On cherche par appID
				{
				sProvidedPath = string.Empty;
				GetFromAppID(myString, bLoadFiles);
				}
			else
				{
				bool bIsPath = false;
				try
					{
					DirectoryInfo cdi = new DirectoryInfo(myString);
					if (cdi.Exists)
						{
						sProvidedPath = myString.Replace('/', '\\'); ;
						GetFromPath(myString, bLoadFiles);
						bIsPath = true;
						}
					else
						{
						if (myString.Contains('\\') || myString.Contains('/'))
							{
							sProvidedPath = myString.Replace('/', '\\'); ;
							GetFromPath(myString, bLoadFiles);
							bIsPath = true;
							}
						}
					}
				catch (Exception)
					{
					}
				if (!bIsPath)
					{
					sProvidedPath = string.Empty;
					GetFromIdent(myString, bLoadFiles);
					}
				}
			}

		public myMods(Dictionary<string, string> ModDatas, bool bLoadFiles)
			{
			InitDatas();
			GetInfos(ModDatas, bLoadFiles);
			}

		private void InitDatas()
			{
			bIsOk = true;
			sModName = string.Empty;
			nHLVersion = 0;
			sHLVersion = string.Empty;
			sIdent = string.Empty;
			nAppID = 0;
			sAppID = "";
			sAllIds = new List<string>();
			nAllIds = new List<uint>();
			sDepotsFiles = new List<string>();
			sOldPath = string.Empty;
			sNewPath = string.Empty;
			bHasAppManifest = false;
			sAppManifestFile = string.Empty;
			sModInstallDir = string.Empty;
			sCfgInstallDir = string.Empty;
			dlastManifestUpdate = 0;
			dlastCacheUpdate = 0;
			sCacheFls = string.Empty;
			sCacheTxs = string.Empty;
			bHasCacheFile = false;
			bMustUpdateCache = false;
			sModRootPath = string.Empty;
			sModMainRoot = string.Empty;
			SystemFiles = new List<string>();
			WadsFiles = new List<WadFileInfo>();
			sTargetPath = string.Empty;
			sLanguage = string.Empty;
			bUseSteamPipe = false;
			nCustomType = 0;
			}

		private void GetInfos(Dictionary<string, string> ModTmp, bool bLoadFiles)
			{
			ParseDictionary(ModTmp);
			CheckAppManifest();
			GetSteamConfigFilePathInfos();
			GetManifestFileInfos();
			GetCacheFlsInfos();
			GetModRootPath();
			CheckSteamPipe();
			if (bLoadFiles)
				GetSystemFiles();
			GetTargetPath();
			}

		private void CheckAppManifest()
			{
			if (nAppID == 0 || sAppID == string.Empty)
				return;

			if (Program.mRc.bGens_NoSteam)
				return;

			string pat = @"appmanifest_(?<appID>\d*)\.acf";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase);

			string szAcfPath = Program.mRc.szGen_SteamPath + "\\steamapps\\";

			try
				{
				DirectoryInfo cdi = new DirectoryInfo(szAcfPath);
				if (cdi.Exists)
					{
					string[] szAcfFilePaths = Directory.GetFiles(szAcfPath, "*.acf");

					foreach (string szfilename in szAcfFilePaths)
						{
						//Calculate appID
						Match m = r.Match(szfilename);
						if (!m.Success)
							continue;
						if (m.Groups["appID"].Value == sAppID)
							{
							bHasAppManifest = true;
							sAppManifestFile = szfilename;
							return;
							}
						}
					}
				}
			catch (Exception)
				{
				}
			}

		private void GetSteamConfigFilePathInfos()
			{
			if (nAppID == 0 || sAppID == string.Empty)
				return;

			if (Program.mRc.bGens_NoSteam)
				return;

			VDFFile obVdfConfig = new VDFFile();
			if (obVdfConfig.bIsOk)
				{
				string szTmp = obVdfConfig.GetAppInstallDir(nAppID);
				if (szTmp != string.Empty)
					sCfgInstallDir = szTmp;
				}
			}
		private void GetManifestFileInfos()
			{
			if (!bHasAppManifest || sAppManifestFile == string.Empty)
				return;

			//Open appID manifest file
			ACFFile acfile = new ACFFile(sAppManifestFile);
			sModInstallDir = acfile.GetAppInstallDir().Replace('/', '\\');
			if (sCfgInstallDir != string.Empty && sCfgInstallDir.ToLower() != sModInstallDir.ToLower())
				sModInstallDir = sCfgInstallDir;
			dlastManifestUpdate = acfile.GetlastUpdate();
			sLanguage = acfile.GetLanguage();
			if (sLanguage == string.Empty || sLanguage == "english")
				sLanguage = string.Empty;
			else
				sLanguage = "_" + sLanguage;
			List<string> DepotsFiles = acfile.GetDepotsFiles(sAllIds);
			//parse all manifest files
			foreach (string szDepFile in DepotsFiles)
				{
				if (File.Exists(szDepFile))
					sDepotsFiles.Add(szDepFile);
				}
			}

		private void GetCacheFlsInfos()
			{
			string szCacheFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Cache";
			if (!FilesTools.CheckDestinationPath(szCacheFolder))
				return;

			sCacheFls = szCacheFolder + "\\Cache_" + sAppID + ".fls.cch17b";

			if (File.Exists(sCacheFls))
				{
				FileInfo f = new FileInfo(sCacheFls);
				long FileLength = f.Length;
				if (FileLength > 0)
					{
					bHasCacheFile = true;
					DateTime lastModified = System.IO.File.GetLastWriteTime(sCacheFls);
					DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);
					TimeSpan elapsedTime = lastModified - Epoch;
					dlastCacheUpdate = elapsedTime.TotalSeconds;
					if (dlastCacheUpdate >= dlastManifestUpdate)
						bMustUpdateCache = false;
					else
						bMustUpdateCache = true;
					}
				}
			}

		private void GetModRootPath()
			{
			if (sIdent == string.Empty)
				return;
			if (sProvidedPath == string.Empty)
				{
				if (sModInstallDir == string.Empty)
					return;
				sModRootPath = sModInstallDir + "\\" + sIdent;
				sModMainRoot = sModInstallDir + "\\";
				if (!FilesTools.CheckDestinationPath(sModRootPath))
					{
					sModRootPath = string.Empty;
					sModMainRoot = string.Empty;
					}
				return;
				}

			Regex CountIdent = new Regex(@"[\\/]" + sIdent + @"_?[0-9a-zA-Z\s]*", RegexOptions.IgnoreCase);
			MatchCollection mc = CountIdent.Matches(sProvidedPath);

			if (mc.Count > 0)
				{
				if (mc.Count == 1) //un seul ident on ne peut pas se tromper
					{
					Regex IdentReg = new Regex(@"^" + sIdent + @"_?[0-9a-zA-Z\s]*", RegexOptions.IgnoreCase);

					string[] parts = sProvidedPath.Split('\\');
					foreach (string part in parts)
						{
						Match m = IdentReg.Match(part);
						if (!m.Success)
							{
							sModRootPath += part + "\\";
							}
						else
							{
							sModMainRoot = sModRootPath;
							sModRootPath += part;
							break;
							}
						}
					}
				else //on a plusieurs ident dans la chaine fournie
					{
					int nPos = mc[mc.Count - 1].Groups[0].Index;
					int nLength = mc[mc.Count - 1].Groups[0].Length;
					sModMainRoot = sProvidedPath.Substring(0, nPos);
					sModRootPath = sProvidedPath.Substring(0, nPos + nLength);
					}

				if (!FilesTools.CheckDestinationPath(sModRootPath))
					{
					sModRootPath = string.Empty;
					sModMainRoot = string.Empty;
					}
				}
			}
		private void CheckSteamPipe()
			{
			string szTmp = sModInstallDir + "\\";
			if (szTmp.ToLower() == sModMainRoot.ToLower() && bHasAppManifest && sAppManifestFile != string.Empty && sDepotsFiles.Count != 0)
				{
				bUseSteamPipe = true;
				return;
				}
			if (sModInstallDir == string.Empty && sModMainRoot != string.Empty)
				{
				int nPos = sModMainRoot.LastIndexOf('\\');
				sModInstallDir = sModMainRoot.Substring(0, nPos);
				}
			}
		private void GetTargetPath()
			{
			if (nCustomType == 1)
				{
				if (bUseSteamPipe)
					sTargetPath = sModInstallDir + "\\" + sIdent + "_downloads";
				else
					sTargetPath = sModRootPath;
				}
			else if (nCustomType == 2)
				{
				if (bUseSteamPipe)
					sTargetPath = sModInstallDir + "\\" + sIdent + "\\custom\\17Buddies";
				else
					sTargetPath = sModRootPath;

				}
			else
				{
				sTargetPath = sModRootPath;
				}
			try
				{
				if (sTargetPath != string.Empty)
					{
					DirectoryInfo cdi = new DirectoryInfo(sTargetPath);
					if (!cdi.Exists)
						System.IO.Directory.CreateDirectory(sTargetPath);
					}
				}
			catch (Exception)
				{
				}
			}
		public void SetTargetPath(string szPath)
			{
			sTargetPath = szPath;
			try
				{
				DirectoryInfo cdi = new DirectoryInfo(sTargetPath);
				if (!cdi.Exists)
					System.IO.Directory.CreateDirectory(sTargetPath);
				}
			catch (Exception ex)
				{
				MessageBox.Show(ex.Message);
				return;
				}
			}
		private void GetFromAppID(string szAppID, bool bLoadFiles)
			{
			InitDatas();
			string szCfgFileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\appManifests.cfg";
			myModsParser Mods = new myModsParser(szCfgFileName);
			if (Mods.bIsOk)
				{
				Dictionary<string, string> ModTmp = new Dictionary<string, string>();
				ModTmp = Mods.GetModFromAppID(szAppID);
				if (ModTmp.ContainsKey("appid") && ModTmp["appid"] == szAppID)
					GetInfos(ModTmp, bLoadFiles);
				else
					bIsOk = false;
				}
			else
				{
				bIsOk = false;
				}
			}

		private void GetFromPath(string szPath, bool bLoadFiles)
			{
			InitDatas();
			string szCfgFileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\appManifests.cfg";
			myModsParser ManifestCfgMods = new myModsParser(szCfgFileName);
			if (ManifestCfgMods.bIsOk)
				{
				var ModTmp = new List<Dictionary<string, string>>();
				ModTmp = ManifestCfgMods.GetModFromPath(szPath);
				if (ModTmp.Count == 1)
					{
					if (ModTmp[0].ContainsKey("appid") && ModTmp[0]["appid"] != string.Empty)
						GetInfos(ModTmp[0], bLoadFiles);
					else
						bIsOk = false;
					}
				else
					{
					bIsOk = false;
					VDFFile obVdfConfig = new VDFFile();
					if (obVdfConfig.bIsOk)
						{
						List<ModName> MdsCfg = ManifestCfgMods.GetAllMods();
						List<VDFInfo> VdfInfos = obVdfConfig.GetAllInstallDir(MdsCfg);
						List<int> AppsId = new List<int>();
						foreach (VDFInfo Vdi in VdfInfos)
							{
							if (szPath.IndexOf(Vdi.Path, StringComparison.OrdinalIgnoreCase) == 0)
								{
								AppsId.Add(Vdi.ID);
								}
							}
						if (AppsId.Count == 1)
							{
							bIsOk = true;
							GetFromAppID(AppsId[0].ToString(), bLoadFiles);
							}
						}
					}
				}
			}
		private void GetFromIdent(string szIdent, bool bLoadFiles)
			{
			InitDatas();
			string szCfgFileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\appManifests.cfg";
			myModsParser Mods = new myModsParser(szCfgFileName);
			if (Mods.bIsOk)
				{
				var ModTmp = new List<Dictionary<string, string>>();
				ModTmp = Mods.GetModFromIdent(szIdent);
				if (ModTmp.Count == 1)
					{
					if (ModTmp[0].ContainsKey("appid") && ModTmp[0]["appid"] != string.Empty)
						GetInfos(ModTmp[0], bLoadFiles);
					else
						bIsOk = false;
					}
				else
					{
					bIsOk = false;
					}
				}
			else
				{
				bIsOk = false;
				}
			}

		private void ParseDictionary(Dictionary<string, string> ModDatas)
			{
			if (ModDatas.ContainsKey("modname"))
				{
				sModName = ModDatas["modname"];
				}

			if (ModDatas.ContainsKey("hlversion"))
				{
				sHLVersion = ModDatas["hlversion"];
				nHLVersion = Convert.ToUInt32(sHLVersion);
				}

			if (ModDatas.ContainsKey("ident"))
				{
				sIdent = ModDatas["ident"];
				}

			if (ModDatas.ContainsKey("appid"))
				{
				sAppID = ModDatas["appid"];
				nAppID = Convert.ToUInt32(sAppID);
				}
			if (ModDatas.ContainsKey("customtype"))
				{
				nCustomType = Convert.ToUInt32(ModDatas["customtype"]);
				}

			int number = 0;
			bool result = false;

			if (ModDatas.ContainsKey("useddepots"))
				{
				string[] appsids = ModDatas["useddepots"].Split(',');
				foreach (string id in appsids)
					{
					result = Int32.TryParse(id, out number);
					if (result) //l'ID est bien un nombre
						{
						sAllIds.Add(id);
						nAllIds.Add(Convert.ToUInt32(id));
						}
					}
				}

			if (ModDatas.ContainsKey("oldpath"))
				{
				sOldPath = ModDatas["oldpath"].Replace('/', '\\'); ;
				}

			if (ModDatas.ContainsKey("newpath"))
				{
				sNewPath = ModDatas["newpath"].Replace('/', '\\'); ;
				}
			}

		public bool GetSystemFiles()
			{
			if (sCacheFls != string.Empty)
				{
				if (!File.Exists(sCacheFls))
					{
					if (!RebuildCacheFile())
						return false;
					}
				if (bMustUpdateCache)
					{
					DialogResult r = MessageBox.Show(Program.appLng._i18n("cchfile_must_be_updated"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					if (r == DialogResult.Yes)
						{
						RebuildCacheFile();
						}
					}
				var utf8WithoutBom = new System.Text.UTF8Encoding(false);
				try
					{
					SystemFiles = new List<string>(File.ReadAllLines(sCacheFls, utf8WithoutBom));
					FillWadsTexes(SystemFiles, false);
					}
				catch (Exception)
					{
					return false;
					}
				return true;
				}
			return false;
			}

		public bool RebuildCacheFile()
			{
			return RebuildCacheFile(false);
			}

		public bool RebuildCacheFile(bool confirmOverwrite)
			{
			if (sDepotsFiles.Count == 0)
				return false;

			//Create Cache Folder if not exists
			string szCacheFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Cache";
			try
				{
				DirectoryInfo cdi = new DirectoryInfo(szCacheFolder);
				if (!cdi.Exists)
					System.IO.Directory.CreateDirectory(szCacheFolder);
				}
			catch (Exception ex)
				{
				MessageBox.Show(ex.Message);
				return false;
				}

			//Check if file already exists
			if (File.Exists(sCacheFls) && confirmOverwrite)
				{
				string szMsg = String.Format(Program.appLng._i18n("common_file_replace"), sCacheFls);
				DialogResult r = MessageBox.Show(szMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (r != DialogResult.Yes)
					return false;
				}

			//Create Cache Files List
			List<string> DepotFiles = new List<string>();
			foreach (string szDepot in sDepotsFiles)
				{
				MFSTFile Manifest = new MFSTFile(szDepot, true);
				if (Manifest.Open())
					{
					if (!Manifest.MapDataStructure())
						{
						Manifest.Close();
						continue;
						}
					DepotFiles.AddRange(Manifest.CreateFullRoot(sModInstallDir, sIdent));
					Manifest.Close();
					}
				}
			//Write Cache File
			var utf8WithoutBom = new System.Text.UTF8Encoding(false);
			try
				{
				File.WriteAllLines(sCacheFls, DepotFiles, utf8WithoutBom);
				}
			catch (Exception)
				{
				return false;
				}
			FillWadsTexes(DepotFiles, true);
			FileInfo f = new FileInfo(sCacheFls);
			long FileLength = f.Length;
			if (FileLength > 0)
				{
				GetCacheFlsInfos();
				return true;
				}
			return false;
			}

		public bool FillWadsTexes(List<string> SystemFiles, bool bForceScan)
			{
			string szCacheFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Cache";
			if (!FilesTools.CheckDestinationPath(szCacheFolder))
				return false;
			if (nHLVersion != 1)
				return false;
			if (SystemFiles.Count == 0)
				return false;

			sCacheTxs = szCacheFolder + "\\Cache_" + sAppID + ".txs.cch17b";
			if (!bForceScan)
				{
				if (File.Exists(sCacheTxs))
					return ReadCachedTextures();
				}
			string szFullPath = string.Empty;
			bool bExists = false;

			//Chercher Path complet des wads
			string szPrefix = string.Empty;
			string szCarPath = "\\steamapps\\common";
			int nPos = 0;
			nPos = sModInstallDir.IndexOf(szCarPath, StringComparison.OrdinalIgnoreCase);
			if (nPos != -1)
				szPrefix = sModInstallDir.Substring(0, nPos + szCarPath.Length);
			else
				szPrefix = sModInstallDir;
			szPrefix = szPrefix.ToLower();

			WadFileInfo WadInfo;
			WadsFiles.Clear();
			foreach (string szSysFile in SystemFiles)
				{
				if (FilesTools.GetExtension(szSysFile).ToLower() == "wad")
					{
					bExists = false;
					szFullPath = szPrefix + "\\" + szSysFile;
					try
						{
						bExists = File.Exists(szFullPath);
						}
					catch (Exception)
						{
						bExists = false;
						}
					WadInfo = new WadFileInfo(szSysFile, szFullPath, true, bExists);
					if (!WadsFiles.Contains(WadInfo))
						WadsFiles.Add(WadInfo);
					if (bExists)
						{
						WADFile TmpWad = new WADFile(szFullPath);
						bool bResult = false;
						if (TmpWad.Open())
							{
							bResult = TmpWad.ReadTexes(false);
							TmpWad.Close();
							if (bResult)
								{
								WadInfo.SetTextures(TmpWad.wadTexes);
								}
							}
						}
					}
				}
			WriteCachedTextures();
			return true;
			}

		public bool WriteCachedTextures()
			{
			string szTmp = string.Empty;
			List<string> lLines = new List<string>();
			foreach (WadFileInfo wadfi in WadsFiles)
				{
				if (wadfi.TexList.Count == 0)
					continue;
				szTmp = wadfi.SysName + "|";
				foreach (string TexName in wadfi.TexList)
					{
					szTmp += TexName + ";";
					}
				szTmp = szTmp.Substring(0, szTmp.Length - 1);
				lLines.Add(szTmp);
				}
			var utf8WithoutBom = new System.Text.UTF8Encoding(false);
			try
				{
				File.WriteAllLines(sCacheTxs, lLines, utf8WithoutBom);
				}
			catch (Exception)
				{
				return false;
				}
			return true;
			}

		public bool ReadCachedTextures()
			{
			string szCacheFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Cache";
			if (!FilesTools.CheckDestinationPath(szCacheFolder))
				return false;
			if (nHLVersion != 1)
				return false;
			sCacheTxs = szCacheFolder + "\\Cache_" + sAppID + ".txs.cch17b";
			if (!File.Exists(sCacheTxs))
				return false;

			List<string> lLines = new List<string>();
			string SysName = string.Empty;
			string WadFull = string.Empty;
			List<string> Texes = new List<string>();
			bool bExists = false;
			WadFileInfo WadInfo;
			WadsFiles.Clear();
			//Ouverture du fichier cache et remplissage Liste temporaire
			var utf8WithoutBom = new System.Text.UTF8Encoding(false);
			lLines = new List<string>(File.ReadAllLines(sCacheTxs, utf8WithoutBom));
			//Parse Lines
			foreach (string line in lLines)
				{
				string[] parts = line.Split('|');
				SysName = parts[0];
				WadFull = sModInstallDir + "\\" + SysName;
				bExists = false;
				try
					{
					bExists = File.Exists(WadFull);
					}
				catch (Exception)
					{
					bExists = false;
					}
				WadInfo = new WadFileInfo(SysName, WadFull, true, bExists);
				if (!WadsFiles.Contains(WadInfo))
					WadsFiles.Add(WadInfo);
				Texes = new List<string>(parts[1].Split(';'));
				WadInfo.SetTextures(Texes);
				}
			return true;
			}
		}
	}
