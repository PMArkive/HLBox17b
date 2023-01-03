using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HLBox17b.Classes.Files
	{
	class VDFFile
		{
		private string szFullFile;
		private FileStream fs;
		private Dictionary<string, string> Items;
		public bool bIsOk;

		public VDFFile(string szFile = null)
			{
			bIsOk = false;
			if (szFile == null)
				szFullFile = Program.mRc.szGen_SteamPath + "\\config\\config.vdf";
			else
				szFullFile = szFile;
			Run(szFile);
			}

		private void Run(string szFile)
			{
			Items = new Dictionary<string, string>();

			if (Open())
				{
				bIsOk = ParseItems();
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


		private bool ParseItems()
			{
			string szFullPath = "";
			string szItemVal = "";
			string szItemKey = "";
			string szTokenType = "";

			szTokenType = ScanForNextToken();
			while (szTokenType != null)
				{
				if (szTokenType != "\"" && szTokenType != "{" && szTokenType != "}")
					return false;
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
				if (szTokenType == "{")
					{
					szFullPath = szItemKey + ".";
					}
				szTokenType = ScanForNextToken();
				}
			if (Items.Count == 0)
				return false;
			return true;
			}

		public string GetAppInstallDir(uint nAppId)
			{
			string szToFind = "steam.apps." + nAppId.ToString() + ".installdir";
			string szPath = string.Empty;
			string szKey = "";

			foreach (KeyValuePair<string, string> Item in Items)
				{
				szKey = Item.Key;
				if (szKey.ToLower().Contains(szToFind))
					{
					szPath = Item.Value;
					}
				}
			return szPath.Replace("\\\\", "\\");
			}

		public List<VDFInfo> GetAllInstallDir(List<ModName> ModsCfg)
			{
			List<VDFInfo> VdfPaths = new List<VDFInfo>();
			string szKey = "";

			foreach (ModName ModInfo in ModsCfg)
				{
				Regex r = new Regex(@"steam\.apps\." + ModInfo.Id.ToString() + @"\.installdir", RegexOptions.IgnoreCase);
				foreach (KeyValuePair<string, string> Item in Items)
					{
					szKey = Item.Key;
					Match m = r.Match(szKey);
					if (m.Success)
						{
						string szTmp = Item.Value.Replace("/", "\\");
						szTmp = szTmp.Replace("\\\\", "\\");
						VdfPaths.Add(new VDFInfo(ModInfo.Id, szTmp));
						}
					}
				}
			return VdfPaths;
			}
		}

	class VDFInfo
		{
		public int ID;
		public string Path;
		public VDFInfo(int id, string path)
			{
			ID = id;
			Path = path;
			}
		}
	
	}
