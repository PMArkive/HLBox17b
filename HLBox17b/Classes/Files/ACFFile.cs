using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using HLBox17b.Classes.Tools;

namespace HLBox17b.Classes.Files
	{
	class ACFFile
		{
		private string szFullFile;
		private string szAppInstallDir;
		private string szGenInstallDir;
		private string szLanguage;
		private FileStream fs;
		private Dictionary<string, string> Items;
		private double dLastUpdate;

		public ACFFile(int appID)
			{
			szFullFile = Program.mRc.szGen_SteamPath + "\\steamapps\\appmanifest_" + appID.ToString() + ".acf";
			szAppInstallDir = string.Empty;
			szGenInstallDir = string.Empty;
			szLanguage = string.Empty;
			dLastUpdate = 0;
			Run();
			}


		public ACFFile(string szFile)
			{
			int number;
			bool result = Int32.TryParse(szFile, out number);
			if (result) //On cherche par appID
				szFullFile = Program.mRc.szGen_SteamPath + "\\steamapps\\appmanifest_" + szFile + ".acf";
			else
				szFullFile = szFile;
			szAppInstallDir = "";
			dLastUpdate = 0;
			Run();
			}

		private void Run()
			{
			Items = new Dictionary<string, string>();

			if (Open())
				{
				ParseItems();
				Close();
				}
			}

		private bool Open()
			{
			FileInfo fi = new FileInfo(szFullFile);
			if (fi.Exists)
				{
				try
					{
					fs = new FileStream(szFullFile, FileMode.Open, FileAccess.Read);
					}
				catch (Exception)
					{
					}
				if (fs != null)
					{
					return true;
					}
				}
			return false;
			}

		private bool Close()
			{
			fs.Close();
			return true;
			}

		private string ReadNextChar()
			{
			int ch = fs.ReadByte();

			if (ch > -1)
				{
				char converted = Convert.ToChar(ch);

				string szChar = converted.ToString();

				return szChar;
				}
			return null;
			}

		private string ScanForNextToken()
			{
			string szChar = ReadNextChar();

			while (szChar != null && Char.IsWhiteSpace(szChar, 0))
				{
				szChar = ReadNextChar();
				}
			return szChar;
			}

		private string ParseQuotedToken()
			{
			string szToken = "";

			string szChar = ReadNextChar();

			while (szChar != "\"")
				{
				szToken += szChar;
				szChar = ReadNextChar();
				if (szChar == null)
					return null;
				}
			return szToken;
			}


		private void ParseItems()
			{
			string szFullPath = "";
			string szItemVal = "";
			string szItemKey = "";
			string szTokenType = "";

			szTokenType = ScanForNextToken();
			while (szTokenType != null)
				{
				if (szTokenType != "\"" && szTokenType != "{" && szTokenType != "}")
					return;
				if (szTokenType == "}")
					{
					szFullPath = szFullPath.Remove(szFullPath.Length - 1);
					int nPos = szFullPath.LastIndexOf('.');
					szFullPath = szFullPath.Substring(0, nPos + 1);
					szTokenType = ScanForNextToken();
					continue;
					}
				szItemKey = szFullPath + ParseQuotedToken();
				szTokenType = ScanForNextToken();
				if (szTokenType == "\"")
					{
					szItemVal = ParseQuotedToken();
					Items.Add(szItemKey, szItemVal);
					}
				if (szItemKey.ToLower() == "appstate.installdir")
					{
					szGenInstallDir = Program.mRc.szGen_SteamPath + "\\steamapps\\common\\" + szItemVal;
					if (!FilesTools.CheckDestinationPath(szGenInstallDir))
						{
						szGenInstallDir = szItemVal;
						if (!FilesTools.CheckDestinationPath(szGenInstallDir))
							szGenInstallDir = string.Empty;
						}
					}
				if (szItemKey.ToLower() == "appstate.lastupdated")
					dLastUpdate = Convert.ToDouble(szItemVal);

				if (szItemKey.ToLower() == "appstate.userconfig.language")
					szLanguage = szItemVal;

				if (szItemKey.ToLower() == "appstate.userconfig.appinstalldir")
					{
					szAppInstallDir = szItemVal;
					if (!FilesTools.CheckDestinationPath(szAppInstallDir))
						szAppInstallDir = string.Empty;
					}

				if (szTokenType == "{")
					{
					szFullPath = szItemKey + ".";
					}
				szTokenType = ScanForNextToken();
				}
			if (szAppInstallDir == string.Empty)
				szAppInstallDir = szGenInstallDir;
			}

		public string GetAppInstallDir()
			{
			return szAppInstallDir.Replace("\\\\", "\\");
			}

		public double GetlastUpdate()
			{
			return dLastUpdate;
			}

		public string GetLanguage()
			{
			return szLanguage.ToLower();
			}

		public List<string> GetSharedManifestFile(string sharedAppId, List<string> existingFiles, List<string> depIds)
			{
			List<string> SharedFiles = new List<string>();
			List<string> SharedTmp = new List<string>();
			string szSharedFile = Program.mRc.szGen_SteamPath + "\\steamapps\\appmanifest_" + sharedAppId + ".acf";

			if (File.Exists(szSharedFile))
				{
				ACFFile acfile = new ACFFile(szSharedFile);
				SharedTmp = acfile.GetDepotsFiles(depIds);
				foreach (string tmp in SharedTmp)
					{
					if (!existingFiles.Contains(tmp))
						SharedFiles.Add(tmp);
					}
				}
			return SharedFiles;
			}

		public List<string> GetDepotsFiles(List<string> depIds)
			{
			if (depIds.Count == 0)
				return GetDepotsFiles();

			List<string> DepotsFiles = new List<string>();
			string szKey = "";
			string szManifest = "";
			int nPos = 0;
			string szId = string.Empty;
			string szParentMfst = string.Empty;

			foreach (KeyValuePair<string, string> Item in Items)
				{
				szKey = Item.Key;
				if (szKey.ToLower().Contains("mounteddepots"))
					{
					nPos = szKey.LastIndexOf('.');
					szId = szKey.Substring(nPos + 1);
					if (depIds.Contains(szId))
						{
						szManifest = Program.mRc.szGen_SteamPath + "\\depotcache\\" + szId + "_" + Item.Value + ".manifest";
						if (!DepotsFiles.Contains(szManifest))
							DepotsFiles.Add(szManifest);
						}
					}
				else if (szKey.ToLower().Contains("shareddepots"))
					{
					nPos = szKey.LastIndexOf('.');
					szId = szKey.Substring(nPos + 1);
					if (depIds.Contains(szId))
						{
						DepotsFiles.AddRange(GetSharedManifestFile(Item.Value, DepotsFiles, depIds));
						}
					}
				}
			return DepotsFiles;
			}


		public List<string> GetDepotsFiles()
			{
			List<string> DepotsFiles = new List<string>();
			List<string> depIds = new List<string>();
			string szKey = "";
			string szManifest = "";
			int nPos = 0;
			string szId = string.Empty;

			foreach (KeyValuePair<string, string> Item in Items)
				{
				szKey = Item.Key;
				if (szKey.ToLower().Contains("mounteddepots"))
					{
					nPos = szKey.LastIndexOf('.');
					szManifest = Program.mRc.szGen_SteamPath + "\\depotcache\\" + szKey.Substring(nPos + 1) + "_" + Item.Value + ".manifest";
					if (!DepotsFiles.Contains(szManifest))
						DepotsFiles.Add(szManifest);
					}
				else if (szKey.ToLower().Contains("shareddepots"))
					{
					nPos = szKey.LastIndexOf('.');
					szId = szKey.Substring(nPos + 1);
					DepotsFiles.AddRange(GetSharedManifestFile(Item.Value, DepotsFiles, depIds));
					}
				}
			return DepotsFiles;
			}
		}
	}
