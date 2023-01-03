namespace HLBox17b
	{
	partial class CfgFrm
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CfgFrm));
			this.ctl_cancel = new System.Windows.Forms.Button();
			this.ctl_ok = new System.Windows.Forms.Button();
			this.cfgtabControl = new System.Windows.Forms.TabControl();
			this.tabCfgGeneral = new System.Windows.Forms.TabPage();
			this.gb_simulation_mode = new System.Windows.Forms.GroupBox();
			this.chk_activate_simulation = new System.Windows.Forms.CheckBox();
			this.gb_language = new System.Windows.Forms.GroupBox();
			this.ctl_gen_Language = new System.Windows.Forms.ComboBox();
			this.gb_new_version = new System.Windows.Forms.GroupBox();
			this.chk_check_new_version = new System.Windows.Forms.CheckBox();
			this.btn_check_new_version = new System.Windows.Forms.Button();
			this.gb_exit = new System.Windows.Forms.GroupBox();
			this.chk_ask_before_exit = new System.Windows.Forms.CheckBox();
			this.gb_start = new System.Windows.Forms.GroupBox();
			this.chk_show_welcome = new System.Windows.Forms.CheckBox();
			this.tabCfgPack = new System.Windows.Forms.TabPage();
			this.gb_wad_files = new System.Windows.Forms.GroupBox();
			this.chk_include_wads = new System.Windows.Forms.CheckBox();
			this.gb_compression_level = new System.Windows.Forms.GroupBox();
			this.ctl_pack_ziplevels = new System.Windows.Forms.ComboBox();
			this.gb_existing_packs = new System.Windows.Forms.GroupBox();
			this.chk_replace_packs = new System.Windows.Forms.CheckBox();
			this.gb_empty_dest_path = new System.Windows.Forms.GroupBox();
			this.rd_emptypath_pack = new System.Windows.Forms.RadioButton();
			this.rd_emptypath_error = new System.Windows.Forms.RadioButton();
			this.rd_emptypath_warn = new System.Windows.Forms.RadioButton();
			this.gb_multiple_selection = new System.Windows.Forms.GroupBox();
			this.rd_multiple_one = new System.Windows.Forms.RadioButton();
			this.rd_multiple_all = new System.Windows.Forms.RadioButton();
			this.rd_multiple_ask = new System.Windows.Forms.RadioButton();
			this.gb_resources_files = new System.Windows.Forms.GroupBox();
			this.chk_create_res = new System.Windows.Forms.CheckBox();
			this.tabCfgUnpack = new System.Windows.Forms.TabPage();
			this.gb_existing_files = new System.Windows.Forms.GroupBox();
			this.chk_replace = new System.Windows.Forms.CheckBox();
			this.chk_ask_datetime = new System.Windows.Forms.CheckBox();
			this.gb_map_installation = new System.Windows.Forms.GroupBox();
			this.chk_detect_target_folder = new System.Windows.Forms.CheckBox();
			this.ctl_smi_unpackmiscpath = new System.Windows.Forms.TextBox();
			this.txt_misc_folder = new System.Windows.Forms.Label();
			this.chk_parse_folders = new System.Windows.Forms.CheckBox();
			this.tabCfgRes = new System.Windows.Forms.TabPage();
			this.gb_existing_resfile = new System.Windows.Forms.GroupBox();
			this.chk_res_overwrite = new System.Windows.Forms.CheckBox();
			this.gb_unnecessary_files = new System.Windows.Forms.GroupBox();
			this.chk_include_commented = new System.Windows.Forms.CheckBox();
			this.tabCfgTextures = new System.Windows.Forms.TabPage();
			this.gb_new_texcolor = new System.Windows.Forms.GroupBox();
			this.nwTexColor2 = new System.Windows.Forms.Label();
			this.lbl_NewTexColor2 = new System.Windows.Forms.Label();
			this.nwTexColor1 = new System.Windows.Forms.Label();
			this.lbl_NewTexColor1 = new System.Windows.Forms.Label();
			this.gb_external_editor = new System.Windows.Forms.GroupBox();
			this.btn_browse_editor = new System.Windows.Forms.Button();
			this.tb_external_editor = new System.Windows.Forms.TextBox();
			this.gb_export_format = new System.Windows.Forms.GroupBox();
			this.cb_export_format = new System.Windows.Forms.ComboBox();
			this.gb_thumbnail_size = new System.Windows.Forms.GroupBox();
			this.cb_thumbnail_size = new System.Windows.Forms.ComboBox();
			this.tabCfgSystem = new System.Windows.Forms.TabPage();
			this.gb_check_and_restore = new System.Windows.Forms.GroupBox();
			this.chk_restore_startup = new System.Windows.Forms.CheckBox();
			this.gb_associate_files = new System.Windows.Forms.GroupBox();
			this.chk_associate_texes = new System.Windows.Forms.CheckBox();
			this.chk_associate_packs = new System.Windows.Forms.CheckBox();
			this.chk_associate_maps = new System.Windows.Forms.CheckBox();
			this.ImgEditDlg = new System.Windows.Forms.OpenFileDialog();
			this.colorDlg = new System.Windows.Forms.ColorDialog();
			this.cfgtabControl.SuspendLayout();
			this.tabCfgGeneral.SuspendLayout();
			this.gb_simulation_mode.SuspendLayout();
			this.gb_language.SuspendLayout();
			this.gb_new_version.SuspendLayout();
			this.gb_exit.SuspendLayout();
			this.gb_start.SuspendLayout();
			this.tabCfgPack.SuspendLayout();
			this.gb_wad_files.SuspendLayout();
			this.gb_compression_level.SuspendLayout();
			this.gb_existing_packs.SuspendLayout();
			this.gb_empty_dest_path.SuspendLayout();
			this.gb_multiple_selection.SuspendLayout();
			this.gb_resources_files.SuspendLayout();
			this.tabCfgUnpack.SuspendLayout();
			this.gb_existing_files.SuspendLayout();
			this.gb_map_installation.SuspendLayout();
			this.tabCfgRes.SuspendLayout();
			this.gb_existing_resfile.SuspendLayout();
			this.gb_unnecessary_files.SuspendLayout();
			this.tabCfgTextures.SuspendLayout();
			this.gb_new_texcolor.SuspendLayout();
			this.gb_external_editor.SuspendLayout();
			this.gb_export_format.SuspendLayout();
			this.gb_thumbnail_size.SuspendLayout();
			this.tabCfgSystem.SuspendLayout();
			this.gb_check_and_restore.SuspendLayout();
			this.gb_associate_files.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctl_cancel
			// 
			this.ctl_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ctl_cancel.Location = new System.Drawing.Point(335, 280);
			this.ctl_cancel.Name = "ctl_cancel";
			this.ctl_cancel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.ctl_cancel.Size = new System.Drawing.Size(90, 30);
			this.ctl_cancel.TabIndex = 2;
			this.ctl_cancel.Text = "Cancel";
			this.ctl_cancel.UseVisualStyleBackColor = true;
			// 
			// ctl_ok
			// 
			this.ctl_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ctl_ok.Location = new System.Drawing.Point(160, 280);
			this.ctl_ok.Name = "ctl_ok";
			this.ctl_ok.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.ctl_ok.Size = new System.Drawing.Size(90, 30);
			this.ctl_ok.TabIndex = 1;
			this.ctl_ok.Text = "Ok";
			this.ctl_ok.UseVisualStyleBackColor = true;
			// 
			// cfgtabControl
			// 
			this.cfgtabControl.Controls.Add(this.tabCfgGeneral);
			this.cfgtabControl.Controls.Add(this.tabCfgPack);
			this.cfgtabControl.Controls.Add(this.tabCfgUnpack);
			this.cfgtabControl.Controls.Add(this.tabCfgRes);
			this.cfgtabControl.Controls.Add(this.tabCfgTextures);
			this.cfgtabControl.Controls.Add(this.tabCfgSystem);
			this.cfgtabControl.Location = new System.Drawing.Point(10, 10);
			this.cfgtabControl.Name = "cfgtabControl";
			this.cfgtabControl.Padding = new System.Drawing.Point(20, 8);
			this.cfgtabControl.SelectedIndex = 0;
			this.cfgtabControl.Size = new System.Drawing.Size(585, 260);
			this.cfgtabControl.TabIndex = 9;
			// 
			// tabCfgGeneral
			// 
			this.tabCfgGeneral.Controls.Add(this.gb_simulation_mode);
			this.tabCfgGeneral.Controls.Add(this.gb_language);
			this.tabCfgGeneral.Controls.Add(this.gb_new_version);
			this.tabCfgGeneral.Controls.Add(this.gb_exit);
			this.tabCfgGeneral.Controls.Add(this.gb_start);
			this.tabCfgGeneral.Location = new System.Drawing.Point(4, 32);
			this.tabCfgGeneral.Name = "tabCfgGeneral";
			this.tabCfgGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabCfgGeneral.Size = new System.Drawing.Size(577, 224);
			this.tabCfgGeneral.TabIndex = 0;
			this.tabCfgGeneral.Text = "General";
			this.tabCfgGeneral.UseVisualStyleBackColor = true;
			// 
			// gb_simulation_mode
			// 
			this.gb_simulation_mode.Controls.Add(this.chk_activate_simulation);
			this.gb_simulation_mode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.gb_simulation_mode.Location = new System.Drawing.Point(215, 10);
			this.gb_simulation_mode.Name = "gb_simulation_mode";
			this.gb_simulation_mode.Size = new System.Drawing.Size(354, 55);
			this.gb_simulation_mode.TabIndex = 18;
			this.gb_simulation_mode.TabStop = false;
			this.gb_simulation_mode.Text = "Simulation Mode";
			// 
			// chk_activate_simulation
			// 
			this.chk_activate_simulation.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chk_activate_simulation.Location = new System.Drawing.Point(6, 21);
			this.chk_activate_simulation.Name = "chk_activate_simulation";
			this.chk_activate_simulation.Size = new System.Drawing.Size(339, 19);
			this.chk_activate_simulation.TabIndex = 2;
			this.chk_activate_simulation.Text = "Activate Simulation (No files will be deleted nor created)";
			this.chk_activate_simulation.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chk_activate_simulation.UseVisualStyleBackColor = true;
			// 
			// gb_language
			// 
			this.gb_language.Controls.Add(this.ctl_gen_Language);
			this.gb_language.Location = new System.Drawing.Point(10, 10);
			this.gb_language.Name = "gb_language";
			this.gb_language.Size = new System.Drawing.Size(195, 55);
			this.gb_language.TabIndex = 18;
			this.gb_language.TabStop = false;
			this.gb_language.Text = "Language";
			// 
			// ctl_gen_Language
			// 
			this.ctl_gen_Language.FormattingEnabled = true;
			this.ctl_gen_Language.Location = new System.Drawing.Point(26, 19);
			this.ctl_gen_Language.Name = "ctl_gen_Language";
			this.ctl_gen_Language.Size = new System.Drawing.Size(154, 21);
			this.ctl_gen_Language.TabIndex = 1;
			// 
			// gb_new_version
			// 
			this.gb_new_version.Controls.Add(this.chk_check_new_version);
			this.gb_new_version.Controls.Add(this.btn_check_new_version);
			this.gb_new_version.Location = new System.Drawing.Point(10, 80);
			this.gb_new_version.Name = "gb_new_version";
			this.gb_new_version.Size = new System.Drawing.Size(560, 54);
			this.gb_new_version.TabIndex = 17;
			this.gb_new_version.TabStop = false;
			this.gb_new_version.Text = "New Version";
			// 
			// chk_check_new_version
			// 
			this.chk_check_new_version.Location = new System.Drawing.Point(11, 19);
			this.chk_check_new_version.Name = "chk_check_new_version";
			this.chk_check_new_version.Size = new System.Drawing.Size(320, 22);
			this.chk_check_new_version.TabIndex = 3;
			this.chk_check_new_version.Text = "Check newer version at startup";
			this.chk_check_new_version.UseVisualStyleBackColor = true;
			// 
			// btn_check_new_version
			// 
			this.btn_check_new_version.Location = new System.Drawing.Point(420, 15);
			this.btn_check_new_version.Name = "btn_check_new_version";
			this.btn_check_new_version.Size = new System.Drawing.Size(128, 30);
			this.btn_check_new_version.TabIndex = 4;
			this.btn_check_new_version.Text = "Check Now";
			this.btn_check_new_version.UseVisualStyleBackColor = true;
			this.btn_check_new_version.Click += new System.EventHandler(this.ctl_checkversionnow_Click);
			// 
			// gb_exit
			// 
			this.gb_exit.Controls.Add(this.chk_ask_before_exit);
			this.gb_exit.Location = new System.Drawing.Point(310, 150);
			this.gb_exit.Name = "gb_exit";
			this.gb_exit.Size = new System.Drawing.Size(260, 49);
			this.gb_exit.TabIndex = 17;
			this.gb_exit.TabStop = false;
			this.gb_exit.Text = "Quit";
			// 
			// chk_ask_before_exit
			// 
			this.chk_ask_before_exit.Location = new System.Drawing.Point(6, 19);
			this.chk_ask_before_exit.Name = "chk_ask_before_exit";
			this.chk_ask_before_exit.Size = new System.Drawing.Size(249, 20);
			this.chk_ask_before_exit.TabIndex = 6;
			this.chk_ask_before_exit.Text = "Confirm before exit";
			this.chk_ask_before_exit.UseVisualStyleBackColor = true;
			// 
			// gb_start
			// 
			this.gb_start.Controls.Add(this.chk_show_welcome);
			this.gb_start.Location = new System.Drawing.Point(10, 150);
			this.gb_start.Name = "gb_start";
			this.gb_start.Size = new System.Drawing.Size(270, 49);
			this.gb_start.TabIndex = 16;
			this.gb_start.TabStop = false;
			this.gb_start.Text = "Start";
			// 
			// chk_show_welcome
			// 
			this.chk_show_welcome.Location = new System.Drawing.Point(10, 20);
			this.chk_show_welcome.Name = "chk_show_welcome";
			this.chk_show_welcome.Size = new System.Drawing.Size(249, 20);
			this.chk_show_welcome.TabIndex = 5;
			this.chk_show_welcome.Text = "Show welcome screen";
			this.chk_show_welcome.UseVisualStyleBackColor = true;
			// 
			// tabCfgPack
			// 
			this.tabCfgPack.Controls.Add(this.gb_wad_files);
			this.tabCfgPack.Controls.Add(this.gb_compression_level);
			this.tabCfgPack.Controls.Add(this.gb_existing_packs);
			this.tabCfgPack.Controls.Add(this.gb_empty_dest_path);
			this.tabCfgPack.Controls.Add(this.gb_multiple_selection);
			this.tabCfgPack.Controls.Add(this.gb_resources_files);
			this.tabCfgPack.Location = new System.Drawing.Point(4, 32);
			this.tabCfgPack.Name = "tabCfgPack";
			this.tabCfgPack.Padding = new System.Windows.Forms.Padding(3);
			this.tabCfgPack.Size = new System.Drawing.Size(577, 224);
			this.tabCfgPack.TabIndex = 1;
			this.tabCfgPack.Text = "Pack";
			this.tabCfgPack.UseVisualStyleBackColor = true;
			// 
			// gb_wad_files
			// 
			this.gb_wad_files.Controls.Add(this.chk_include_wads);
			this.gb_wad_files.Location = new System.Drawing.Point(10, 165);
			this.gb_wad_files.Name = "gb_wad_files";
			this.gb_wad_files.Size = new System.Drawing.Size(280, 49);
			this.gb_wad_files.TabIndex = 10;
			this.gb_wad_files.TabStop = false;
			this.gb_wad_files.Text = "Wad Files";
			// 
			// chk_include_wads
			// 
			this.chk_include_wads.Location = new System.Drawing.Point(20, 19);
			this.chk_include_wads.Name = "chk_include_wads";
			this.chk_include_wads.Size = new System.Drawing.Size(250, 19);
			this.chk_include_wads.TabIndex = 10;
			this.chk_include_wads.Text = "Include in pack";
			this.chk_include_wads.UseVisualStyleBackColor = true;
			// 
			// gb_compression_level
			// 
			this.gb_compression_level.Controls.Add(this.ctl_pack_ziplevels);
			this.gb_compression_level.Location = new System.Drawing.Point(10, 60);
			this.gb_compression_level.Name = "gb_compression_level";
			this.gb_compression_level.Size = new System.Drawing.Size(280, 49);
			this.gb_compression_level.TabIndex = 19;
			this.gb_compression_level.TabStop = false;
			this.gb_compression_level.Text = "Compression Level";
			// 
			// ctl_pack_ziplevels
			// 
			this.ctl_pack_ziplevels.FormattingEnabled = true;
			this.ctl_pack_ziplevels.Location = new System.Drawing.Point(20, 15);
			this.ctl_pack_ziplevels.Name = "ctl_pack_ziplevels";
			this.ctl_pack_ziplevels.Size = new System.Drawing.Size(239, 21);
			this.ctl_pack_ziplevels.TabIndex = 8;
			// 
			// gb_existing_packs
			// 
			this.gb_existing_packs.Controls.Add(this.chk_replace_packs);
			this.gb_existing_packs.Location = new System.Drawing.Point(10, 10);
			this.gb_existing_packs.Name = "gb_existing_packs";
			this.gb_existing_packs.Size = new System.Drawing.Size(280, 49);
			this.gb_existing_packs.TabIndex = 12;
			this.gb_existing_packs.TabStop = false;
			this.gb_existing_packs.Text = "Existing Packs";
			// 
			// chk_replace_packs
			// 
			this.chk_replace_packs.Location = new System.Drawing.Point(20, 20);
			this.chk_replace_packs.Name = "chk_replace_packs";
			this.chk_replace_packs.Size = new System.Drawing.Size(250, 19);
			this.chk_replace_packs.TabIndex = 7;
			this.chk_replace_packs.Text = "Replace without confirmation";
			this.chk_replace_packs.UseVisualStyleBackColor = true;
			// 
			// gb_empty_dest_path
			// 
			this.gb_empty_dest_path.Controls.Add(this.rd_emptypath_pack);
			this.gb_empty_dest_path.Controls.Add(this.rd_emptypath_error);
			this.gb_empty_dest_path.Controls.Add(this.rd_emptypath_warn);
			this.gb_empty_dest_path.Location = new System.Drawing.Point(300, 115);
			this.gb_empty_dest_path.Name = "gb_empty_dest_path";
			this.gb_empty_dest_path.Size = new System.Drawing.Size(265, 100);
			this.gb_empty_dest_path.TabIndex = 11;
			this.gb_empty_dest_path.TabStop = false;
			this.gb_empty_dest_path.Text = "Empty Destination Path";
			// 
			// rd_emptypath_pack
			// 
			this.rd_emptypath_pack.Location = new System.Drawing.Point(25, 74);
			this.rd_emptypath_pack.Name = "rd_emptypath_pack";
			this.rd_emptypath_pack.Size = new System.Drawing.Size(230, 20);
			this.rd_emptypath_pack.TabIndex = 16;
			this.rd_emptypath_pack.TabStop = true;
			this.rd_emptypath_pack.Text = "Pack in file\'s folder";
			this.rd_emptypath_pack.UseVisualStyleBackColor = true;
			// 
			// rd_emptypath_error
			// 
			this.rd_emptypath_error.Location = new System.Drawing.Point(25, 47);
			this.rd_emptypath_error.Name = "rd_emptypath_error";
			this.rd_emptypath_error.Size = new System.Drawing.Size(230, 20);
			this.rd_emptypath_error.TabIndex = 15;
			this.rd_emptypath_error.TabStop = true;
			this.rd_emptypath_error.Text = "Throw an error";
			this.rd_emptypath_error.UseVisualStyleBackColor = true;
			// 
			// rd_emptypath_warn
			// 
			this.rd_emptypath_warn.Location = new System.Drawing.Point(25, 20);
			this.rd_emptypath_warn.Name = "rd_emptypath_warn";
			this.rd_emptypath_warn.Size = new System.Drawing.Size(230, 20);
			this.rd_emptypath_warn.TabIndex = 14;
			this.rd_emptypath_warn.TabStop = true;
			this.rd_emptypath_warn.Text = "Warn and pack in file\'s folder";
			this.rd_emptypath_warn.UseVisualStyleBackColor = true;
			// 
			// gb_multiple_selection
			// 
			this.gb_multiple_selection.Controls.Add(this.rd_multiple_one);
			this.gb_multiple_selection.Controls.Add(this.rd_multiple_all);
			this.gb_multiple_selection.Controls.Add(this.rd_multiple_ask);
			this.gb_multiple_selection.Location = new System.Drawing.Point(300, 10);
			this.gb_multiple_selection.Name = "gb_multiple_selection";
			this.gb_multiple_selection.Size = new System.Drawing.Size(265, 100);
			this.gb_multiple_selection.TabIndex = 10;
			this.gb_multiple_selection.TabStop = false;
			this.gb_multiple_selection.Text = "Multiple Map Selection";
			// 
			// rd_multiple_one
			// 
			this.rd_multiple_one.Location = new System.Drawing.Point(25, 74);
			this.rd_multiple_one.Name = "rd_multiple_one";
			this.rd_multiple_one.Size = new System.Drawing.Size(230, 20);
			this.rd_multiple_one.TabIndex = 13;
			this.rd_multiple_one.TabStop = true;
			this.rd_multiple_one.Text = "Pack each file separatly";
			this.rd_multiple_one.UseVisualStyleBackColor = true;
			// 
			// rd_multiple_all
			// 
			this.rd_multiple_all.Location = new System.Drawing.Point(25, 47);
			this.rd_multiple_all.Name = "rd_multiple_all";
			this.rd_multiple_all.Size = new System.Drawing.Size(230, 20);
			this.rd_multiple_all.TabIndex = 12;
			this.rd_multiple_all.TabStop = true;
			this.rd_multiple_all.Text = "Make a pack of all files";
			this.rd_multiple_all.UseVisualStyleBackColor = true;
			// 
			// rd_multiple_ask
			// 
			this.rd_multiple_ask.Location = new System.Drawing.Point(25, 20);
			this.rd_multiple_ask.Name = "rd_multiple_ask";
			this.rd_multiple_ask.Size = new System.Drawing.Size(230, 20);
			this.rd_multiple_ask.TabIndex = 11;
			this.rd_multiple_ask.TabStop = true;
			this.rd_multiple_ask.Text = "Ask what to do";
			this.rd_multiple_ask.UseVisualStyleBackColor = true;
			// 
			// gb_resources_files
			// 
			this.gb_resources_files.Controls.Add(this.chk_create_res);
			this.gb_resources_files.Location = new System.Drawing.Point(10, 115);
			this.gb_resources_files.Name = "gb_resources_files";
			this.gb_resources_files.Size = new System.Drawing.Size(280, 49);
			this.gb_resources_files.TabIndex = 9;
			this.gb_resources_files.TabStop = false;
			this.gb_resources_files.Text = "Resources Files";
			// 
			// chk_create_res
			// 
			this.chk_create_res.Location = new System.Drawing.Point(20, 19);
			this.chk_create_res.Name = "chk_create_res";
			this.chk_create_res.Size = new System.Drawing.Size(250, 19);
			this.chk_create_res.TabIndex = 9;
			this.chk_create_res.Text = "Create .res file(s)";
			this.chk_create_res.UseVisualStyleBackColor = true;
			// 
			// tabCfgUnpack
			// 
			this.tabCfgUnpack.Controls.Add(this.gb_existing_files);
			this.tabCfgUnpack.Controls.Add(this.gb_map_installation);
			this.tabCfgUnpack.Location = new System.Drawing.Point(4, 32);
			this.tabCfgUnpack.Name = "tabCfgUnpack";
			this.tabCfgUnpack.Size = new System.Drawing.Size(577, 224);
			this.tabCfgUnpack.TabIndex = 2;
			this.tabCfgUnpack.Text = "UnPack";
			this.tabCfgUnpack.UseVisualStyleBackColor = true;
			// 
			// gb_existing_files
			// 
			this.gb_existing_files.Controls.Add(this.chk_replace);
			this.gb_existing_files.Controls.Add(this.chk_ask_datetime);
			this.gb_existing_files.Location = new System.Drawing.Point(8, 114);
			this.gb_existing_files.Name = "gb_existing_files";
			this.gb_existing_files.Size = new System.Drawing.Size(557, 96);
			this.gb_existing_files.TabIndex = 20;
			this.gb_existing_files.TabStop = false;
			this.gb_existing_files.Text = "Existing files";
			// 
			// chk_replace
			// 
			this.chk_replace.Location = new System.Drawing.Point(20, 25);
			this.chk_replace.Name = "chk_replace";
			this.chk_replace.Size = new System.Drawing.Size(525, 20);
			this.chk_replace.TabIndex = 20;
			this.chk_replace.Text = "Replace without confirmation";
			this.chk_replace.UseVisualStyleBackColor = true;
			this.chk_replace.CheckedChanged += new System.EventHandler(this.ctl_smireplace_CheckedChanged);
			// 
			// chk_ask_datetime
			// 
			this.chk_ask_datetime.Location = new System.Drawing.Point(85, 55);
			this.chk_ask_datetime.Name = "chk_ask_datetime";
			this.chk_ask_datetime.Size = new System.Drawing.Size(460, 22);
			this.chk_ask_datetime.TabIndex = 21;
			this.chk_ask_datetime.Text = "Ask only when date/time differs";
			this.chk_ask_datetime.UseVisualStyleBackColor = true;
			// 
			// gb_map_installation
			// 
			this.gb_map_installation.Controls.Add(this.chk_detect_target_folder);
			this.gb_map_installation.Controls.Add(this.ctl_smi_unpackmiscpath);
			this.gb_map_installation.Controls.Add(this.txt_misc_folder);
			this.gb_map_installation.Controls.Add(this.chk_parse_folders);
			this.gb_map_installation.Location = new System.Drawing.Point(8, 12);
			this.gb_map_installation.Name = "gb_map_installation";
			this.gb_map_installation.Size = new System.Drawing.Size(557, 96);
			this.gb_map_installation.TabIndex = 19;
			this.gb_map_installation.TabStop = false;
			this.gb_map_installation.Text = "Maps Installation";
			// 
			// chk_detect_target_folder
			// 
			this.chk_detect_target_folder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chk_detect_target_folder.Location = new System.Drawing.Point(20, 70);
			this.chk_detect_target_folder.Name = "chk_detect_target_folder";
			this.chk_detect_target_folder.Size = new System.Drawing.Size(523, 21);
			this.chk_detect_target_folder.TabIndex = 19;
			this.chk_detect_target_folder.Text = "Automaticaly detect target folder";
			this.chk_detect_target_folder.UseVisualStyleBackColor = true;
			// 
			// ctl_smi_unpackmiscpath
			// 
			this.ctl_smi_unpackmiscpath.Location = new System.Drawing.Point(403, 44);
			this.ctl_smi_unpackmiscpath.Name = "ctl_smi_unpackmiscpath";
			this.ctl_smi_unpackmiscpath.Size = new System.Drawing.Size(139, 20);
			this.ctl_smi_unpackmiscpath.TabIndex = 18;
			// 
			// txt_misc_folder
			// 
			this.txt_misc_folder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txt_misc_folder.Location = new System.Drawing.Point(28, 44);
			this.txt_misc_folder.Name = "txt_misc_folder";
			this.txt_misc_folder.Size = new System.Drawing.Size(369, 20);
			this.txt_misc_folder.TabIndex = 12;
			this.txt_misc_folder.Text = "Name of \"Misc\" folder, where to put unclassified files";
			this.txt_misc_folder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chk_parse_folders
			// 
			this.chk_parse_folders.Location = new System.Drawing.Point(20, 20);
			this.chk_parse_folders.Name = "chk_parse_folders";
			this.chk_parse_folders.Size = new System.Drawing.Size(313, 21);
			this.chk_parse_folders.TabIndex = 17;
			this.chk_parse_folders.Text = "Parse and automaticaly sort folders";
			this.chk_parse_folders.UseVisualStyleBackColor = true;
			this.chk_parse_folders.CheckedChanged += new System.EventHandler(this.ctl_analysepath_CheckedChanged);
			// 
			// tabCfgRes
			// 
			this.tabCfgRes.Controls.Add(this.gb_existing_resfile);
			this.tabCfgRes.Controls.Add(this.gb_unnecessary_files);
			this.tabCfgRes.Location = new System.Drawing.Point(4, 32);
			this.tabCfgRes.Name = "tabCfgRes";
			this.tabCfgRes.Size = new System.Drawing.Size(577, 224);
			this.tabCfgRes.TabIndex = 3;
			this.tabCfgRes.Text = "Resgen";
			this.tabCfgRes.UseVisualStyleBackColor = true;
			// 
			// gb_existing_resfile
			// 
			this.gb_existing_resfile.Controls.Add(this.chk_res_overwrite);
			this.gb_existing_resfile.Location = new System.Drawing.Point(140, 50);
			this.gb_existing_resfile.Name = "gb_existing_resfile";
			this.gb_existing_resfile.Size = new System.Drawing.Size(275, 55);
			this.gb_existing_resfile.TabIndex = 19;
			this.gb_existing_resfile.TabStop = false;
			this.gb_existing_resfile.Text = "Existing .res file";
			// 
			// chk_res_overwrite
			// 
			this.chk_res_overwrite.Location = new System.Drawing.Point(20, 20);
			this.chk_res_overwrite.Name = "chk_res_overwrite";
			this.chk_res_overwrite.Size = new System.Drawing.Size(245, 23);
			this.chk_res_overwrite.TabIndex = 23;
			this.chk_res_overwrite.Text = "Overwrite existing file(s)";
			this.chk_res_overwrite.UseVisualStyleBackColor = true;
			// 
			// gb_unnecessary_files
			// 
			this.gb_unnecessary_files.Controls.Add(this.chk_include_commented);
			this.gb_unnecessary_files.Location = new System.Drawing.Point(140, 115);
			this.gb_unnecessary_files.Name = "gb_unnecessary_files";
			this.gb_unnecessary_files.Size = new System.Drawing.Size(275, 55);
			this.gb_unnecessary_files.TabIndex = 17;
			this.gb_unnecessary_files.TabStop = false;
			this.gb_unnecessary_files.Text = "Unnecessary Files";
			// 
			// chk_include_commented
			// 
			this.chk_include_commented.Location = new System.Drawing.Point(20, 20);
			this.chk_include_commented.Name = "chk_include_commented";
			this.chk_include_commented.Size = new System.Drawing.Size(245, 23);
			this.chk_include_commented.TabIndex = 22;
			this.chk_include_commented.Text = "List them commented in res file";
			this.chk_include_commented.UseVisualStyleBackColor = true;
			// 
			// tabCfgTextures
			// 
			this.tabCfgTextures.Controls.Add(this.gb_new_texcolor);
			this.tabCfgTextures.Controls.Add(this.gb_external_editor);
			this.tabCfgTextures.Controls.Add(this.gb_export_format);
			this.tabCfgTextures.Controls.Add(this.gb_thumbnail_size);
			this.tabCfgTextures.Location = new System.Drawing.Point(4, 32);
			this.tabCfgTextures.Name = "tabCfgTextures";
			this.tabCfgTextures.Size = new System.Drawing.Size(577, 224);
			this.tabCfgTextures.TabIndex = 5;
			this.tabCfgTextures.Text = "Textures";
			this.tabCfgTextures.UseVisualStyleBackColor = true;
			// 
			// gb_new_texcolor
			// 
			this.gb_new_texcolor.Controls.Add(this.nwTexColor2);
			this.gb_new_texcolor.Controls.Add(this.lbl_NewTexColor2);
			this.gb_new_texcolor.Controls.Add(this.nwTexColor1);
			this.gb_new_texcolor.Controls.Add(this.lbl_NewTexColor1);
			this.gb_new_texcolor.Location = new System.Drawing.Point(70, 155);
			this.gb_new_texcolor.Name = "gb_new_texcolor";
			this.gb_new_texcolor.Size = new System.Drawing.Size(405, 62);
			this.gb_new_texcolor.TabIndex = 28;
			this.gb_new_texcolor.TabStop = false;
			this.gb_new_texcolor.Text = "New Texture Background Colors";
			// 
			// nwTexColor2
			// 
			this.nwTexColor2.BackColor = System.Drawing.Color.Black;
			this.nwTexColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nwTexColor2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.nwTexColor2.Location = new System.Drawing.Point(225, 25);
			this.nwTexColor2.Name = "nwTexColor2";
			this.nwTexColor2.Size = new System.Drawing.Size(40, 23);
			this.nwTexColor2.TabIndex = 3;
			this.nwTexColor2.Click += new System.EventHandler(this.bgColor2_OnClick);
			// 
			// lbl_NewTexColor2
			// 
			this.lbl_NewTexColor2.Location = new System.Drawing.Point(280, 25);
			this.lbl_NewTexColor2.Name = "lbl_NewTexColor2";
			this.lbl_NewTexColor2.Size = new System.Drawing.Size(100, 23);
			this.lbl_NewTexColor2.TabIndex = 2;
			this.lbl_NewTexColor2.Text = "Color2";
			this.lbl_NewTexColor2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nwTexColor1
			// 
			this.nwTexColor1.BackColor = System.Drawing.Color.Purple;
			this.nwTexColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nwTexColor1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.nwTexColor1.Location = new System.Drawing.Point(155, 25);
			this.nwTexColor1.Name = "nwTexColor1";
			this.nwTexColor1.Size = new System.Drawing.Size(40, 23);
			this.nwTexColor1.TabIndex = 1;
			this.nwTexColor1.Click += new System.EventHandler(this.bgColor1_OnClick);
			// 
			// lbl_NewTexColor1
			// 
			this.lbl_NewTexColor1.Location = new System.Drawing.Point(40, 25);
			this.lbl_NewTexColor1.Name = "lbl_NewTexColor1";
			this.lbl_NewTexColor1.Size = new System.Drawing.Size(100, 23);
			this.lbl_NewTexColor1.TabIndex = 0;
			this.lbl_NewTexColor1.Text = "Color1";
			this.lbl_NewTexColor1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gb_external_editor
			// 
			this.gb_external_editor.Controls.Add(this.btn_browse_editor);
			this.gb_external_editor.Controls.Add(this.tb_external_editor);
			this.gb_external_editor.Location = new System.Drawing.Point(70, 10);
			this.gb_external_editor.Name = "gb_external_editor";
			this.gb_external_editor.Size = new System.Drawing.Size(405, 70);
			this.gb_external_editor.TabIndex = 22;
			this.gb_external_editor.TabStop = false;
			this.gb_external_editor.Text = "External Editor";
			// 
			// btn_browse_editor
			// 
			this.btn_browse_editor.Image = global::HLBox17b.Properties.Resources.Explorer;
			this.btn_browse_editor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btn_browse_editor.Location = new System.Drawing.Point(360, 15);
			this.btn_browse_editor.Name = "btn_browse_editor";
			this.btn_browse_editor.Size = new System.Drawing.Size(33, 29);
			this.btn_browse_editor.TabIndex = 25;
			this.btn_browse_editor.UseVisualStyleBackColor = true;
			this.btn_browse_editor.Click += new System.EventHandler(this.btn40_Click);
			// 
			// tb_external_editor
			// 
			this.tb_external_editor.Location = new System.Drawing.Point(15, 20);
			this.tb_external_editor.Name = "tb_external_editor";
			this.tb_external_editor.Size = new System.Drawing.Size(330, 20);
			this.tb_external_editor.TabIndex = 24;
			// 
			// gb_export_format
			// 
			this.gb_export_format.Controls.Add(this.cb_export_format);
			this.gb_export_format.Location = new System.Drawing.Point(285, 85);
			this.gb_export_format.Name = "gb_export_format";
			this.gb_export_format.Size = new System.Drawing.Size(190, 62);
			this.gb_export_format.TabIndex = 21;
			this.gb_export_format.TabStop = false;
			this.gb_export_format.Text = "Export Format";
			// 
			// cb_export_format
			// 
			this.cb_export_format.FormattingEnabled = true;
			this.cb_export_format.Location = new System.Drawing.Point(73, 23);
			this.cb_export_format.Name = "cb_export_format";
			this.cb_export_format.Size = new System.Drawing.Size(88, 21);
			this.cb_export_format.TabIndex = 28;
			// 
			// gb_thumbnail_size
			// 
			this.gb_thumbnail_size.Controls.Add(this.cb_thumbnail_size);
			this.gb_thumbnail_size.Location = new System.Drawing.Point(70, 85);
			this.gb_thumbnail_size.Name = "gb_thumbnail_size";
			this.gb_thumbnail_size.Size = new System.Drawing.Size(206, 62);
			this.gb_thumbnail_size.TabIndex = 20;
			this.gb_thumbnail_size.TabStop = false;
			this.gb_thumbnail_size.Text = "Thumbnail Size";
			// 
			// cb_thumbnail_size
			// 
			this.cb_thumbnail_size.FormattingEnabled = true;
			this.cb_thumbnail_size.Location = new System.Drawing.Point(25, 25);
			this.cb_thumbnail_size.Name = "cb_thumbnail_size";
			this.cb_thumbnail_size.Size = new System.Drawing.Size(136, 21);
			this.cb_thumbnail_size.TabIndex = 27;
			// 
			// tabCfgSystem
			// 
			this.tabCfgSystem.Controls.Add(this.gb_check_and_restore);
			this.tabCfgSystem.Controls.Add(this.gb_associate_files);
			this.tabCfgSystem.Location = new System.Drawing.Point(4, 32);
			this.tabCfgSystem.Name = "tabCfgSystem";
			this.tabCfgSystem.Size = new System.Drawing.Size(577, 224);
			this.tabCfgSystem.TabIndex = 4;
			this.tabCfgSystem.Text = "System";
			this.tabCfgSystem.UseVisualStyleBackColor = true;
			// 
			// gb_check_and_restore
			// 
			this.gb_check_and_restore.Controls.Add(this.chk_restore_startup);
			this.gb_check_and_restore.Location = new System.Drawing.Point(80, 150);
			this.gb_check_and_restore.Name = "gb_check_and_restore";
			this.gb_check_and_restore.Size = new System.Drawing.Size(390, 60);
			this.gb_check_and_restore.TabIndex = 22;
			this.gb_check_and_restore.TabStop = false;
			this.gb_check_and_restore.Text = "Check and Restore";
			// 
			// chk_restore_startup
			// 
			this.chk_restore_startup.Location = new System.Drawing.Point(45, 25);
			this.chk_restore_startup.Name = "chk_restore_startup";
			this.chk_restore_startup.Size = new System.Drawing.Size(320, 20);
			this.chk_restore_startup.TabIndex = 32;
			this.chk_restore_startup.Text = "Check and restore at startup";
			this.chk_restore_startup.UseVisualStyleBackColor = true;
			// 
			// gb_associate_files
			// 
			this.gb_associate_files.Controls.Add(this.chk_associate_texes);
			this.gb_associate_files.Controls.Add(this.chk_associate_packs);
			this.gb_associate_files.Controls.Add(this.chk_associate_maps);
			this.gb_associate_files.Location = new System.Drawing.Point(80, 25);
			this.gb_associate_files.Name = "gb_associate_files";
			this.gb_associate_files.Size = new System.Drawing.Size(390, 120);
			this.gb_associate_files.TabIndex = 21;
			this.gb_associate_files.TabStop = false;
			this.gb_associate_files.Text = "Associate Files to HLBox17b";
			// 
			// chk_associate_texes
			// 
			this.chk_associate_texes.Location = new System.Drawing.Point(45, 89);
			this.chk_associate_texes.Name = "chk_associate_texes";
			this.chk_associate_texes.Size = new System.Drawing.Size(320, 20);
			this.chk_associate_texes.TabIndex = 31;
			this.chk_associate_texes.Text = "Textures Files (*.wad, *.vtf, *.vmt)";
			this.chk_associate_texes.UseVisualStyleBackColor = true;
			// 
			// chk_associate_packs
			// 
			this.chk_associate_packs.Location = new System.Drawing.Point(45, 57);
			this.chk_associate_packs.Name = "chk_associate_packs";
			this.chk_associate_packs.Size = new System.Drawing.Size(320, 20);
			this.chk_associate_packs.TabIndex = 30;
			this.chk_associate_packs.Text = "Package Files (*.gcf,*.vpk,*.ncf,*.manifest)";
			this.chk_associate_packs.UseVisualStyleBackColor = true;
			// 
			// chk_associate_maps
			// 
			this.chk_associate_maps.Location = new System.Drawing.Point(45, 25);
			this.chk_associate_maps.Name = "chk_associate_maps";
			this.chk_associate_maps.Size = new System.Drawing.Size(320, 20);
			this.chk_associate_maps.TabIndex = 29;
			this.chk_associate_maps.Text = "Maps files (*.bsp)";
			this.chk_associate_maps.UseVisualStyleBackColor = true;
			// 
			// ImgEditDlg
			// 
			this.ImgEditDlg.DefaultExt = "*.exe";
			this.ImgEditDlg.FileName = "ImgEditDlg";
			this.ImgEditDlg.SupportMultiDottedExtensions = true;
			this.ImgEditDlg.ValidateNames = false;
			// 
			// colorDlg
			// 
			this.colorDlg.AllowFullOpen = false;
			this.colorDlg.SolidColorOnly = true;
			// 
			// CfgFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(604, 322);
			this.Controls.Add(this.cfgtabControl);
			this.Controls.Add(this.ctl_cancel);
			this.Controls.Add(this.ctl_ok);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CfgFrm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "CfgFrm";
			this.cfgtabControl.ResumeLayout(false);
			this.tabCfgGeneral.ResumeLayout(false);
			this.gb_simulation_mode.ResumeLayout(false);
			this.gb_language.ResumeLayout(false);
			this.gb_new_version.ResumeLayout(false);
			this.gb_exit.ResumeLayout(false);
			this.gb_start.ResumeLayout(false);
			this.tabCfgPack.ResumeLayout(false);
			this.gb_wad_files.ResumeLayout(false);
			this.gb_compression_level.ResumeLayout(false);
			this.gb_existing_packs.ResumeLayout(false);
			this.gb_empty_dest_path.ResumeLayout(false);
			this.gb_multiple_selection.ResumeLayout(false);
			this.gb_resources_files.ResumeLayout(false);
			this.tabCfgUnpack.ResumeLayout(false);
			this.gb_existing_files.ResumeLayout(false);
			this.gb_map_installation.ResumeLayout(false);
			this.gb_map_installation.PerformLayout();
			this.tabCfgRes.ResumeLayout(false);
			this.gb_existing_resfile.ResumeLayout(false);
			this.gb_unnecessary_files.ResumeLayout(false);
			this.tabCfgTextures.ResumeLayout(false);
			this.gb_new_texcolor.ResumeLayout(false);
			this.gb_external_editor.ResumeLayout(false);
			this.gb_external_editor.PerformLayout();
			this.gb_export_format.ResumeLayout(false);
			this.gb_thumbnail_size.ResumeLayout(false);
			this.tabCfgSystem.ResumeLayout(false);
			this.gb_check_and_restore.ResumeLayout(false);
			this.gb_associate_files.ResumeLayout(false);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Button ctl_cancel;
		private System.Windows.Forms.Button ctl_ok;
		private System.Windows.Forms.TabControl cfgtabControl;
		private System.Windows.Forms.TabPage tabCfgGeneral;
		private System.Windows.Forms.GroupBox gb_simulation_mode;
		public System.Windows.Forms.CheckBox chk_activate_simulation;
		private System.Windows.Forms.GroupBox gb_language;
		private System.Windows.Forms.ComboBox ctl_gen_Language;
		private System.Windows.Forms.GroupBox gb_new_version;
		public System.Windows.Forms.CheckBox chk_check_new_version;
		private System.Windows.Forms.Button btn_check_new_version;
		private System.Windows.Forms.GroupBox gb_exit;
		public System.Windows.Forms.CheckBox chk_ask_before_exit;
		private System.Windows.Forms.GroupBox gb_start;
		public System.Windows.Forms.CheckBox chk_show_welcome;
		private System.Windows.Forms.TabPage tabCfgPack;
		private System.Windows.Forms.GroupBox gb_wad_files;
		public System.Windows.Forms.CheckBox chk_include_wads;
		private System.Windows.Forms.GroupBox gb_compression_level;
		public System.Windows.Forms.ComboBox ctl_pack_ziplevels;
		private System.Windows.Forms.GroupBox gb_existing_packs;
		public System.Windows.Forms.CheckBox chk_replace_packs;
		private System.Windows.Forms.GroupBox gb_empty_dest_path;
		public System.Windows.Forms.RadioButton rd_emptypath_pack;
		public System.Windows.Forms.RadioButton rd_emptypath_error;
		public System.Windows.Forms.RadioButton rd_emptypath_warn;
		private System.Windows.Forms.GroupBox gb_multiple_selection;
		public System.Windows.Forms.RadioButton rd_multiple_one;
		public System.Windows.Forms.RadioButton rd_multiple_all;
		public System.Windows.Forms.RadioButton rd_multiple_ask;
		private System.Windows.Forms.GroupBox gb_resources_files;
		public System.Windows.Forms.CheckBox chk_create_res;
		private System.Windows.Forms.TabPage tabCfgUnpack;
		private System.Windows.Forms.GroupBox gb_existing_files;
		public System.Windows.Forms.CheckBox chk_replace;
		public System.Windows.Forms.CheckBox chk_ask_datetime;
		private System.Windows.Forms.GroupBox gb_map_installation;
		public System.Windows.Forms.CheckBox chk_detect_target_folder;
		public System.Windows.Forms.TextBox ctl_smi_unpackmiscpath;
		private System.Windows.Forms.Label txt_misc_folder;
		public System.Windows.Forms.CheckBox chk_parse_folders;
		private System.Windows.Forms.TabPage tabCfgRes;
		private System.Windows.Forms.GroupBox gb_existing_resfile;
		public System.Windows.Forms.CheckBox chk_res_overwrite;
		private System.Windows.Forms.GroupBox gb_unnecessary_files;
		public System.Windows.Forms.CheckBox chk_include_commented;
		private System.Windows.Forms.TabPage tabCfgTextures;
		private System.Windows.Forms.GroupBox gb_external_editor;
		private System.Windows.Forms.Button btn_browse_editor;
		public System.Windows.Forms.TextBox tb_external_editor;
		private System.Windows.Forms.GroupBox gb_export_format;
		public System.Windows.Forms.ComboBox cb_export_format;
		private System.Windows.Forms.GroupBox gb_thumbnail_size;
		public System.Windows.Forms.ComboBox cb_thumbnail_size;
		private System.Windows.Forms.TabPage tabCfgSystem;
		private System.Windows.Forms.GroupBox gb_check_and_restore;
		public System.Windows.Forms.CheckBox chk_restore_startup;
		private System.Windows.Forms.GroupBox gb_associate_files;
		public System.Windows.Forms.CheckBox chk_associate_texes;
		public System.Windows.Forms.CheckBox chk_associate_packs;
		public System.Windows.Forms.CheckBox chk_associate_maps;
		private System.Windows.Forms.OpenFileDialog ImgEditDlg;
		private System.Windows.Forms.GroupBox gb_new_texcolor;
		private System.Windows.Forms.Label nwTexColor2;
		private System.Windows.Forms.Label lbl_NewTexColor2;
		private System.Windows.Forms.Label nwTexColor1;
		private System.Windows.Forms.Label lbl_NewTexColor1;
		private System.Windows.Forms.ColorDialog colorDlg;
		}
	}