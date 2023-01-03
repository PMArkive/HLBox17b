using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;


namespace HLBox17b.Classes.Tools
	{
	class FilesTools
		{
		/////////////////////////////////////////////////////////////////////////////////		 
		/////////////////////////////////////////////////////////////////////////////////
		/// Créer ou effacer le répertoire temporaire
		/// 
		/////////////////////////////////////////////////////////////////////////////////
		public static bool Create_Temp_Folder()
			{
			try
				{
				if (!FilesTools.CheckDestinationPath(Program.mRc.szHlBoxTmpPath))
					{
					return FilesTools.CreateFolder(Program.mRc.szHlBoxTmpPath);
					}
				}
			catch (Exception)
				{
				return false;
				}
			return true;
			}

		public static void Delete_Temp_Folder()
			{
			try
				{
				if (FilesTools.CheckDestinationPath(Program.mRc.szHlBoxTmpPath))
					Directory.Delete(Program.mRc.szHlBoxTmpPath, true);
				}
			catch (Exception)
				{
				}
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie la validité d'un répertoire
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szPath">The destination path.</param>
		/// <returns>true or false</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CheckDestinationPath(string szPath)
			{
			return CheckDestinationPath(szPath, false);
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie la validité d'un répertoire
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szPath">The destination path.</param>
		/// <param labelName="bCanBeEmpty">Indicates if destination path can be empty.</param>
		/// <returns>true or false</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CheckDestinationPath(string szPath, bool bCanBeEmpty)
			{
			if (szPath.Trim() == "")
				return bCanBeEmpty;
			try
				{
				DirectoryInfo cdi = new DirectoryInfo(szPath);
				if (!cdi.Exists)
					return false;
				}
			catch (Exception)
				{
				return false;
				}
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Crée un répertoire
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szPath">The Folder to create.</param>
		/// <returns>false if folder can't be created</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CreateFolder(string szPath)
			{
			try
				{
				DirectoryInfo cdi = new DirectoryInfo(szPath);
				if (cdi.Exists)
					return true;
				System.IO.Directory.CreateDirectory(szPath);
				}
			catch (Exception ex)
				{
				MessageBox.Show(ex.Message);
				return false;
				}
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie qu'un fichier existe
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <returns>true if file exists and is not null</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CheckFile(string szFilename)
			{
			if (szFilename.Trim() == "")
				return false;
			//Vérifie que le fichier existe
			try
				{
				FileInfo f = new FileInfo(szFilename);
				if (!f.Exists)
					return false;
				}
			catch (Exception)
				{
				return false;
				}
			//Vérifie que 'lon peut l'ouvrir
			try
				{
				FileStream fs = new FileStream(szFilename, FileMode.Open, FileAccess.Read);
				fs.Close();
				}
			catch (Exception e)
				{
				MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
				}
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie qu'un fichier existe et correspond à l'extension donnée
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <param labelName="szDefExt">The extension(s) to validate (can be separated with '|').</param> 
		/// <returns>true if file exists and extension is valid</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CheckFile(string szFilename, string szDefExt)
			{
			bool bTrouve = false;
			string tmpFile = szFilename.Trim();
			if (tmpFile == "")
				return false;
			try
				{
				FileInfo f = new FileInfo(tmpFile);
				if (!f.Exists)
					return false;
				string[] seps = szDefExt.Split('|');
				foreach (string sep in seps)
					{
					if (f.Extension.ToLower() == sep.ToLower())
						{
						bTrouve = true;
						break;
						}
					}
				}
			catch (Exception e)
				{
				MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
				}
			return bTrouve;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie qu'un fichier existe et correspond à l'extension donnée
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <param labelName="szDefExt">The valids extensions list</param> 
		/// <returns>true if file exists and extension is valid</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CheckFile(string szFilename, List<string> szDefExts)
			{
			return CheckFile(szFilename, szDefExts.ToArray());
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie qu'un fichier existe et correspond à l'extension donnée
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <param labelName="szDefExt">The valids extensions list.</param> 
		/// <returns>true if file exists and extension is valid</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool CheckFile(string szFilename, string [] szDefExts)
			{
			bool bTrouve = false;
			string tmpFile = szFilename.Trim();
			if (tmpFile == "")
				return false;
			try
				{
				FileInfo f = new FileInfo(tmpFile);
				if (!f.Exists)
					return false;
				foreach (string sep in szDefExts)
					{
					if (f.Extension.ToLower() == sep.ToLower())
						{
						bTrouve = true;
						break;
						}
					}
				}
			catch (Exception e)
				{
				MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
				}
			return bTrouve;
			}
		/// <summary>
		/// Shortens a pathname for display purposes.
		/// </summary>
		/// <param labelName="pathname">The pathname to shorten.</param>
		/// <param labelName="maxLength">The maximum number of characters to be displayed.</param>
		/// <remarks>Shortens a pathname by either removing consecutive components of a path
		/// and/or by removing characters from the end of the filename and replacing
		/// then with three elipses (...)
		/// <para>In all cases, the root of the passed path will be preserved in it's entirety.</para>
		/// <para>If a UNC path is used or the pathname and maxLength are particularly short,
		/// the resulting path may be longer than maxLength.</para>
		/// <para>This method expects fully resolved pathnames to be passed to it.
		/// (Use Path.GetFullPath() to obtain this.)</para>
		/// </remarks>
		/// <returns></returns>
		public static string ShortenPathName(string pathname, int maxLength)
			{
			if (pathname.Length <= maxLength)
				return pathname;

			string root = Path.GetPathRoot(pathname);
			if (root.Length > 3)
				root += Path.DirectorySeparatorChar;

			string[] elements = pathname.Substring(root.Length).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			int filenameIndex = elements.GetLength(0) - 1;

			if (elements.GetLength(0) == 1) // pathname is just a root and filename
				{
				if (elements[0].Length > 5) // long enough to shorten
					{
					// if path is a UNC path, root may be rather long
					if (root.Length + 6 >= maxLength)
						{
						return root + elements[0].Substring(0, 3) + "...";
						}
					else
						{
						return pathname.Substring(0, maxLength - 3) + "...";
						}
					}
				}
			else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength) // pathname is just a root and filename
				{
				root += "...\\";

				int len = elements[filenameIndex].Length;
				if (len < 6)
					return root + elements[filenameIndex];

				if ((root.Length + 6) >= maxLength)
					{
					len = 3;
					}
				else
					{
					len = maxLength - root.Length - 3;
					}
				return root + elements[filenameIndex].Substring(0, len) + "...";
				}
			else if (elements.GetLength(0) == 2)
				{
				return root + "...\\" + elements[1];
				}
			else
				{
				int len = 0;
				int begin = 0;

				for (int i = 0; i < filenameIndex; i++)
					{
					if (elements[i].Length > len)
						{
						begin = i;
						len = elements[i].Length;
						}
					}

				int totalLength = pathname.Length - len + 3;
				int end = begin + 1;

				while (totalLength > maxLength)
					{
					if (begin > 0)
						totalLength -= elements[--begin].Length - 1;

					if (totalLength <= maxLength)
						break;

					if (end < filenameIndex)
						totalLength -= elements[++end].Length - 1;

					if (begin == 0 && end == filenameIndex)
						break;
					}

				// assemble final string

				for (int i = 0; i < begin; i++)
					{
					root += elements[i] + '\\';
					}

				root += "...\\";

				for (int i = end; i < filenameIndex; i++)
					{
					root += elements[i] + '\\';
					}

				return root + elements[filenameIndex];
				}
			return pathname;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'extension seule (sans le point) d'un fichier
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <returns>lowercase extension without the point</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static string GetExtension(string szEntity)
			{
			return GetExtension(szEntity,true);
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'extension seule (sans le point) d'un fichier
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <param labelName="blowercase">returns lowercase extension.</param>
		/// <returns>extension without point</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static string GetExtension(string szEntity,bool bLowercase)
			{
			string szExt = "";
			int nPos = szEntity.LastIndexOf('.');
			if (nPos != -1)
				szExt = szEntity.Substring(nPos + 1);
			if (bLowercase)
				return szExt.ToLower();
			return szExt;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne uniquement le nom d'un fichier (lowercase) sans path ni extension
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <returns>lowercase name only</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static string GetOnlyName(string szEntity)
			{
			return GetOnlyName(szEntity, true);
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne uniquement le nom d'un fichier sans path ni extension
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <param labelName="blowercase">returns in lowercase.</param>
		/// <returns>name only</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static string GetOnlyName(string szEntity,bool bLowercase)
			{
			string szOnlyName = "";

			szEntity = szEntity.Replace("/", "\\");

			int nPos1 = szEntity.LastIndexOf("\\");
			if (nPos1 == -1)
				nPos1 = 0;
			else
				nPos1++;
			int nPos2 = szEntity.LastIndexOf('.');
			if (nPos2 == -1)
				nPos2 = szEntity.Length;
			if (nPos2 < nPos1)
				{
				if (bLowercase)
					return szOnlyName.ToLower();
				return szOnlyName;
				}
			szOnlyName = szEntity.Substring(nPos1, (nPos2 - nPos1));
			if (bLowercase)
				return szOnlyName.ToLower();
			return szOnlyName;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Initialise la liste des caractères invalides dans un chemin
		/// </summary>
		/// <param></param>
		/// <returns>InvalidChars string</returns>
		//////////////////////////////////////////////////////////////////////////////////// 
		public static string InitInvalidChars()
			{
			string invalidCharsStr;
			string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			invalidCharsStr = string.Format(@"[{0}]", invalidChars);
			invalidCharsStr = invalidCharsStr.Replace("\\\\", "");
			invalidCharsStr = invalidCharsStr.Replace(":", "");
			return invalidCharsStr;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne si un fichier contient des caractères invalides
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">The File to check.</param>
		/// <returns>true or false</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static bool HasInvalidChars(string name)
			{
			string invalidCharsStr = InitInvalidChars();
			return Regex.IsMatch(name, invalidCharsStr);
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Raccourci le chemin d'un fichier		
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szPath">Nom du fichier</param>
		/// <param labelName="nLenght">Taille maxi</param>/// 
		/// <returns>string</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static string TruncatePath(string szPath, int nLength)
			{
			if (szPath.Length <= nLength)
				return szPath;

			const string pattern = @"^(\w+:|\\)(\\[^\\]+\\[^\\]+\\).*(\\[^\\]+\\[^\\]+)$";
			const string replacement = "$1$2...$3";
			if (Regex.IsMatch(szPath, pattern))
				return Regex.Replace(szPath, pattern, replacement);
			return szPath;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Formatte la taille d'un fichier		
		/// </summary>
		///////////////////////////////////////////////////////////////////////////////// 
		/// <param labelName="szFilename">taille du fichier.</param>
		/// <returns>string</returns>
		///////////////////////////////////////////////////////////////////////////////// 
		public static string formatSize(Int64 lSize)
			{
			//Format number to KB
			string stringSize = "";
			CultureInfo CultInfo = System.Globalization.CultureInfo.InstalledUICulture;
			NumberFormatInfo myNfi = CultInfo.NumberFormat;
			Int64 lKBSize = 0;
			if (lSize < 1024)
				{
				if (lSize == 0)
					{
					//zero byte
					stringSize = "0";
					}
				else
					{
					//less than 1K but not zero byte
					stringSize = "1";
					}
				}
			else
				{
				//convert to KB
				lKBSize = lSize / 1024;
				//format number with default format
				stringSize = lKBSize.ToString("n", myNfi);
				//remove decimal
				stringSize = stringSize.Replace(myNfi.CurrencyDecimalSeparator + "00", "");
				}
			return stringSize + " Kb";
			}
		}
	}
