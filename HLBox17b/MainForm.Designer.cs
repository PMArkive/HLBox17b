namespace HLBox17b
	{
	partial class MainForm
		{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
			{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menu_17b_Check = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_17b_CheckMaps = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_17b_CheckWads = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_File_Sep1 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_File_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_File_CloseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_File_Sep2 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_File_Recents = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_File_Options = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_File_Quit = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Paste = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Maps = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Maps_Pack = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Maps_Install = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_Maps_Resgen = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Textures = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Textures_NewWad = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_17b = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_17b_UserLogin = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_17b_Visit17b = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Steam = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Steam_Banner = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Steam_Clean = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Window = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Window_Arrange = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Window_Cascade = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_Window_Horizontal = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Window_Vertical = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_Window_Maximize = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Window_Minimize = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Help_About = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.menuMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// menu_17b_Check
			// 
			this.menu_17b_Check.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_17b_CheckMaps,
            this.menu_17b_CheckWads});
			this.menu_17b_Check.Image = global::HLBox17b.Properties.Resources.database;
			this.menu_17b_Check.Name = "menu_17b_Check";
			this.menu_17b_Check.Size = new System.Drawing.Size(176, 22);
			this.menu_17b_Check.Text = "Check Existing Files";
			// 
			// menu_17b_CheckMaps
			// 
			this.menu_17b_CheckMaps.Name = "menu_17b_CheckMaps";
			this.menu_17b_CheckMaps.Size = new System.Drawing.Size(103, 22);
			this.menu_17b_CheckMaps.Text = "Maps";
			this.menu_17b_CheckMaps.Click += new System.EventHandler(this.menu_17b_CheckMaps_Click);
			// 
			// menu_17b_CheckWads
			// 
			this.menu_17b_CheckWads.Name = "menu_17b_CheckWads";
			this.menu_17b_CheckWads.Size = new System.Drawing.Size(103, 22);
			this.menu_17b_CheckWads.Text = "Wads";
			this.menu_17b_CheckWads.Click += new System.EventHandler(this.menu_17b_CheckWads_Click);
			// 
			// menuMain
			// 
			this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_Edit,
            this.menu_Maps,
            this.menu_Textures,
            this.menu_17b,
            this.menu_Steam,
            this.menu_Window,
            this.menu_Help});
			this.menuMain.Location = new System.Drawing.Point(0, 0);
			this.menuMain.MdiWindowListItem = this.menu_Window;
			this.menuMain.Name = "menuMain";
			this.menuMain.Size = new System.Drawing.Size(657, 24);
			this.menuMain.TabIndex = 4;
			this.menuMain.Text = "menuStrip1";
			// 
			// menu_File
			// 
			this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_Open,
            this.menu_File_Sep1,
            this.menu_File_Close,
            this.menu_File_CloseAll,
            this.menu_File_Sep2,
            this.menu_File_Recents,
            this.toolStripSeparator3,
            this.menu_File_Options,
            this.toolStripSeparator6,
            this.menu_File_Quit});
			this.menu_File.Name = "menu_File";
			this.menu_File.Size = new System.Drawing.Size(37, 20);
			this.menu_File.Text = "File";
			// 
			// menu_File_Open
			// 
			this.menu_File_Open.Image = global::HLBox17b.Properties.Resources.mnuOpen;
			this.menu_File_Open.Name = "menu_File_Open";
			this.menu_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
			this.menu_File_Open.Size = new System.Drawing.Size(174, 22);
			this.menu_File_Open.Text = "Open";
			this.menu_File_Open.Click += new System.EventHandler(this.menu_File_Open_Click);
			// 
			// menu_File_Sep1
			// 
			this.menu_File_Sep1.Name = "menu_File_Sep1";
			this.menu_File_Sep1.Size = new System.Drawing.Size(171, 6);
			// 
			// menu_File_Close
			// 
			this.menu_File_Close.Name = "menu_File_Close";
			this.menu_File_Close.Size = new System.Drawing.Size(174, 22);
			this.menu_File_Close.Text = "Close";
			this.menu_File_Close.Click += new System.EventHandler(this.menu_File_Close_Click);
			// 
			// menu_File_CloseAll
			// 
			this.menu_File_CloseAll.Name = "menu_File_CloseAll";
			this.menu_File_CloseAll.Size = new System.Drawing.Size(174, 22);
			this.menu_File_CloseAll.Text = "CloseAll";
			this.menu_File_CloseAll.Click += new System.EventHandler(this.menu_File_CloseAll_Click);
			// 
			// menu_File_Sep2
			// 
			this.menu_File_Sep2.Name = "menu_File_Sep2";
			this.menu_File_Sep2.Size = new System.Drawing.Size(171, 6);
			// 
			// menu_File_Recents
			// 
			this.menu_File_Recents.Name = "menu_File_Recents";
			this.menu_File_Recents.Size = new System.Drawing.Size(174, 22);
			this.menu_File_Recents.Text = "Recents";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(171, 6);
			// 
			// menu_File_Options
			// 
			this.menu_File_Options.Image = global::HLBox17b.Properties.Resources.settings_16;
			this.menu_File_Options.Name = "menu_File_Options";
			this.menu_File_Options.Size = new System.Drawing.Size(174, 22);
			this.menu_File_Options.Text = "Options";
			this.menu_File_Options.Click += new System.EventHandler(this.menu_File_Options_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(171, 6);
			// 
			// menu_File_Quit
			// 
			this.menu_File_Quit.Name = "menu_File_Quit";
			this.menu_File_Quit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.menu_File_Quit.Size = new System.Drawing.Size(174, 22);
			this.menu_File_Quit.Text = "Exit";
			this.menu_File_Quit.Click += new System.EventHandler(this.menu_File_Quit_Click);
			// 
			// menu_Edit
			// 
			this.menu_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Copy,
            this.menuItem_Paste});
			this.menu_Edit.Name = "menu_Edit";
			this.menu_Edit.Size = new System.Drawing.Size(39, 20);
			this.menu_Edit.Text = "Edit";
			// 
			// menuItem_Copy
			// 
			this.menuItem_Copy.Enabled = false;
			this.menuItem_Copy.Image = global::HLBox17b.Properties.Resources.CopyHS;
			this.menuItem_Copy.Name = "menuItem_Copy";
			this.menuItem_Copy.ShortcutKeyDisplayString = "Ctrl+C";
			this.menuItem_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.menuItem_Copy.Size = new System.Drawing.Size(144, 22);
			this.menuItem_Copy.Text = "Copy";
			// 
			// menuItem_Paste
			// 
			this.menuItem_Paste.Enabled = false;
			this.menuItem_Paste.Image = global::HLBox17b.Properties.Resources.PasteHS;
			this.menuItem_Paste.Name = "menuItem_Paste";
			this.menuItem_Paste.ShortcutKeyDisplayString = "Ctrl+V";
			this.menuItem_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.menuItem_Paste.Size = new System.Drawing.Size(144, 22);
			this.menuItem_Paste.Text = "Paste";
			this.menuItem_Paste.Click += new System.EventHandler(this.menu_Edit_Paste_Click);
			// 
			// menu_Maps
			// 
			this.menu_Maps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Maps_Pack,
            this.menu_Maps_Install,
            this.toolStripSeparator7,
            this.menu_Maps_Resgen});
			this.menu_Maps.Name = "menu_Maps";
			this.menu_Maps.Size = new System.Drawing.Size(48, 20);
			this.menu_Maps.Text = "Maps";
			// 
			// menu_Maps_Pack
			// 
			this.menu_Maps_Pack.Image = global::HLBox17b.Properties.Resources.box;
			this.menu_Maps_Pack.Name = "menu_Maps_Pack";
			this.menu_Maps_Pack.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
			this.menu_Maps_Pack.Size = new System.Drawing.Size(224, 22);
			this.menu_Maps_Pack.Text = "Create Package";
			this.menu_Maps_Pack.Click += new System.EventHandler(this.menu_Maps_Pack_Click);
			// 
			// menu_Maps_Install
			// 
			this.menu_Maps_Install.Image = global::HLBox17b.Properties.Resources.nodeselectall;
			this.menu_Maps_Install.Name = "menu_Maps_Install";
			this.menu_Maps_Install.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
			this.menu_Maps_Install.Size = new System.Drawing.Size(224, 22);
			this.menu_Maps_Install.Text = "Install a map";
			this.menu_Maps_Install.Click += new System.EventHandler(this.menu_Maps_Install_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(221, 6);
			// 
			// menu_Maps_Resgen
			// 
			this.menu_Maps_Resgen.Name = "menu_Maps_Resgen";
			this.menu_Maps_Resgen.Size = new System.Drawing.Size(224, 22);
			this.menu_Maps_Resgen.Text = "Make res file";
			this.menu_Maps_Resgen.Click += new System.EventHandler(this.menu_Maps_Resgen_Click);
			// 
			// menu_Textures
			// 
			this.menu_Textures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Textures_NewWad});
			this.menu_Textures.Name = "menu_Textures";
			this.menu_Textures.Size = new System.Drawing.Size(63, 20);
			this.menu_Textures.Text = "Textures";
			// 
			// menu_Textures_NewWad
			// 
			this.menu_Textures_NewWad.Image = global::HLBox17b.Properties.Resources.pictures;
			this.menu_Textures_NewWad.Name = "menu_Textures_NewWad";
			this.menu_Textures_NewWad.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
			this.menu_Textures_NewWad.Size = new System.Drawing.Size(235, 22);
			this.menu_Textures_NewWad.Text = "Create New Wad";
			this.menu_Textures_NewWad.Click += new System.EventHandler(this.menu_Textures_NewWad_Click);
			// 
			// menu_17b
			// 
			this.menu_17b.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_17b_Check,
            this.toolStripSeparator2,
            this.menu_17b_UserLogin,
            this.toolStripSeparator8,
            this.menu_17b_Visit17b});
			this.menu_17b.Name = "menu_17b";
			this.menu_17b.Size = new System.Drawing.Size(73, 20);
			this.menu_17b.Text = "17Buddies";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
			// 
			// menu_17b_UserLogin
			// 
			this.menu_17b_UserLogin.Image = global::HLBox17b.Properties.Resources.usersilhouettequestion;
			this.menu_17b_UserLogin.Name = "menu_17b_UserLogin";
			this.menu_17b_UserLogin.Size = new System.Drawing.Size(176, 22);
			this.menu_17b_UserLogin.Text = "Login";
			this.menu_17b_UserLogin.Click += new System.EventHandler(this.menu_17b_UserLogin_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(173, 6);
			// 
			// menu_17b_Visit17b
			// 
			this.menu_17b_Visit17b.Image = global::HLBox17b.Properties.Resources.Ico17b;
			this.menu_17b_Visit17b.Name = "menu_17b_Visit17b";
			this.menu_17b_Visit17b.Size = new System.Drawing.Size(176, 22);
			this.menu_17b_Visit17b.Text = "Visit Website";
			this.menu_17b_Visit17b.Click += new System.EventHandler(this.menu_17b_Visit17b_Click);
			// 
			// menu_Steam
			//
			this.menu_Steam.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Steam_Banner,
			this.menu_Steam_Clean});
			this.menu_Steam.Name = "menu_Steam";
			this.menu_Steam.Size = new System.Drawing.Size(84, 20);
			this.menu_Steam.Text = "Steam Tools";
			// 
			// menu_Steam_Banner
			// 
			this.menu_Steam_Banner.Image = global::HLBox17b.Properties.Resources.steambanner;
			this.menu_Steam_Banner.Name = "menu_Steam_Banner";
			this.menu_Steam_Banner.Size = new System.Drawing.Size(155, 22);
			this.menu_Steam_Banner.Text = "Change Banner";
			this.menu_Steam_Banner.Click += new System.EventHandler(this.menu_Steam_Banner_Click);
			// 
			// menu_Steam_Clean
			// 
			this.menu_Steam_Clean.Image = global::HLBox17b.Properties.Resources.broom;
			this.menu_Steam_Clean.Name = "menu_Steam_Clean";
			this.menu_Steam_Clean.Size = new System.Drawing.Size(155, 22);
			this.menu_Steam_Clean.Text = "Clean Folders";
			this.menu_Steam_Clean.Click += new System.EventHandler(this.menu_Steam_Clean_Click);
			// 
			// menu_Window
			// 
			this.menu_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Window_Arrange,
            this.menu_Window_Cascade,
            this.toolStripSeparator4,
            this.menu_Window_Horizontal,
            this.menu_Window_Vertical,
            this.toolStripSeparator5,
            this.menu_Window_Maximize,
            this.menu_Window_Minimize,
            this.toolStripSeparator1});
			this.menu_Window.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menu_Window.MergeIndex = 99;
			this.menu_Window.Name = "menu_Window";
			this.menu_Window.Size = new System.Drawing.Size(63, 20);
			this.menu_Window.Text = "Window";
			// 
			// menu_Window_Arrange
			// 
			this.menu_Window_Arrange.Name = "menu_Window_Arrange";
			this.menu_Window_Arrange.Size = new System.Drawing.Size(129, 22);
			this.menu_Window_Arrange.Text = "Arrange";
			this.menu_Window_Arrange.Click += new System.EventHandler(this.arrangeToolStripMenuItem_Click);
			// 
			// menu_Window_Cascade
			// 
			this.menu_Window_Cascade.Image = global::HLBox17b.Properties.Resources.mnuCas;
			this.menu_Window_Cascade.Name = "menu_Window_Cascade";
			this.menu_Window_Cascade.Size = new System.Drawing.Size(129, 22);
			this.menu_Window_Cascade.Text = "Cascade";
			this.menu_Window_Cascade.Click += new System.EventHandler(this.menu_Window_Cascade_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(126, 6);
			// 
			// menu_Window_Horizontal
			// 
			this.menu_Window_Horizontal.Image = global::HLBox17b.Properties.Resources.mnuTileWindowsHorizontally;
			this.menu_Window_Horizontal.Name = "menu_Window_Horizontal";
			this.menu_Window_Horizontal.Size = new System.Drawing.Size(129, 22);
			this.menu_Window_Horizontal.Text = "Horizontal";
			this.menu_Window_Horizontal.Click += new System.EventHandler(this.menu_Window_Horizontal_Click);
			// 
			// menu_Window_Vertical
			// 
			this.menu_Window_Vertical.Name = "menu_Window_Vertical";
			this.menu_Window_Vertical.Size = new System.Drawing.Size(129, 22);
			this.menu_Window_Vertical.Text = "Vertical";
			this.menu_Window_Vertical.Click += new System.EventHandler(this.menu_Window_Vertical_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(126, 6);
			// 
			// menu_Window_Maximize
			// 
			this.menu_Window_Maximize.Name = "menu_Window_Maximize";
			this.menu_Window_Maximize.Size = new System.Drawing.Size(129, 22);
			this.menu_Window_Maximize.Text = "Maximize";
			this.menu_Window_Maximize.Click += new System.EventHandler(this.menu_Window_Maximize_Click);
			// 
			// menu_Window_Minimize
			// 
			this.menu_Window_Minimize.Name = "menu_Window_Minimize";
			this.menu_Window_Minimize.Size = new System.Drawing.Size(129, 22);
			this.menu_Window_Minimize.Text = "Minimize";
			this.menu_Window_Minimize.Click += new System.EventHandler(this.menu_Window_Minimize_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
			// 
			// menu_Help
			// 
			this.menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Help_About});
			this.menu_Help.Name = "menu_Help";
			this.menu_Help.Size = new System.Drawing.Size(44, 20);
			this.menu_Help.Text = "Help";
			// 
			// menu_Help_About
			// 
			this.menu_Help_About.Name = "menu_Help_About";
			this.menu_Help_About.Size = new System.Drawing.Size(107, 22);
			this.menu_Help_About.Text = "About";
			this.menu_Help_About.Click += new System.EventHandler(this.menu_Help_About_Click);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 390);
			this.Controls.Add(this.menuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuMain;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Title";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Activated += new System.EventHandler(this.mainform_Activated);
			this.Deactivate += new System.EventHandler(this.mainform_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainform_FormClosing);
			this.Shown += new System.EventHandler(this.mainform_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainform_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.mainform_DragOver);
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem menu_File;
		private System.Windows.Forms.ToolStripMenuItem menu_File_Open;
		private System.Windows.Forms.ToolStripSeparator menu_File_Sep1;
		private System.Windows.Forms.ToolStripMenuItem menu_File_Close;
		private System.Windows.Forms.ToolStripMenuItem menu_File_CloseAll;
		private System.Windows.Forms.ToolStripSeparator menu_File_Sep2;
		private System.Windows.Forms.ToolStripMenuItem menu_File_Recents;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem menu_File_Quit;
		private System.Windows.Forms.ToolStripMenuItem menu_Window;
		private System.Windows.Forms.ToolStripMenuItem menu_Window_Cascade;
		private System.Windows.Forms.ToolStripMenuItem menu_Window_Horizontal;
		private System.Windows.Forms.ToolStripMenuItem menu_Window_Vertical;
		private System.Windows.Forms.ToolStripMenuItem menu_Window_Maximize;
		private System.Windows.Forms.ToolStripMenuItem menu_Window_Minimize;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem menu_Window_Arrange;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menu_File_Options;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem menu_Maps;
		private System.Windows.Forms.ToolStripMenuItem menu_Maps_Install;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem menu_Maps_Resgen;
		private System.Windows.Forms.ToolStripMenuItem menu_Maps_Pack;
		private System.Windows.Forms.ToolStripMenuItem menu_17b;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem menu_17b_Visit17b;
		private System.Windows.Forms.ToolStripMenuItem menu_Steam;
		private System.Windows.Forms.ToolStripMenuItem menu_Steam_Banner;
		private System.Windows.Forms.ToolStripMenuItem menu_Steam_Clean;
		private System.Windows.Forms.ToolStripMenuItem menu_Textures;
		private System.Windows.Forms.ToolStripMenuItem menu_Textures_NewWad;
		private System.Windows.Forms.ToolStripMenuItem menu_17b_UserLogin;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem menu_17b_CheckMaps;
		private System.Windows.Forms.ToolStripMenuItem menu_17b_CheckWads;
		private System.Windows.Forms.ToolStripMenuItem menu_Help;
		private System.Windows.Forms.ToolStripMenuItem menu_Help_About;
		private System.Windows.Forms.ToolStripMenuItem menu_17b_Check;
		private System.Windows.Forms.ToolStripMenuItem menu_Edit;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Paste;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Copy;


		}
	}

