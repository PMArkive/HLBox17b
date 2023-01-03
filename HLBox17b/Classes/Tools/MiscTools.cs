using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using HLBox17b.Classes.Tools;

namespace HLBox17b.Classes.Tools
	{
	class MiscTools
		{
		public static bool CheckChapo()
			{
			if (Program.bIsChapo)
				return true;
			MessageBox.Show(Program.appLng._i18n("common_development"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return false;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Récupérer le répertoire Steam
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////
		/// <param></param>
		/// <returns>null if path not found</returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static string GetSteamInstallPath()
			{
			string szPath = string.Empty;
			RegistryKey rku = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam",false);
			if (rku != null)
				{
				szPath = (string)rku.GetValue("SteamPath", "");
				rku.Close();
				}
			else
				{
				RegistryKey rkm = Registry.LocalMachine.OpenSubKey(@"Software\Valve\Steam",false);
				if (rkm != null)
					{
					szPath = (string)rkm.GetValue("InstallPath", "");
					rkm.Close();
					}
				}
			if (szPath != "")
				szPath = szPath.Replace('/', '\\');
			return szPath;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Analyse un fichier et retourne le path steam
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////
		/// <param></param>
		/// <returns>Steam path</returns>		
		/////////////////////////////////////////////////////////////////////////////////
		public static string GetSteamPath(string szZipName, List<string> arMaps, string szEntity)
			{
			string szPathName = "";
			if (szEntity == "")
				return "";
			if (arMaps.Count != 1 && szZipName == "")
				return "";
			szEntity = szEntity.Replace('/', '\\');

			if (FilesTools.HasInvalidChars(szEntity))
				return "";

			szPathName = Path.GetDirectoryName(szEntity) + "\\";
			string szPth = szPathName;

			if (szPth == "\\")
				szPth = "";

			string szNom = FilesTools.GetOnlyName(szEntity);
			string szSubPath;
			string szMapName;
			string szExt = FilesTools.GetExtension(szEntity);
			int nPos;
			string szTmpName = "";

			if (arMaps.Count == 0)
				{
				szMapName = FilesTools.GetOnlyName(szZipName);
				}
			else
				{
				nPos = szEntity.IndexOf("\\");
				if (nPos != -1)
					szMapName = szEntity.Substring(0, nPos);
				else
					szMapName = FilesTools.GetOnlyName(arMaps.ElementAt(0));
				}

			string szMisc = Program.mRc.szInst_MiscPath + "\\";
			if (szMapName != "")
				szMisc += szMapName + "\\";

			if (szExt.Length == 0)
				return szMisc;

			if (szNom.ToLower() == "thumbs" && szExt == "db")
				return szMisc;

			if (szExt == "jpg")
				{
				nPos = szPth.IndexOf("gfx\\detail\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("gfx\\detail\\" + szPth.Substring(nPos));
				nPos = szPth.IndexOf("models\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szPth.IndexOf("maps\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					{
					foreach (string szTmpMap in arMaps)
						{
						szTmpName = Path.GetFileNameWithoutExtension(szTmpMap).ToLower();
						if (szNom.ToLower() == szTmpName)
							return "maps\\";
						szTmpName += "_loading";
						if (szNom.ToLower() == szTmpName)
							return "maps\\";
						}
					}
				return szMisc;
				}

			if (szExt == "pcx")
				{
				nPos = szPth.IndexOf("gfx\\detail\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szPth.IndexOf("overviews\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szPth.IndexOf("gfx\\env\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("gfx\\env\\");
				return szMisc;
				}

			if (szExt == "pcf")
				{
				nPos = szPth.IndexOf("particles\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				return "particles\\" + szPth;
				}

			if (szExt == "db")
				return szMisc;

			if (szExt == "cache")
				return "maps\\soundcache\\";

			if (szExt == "ain" || szExt == "nod")
				return "maps\\graphs\\";

			if (szExt == "bsp" || szExt == "nav" || szExt == "res" || szExt == "kv")
				return "maps\\";

			if (szExt == "vcs")
				{
				nPos = szPth.IndexOf("shaders\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				return "shaders\\" + szPth;
				}

			if (szExt == "vmt" || szExt == "vtf")
				{
				nPos = szPth.IndexOf("materials\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				return "materials\\" + szPth;
				}

			if (szExt == "mdl" || szExt == "vtx" || szExt == "phy" || szExt == "vvd" || szExt == "ctx" || szExt == "ani")
				{
				nPos = szPth.IndexOf("mapmodels\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("models\\" + szPth.Substring(nPos));
				nPos = szPth.IndexOf("models\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				//return "models\\" + szPth;
				return "";
				}

			if (szExt == "wav" || szExt == "mp3")
				{
				nPos = szPth.IndexOf("mapsounds\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("sound\\" + szPth.Substring(nPos));
				nPos = szPth.IndexOf("sound\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szPth.IndexOf("sounds\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					{
					szSubPath = "sound\\";
					szSubPath += szPth.Substring(nPos + 7);
					return szSubPath;
					}
				return "sound\\" + szPth;
				}

			if (szExt == "spr")
				{
				nPos = szPth.IndexOf("mapsprites\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("sprites\\" + szPth.Substring(nPos));
				nPos = szPth.IndexOf("sprites\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				return "sprites\\" + szPth;
				}

			if (szExt == "tga")
				{
				nPos = szPth.IndexOf("gfx\\detail\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				return "gfx\\env\\";
				}

			if (szExt == "mat")
				return "";

			if (szExt == "wad")
				{
				return "";
				}

			if (szExt == "rbr")
				return "RealBot\\Learned\rbr\\";

			if (szExt == "pwf")
				return "PODBot\\WPTDefault\\";

			if (szExt == "pxp")
				return "PODBot\\WPTDefault\\";

			if (szExt == "wpt")
				return "Sturmbot\\waypoints\\";

			if (szExt == "map"
				|| szExt == "rmf"
				|| szExt == "p0"
				|| szExt == "p1"
				|| szExt == "p2"
				|| szExt == "p3"
				|| szExt == "rmx"
				|| szExt == "lin"
				|| szExt == "pts"
				|| szExt == "wc"
				|| szExt == "loc"
				|| szExt == "log"
				|| szExt == "max"
				|| szExt == "prt"
				|| szExt == "wic"
				|| szExt == "gpk"
				|| szExt == "rfk"
				)
				{
				szSubPath = "WorldCraft\\";
				if (szMapName != "")
					szSubPath += szMapName + "\\";
				return szSubPath;
				}

			if (szExt == "bmp")
				{
				nPos = szPth.IndexOf("gfx\\detail\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szPth.IndexOf("overviews\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szPth.IndexOf("gfx\\env\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("gfx\\env\\");
				return szMisc;
				}

			if (szExt == "swf")
				{
				nPos = szPth.IndexOf("resource\\flash\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				return szMisc;
				}

			if (szExt == "dds")
				{
				nPos = szPth.IndexOf("resource\\overviews\\", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return (szPth.Substring(nPos));
				nPos = szNom.IndexOf("_radar", StringComparison.OrdinalIgnoreCase);
				if (nPos != -1)
					return ("resource\\overviews\\");
				return szMisc;
				}


			if (szExt == "lst")
				{
				foreach (string szTmpMap in arMaps)
					{
					szTmpName = Path.GetFileNameWithoutExtension(szTmpMap).ToLower();
					if (szNom.ToLower() == szTmpName)
						return "DownloadLists\\";
					szTmpName += "_exclude";
					if (szNom.ToLower() == szTmpName)
						return "maps\\";
					}
				return szMisc;
				}

			if (szExt == "txt")
				{
				bool bFound1 = false;
				bool bFound2 = false;
				foreach (string szTmpMap in arMaps)
					{
					szTmpName = Path.GetFileNameWithoutExtension(szTmpMap).ToLower();
					if (szNom.ToLower() == szTmpName)
						{
						bFound1 = true;
						break;
						}
					szTmpName += "_detail";
					if (szNom.ToLower() == szTmpName)
						{
						bFound2 = true;
						break;
						}
					}

				// Le txt ne correspond à aucune map présente dans le zip
				if (!bFound1 && !bFound2)
					{
					if (szNom.ToLower() == "www.17buddies.net")
						return ("");
					return szMisc;
					}

				if (bFound1)
					{
					nPos = szPth.IndexOf("resource\\overviews\\", StringComparison.OrdinalIgnoreCase);
					if (nPos != -1)
						return ("resource\\overviews\\");
					nPos = szPth.IndexOf("overviews\\", StringComparison.OrdinalIgnoreCase);
					if (nPos != -1)
						return ("overviews\\");
					}
				return "maps\\";
				}

			if (szExt == "cfg")
				{
				if (szNom.ToLower() == szMapName.ToLower())
					return "maps\\cfg\\";
				return "";
				}

			//L'archive contient une autre archive
			if (szExt == "exe" || szExt == "zip" || szExt == "rar")
				{
				return "maps\\";
				}

			return szMisc;
			}
		}
	}
