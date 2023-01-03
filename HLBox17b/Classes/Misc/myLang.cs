using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace HLBox17b
{
	class myLang
			{
			public Dictionary<string, string> _strLng;
			private readonly Regex _keyValueRegex = new Regex(@"(?<Key>[^=]+)=(?<Value>.+)");

			public myLang()
				{
				LoadLanguage_File();
				}
	
        /// <summary>
        /// Load a Language File
        /// </summary>
        /// <returns></returns>
			private void LoadLanguage_File()
				{
				string lngPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Langs\";
				
				string filename ="";
				filename = lngPath + Application.ProductName + "-" + Program.mRc.szGen_Language + ".lng";
				if (!File.Exists(filename))
					filename = lngPath + Application.ProductName + "-en.lng";
				try
					{
					var content = File.ReadAllLines(filename);
					_strLng = new Dictionary<string, string>();
					foreach (var line in content)
						{
						Match m = _keyValueRegex.Match(line);
						if (m.Success)
							{
							string key = m.Groups["Key"].Value;
							string value = m.Groups["Value"].Value;
							if (!_strLng.ContainsKey(key))
								{
								_strLng[key] = value;
								}
							}
						}
					return;
					}
				catch (Exception e)
					{
					string szError = filename+":\n\n" + e.Message;
					MessageBox.Show(szError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					Application.Exit();
					}
				}
			/// <summary>
			/// Load Value of a Key
			/// </summary>
			/// <param name="key"></param>
			/// <returns>Value</returns>
			public string _i18n(string key)
				{
				if (_strLng != null)
					{
					if (_strLng.Count != 0)
						{
						if (_strLng.ContainsKey(key))
							{
							string retstr = _strLng[key].Replace(@"\n", "\n");
							return retstr;
							}
						}
					}
				return "!#"+key+"#!";
				}
			/// <summary>
			/// Get the list of installed languages
			/// </summary>
			/// <param name="key"></param>
			/// <returns><installed_lang> List of installed languages</returns>		
			public List<installed_lang> GetInstalledLangs()
				{
				string key="";
				string value="";

				List<installed_lang> DispLngs = new List<installed_lang>();

				List<string> addedlangs = new List<string>();

				string wildcards = Application.ProductName + "-*.lng";

				string szActDir = Path.GetDirectoryName(Application.ExecutablePath)+@"\Langs";
				
				string[] filePaths = Directory.GetFiles(szActDir, wildcards, SearchOption.AllDirectories);

				foreach (string lngfile in filePaths)
					{
					var content = File.ReadAllLines(lngfile);
					string Lng_Lng = "";
					string Lng_Lib = "";
					string Lng_Rst = "";
					foreach (var line in content)
						{
						Match m = _keyValueRegex.Match(line);
						if (m.Success)
							{
							key = m.Groups["Key"].Value;
							value = m.Groups["Value"].Value;
							if (key == "Lng_Lng")
								Lng_Lng = value;
							if (key == "Lng_Lib")
								Lng_Lib = value;
							if (key == "Lng_Rst")
								Lng_Rst = value.Replace(@"\n", "\n"); ;
							if (Lng_Lng != "" && Lng_Lib != "" && Lng_Rst != "")
								{
								if (!addedlangs.Contains(Lng_Lng))
									{
									addedlangs.Add(Lng_Lng);
									DispLngs.Add(new installed_lang(Lng_Lng, Lng_Lib, Lng_Rst));
									break;
									}
								}
							}
						}
					}
				if (DispLngs.Count>1)
					DispLngs.Sort(CompareLngs);
				return DispLngs;
				}
			/// <summary>
			/// Compare two languages objects
			/// </summary>
		 public int CompareLngs(installed_lang x1, installed_lang x2)
				{
				string Lib1 = x1.Lib;
				string Lib2 = x2.Lib;
				return string.Compare(Lib1, Lib2, true);
				}
    }
	/// <summary>
	/// Installed Languages class
	/// </summary>
	public class installed_lang
		{
		public string Lng;
		public string Lib;
		public string Rst;
		public installed_lang(string Lng, string Lib, string Rst)
			{
			this.Lng = Lng;
			this.Lib = Lib;
			this.Rst = Rst;
			}
		}
	}
