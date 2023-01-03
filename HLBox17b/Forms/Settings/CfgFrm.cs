using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;


namespace HLBox17b
	{
	public partial class CfgFrm : Form
		{
		private MainForm m_parent;
		public string NewLanguage;
		public List<installed_lang> installedlangs;
		public List<string> installedMods;
		private Color _texcolor1;
		private Color _texcolor2;

		public Color TexColor1 { get { return _texcolor1; } }
		public Color TexColor2 { get { return _texcolor2; } }

		public CfgFrm(MainForm mp)
			{
			InitializeComponent();

			m_parent = mp;
	
			//Tabs Titles
			cfgtabControl.SelectTab(0);

			TranslateForm();

			installedlangs = Program.appLng.GetInstalledLangs();

			//Langues
			List<Langs> arLangs = new List<Langs>();
			foreach (installed_lang ilng in installedlangs)
				{
				arLangs.Add(new Langs(ilng.Lib, ilng.Lng));
				}
			ctl_gen_Language.DropDownStyle = ComboBoxStyle.DropDownList;
			ctl_gen_Language.DisplayMember = "Lib";
			ctl_gen_Language.ValueMember = "Lng";
			ctl_gen_Language.DataSource = arLangs;
			//Liste des Niveaux de compression
			List<Level> arZipLevels = new List<Level>();
			arZipLevels.Add(new Level(0, Program.appLng._i18n("confdlg_level0")));
			arZipLevels.Add(new Level(1, Program.appLng._i18n("confdlg_level1")));
			arZipLevels.Add(new Level(2, Program.appLng._i18n("confdlg_level2")));
			arZipLevels.Add(new Level(3, Program.appLng._i18n("confdlg_level3")));
			ctl_pack_ziplevels.DropDownStyle = ComboBoxStyle.DropDownList;
			ctl_pack_ziplevels.DisplayMember = "Lib";
			ctl_pack_ziplevels.ValueMember = "Idx";
			ctl_pack_ziplevels.DataSource = arZipLevels;
			}

		public void InitConfDatas(stRegConf rc)
			{
			//Vitesse de compression
			foreach (Level lvl in ctl_pack_ziplevels.Items)
				{
				if (lvl.Idx == rc.nPack_ZipLevel)
					{
					int idx = ctl_pack_ziplevels.Items.IndexOf(lvl);
					ctl_pack_ziplevels.SelectedIndex = idx;
					break;
					}
				}
			NewLanguage = rc.szGen_Language;
			foreach (Langs lng in ctl_gen_Language.Items)
				{
				if (lng.Lng == NewLanguage)
					{
					int idx = ctl_gen_Language.Items.IndexOf(lng);
					ctl_gen_Language.SelectedIndex = idx;
					break;
					}
				}
			ctl_gen_Language.SelectedIndexChanged += delegate
				{
				Langs Lang = ctl_gen_Language.SelectedItem as Langs;
				string tmplng = Lang.Lng;
				string szMessage="";
				if (tmplng != NewLanguage)
					{
					foreach (installed_lang ilng in installedlangs)
						{
						if (ilng.Lng == tmplng)
							{
							szMessage = ilng.Rst;
							break;
							}
						}
					if (szMessage == "")
						szMessage = Program.appLng._i18n("Lng_Rst");
					MessageBox.Show(szMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
					}
				NewLanguage = Lang.Lng;
				};


			chk_activate_simulation.Checked = rc.bGen_Simulate ? true : false;
			chk_show_welcome.Checked = rc.bGen_ShowSplash ? true : false;
			chk_ask_before_exit.Checked = rc.bGen_AskBeforeExit ? true : false;
			chk_check_new_version.Checked = rc.bGen_CheckVerAtStartup ? true : false;
			chk_create_res.Checked = rc.bPack_CreateRes ? true : false;
			chk_res_overwrite.Checked = rc.bPack_OverwriteRes ? true : false;
			chk_parse_folders.Checked = rc.bInst_AnalysePath ? true : false;
			chk_detect_target_folder.Checked = rc.bInst_AutoDetectFolder ? true : false;
			ctl_smi_unpackmiscpath.Text = rc.szInst_MiscPath;
			ctl_smi_unpackmiscpath.Enabled = chk_parse_folders.Checked;
			chk_replace.Checked = rc.bInst_Replace ? true : false;
			chk_ask_datetime.Checked = rc.bInst_AskWhenDiffers ? true : false;
			chk_ask_datetime.Enabled = chk_replace.Checked;
			chk_replace_packs.Checked = rc.bPack_PakReplace ? true : false;
			chk_include_wads.Checked = rc.bPack_IncludeWads ? true : false;

			chk_associate_maps.Checked = rc.bSys_AssociateBsp ? true : false;
			chk_associate_packs.Checked = rc.bSys_AssociatePck ? true : false;
			chk_associate_texes.Checked = rc.bSys_AssociateTex ? true : false;
			chk_restore_startup.Enabled = (rc.bSys_AssociateBsp || rc.bSys_AssociatePck || rc.bSys_AssociateTex) ? true : false;
			chk_restore_startup.Checked = rc.bSys_CheckAndRestore ? true : false;


			// One For All
			if (rc.nPack_OneForAll == 1)
				rd_multiple_all.Checked = true;
			else if (rc.nPack_OneForAll == 2)
				rd_multiple_one.Checked = true;
			else
				rd_multiple_ask.Checked = true;

			//Empty Dst Folder
			if (rc.nPack_EmptyPath == 1)
				rd_emptypath_error.Checked = true;
			else if (rc.nPack_EmptyPath == 2)
				rd_emptypath_pack.Checked = true;
			else
				rd_emptypath_warn.Checked = true;

			//Textures
			tb_external_editor.Text = rc.szTxs_DefEditor;
			FillThumbSizes(rc);
			FillImgTypes(rc);
			_texcolor1 = Color.FromArgb(Program.mRc.nTxs_NewColor1);
			_texcolor2 = Color.FromArgb(Program.mRc.nTxs_NewColor2);
			nwTexColor1.BackColor = _texcolor1;
			nwTexColor2.BackColor = _texcolor2;

			//Resgen
			chk_include_commented.Checked = rc.bRes_CommentUnnecessaryFiles ? true : false;
		}

		private void FillThumbSizes(stRegConf rc)
			{
			string defsizes = string.Empty;
			string[] sizes = new string[]{"32","48","64","128"};
			foreach (string sz in sizes)
				{
				cb_thumbnail_size.Items.Add(sz+"x"+sz);
				}
			defsizes = rc.nTxs_DefThumbSize.ToString() + "x" + rc.nTxs_DefThumbSize.ToString();
			cb_thumbnail_size.SelectedIndex = cb_thumbnail_size.Items.IndexOf(defsizes); 
			}

		private void FillImgTypes(stRegConf rc)
			{
			string[] tps = new string[]{"png","jpg","bmp","gif"};
			foreach (string tp in tps)
				{
				cb_export_format.Items.Add(tp);
				}
			cb_export_format.SelectedIndex = cb_export_format.Items.IndexOf(rc.szTxs_DefImgType);
			}

		private void ctl_analysepath_CheckedChanged(object sender, EventArgs e)
		{
			ctl_smi_unpackmiscpath.Enabled = chk_parse_folders.Checked;
		}

		private void ctl_smireplace_CheckedChanged(object sender, EventArgs e)
		{
			chk_ask_datetime.Enabled = chk_replace.Checked;
		}

		private void ctl_checkversionnow_Click(object sender, EventArgs e)
		{
			WebTools.NewVersion_StartCheck(true);
		}

		private void TranslateForm()
			{
			//Title
			this.Text = Program.appLng._i18n("confdlg_title");
			//Tabs
			tabCfgGeneral.Text = Application.ProductName;
			tabCfgPack.Text = Program.appLng._i18n("confdlg_tab_pack");
			tabCfgUnpack.Text = Program.appLng._i18n("confdlg_tab_unpack");
			tabCfgRes.Text = Program.appLng._i18n("confdlg_tab_resgen");
			tabCfgSystem.Text = Program.appLng._i18n("confdlg_tab_system");
			tabCfgTextures.Text = Program.appLng._i18n("confdlg_tab_textures");
			//Buttons
			ctl_ok.Text = Program.appLng._i18n("common_ok");
			ctl_cancel.Text = Program.appLng._i18n("common_cancel");

			//Tabs Gen
			//Groupboxes
			gb_language.Text = Program.appLng._i18n("confdlg_gb_language");
			gb_new_version.Text = Program.appLng._i18n("confdlg_gb_new_version");
			gb_start.Text = Program.appLng._i18n("confdlg_gb_start");
			gb_exit.Text = Program.appLng._i18n("confdlg_gb_confirmexit");
			gb_simulation_mode.Text = Program.appLng._i18n("confdlg_gb_simulation");
	
			//labels
			chk_check_new_version.Text = Program.appLng._i18n("confdlg_chk_new_version");
			chk_show_welcome.Text = Program.appLng._i18n("confdlg_chk_show_welcome");
			chk_ask_before_exit.Text = Program.appLng._i18n("confdlg_chk_ask_before_exit");
			chk_activate_simulation.Text = Program.appLng._i18n("confdlg_chk_activate_simulation");
			//button
			btn_check_new_version.Text = Program.appLng._i18n("confdlg_lbl_checkversionnow");

			//Tab Pack
			//Groupboxes
			gb_resources_files.Text = Program.appLng._i18n("confdlg_gb_resources");
			gb_multiple_selection.Text = Program.appLng._i18n("confdgl_gb_multiple_sel");
			gb_empty_dest_path.Text = Program.appLng._i18n("confdgl_gb_empty_dstpath");
			gb_existing_packs.Text = Program.appLng._i18n("confdlg_gb_existing_packs");
			gb_compression_level.Text = Program.appLng._i18n("confdlg_gb_ziplevel");
			gb_wad_files.Text = Program.appLng._i18n("confdgl_gb_includewads");
			
			//labels
			chk_create_res.Text = Program.appLng._i18n("confdlg_chk_createres");
			rd_multiple_ask.Text = Program.appLng._i18n("confdlg_rd_multi_ask");
			rd_multiple_all.Text = Program.appLng._i18n("confdlg_rd_multi_pack");
			rd_multiple_one.Text = Program.appLng._i18n("confdlg_rd_multi_one");
			chk_replace_packs.Text = Program.appLng._i18n("confdlg_chk_pakreplace");
			chk_include_wads.Text = Program.appLng._i18n("confdlg_chk_include_wads");
			rd_emptypath_warn.Text = Program.appLng._i18n("confdlg_rd_empty_warn");
			rd_emptypath_error.Text = Program.appLng._i18n("confdlg_rd_empty_error");
			rd_emptypath_pack.Text = Program.appLng._i18n("confdlg_rd_empty_folder");

			//Tab Unpack
			//Groupboxes
			gb_map_installation.Text = Program.appLng._i18n("confdlg_gb_mapinstal");
			gb_existing_files.Text = Program.appLng._i18n("confdlg_gb_replacing");
			//labels
			chk_parse_folders.Text = Program.appLng._i18n("confdlg_chk_parsefolders");
			txt_misc_folder.Text = Program.appLng._i18n("confdlg_txt_miscfolder");
			chk_detect_target_folder.Text = Program.appLng._i18n("confdlg_chk_detectfolder");
			chk_replace.Text = Program.appLng._i18n("confdlg_chk_replace");
			chk_ask_datetime.Text = Program.appLng._i18n("confdlg_chk_askdatetime");

			//Tab Resgen
			//Groupboxes
			gb_unnecessary_files.Text = Program.appLng._i18n("confdgl_gb_unnecessary");
			gb_existing_resfile.Text = Program.appLng._i18n("confdgl_gb_existingres");
			//labels
			chk_res_overwrite.Text = Program.appLng._i18n("confdlg_chk_overwriteres");
			chk_include_commented.Text = Program.appLng._i18n("confdlg_chk_includecommented");

			//Tab Textures
			gb_external_editor.Text = Program.appLng._i18n("confdgl_gb_exteditor");
			gb_thumbnail_size.Text = Program.appLng._i18n("confdgl_gb_thumbsize");
			gb_export_format.Text = Program.appLng._i18n("confdgl_gb_export_format");
			gb_new_texcolor.Text = Program.appLng._i18n("confdgl_gb_newtex_colors");
			lbl_NewTexColor1.Text = Program.appLng._i18n("confdgl_lbl_newtex_color1");
			lbl_NewTexColor2.Text = Program.appLng._i18n("confdgl_lbl_newtex_color2");
			
			//Tab System
			gb_associate_files.Text = Program.appLng._i18n("confdgl_gb_associate");
			gb_check_and_restore.Text = Program.appLng._i18n("confdgl_gb_checkandrestore");
			chk_associate_maps.Text = Program.appLng._i18n("confdlg_chk_mapsfiles");
			chk_associate_packs.Text = Program.appLng._i18n("confdlg_chk_paksfiles");
			chk_associate_texes.Text = Program.appLng._i18n("confdlg_chk_texfiles");
			chk_restore_startup.Text = Program.appLng._i18n("confdlg_chk_checknrestore");
			}
		
		private void lbl41_CheckChanged(object sender, EventArgs e)
			{
			Control_AssociationCheckStatus();
			}

		private void lbl42_CheckChanged(object sender, EventArgs e)
			{
			Control_AssociationCheckStatus();
			}

		private void lbl43_CheckChanged(object sender, EventArgs e)
			{
			Control_AssociationCheckStatus();
			}
		private void Control_AssociationCheckStatus()
			{
			chk_restore_startup.Enabled = (chk_associate_maps.Checked || chk_associate_packs.Checked || chk_associate_texes.Checked) ? true : false;
			}

		private void btn40_Click(object sender, EventArgs e)
			{
			ImgEditDlg.AddExtension = false;
			ImgEditDlg.CheckFileExists = true;
			ImgEditDlg.CheckPathExists = true;
			ImgEditDlg.ValidateNames = true;
			ImgEditDlg.SupportMultiDottedExtensions = false;
			ImgEditDlg.ShowReadOnly = false;
			ImgEditDlg.DereferenceLinks = true;
			ImgEditDlg.Title = Program.appLng._i18n("confdlg_browse_img_editor");
			ImgEditDlg.FilterIndex = 1;
			ImgEditDlg.Multiselect = false;
			ImgEditDlg.DefaultExt = "*.exe";
			ImgEditDlg.Filter = Program.appLng._i18n("common_exefiles");
			ImgEditDlg.InitialDirectory = tb_external_editor.Text != "" ? Path.GetDirectoryName(tb_external_editor.Text) : "";
			ImgEditDlg.FileName = tb_external_editor.Text != "" ? Path.GetFileName(tb_external_editor.Text) : "";
			if (ImgEditDlg.ShowDialog() == DialogResult.OK)
				{
				tb_external_editor.Text = ImgEditDlg.FileName;
				}
			}

		private void bgColor1_OnClick(object sender, EventArgs e)
			{
			_texcolor1 = SelectBackgroundColor(_texcolor1);
			nwTexColor1.BackColor = _texcolor1;
			}

		private void bgColor2_OnClick(object sender, EventArgs e)
			{
			_texcolor2 = SelectBackgroundColor(_texcolor2);
			nwTexColor2.BackColor = _texcolor2;
			}

		private Color SelectBackgroundColor(Color defColor)
			{
			colorDlg.Color=defColor;
			if (colorDlg.ShowDialog() == DialogResult.OK)
				{
				defColor = colorDlg.Color;
				}
			return defColor;
			}

	}

	class Langs
		{
		private string _lib;
		private string _lng;
		public string Lib { get { return _lib; } set { _lib = value; } }
		public string Lng { get { return _lng; } set { _lng = value; } }
		public Langs(string name, string cult)
			{
			_lib = name;
			_lng = cult;
			}
		}

	class Level
		{
		private int _idx;
		private string _lib;
		public int Idx { get { return _idx; } set { _idx = value; } }
		public string Lib { get { return _lib; } set { _lib = value; } }
		public Level(int idx, string lib)
			{
			_idx = idx;
			_lib = lib;
			}
		}

	}
