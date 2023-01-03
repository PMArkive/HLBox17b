using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using HLBox17b.Classes.Tools;
using SevenZip;
using System.Diagnostics;


namespace HLBox17b
	{
	public partial class DownDlg : Form
		{
		string szDLID;
		string szTargetFile;
		string szScheme;
		ToolTip ttip;
		bool bStatus;
		bool bBusy;
		bool bCanceled;
		int nRunningPhase;
		int nFSize;
		int nappID;
		int nflTyp;
		FileStream strLocal;
		string szDstPath;
		string szobJName;

		public DownDlg(string sDLID, string szFile, int FSize, int appID, int flTyp, string Scheme)
			{
			InitializeComponent();
			szDLID = sDLID;
			szTargetFile = szFile;
			szobJName = Path.GetFileNameWithoutExtension(szTargetFile).ToLower();
			szScheme = Scheme;
			szDstPath = string.Empty;
			bStatus = false;
			nRunningPhase = 0;
			nFSize = FSize;
			nappID = appID;
			nflTyp = flTyp;
			//Set Tooltips for buttons
			ttip = new ToolTip();
			ttip.AutoPopDelay = 4000;
			ttip.InitialDelay = 1000;
			ttip.ReshowDelay = 500;
			ttip.ShowAlways = true;
			TranslateForm();
			if (!FillComboMods())
				return;
			CheckTargetPath();	
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Translate Form
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void TranslateForm()
			{
			root_folder_mod.Text = Program.appLng._i18n("root_folder_mod");
			destination_mod.Text = Program.appLng._i18n("destination_mod");
			ttip.SetToolTip(btnbrowsefolder, Program.appLng._i18n("common_browse"));			
			if (Program.mRc.bGens_NoSteam) //No Steam
				{
				this.Text = Program.appLng._i18n("down_selectfolder");
				ttip.SetToolTip(btnCloseCancel, Program.appLng._i18n("download"));
				btnCloseCancel.Text = Program.appLng._i18n("download");
				ttip.SetToolTip(btnFinalAction, Program.appLng._i18n("view_folder"));
				btnFinalAction.Text = Program.appLng._i18n("view_folder");				
				Toggle_BrowseFolder(true);
				}
			else ///Official
				{
				if (nflTyp == 2) //Wad
					{
					this.Text = Program.appLng._i18n("select_dest_mod");
					ttip.SetToolTip(btnCloseCancel, Program.appLng._i18n("download"));
					btnCloseCancel.Text = Program.appLng._i18n("download");
					ttip.SetToolTip(btnFinalAction, Program.appLng._i18n("view_folder"));
					btnFinalAction.Text = Program.appLng._i18n("view_folder");
					}
				else //Map
					{
					this.Text = Program.appLng._i18n("common_please_wait");
					ttip.SetToolTip(btnCloseCancel, Program.appLng._i18n("common_cancel_ttip"));
					btnCloseCancel.Text = Program.appLng._i18n("common_cancel");
					ttip.SetToolTip(btnFinalAction, Program.appLng._i18n("play_map_ttip"));
					btnFinalAction.Text = Program.appLng._i18n("play_map");
					}
				Toggle_BrowseFolder(false);
				}
			btnFinalAction.Visible = false;
			}
		///////////////////////////////////////////////////////////
		//Toggle Browse Folder
		///////////////////////////////////////////////////////////
		private void Toggle_BrowseFolder(bool bActive)
			{
			if (bActive)
				{
				btnbrowsefolder.Visible = true;
				btnbrowsefolder.Enabled = true;
				edtModRootFolder.Width = 365;
				edtModRootFolder.ReadOnly = false;
				}
			else
				{
				btnbrowsefolder.Visible = false;
				btnbrowsefolder.Enabled = false;
				edtModRootFolder.Width = progressBar.Width;
				edtModRootFolder.ReadOnly = true;
				}
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Remplir la ComboBox de liste des Mods
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private bool FillComboMods()
			{
			List<ModName> arModsNames = new List<ModName>();

			ctl_tools_lstmods.DropDownStyle = ComboBoxStyle.DropDownList;
			ctl_tools_lstmods.DisplayMember = "Mod";
			ctl_tools_lstmods.ValueMember = "Id";

			//No Steam Version
			//On doit sélectionner un répertoire
			if (Program.mRc.bGens_NoSteam)
				{
				ctl_tools_lstmods.DataSource = arModsNames;
				ctl_tools_lstmods.Enabled = false;
				edtModRootFolder.Text = Program.mRc.szGen_DLTarget;
				return true;
				}
			//Version Officielle
			if (nappID != 0) //Installation d'une Map
				{
				myMods objMod = new myMods((uint)nappID,false);
				if (objMod.bIsOk)
					arModsNames.Add(new ModName(objMod.sModName, 0));
				else
					return false;
				ctl_tools_lstmods.Enabled = false;
				ctl_tools_lstmods.DataSource = arModsNames;
				ctl_tools_lstmods.SelectedIndex = 0;
				edtModRootFolder.Text = objMod.sTargetPath;
				//Si le répertoire du mod n'existe pas, on sélectionne un répertoire
				if (!FilesTools.CheckDestinationPath(objMod.sTargetPath))
					{
					Toggle_BrowseFolder(true);
					btnCloseCancel.Enabled = false;
					}
				}
			else //Installation d'un Wad
				{
				arModsNames = ModsTools.ReadLstMods(false, false, 1, true);
				ctl_tools_lstmods.Enabled = true;
				ctl_tools_lstmods.DataSource = arModsNames;
				if (Program.mRc.nDwn_DefAppId == 0)
					Program.mRc.nDwn_DefAppId = 10;
				ctl_tools_lstmods.SelectedIndex = ModsTools.GetComboAppIDIndex(Program.mRc.nDwn_DefAppId, arModsNames);
				}
			return true;
			}
		///////////////////////////////////////////////////////////
		//Changer le mod depuis la ComboBox
		///////////////////////////////////////////////////////////
		private void ctlLstMods_ChangeSelectedIndex(object sender, EventArgs e)
			{
			ModName ModTmp = (ModName)ctl_tools_lstmods.Items[ctl_tools_lstmods.SelectedIndex];
			myMods objMod = new myMods((uint)ModTmp.Id,false);
			if (objMod.bIsOk)
				edtModRootFolder.Text = objMod.sTargetPath;
			if (CheckTargetPath())
				nappID = ModTmp.Id;
			}
		///////////////////////////////////////////////////////////
		//Vérifier si le path est bon
		///////////////////////////////////////////////////////////
		private bool CheckTargetPath()
			{
			if (edtModRootFolder.Text == string.Empty)
				{
				btnCloseCancel.Enabled = false;
				return false;
				}
			else
				{
				btnCloseCancel.Enabled = true;
				return true;
				}
			}
		///////////////////////////////////////////////////////////
		//Parcourir le dossier de destination
		///////////////////////////////////////////////////////////
		private void btnbrowsefolder_Click(object sender, EventArgs e)
			{
			folderBrowserDialog1.SelectedPath = edtModRootFolder.Text;

			DialogResult dlgRes = folderBrowserDialog1.ShowDialog();

			if (dlgRes == DialogResult.OK)
				{
				edtModRootFolder.Text = folderBrowserDialog1.SelectedPath;
				}
			CheckTargetPath();
			}
		///////////////////////////////////////////////////////////
		//Changer le texte du folder
		///////////////////////////////////////////////////////////
		private void edtTextChanged(object sender, EventArgs e)
			{
			if (!FilesTools.CheckDestinationPath(edtModRootFolder.Text))
				btnCloseCancel.Enabled = false;
			else
				btnCloseCancel.Enabled = true;
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Click sur bouton Annuler et/ou Télécharger
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void Down_BtnClickCancel(object sender, EventArgs e)
			{
			if (nRunningPhase == 1 || nRunningPhase==2)
				{
				nRunningPhase = 2;
				bStatus = false;
				bgWork1.CancelAsync();
				DialogResult = DialogResult.Cancel;
				}
			else
				{
				this.Text = Program.appLng._i18n("common_please_wait");
				ttip.SetToolTip(btnCloseCancel, Program.appLng._i18n("common_cancel_ttip"));
				btnCloseCancel.Text = Program.appLng._i18n("common_cancel");
				if (!Program.mRc.bGens_NoSteam)
					{
					ModName ModTmp = (ModName)ctl_tools_lstmods.Items[ctl_tools_lstmods.SelectedIndex];
					nappID = ModTmp.Id;
					Program.mRc.nDwn_DefAppId = nappID;
					}
				Program.mRc.szGen_DLTarget = edtModRootFolder.Text;
				ctl_tools_lstmods.Enabled = false;
				edtModRootFolder.ReadOnly = true;
				btnbrowsefolder.Enabled = false;
				bgWork1.RunWorkerAsync();
				}
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Afficher le dialog
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void Down_OnShow(object sender, EventArgs e)
			{
			if (!Program.mRc.bGens_NoSteam && nflTyp == 1)
				{
				bgWork1.RunWorkerAsync();
				}
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Fermeture du dialogue
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		protected override void OnFormClosing(FormClosingEventArgs e)
			{
			base.OnFormClosing(e);
			if (bgWork1.IsBusy)
				{
				bgWork1.Dispose();
				if (strLocal != null)
					{
					try
						{
						strLocal.Close();
						}
					catch (Exception)
						{
						}
					}
				}
			DialogResult = DialogResult.Cancel;
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Démarrer Téléchargement
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void bgWork1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
			{
			string szUrl = Program.szScheme + "/17b2/Get/" + szDLID + ".zip";

			nRunningPhase = 1;

			try
				{
				Uri url = new Uri(szUrl);
				long iRunningByteTotal = 0;

				WebClient client = new WebClient();
				Stream strRemote = client.OpenRead(url);
				strLocal = new FileStream(szTargetFile, FileMode.Create, FileAccess.Write, FileShare.None);
				int iByteSize = 0;
				byte[] byteBuffer = new byte[1024];
				while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
					{
					if (bgWork1.CancellationPending)
						{
						e.Cancel = true;
						nRunningPhase = 0;
						break;
						}
					strLocal.Write(byteBuffer, 0, iByteSize);
					iRunningByteTotal += iByteSize;
					double dIndex = (double)(iRunningByteTotal);
					double dTotal = (double)nFSize;
					double dProgressPercentage = (dIndex / dTotal);
					int iProgressPercentage = (int)(dProgressPercentage * 100);
					bgWork1.ReportProgress(iProgressPercentage);
					}
				strRemote.Close();
				strLocal.Close();
				if (nRunningPhase==1)
					bStatus = true;
				}

			catch (Exception)
				{
				bStatus = false;
				}
			nRunningPhase = 2;
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Mise à jour Progress Bar
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void bgWork1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
			{
			progressBar.Value = e.ProgressPercentage;
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Fin du Téléchargement
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void bgWork1_Completed(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
			{
			if (bStatus)
				{
				StartInstalling();
				}
			nRunningPhase = 2;
			}

		private void StartInstalling()
			{
			bBusy=true;
			bCanceled=false;
			progressBar.Value = 0;
			btnCloseCancel.Click += new System.EventHandler(this.Unpak_ClickCancel);
			if (Program.mRc.bGens_NoSteam)
				{
				szDstPath = Program.mRc.szGen_DLTarget;
				}
			else
				{
				myMods objMod = new myMods((uint)nappID,false);
				if (!objMod.bIsOk)
					return;
				szDstPath = objMod.sTargetPath;
				}
			bgWork2.RunWorkerAsync();
			}

		void Unpak_HandleExistingFile(object sender, FileOverwriteEventArgs e)
			{
			}

		private void Unpak_ClickCancel(object sender, EventArgs e)
			{
			if (bBusy)
				{
				bCanceled = true;
				bBusy = false;
				}
			else
				{
				this.Close();
				}
			}
		private void bgWork2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
			{
			string szSevenZipPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\7z.dll";
			SevenZipExtractor.SetLibraryPath(szSevenZipPath);

			int szIdx = 0;
			double ActFileIdx = 0;
			double TotalFiles = 0;
			List<string> arMapsNames = new List<string>();
			string szActFile = string.Empty;
			string szTotFile = string.Empty;
			string szTargetPath = string.Empty;
			
			string Newpath = string.Empty;
			SevenZipExtractor sZip;
			ArchiveFileInfo entry;
			string szPakName = Path.GetFileName(szTargetFile);
			try
				{
				using (sZip = new SevenZipExtractor(szTargetFile))
					{
					for (szIdx = 0; szIdx < sZip.ArchiveFileData.Count; szIdx++)
						{
						entry = sZip.ArchiveFileData[szIdx];
						if (entry.IsDirectory)
							continue;
						string szExt = FilesTools.GetExtension(entry.FileName).ToLower();
						if (nflTyp == 1 && szExt == "bsp")
							arMapsNames.Add(entry.FileName);
						TotalFiles++;
						}
					//On lance l'extraction
					sZip.PreserveDirectoryStructure = false;
					sZip.FileExists += new EventHandler<FileOverwriteEventArgs>(Unpak_HandleExistingFile);
					for (szIdx = 0; szIdx < sZip.ArchiveFileData.Count; szIdx++)
						{
						entry = sZip.ArchiveFileData[szIdx];
						if (bCanceled)
							{
							return;
							}
						else
							{
							if (entry.IsDirectory)
								continue;
							szActFile = entry.FileName;
							string szExt = FilesTools.GetExtension(szActFile).ToLower();
							if (nflTyp == 1 || (nflTyp == 2 && szExt == "wad"))
								{
								if (szActFile == "")
									continue;
								Newpath = string.Empty;
								if (nflTyp == 1)
									Newpath = MiscTools.GetSteamPath(szPakName, arMapsNames, szActFile);
								szActFile = Path.Combine(Newpath,Path.GetFileName(szActFile));
								/////////////////////////
								szTotFile = System.IO.Path.Combine(szDstPath, szActFile);
								szTargetPath = Path.GetDirectoryName(szTotFile);
								try
									{
									sZip.ExtractFiles(szTargetPath, entry.Index);
									}
								catch (Exception)
									{
									}
								}
							ActFileIdx++;
							int Percent = (int)(((double)ActFileIdx) / (double)TotalFiles * 100);
							bgWork2.ReportProgress(Percent);
							/////////////////////////
							}
						}
					}
				}
			catch (Exception)
				{
				return;
				}
			return;
			}
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		//Mise à jour Progress Bar
		///////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////
		private void bgWork2_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
			{
			progressBar.Value = e.ProgressPercentage;
			}


		private void bgWork2_Completed(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
			{
			this.Text = Program.appLng._i18n("install_completed");
			btnFinalAction.Visible = true;
			ttip.SetToolTip(btnCloseCancel, Program.appLng._i18n("common_quit_ttip"));
			btnCloseCancel.Text = Program.appLng._i18n("common_quit");
			}

		private void btnLastAction_Click(object sender, EventArgs e)
			{
			if (!Program.mRc.bGens_NoSteam && nflTyp == 1)
				{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = Program.mRc.szGen_SteamPath + "\\steam.exe";
				startInfo.Arguments = "-applaunch " + nappID;
				Process.Start(startInfo);
				}
			else
				{
				Process.Start(szDstPath);
				}
			this.Close();
			}
		}
	}
