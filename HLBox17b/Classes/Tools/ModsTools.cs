using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HLBox17b.Classes.Tools
	{
	class ModsTools
		{
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Crée une liste des Mods indiqués dans le fichier de configuration
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////
		/// <param></param>
		/// <returns>Liste des mods trouvés dans le fichier de configuration</returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static List<ModName> ReadLstMods(bool Addnone, bool Addall, int nGame, bool bMustExists)
			{
			List<ModName> arMods = new List<ModName>();
			List<ModName> SortedMods = new List<ModName>();
			string szCfgFileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\Config\\appManifests.cfg";

			if (Addnone)
				arMods.Add(new ModName(Program.appLng._i18n("common_none"), 0));
			if (Addall)
				arMods.Add(new ModName(Program.appLng._i18n("common_all"), -1));

			myModsParser Mods = new myModsParser(szCfgFileName);
			if (Mods.bIsOk)
				{
				if (nGame != 0 && bMustExists)
					SortedMods = (Mods.GetAllMods(1, true)).OrderBy(o => o.Mod).ToList();
				else
					SortedMods = (Mods.GetAllMods()).OrderBy(o => o.Mod).ToList();
				arMods.AddRange(SortedMods);
				}
			return arMods;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'index d'un mod trouvé parmi une liste
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////
		/// <param></param>
		/// <returns>Index du Mod</returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static int GetComboAppIDIndex(int appID, List<ModName> arModsLst)
			{
			int nIdx = 0;
			foreach (ModName md in arModsLst)
				{
				if (md.Id == appID)
					return nIdx;
				nIdx++;
				}
			if (nIdx >= arModsLst.Count)
				nIdx = 0;
			return nIdx;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le répertoire "steamapps\common" d'un fichier si il existe
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////
		/// <param></param>
		/// <returns>Index du Mod</returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static string Get_Full_Common_From_Mod(string szFile, string szModPath, bool rToLower)
			{
			string szCarPath = "\\steamapps\\common\\";
			string szPrefix = string.Empty;
			int nPos = 0;

			if (szModPath == "")
				{
				if (rToLower)
					return szFile.ToLower();
				return szFile;
				}

			string vpkfile = szModPath + "\\" + szFile;

			nPos = vpkfile.IndexOf(szCarPath, StringComparison.OrdinalIgnoreCase);
			if (nPos != -1)
				szPrefix = Path.GetDirectoryName(vpkfile.Substring(nPos + szCarPath.Length));
			else
				szPrefix = Path.GetDirectoryName(szFile);

			if (rToLower)
				return szPrefix.ToLower();
			return szPrefix;
			}

		/////////////////////////////////////////////////////////////////////////////////		 
		/////////////////////////////////////////////////////////////////////////////////
		/// Fichiers - Généralités
		/////////////////////////////////////////////////////////////////////////////////
		public static List<int> GetAllAppsIDs()
			{
			List<int> appsIDs = new List<int>();
			int appID = 0;
			string pat = @"appmanifest_(?<appID>\d*)\.acf";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase);

			string szAcfPath = Program.mRc.szGen_SteamPath + "\\steamapps\\";
			string[] szAcfFilePaths = Directory.GetFiles(szAcfPath, "*.acf");

			foreach (string szfilename in szAcfFilePaths)
				{
				//Calculate appID
				Match m = r.Match(szfilename);
				if (!m.Success)
					continue;
				appID = Convert.ToInt32(m.Groups["appID"].Value);
				if (!appsIDs.Contains(appID))
					appsIDs.Add(appID);
				}
			return appsIDs;
			}

		public static void RebuildCacheFiles()
			{
			//Read acf files
			List<int> AppsIds = GetAllAppsIDs();

			foreach (int appID in AppsIds)
				{
				myMods clMod = new myMods((uint)appID, true);
				clMod.RebuildCacheFile();
				}
			}
		}
	}
