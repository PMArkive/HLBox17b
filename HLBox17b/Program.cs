using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HLBox17b.Externals;
using HLBox17b.Classes.Tools;
using HLBox17b;


namespace HLBox17b
	{
	static class Program
		{
		public static bool bIsChapo;
		public static myLang appLng;
		public static stRegConf mRc;
		public static myConf mConf;
		public static string szScheme;
		static MainForm mainform;
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
			{
			//////////////////////////////
			//Init Main Variables
			mConf = new myConf();
			mRc = mConf.GetMainConfig();
			appLng = new myLang();
			szScheme = Program.mRc.szGen_17bUrl;
			bIsChapo = false;
			//////////////////////////////
			// Get Download session if exists
			string szDLID = Get_DownloadSession();
			if (szDLID == string.Empty)
				{
				Guid guid = new Guid("{6EAE2E61-E7EE-42bf-8EBE-BAB890C5410F}");
				using (SingleInstance singleInstance = new SingleInstance(guid))
					{
					if (singleInstance.IsFirstInstance)
						{
						singleInstance.ArgumentsReceived += singleInstance_ArgumentsReceived;
						singleInstance.ListenForArgumentsFromSuccessiveInstances();

						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						//Appel du Splash Screen
						if (Program.mRc.bGen_ShowSplash)
							{
							About ab = new About(true);
							ab.ShowDialog();
							}
						FilesTools.Delete_Temp_Folder();
						Application.Run(mainform = new MainForm());
						}
					else
						{
						singleInstance.PassArgumentsToFirstInstance(Environment.GetCommandLineArgs());
						}
					}
				}
			else
				{
				Download_And_Install(szDLID);
				//Vérification de la version
				if (Program.mRc.bGen_CheckVerAtStartup)
					WebTools.NewVersion_StartCheck(false);
				}
			}
		/// <summary>
		/// Charge les arguments de la première instance.
		/// </summary>
		static void singleInstance_ArgumentsReceived(object sender, ArgumentsReceivedEventArgs e)
			{
			if (mainform == null)
				return;
			Action<String[]> updateForm = arguments =>
								{
								//mainform.WindowState = FormWindowState.Normal;
								mainform.mainform_OpenFiles(arguments);
								};
			mainform.Invoke(updateForm, (Object)e.Args); //Execute our delegate on the forms thread!
			}
		/// <summary>
		/// Vérifie les arguments de la ligne de commande pour trouver une session de Download.
		/// </summary>
		static string Get_DownloadSession()
			{
			string[] args = Environment.GetCommandLineArgs();
			string szDLID = string.Empty;

			foreach (string arg in args)
				{
				if (arg == "-chapo")
					{
					bIsChapo = true;
					continue;
					}

				if (arg == "-local")
					{
					szScheme = "http://localhost";
					continue;
					}

				string pat = @"(?<dlID>[\da-z]{32})";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match m = r.Match(arg);
				if (m.Success)
					szDLID = m.Groups["dlID"].Value;
				}
			return szDLID;
			}
		/// <summary>
		/// Lance la session de Download 
		/// </summary>
		/// <param></param>
		/// <returns>true if success</returns>
		static bool Download_And_Install(string szDLID)
			{
			/////////////////////////////////////////////////////
			//Get first Infos
			/////////////////////////////////////////////////////
			var result = string.Empty;
			string szUrl = szScheme + "/17b2/Get/" + szDLID + ".zip";
			try
				{
				result = new WebClient().DownloadString(szUrl);
				}
			catch (Exception)
				{
				MessageBox.Show(appLng._i18n("file_session_not_found"), Application.ProductName);
				return false;
				}
			if (result == string.Empty)
				return false;

			string[] datas = result.Split('|');
			if (datas.Length != 5)
				return false;

			//Récupération des paramètres
			//ID de DL
			string szID = datas[0];
			if (szID != szDLID)
				return false;
			//Filename
			string szFileName = datas[1];
			if (szFileName == string.Empty || szFileName == "noname")
				return false;
			//File Size
			int nFSize;
			bool bSize = Int32.TryParse(datas[2], out nFSize);
			if (!bSize)
				return false;
			if (nFSize <= 0)
				return false;
			//appID
			int nappID;
			bool bappId = Int32.TryParse(datas[3], out nappID);
			if (!bappId)
				return false;
			if (nappID < 0)
				return false;
			//FileType
			int nflTyp;
			bool bflTyp = Int32.TryParse(datas[4], out nflTyp);
			if (!bflTyp)
				return false;
			if (nflTyp != 1 && nflTyp != 2)
				return false;
			if (nflTyp == 1 && nappID == 0)
				return false;
			if (nflTyp == 2 && nappID != 0)
				return false;
			//////////////////////////////////////////////////////////
			//Create temp file
			/////////////////////////////////////////////////////////
			string szTmpFile = string.Empty;
			string szTmpPath = string.Empty;
			try
				{
				szTmpPath = System.IO.Path.GetTempPath();
				szTmpFile = System.IO.Path.Combine(szTmpPath, szFileName);
				if (nflTyp == 1)
					szTmpFile += ".zip";
				else
					szTmpFile += ".wad.zip";
				}
			catch (Exception)
				{
				return false;
				}
			//////////////////////////////////////////////////////////
			//Show Dialog Box
			//////////////////////////////////////////////////////////
			DownDlg myDown = new DownDlg(szDLID, szTmpFile, nFSize, nappID, nflTyp, szScheme);
			DialogResult dlgRes = myDown.ShowDialog();
			Program.mConf.SaveMainConfig(Program.mRc);
			//////////////////////////////////////////////////////////
			//Nettoyer après usage
			//////////////////////////////////////////////////////////
			try
				{
				FileInfo f = new FileInfo(szTmpFile);
				if (f.Exists)
					f.Delete();
				}
			catch (Exception)
				{
				return false;
				}
			return true;
			}
		}
	}
