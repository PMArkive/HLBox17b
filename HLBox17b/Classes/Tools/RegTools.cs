using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;

namespace HLBox17b.Classes.Tools
	{
	class RegTools
		{
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Associer un type d'extension à HLBox
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param>Extension à associer</param>
		/// <returns></returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static void AssociateFileInfo(string szExt)
			{
			myVerInfo vi = new myVerInfo();
			VersionInfo stvi = vi.GetInfos();
			string szProgID = string.Empty;
			string szDesc = string.Empty;
			string szOrgExt = string.Empty;
			int nIconIdx;

			//Initialisation
			szExt = szExt.ToLower();
			if (!szExt.StartsWith("."))
				{
				szOrgExt = szExt;
				szExt = "." + szExt;
				}
			else
				{
				szOrgExt = szExt.Substring(1);
				}
			//On crée les paramètres de l'association
			//ProgID
			//Description
			//Icone
			szProgID = stvi.Product + szExt;
			szDesc = stvi.Product + " " + szOrgExt.ToUpper() + " File";
			switch (szExt)
				{
				case ".wad":
					nIconIdx = 1;
					break;
				case ".gcf":
				case ".ncf":
				case ".vpk":
					nIconIdx = 2;
					break;
				case ".bsp":
					nIconIdx = 3;
					break;
				case ".manifest":
					nIconIdx = 4;
					break;
				case ".vmt":
					nIconIdx = 5;
					break;
				case ".vtf":
					nIconIdx = 6;
					break;
				default:
					nIconIdx = 0;
					break;
				}
			//Sauvegarde de la valeur actuelle si elle n'est pas déjà changée
			RegistryKey rkcu = Registry.CurrentUser;
			RegistryKey rk = null;
			string szDefAssoc = string.Empty;
			rk = rkcu.OpenSubKey(@"Software\Classes\" + szExt, false);
			if (rk == null)
				{
				rkcu.CreateSubKey(@"Software\Classes\" + szExt);
				rk = rkcu.OpenSubKey(@"Software\Classes\" + szExt, false);
				}
			szDefAssoc = (string)rk.GetValue("", string.Empty);
			rk.Close();
			//Si l'association existe et qu'elle n'est pas déjà  
			//attribuée à HLBox, on sauvegarde la valeur existante
			if (szDefAssoc != string.Empty && szDefAssoc != szProgID)
				{
				rk = RegTools.OpenOrCreateRegKey("Sys", true);
				rk.SetValue("szDefFAI" + szExt, szDefAssoc);
				rk.Close();
				}
			//////////////////////////////////////////
			//On sauvegarde la nouvelle association
			//Création ou modification de la clé pour l'extension
			rk = rkcu.OpenSubKey(@"Software\Classes\" + szExt, true);
			rk.SetValue("", szProgID);
			//Création ou modification de la clé pour le ProgID
			rk = rkcu.OpenSubKey(@"Software\Classes\" + szProgID, true);
			if (rk == null)
				{
				rkcu.CreateSubKey(@"Software\Classes\" + szProgID);
				rk = rkcu.OpenSubKey(@"Software\Classes\" + szProgID, true);
				}
			rk.SetValue("", szDesc);
			rk.CreateSubKey(@"Shell\Open\Command").SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
			rk.CreateSubKey("DefaultIcon").SetValue("", Application.ExecutablePath + "," + nIconIdx.ToString());
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Remettre l'association par défaut d'une extension
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param>Extension à associer</param>
		/// <returns></returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static void RestoreFileInfo(string szExt)
			{
			myVerInfo vi = new myVerInfo();
			VersionInfo stvi = vi.GetInfos();
			RegistryKey rk = null;
			//Initialisation
			szExt = szExt.ToLower();
			if (!szExt.StartsWith("."))
				szExt = "." + szExt;

			rk = RegTools.OpenOrCreateRegKey("Sys", true);
			string szDefFAI = (string)rk.GetValue("szDefFAI" + szExt, string.Empty);
			rk.Close();

			RegistryKey rkcu = Registry.CurrentUser;
			rk = rkcu.OpenSubKey(@"Software\Classes\" + szExt, true);
			if (rk == null)
				{
				rkcu.CreateSubKey(@"Software\Classes\" + szExt);
				rk = rkcu.OpenSubKey(@"Software\Classes\" + szExt, true);
				}
			rk.SetValue("", szDefFAI);
			rk.Close();
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Récupérer l'image de lancement steam
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param></param>
		/// <returns>Url de l'image</returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static string GetSteamProvidedByUrl()
			{
			string szUrl = Program.mRc.szBan_defaultUrl;
			string szTmpKey = @"Software\Valve\Steam";
			RegistryKey rkcu = Registry.CurrentUser;
			RegistryKey rk = rkcu.OpenSubKey(szTmpKey, true);
			if (rk == null)
				{
				rkcu.CreateSubKey(szTmpKey);
				rk = rkcu.OpenSubKey(szTmpKey, true);
				rk.SetValue("LastContentProviderURL", szUrl);
				}
			szUrl = (string)rk.GetValue("LastContentProviderURL", szUrl);
			rk.Close();
			rkcu.Close();
			return szUrl;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Changer l'image de lancement steam
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param>Url de la nouvelle image</param>
		/// <returns></returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static void ChangeSteamProvidedByUrl(string szUrl)
			{
			if (szUrl == "")
				return;
			string szTmpKey = @"Software\Valve\Steam";
			RegistryKey rkcu = Registry.CurrentUser;
			RegistryKey rk = rkcu.OpenSubKey(szTmpKey, true);
			if (rk == null)
				{
				rkcu.CreateSubKey(szTmpKey);
				rk = rkcu.OpenSubKey(szTmpKey, true);
				}
			rk.SetValue("LastContentProviderURL", szUrl);
			rk.Close();
			rkcu.Close();
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Ouvre une clé de registre sous HLBox17b ou la crée si elle n'existe pas
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param>Clé à créer</param>
		/// <param>Type d'accès</param>
		/// <returns></returns>		
		////////////////////////////////////////////////////////////////////////////////
		public static RegistryKey OpenOrCreateRegKey(string szPathToAdd, bool baccess)
			{
			myVerInfo vi = new myVerInfo();
			VersionInfo stvi = vi.GetInfos();

			string szRegKey = @"Software\" + stvi.Company + @"\" + stvi.Product;
			
			string szRegPath = szRegKey;

			if (szPathToAdd != "")
				szRegPath = szRegKey + @"\" + szPathToAdd;

			RegistryKey rkcu = Registry.CurrentUser;
			RegistryKey rk = rkcu.OpenSubKey(szRegPath, baccess);
			if (rk == null)
				{
				rkcu.CreateSubKey(szRegPath);
				rk = rkcu.OpenSubKey(szRegPath, baccess);
				}
			return rk;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Récupérer la liste des fichiers récents pour une catégorie donnée
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param>Catégorie</param>
		/// <param>Nombre de fichiers à récupérer</param>
		/// <returns>Liste des derniers fichiers</returns>		
		////////////////////////////////////////////////////////////////////////////////
		public static List<string> GetRecents(string szCat, int numItems)
			{
			string szTmp = string.Empty;
			string szKey = string.Empty;
			List<string> recents = new List<string>();

			RegistryKey rk;

			if (szCat != string.Empty)
				szCat += "\\";
			string szPath = szCat + "Recents";

			rk = RegTools.OpenOrCreateRegKey(szPath, true);

			bool not_at_end = true;
			int i = 0;
			while (not_at_end && i < numItems)
				{
				szTmp = String.Format("{0}", i);
				szKey = (string)rk.GetValue(szTmp, "");
				if (szKey.Trim() != "")
					recents.Add(szKey);
				else
					not_at_end = false;
				i++;
				}
			rk.Close();
			return recents;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Sauvegarder la liste des fichiers récents pour une catégorie donnée
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param>Catégorie</param>
		/// <param>Liste des fichiers</param>
		/// <param>Nombre de fichiers à sauver</param>
		/// <returns></returns>		
		////////////////////////////////////////////////////////////////////////////////
		public static bool SaveRecents(string szCat, List<string> recents, int numItems)
			{
			string szTmp;
			string szKey;
			RegistryKey rk;

			string szPath = szCat + "\\Recents";

			rk = OpenOrCreateRegKey(szPath, true);

			int nItems = recents.Count < numItems ? recents.Count : numItems;

			for (int i = 0; i < nItems; i++)
				{
				szKey = recents.ElementAt(i);
				szTmp = String.Format("{0}", i);
				rk.SetValue(szTmp, szKey);
				}
			rk.Close();
			return true;
			}
		}
	}
