using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;
using HLBox17b.Forms;
using HLBox17b.Forms.MdiForms;
using HLBox17b.Forms.Misc;
using System.Text.RegularExpressions;


namespace HLBox17b
	{
	public partial class MainForm : Form
		{
		private int nChildsCount;
		private List<string> arAuthorizedExts;
		private List<string> arRecentFilesLst;
		private int nRecentFilesHistorySize;
		private int nMaxShortenPathlen;
		public string szDragSource;
		public bool bDragOnListClient;

		public MainForm()
			{
			szDragSource=string.Empty;
			bDragOnListClient=false;
			InitializeComponent();
			TranslateForm();
			mainform_InitDatas();
			mainform_RedrawRecents();
			mainForm_UpdateMenu(false);
			}

		private void TranslateForm()
			{
			this.Text = Application.ProductName;
			menu_File.Text = Program.appLng._i18n("menu_file");
			menu_File_Open.Text = Program.appLng._i18n("menu_file_open");
			menu_File_Close.Text = Program.appLng._i18n("menu_file_close");
			menu_File_CloseAll.Text = Program.appLng._i18n("menu_file_closeall");
			menu_File_Recents.Text = Program.appLng._i18n("menu_file_recents");
			menu_File_Options.Text = Program.appLng._i18n("menu_file_options");
			menu_File_Quit.Text = Program.appLng._i18n("menu_file_quit");
			menu_Maps.Text = Program.appLng._i18n("menu_maps");
			menu_Maps_Pack.Text = Program.appLng._i18n("menu_maps_package");
			menu_Maps_Install.Text = Program.appLng._i18n("menu_maps_install");
			menu_Maps_Resgen.Text = Program.appLng._i18n("menu_maps_resgen");
			menu_Textures.Text = Program.appLng._i18n("menu_texes");
			menu_Textures_NewWad.Text = Program.appLng._i18n("menu_texes_createwad");
			menu_17b.Text = Program.appLng._i18n("menu_17b");
			menu_17b_Check.Text = Program.appLng._i18n("menu_17b_check_files");
			menu_17b_CheckMaps.Text = Program.appLng._i18n("menu_17b_check_maps");
			menu_17b_CheckWads.Text = Program.appLng._i18n("menu_17b_check_wads");
			menu_17b_UserLogin.Text = Program.appLng._i18n("menu_17b_login");
			menu_17b_Visit17b.Text = Program.appLng._i18n("menu_17b_website");
			menu_Steam.Text = Program.appLng._i18n("menu_steam");
			menu_Steam_Banner.Text = Program.appLng._i18n("menu_steam_banner");
			menu_Steam_Clean.Text = Program.appLng._i18n("menu_steam_clean");
			menu_Window.Text = Program.appLng._i18n("menu_window");
			menu_Window_Arrange.Text = Program.appLng._i18n("menu_win_arrange");
			menu_Window_Cascade.Text = Program.appLng._i18n("menu_win_cascade");
			menu_Window_Horizontal.Text = Program.appLng._i18n("menu_win_horizontal");
			menu_Window_Vertical.Text = Program.appLng._i18n("menu_win_vertical");
			menu_Window_Maximize.Text = Program.appLng._i18n("menu_win_maximize");
			menu_Window_Minimize.Text = Program.appLng._i18n("menu_win_minimize");
			menu_Help.Text = Program.appLng._i18n("menu_help");
			menu_Help_About.Text = Program.appLng._i18n("menu_help_about");
			}

		private void mainform_InitDatas()
			{
			nChildsCount = 1;
			arAuthorizedExts = new List<string>() { ".wad", ".bsp", ".vtf", ".vmt", ".gcf", ".vpk", ".ncf", ".manifest", ".zip", ".rar", ".7z",".mdl",".spr" };
			nRecentFilesHistorySize = Program.mRc.nGens_HistorySize;
			arRecentFilesLst = mainform_GetRecents();
			nMaxShortenPathlen = 65;
			}

		private void mainform_Shown(object sender, EventArgs e)
			{
			this.mainform_OpenFiles(Environment.GetCommandLineArgs());
			}

		public void mainform_OpenFiles(string[] files)
			{
			Form[] charr = this.MdiChildren;
			foreach (string file in files)
				{
				foreach (MdiChild wdform in charr)
					{
					if (wdform.szFullFile == file)
						{
						if (files.Length == 1)
							{
							wdform.Activate();
							return;
							}
						else
							{
							continue;
							}
						}
					}
				mainform_AddChild(file);
				}
			}

		private void mainForm_UpdateMenu(bool bClosingForm)
			{
			int NumChilds = 0;
			Form[] charr = this.MdiChildren;
			foreach (Form wdform in charr)
				NumChilds++;

			if (bClosingForm)
				NumChilds--;

			if (NumChilds > 0)
				{
				this.menu_File_Sep1.Visible = true;
				this.menu_File_CloseAll.Visible = true;
				this.menu_File_Close.Visible = true;
				this.menu_Window.Visible = true;
				}
			else
				{
				this.menu_File_Sep1.Visible = false;
				this.menu_File_CloseAll.Visible = false;
				this.menu_File_Close.Visible = false;
				this.menu_Window.Visible = false;
				}
			}
		private List<string> mainform_GetOpenedFiles()
			{
			List<string> OpenedFiles = new List<string>();

			Form[] charr = this.MdiChildren;
			foreach (MdiChild wdform in charr)
				{
				OpenedFiles.Add(wdform.szFullFile);
				}
			return OpenedFiles;
			}

		private void mainform_AddChild(string file)
			{
			if (file == null)
				return;

			string szFileName = file.Trim();


			if (szFileName == string.Empty)
				return;

			if (!FilesTools.CheckFile(szFileName, arAuthorizedExts))
				return;

			List<string> OpenedFiles = mainform_GetOpenedFiles();
			if (OpenedFiles.Contains(file))
				return;
			
			mainform_AddRecents(szFileName);

			FileInfo f = new FileInfo(szFileName);
			Program.mRc.szGen_LastPath = f.DirectoryName;

			MdiChild ChildWnd = new MdiChild(); ;

			string szExt = Path.GetExtension(szFileName).ToLower();
			if (szExt == ".wad")
				{
				WadMdi wdForm = new WadMdi(szFileName,false);
				ChildWnd = wdForm;
				Program.mRc.szTxs_LastPath = f.DirectoryName;
				}
			else if (szExt == ".gcf" ||
					szExt == ".ncf" ||
					szExt == ".manifest"
					)
				{
				XplMdi wdForm = new XplMdi(szFileName);
				ChildWnd = wdForm;
				Program.mRc.szXpl_LastInputPath = f.DirectoryName;
				}
			else if (szExt == ".mdl")
				{
				MdlMdi wdForm = new MdlMdi(szFileName);
				ChildWnd = wdForm;
				Program.mRc.szTxs_LastPath = f.DirectoryName;
				}
			else if (szExt == ".vtf" ||
					szExt == ".vmt"
					)
				{
				VtfMdi wdForm = new VtfMdi(szFileName);
				ChildWnd = wdForm;
				Program.mRc.szTxs_LastPath = f.DirectoryName;
				}
			else if (szExt == ".vpk")
				{
				string szRootFile = mainform_GetVpkDir(szFileName);
				if (szRootFile == null)
					{
					MessageBox.Show(Program.appLng._i18n("xpl_rootfile_notfound"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
					}
				XplMdi wdForm = new XplMdi(szRootFile);
				ChildWnd = wdForm;
				Program.mRc.szXpl_LastInputPath = f.DirectoryName;
				}
			else
				{
				return;
				}

			FormWindowState FrmState = FormWindowState.Maximized;
			if (this.ActiveMdiChild != null)
				FrmState = this.ActiveMdiChild.WindowState;
			ChildWnd.MdiParent = this;
			ChildWnd.Location = new Point(0, 0);
			ChildWnd.Text = Path.GetFileName(szFileName);
			ChildWnd.Show();
			ChildWnd.WindowState = FrmState;
			ChildWnd.FormClosed += new FormClosedEventHandler(mainForm_ActiveMdiChild_FormClosed);
			this.mainForm_UpdateMenu(false);
			}


		private string mainform_GetVpkDir(string szFilename)
			{
			string szExt = FilesTools.GetExtension(szFilename);
			string szPathName = Path.GetDirectoryName(szFilename) + "\\";
			string szNom = FilesTools.GetOnlyName(szFilename);

			string pat = @"(.*)_\d+\.vpk$";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase);
			Match m = r.Match(szFilename);

			if (m.Success)
				{
				if (m.Groups[1].Value != null && m.Groups[1].Value != "")
					{
					szFilename = m.Groups[1].Value + "_dir." + szExt;
					return szFilename;
					}
				}
			return null;
			}

		#region MenusClicks

		private void menu_File_Open_Click(object sender, EventArgs e)
			{
			if (openFileDialog.ShowDialog(this) != DialogResult.OK)
				return;
			this.mainform_OpenFiles(openFileDialog.FileNames);
			}

		private void menu_File_Options_Click(object sender, EventArgs e)
			{
			CfgFrm CfgForm = new CfgFrm(this);
			CfgForm.InitConfDatas(Program.mRc);
			DialogResult dlgRes = CfgForm.ShowDialog();
			if (dlgRes == DialogResult.OK)
				{
				Program.mRc.szGen_Language = CfgForm.NewLanguage;
				Program.mRc.bGen_ShowSplash = CfgForm.chk_show_welcome.Checked ? true : false;
				Program.mRc.bGen_Simulate = CfgForm.chk_activate_simulation.Checked ? true : false;
				Program.mRc.bGen_AskBeforeExit = CfgForm.chk_ask_before_exit.Checked ? true : false;
				Program.mRc.bGen_CheckVerAtStartup = CfgForm.chk_check_new_version.Checked ? true : false;
				Program.mRc.bPack_CreateRes = CfgForm.chk_create_res.Checked ? true : false;
				Program.mRc.bPack_OverwriteRes = CfgForm.chk_res_overwrite.Checked ? true : false;
				Program.mRc.bPack_IncludeWads = CfgForm.chk_include_wads.Checked ? true : false;
				Program.mRc.bPack_PakReplace = CfgForm.chk_replace_packs.Checked ? true : false;

				Level LvlTmp = (Level)CfgForm.ctl_pack_ziplevels.Items[CfgForm.ctl_pack_ziplevels.SelectedIndex];
				Program.mRc.nPack_ZipLevel = LvlTmp.Idx;

				Program.mRc.bInst_AnalysePath = CfgForm.chk_parse_folders.Checked ? true : false;
				Program.mRc.szInst_MiscPath = CfgForm.ctl_smi_unpackmiscpath.Text;
				Program.mRc.bInst_Replace = CfgForm.chk_replace.Checked ? true : false;
				Program.mRc.bInst_AskWhenDiffers = CfgForm.chk_ask_datetime.Checked ? true : false;
				Program.mRc.bInst_AutoDetectFolder = CfgForm.chk_detect_target_folder.Checked ? true : false;
				// One For All
				if (CfgForm.rd_multiple_all.Checked)
					Program.mRc.nPack_OneForAll = 1;
				else if (CfgForm.rd_multiple_one.Checked)
					Program.mRc.nPack_OneForAll = 2;
				else
					Program.mRc.nPack_OneForAll = 0;

				if (Program.mRc.nPack_OneForAll == 1)
					Program.mRc.bOneForAll = true;
				else if (Program.mRc.nPack_OneForAll == 2)
					Program.mRc.bOneForAll = false;
				//Empty Dst Folder
				if (CfgForm.rd_emptypath_error.Checked)
					Program.mRc.nPack_EmptyPath = 1;
				else if (CfgForm.rd_emptypath_pack.Checked)
					Program.mRc.nPack_EmptyPath = 2;
				else
					Program.mRc.nPack_EmptyPath = 0;

				Program.mRc.bRes_CommentUnnecessaryFiles = CfgForm.chk_include_commented.Checked ? true : false;


				Program.mRc.bSys_AssociateBsp = CfgForm.chk_associate_maps.Checked ? true : false;
				if (Program.mRc.bSys_AssociateBsp)
					{
					RegTools.AssociateFileInfo(".bsp");
					}
				else
					{
					RegTools.RestoreFileInfo(".bsp");
					}
				Program.mRc.bSys_AssociatePck = CfgForm.chk_associate_packs.Checked ? true : false;
				if (Program.mRc.bSys_AssociatePck)
					{
					RegTools.AssociateFileInfo(".gcf");
					RegTools.AssociateFileInfo(".ncf");
					RegTools.AssociateFileInfo(".vpk");
					RegTools.AssociateFileInfo(".manifest");
					}
				else
					{
					RegTools.RestoreFileInfo(".gcf");
					RegTools.RestoreFileInfo(".ncf");
					RegTools.RestoreFileInfo(".vpk");
					RegTools.RestoreFileInfo(".manifest");
					}
				Program.mRc.bSys_AssociateTex = CfgForm.chk_associate_texes.Checked ? true : false;
				if (Program.mRc.bSys_AssociateTex)
					{
					RegTools.AssociateFileInfo(".wad");
					RegTools.AssociateFileInfo(".vtf");
					RegTools.AssociateFileInfo(".vmt");
					}
				else
					{
					RegTools.RestoreFileInfo(".wad");
					RegTools.RestoreFileInfo(".vtf");
					RegTools.AssociateFileInfo(".vmt");
					}
				Program.mRc.bSys_CheckAndRestore = CfgForm.chk_restore_startup.Checked ? true : false;
				//Textures
				Program.mRc.szTxs_DefEditor = CfgForm.tb_external_editor.Text;
				Program.mRc.szTxs_DefImgType = CfgForm.cb_export_format.Text;
				Program.mRc.nTxs_DefThumbSize = Int32.Parse(CfgForm.cb_thumbnail_size.Text.Split('x')[0]);
				Program.mRc.nTxs_NewColor1 = CfgForm.TexColor1.ToArgb();
				Program.mRc.nTxs_NewColor2 = CfgForm.TexColor2.ToArgb();

				Program.mConf.SaveMainConfig(Program.mRc);
				}

			}
		private void menu_File_Close_Click(object sender, EventArgs e)
			{
			Form wdForm = this.ActiveMdiChild;
			wdForm.Close();
			}
		private void menu_Maps_Pack_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("Pack Map", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return;
			}

		private void menu_Maps_Install_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("Install Map", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return;
			}

		private void menu_Maps_Resgen_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("Resgen", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return;
			}

		private void menu_Textures_NewWad_Click(object sender, EventArgs e)
			{
			FormWindowState FrmState = FormWindowState.Maximized;
			if (this.ActiveMdiChild != null)
				FrmState = this.ActiveMdiChild.WindowState;
			string szFileName = "wad"+nChildsCount.ToString() + ".wad";
			WadMdi ChildWnd = new WadMdi(szFileName,true);
			ChildWnd.MdiParent = this;
			ChildWnd.Location = new Point(0, 0);
			ChildWnd.Text = Path.GetFileName(szFileName);
			ChildWnd.Show();
			ChildWnd.WindowState = FrmState;
			ChildWnd.FormClosed += new FormClosedEventHandler(mainForm_ActiveMdiChild_FormClosed);
			this.mainForm_UpdateMenu(false);
			nChildsCount++;
			}

		private void menu_17b_CheckMaps_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("Check Maps", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return;
			}

		private void menu_17b_CheckWads_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("Check Wads", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return;
			}

		private void menu_17b_UserLogin_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("User Login", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			return;
			}

		private void menu_17b_Visit17b_Click(object sender, EventArgs e)
			{
			WebTools.Goto17bWebSite();
			}

		private void menu_Steam_Banner_Click(object sender, EventArgs e)
			{
			SteamBanner dDlg = new SteamBanner();
			dDlg.ShowDialog();
			}
	
		private void menu_Steam_Clean_Click(object sender, EventArgs e)
			{
			if (!MiscTools.CheckChapo())
				return;
			MessageBox.Show("Steam Clean", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		private void menu_File_CloseAll_Click(object sender, EventArgs e)
			{
			Form[] charr = this.MdiChildren;
			foreach (Form wdform in charr)
				wdform.Close();
			}

		private void menu_File_Quit_Click(object sender, EventArgs e)
			{
			this.Close();
			}

		private void arrangeToolStripMenuItem_Click(object sender, EventArgs e)
			{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
			}

		private void menu_Window_Cascade_Click(object sender, EventArgs e)
			{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
			}

		private void menu_Window_Horizontal_Click(object sender, EventArgs e)
			{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
			}

		private void menu_Window_Vertical_Click(object sender, EventArgs e)
			{
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
			}

		private void menu_Window_Maximize_Click(object sender, EventArgs e)
			{
			Form[] charr = this.MdiChildren;
			foreach (Form wdForm in charr)
				wdForm.WindowState = FormWindowState.Maximized;
			}

		private void menu_Window_Minimize_Click(object sender, EventArgs e)
			{
			Form[] charr = this.MdiChildren;
			foreach (Form wdForm in charr)
				wdForm.WindowState = FormWindowState.Minimized;
			}
		
		private void menu_Help_About_Click(object sender, EventArgs e)
			{
			About ab = new About(false);
			ab.ShowDialog();
			}

		#endregion

		#region Recents

		private void mainform_CheckRecentsState()
			{
			menu_File_Recents.Enabled = menu_File_Recents.HasDropDownItems;
			}

		public void mainform_AddRecents(string szFileName)
			{
			int nIdx = arRecentFilesLst.IndexOf(szFileName);
			if (nIdx >= 0)
				arRecentFilesLst.RemoveAt(nIdx);
			arRecentFilesLst.Insert(0, szFileName);
			RegTools.SaveRecents("", arRecentFilesLst, Program.mRc.nGens_HistorySize);
			mainform_RedrawRecents();
			mainform_CheckRecentsState();
			}
		
		private List<string> mainform_GetRecents()
			{
			List<string> arRecents = new List<string>();
			arRecents = RegTools.GetRecents("", Program.mRc.nGens_HistorySize);
			return arRecents;
			}

		private void mainform_Recent_Clicked(object sender, EventArgs e)
			{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item != null)
				{
				int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
				//Si un formulaire est déjà ouvert avec le même fichier, on lui donne le focus
				Form[] charr = this.MdiChildren;
				foreach (MdiChild wdform in charr)
					{
					if (wdform.szFullFile == arRecentFilesLst[index])
						{
						wdform.Activate();
						return;
						}
					}
				mainform_AddChild(arRecentFilesLst[index]);
				}
			}

		private void mainform_RedrawRecents()
			{
			menu_File_Recents.DropDownItems.Clear();
			for (int nMain = 0; nMain < arRecentFilesLst.Count; nMain++)
				{
				if (nMain > nRecentFilesHistorySize)
					break;
				string szEntry = String.Format("{0} {1}", nMain + 1, FilesTools.ShortenPathName(arRecentFilesLst[nMain], nMaxShortenPathlen));
				ToolStripMenuItem tsmItem = new ToolStripMenuItem(szEntry);
				tsmItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
				tsmItem.Click += new System.EventHandler(mainform_Recent_Clicked);
				menu_File_Recents.DropDownItems.Add(tsmItem);
				}
			mainform_CheckRecentsState();
			}
		#endregion

		#region DragAndDrop

		private void mainform_DragOver(object sender, DragEventArgs e)
			{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
			}

		private void mainform_DragDrop(object sender, DragEventArgs e)
			{
			string[] handles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			Form[] charr = this.MdiChildren;

			foreach (string file in handles)
				{
				foreach (MdiChild wdform in charr)
					{
					if (wdform.szFullFile == file)
						{
						if (handles.Length == 1)
							{
							wdform.Activate();
							return;
							}
						else
							{
							continue;
							}
						}
					}
				mainform_AddChild(file);
				}
			}

		public void mainform_DropDownFile(string szfile)
			{
			if (File.Exists(szfile))
				{
				Form[] charr = this.MdiChildren;

				if (mainform_CheckAuthorizedExts(szfile))
					{
					foreach (MdiChild wdform in charr)
						{
						if (wdform.szFullFile == szfile)
							{
							wdform.Activate();
							return;
							}
						}
					mainform_AddChild(szfile);
					}
				}
			}

		#endregion

		#region MDIChildClose

		private void mainform_FormClosing(object sender, FormClosingEventArgs e)
			{
			if (e.CloseReason.ToString() != "ApplicationExitCall" && Program.mRc.bGen_AskBeforeExit)
				{
				DialogResult r = MessageBox.Show(Program.appLng._i18n("really_want_to_quit"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

				if (r == DialogResult.Yes)
					{
					e.Cancel = false;
					}
				else
					{
					e.Cancel = true;
					return;
					}
				}

			int StillOpen = 0;
			Form[] charr = this.MdiChildren;
			foreach (Form wdform in charr)
				{
				wdform.Close();
				}

			charr = this.MdiChildren;
			foreach (Form wdform in charr)
				StillOpen++;
			if (StillOpen > 0)
				{
				e.Cancel = true;
				return;
				}

			if (!e.Cancel)
				{
				Program.mConf.SaveMainConfig(Program.mRc);
				}
			}

		private void mainForm_ActiveMdiChild_FormClosed(object sender, EventArgs e)
			{
			mainForm_UpdateMenu(true);
			}

		#endregion


		bool mainformIsactivated = false;
		private void mainform_Activated(object sender, EventArgs e)
			{
			if (!this.mainformIsactivated)
				{
				this.mainformIsactivated = true;
				mainform_CheckEditMenusStates();
				}
			}


		private void mainform_Deactivate(object sender, EventArgs e)
			{
			this.mainformIsactivated = false;
			}

		private bool mainform_CheckAuthorizedExts(string szFile)
			{
			foreach (string szX in arAuthorizedExts)
				{
				if (string.Compare(Path.GetExtension(szFile), szX, true) == 0)
					return true;
				}
			return false;
			}

		private void mainform_CheckEditMenusStates()
			{
			if (Clipboard.ContainsFileDropList())
				{
				System.Collections.Specialized.StringCollection collectionOfFiles = Clipboard.GetFileDropList();
				foreach (string szCopiedFile in collectionOfFiles)
					{
					if (mainform_CheckAuthorizedExts(szCopiedFile))
						{
						menuItem_Paste.Enabled = true;
						break;
						}
					}
				}
			else
				{
				menuItem_Paste.Enabled = false;
				}

			}

		private void menu_Edit_Paste_Click(object sender, EventArgs e)
			{
			if (Clipboard.ContainsFileDropList())
				{
				System.Collections.Specialized.StringCollection collectionOfFiles = Clipboard.GetFileDropList();
				foreach (string szCopiedFile in collectionOfFiles)
					{
					if (mainform_CheckAuthorizedExts(szCopiedFile))
						{
						mainform_DropDownFile(szCopiedFile);
						}
					}
				}
			}

		}
	}
