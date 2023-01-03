namespace HLBox17b.Forms.MdiForms
	{
	partial class XplMdi
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XplMdi));
			this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.sb_Progress = new System.Windows.Forms.ToolStripProgressBar();
			this.sb_Infos = new System.Windows.Forms.ToolStripStatusLabel();
			this.lv_Tree = new System.Windows.Forms.TreeView();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ctxmenu_ExtractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ctxmenu_ShellExecMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxmenu_ViewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageFolder = new System.Windows.Forms.ImageList(this.components);
			this.lv_Files = new System.Windows.Forms.ListView();
			this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.FileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.FileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainmenu_ExtractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mainmenu_ShellExecMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainmenu_ViewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bgLoader = new System.ComponentModel.BackgroundWorker();
			this.imageFiles = new System.Windows.Forms.ImageList(this.components);
			this.bgExtract = new System.ComponentModel.BackgroundWorker();
			this.statusBar.SuspendLayout();
			this.contextMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sb_Progress,
            this.sb_Infos});
			this.statusBar.Location = new System.Drawing.Point(0, 316);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(540, 22);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 1;
			// 
			// sb_Progress
			// 
			this.sb_Progress.Name = "sb_Progress";
			this.sb_Progress.Size = new System.Drawing.Size(250, 16);
			// 
			// sb_Infos
			// 
			this.sb_Infos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.sb_Infos.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.sb_Infos.Name = "sb_Infos";
			this.sb_Infos.Size = new System.Drawing.Size(273, 17);
			this.sb_Infos.Spring = true;
			this.sb_Infos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lv_Tree
			// 
			this.lv_Tree.ContextMenuStrip = this.contextMenu;
			this.lv_Tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv_Tree.HideSelection = false;
			this.lv_Tree.ImageIndex = 0;
			this.lv_Tree.ImageList = this.imageFolder;
			this.lv_Tree.Location = new System.Drawing.Point(0, 0);
			this.lv_Tree.Name = "lv_Tree";
			this.lv_Tree.SelectedImageIndex = 0;
			this.lv_Tree.Size = new System.Drawing.Size(220, 316);
			this.lv_Tree.TabIndex = 0;
			this.lv_Tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.lvtree_NodeMouseClick);
			this.lv_Tree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bgExtract_OnKeyDown);
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxmenu_ExtractMenuItem,
            this.toolStripSeparator2,
            this.ctxmenu_ShellExecMenuItem,
            this.ctxmenu_ViewFolderMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(143, 76);
			// 
			// ctxmenu_ExtractMenuItem
			// 
			this.ctxmenu_ExtractMenuItem.Image = global::HLBox17b.Properties.Resources.folder_export;
			this.ctxmenu_ExtractMenuItem.Name = "ctxmenu_ExtractMenuItem";
			this.ctxmenu_ExtractMenuItem.Size = new System.Drawing.Size(142, 22);
			this.ctxmenu_ExtractMenuItem.Text = "Extract";
			this.ctxmenu_ExtractMenuItem.Click += new System.EventHandler(this.menu_ClickExtract);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
			// 
			// ctxmenu_ShellExecMenuItem
			// 
			this.ctxmenu_ShellExecMenuItem.Image = global::HLBox17b.Properties.Resources.lightning;
			this.ctxmenu_ShellExecMenuItem.Name = "ctxmenu_ShellExecMenuItem";
			this.ctxmenu_ShellExecMenuItem.Size = new System.Drawing.Size(142, 22);
			this.ctxmenu_ShellExecMenuItem.Text = "Shell Execute";
			this.ctxmenu_ShellExecMenuItem.Click += new System.EventHandler(this.menu_ClickShellExec);
			// 
			// ctxmenu_ViewFolderMenuItem
			// 
			this.ctxmenu_ViewFolderMenuItem.Image = global::HLBox17b.Properties.Resources.folder_explore;
			this.ctxmenu_ViewFolderMenuItem.Name = "ctxmenu_ViewFolderMenuItem";
			this.ctxmenu_ViewFolderMenuItem.Size = new System.Drawing.Size(142, 22);
			this.ctxmenu_ViewFolderMenuItem.Text = "View Folder";
			this.ctxmenu_ViewFolderMenuItem.Click += new System.EventHandler(this.menu_ClickViewFolder);
			// 
			// imageFolder
			// 
			this.imageFolder.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageFolder.ImageSize = new System.Drawing.Size(16, 16);
			this.imageFolder.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lv_Files
			// 
			this.lv_Files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileName,
            this.FileType,
            this.FileSize});
			this.lv_Files.ContextMenuStrip = this.contextMenu;
			this.lv_Files.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv_Files.HideSelection = false;
			this.lv_Files.Location = new System.Drawing.Point(0, 0);
			this.lv_Files.Name = "lv_Files";
			this.lv_Files.Size = new System.Drawing.Size(316, 316);
			this.lv_Files.TabIndex = 0;
			this.lv_Files.UseCompatibleStateImageBehavior = false;
			this.lv_Files.View = System.Windows.Forms.View.Details;
			this.lv_Files.VirtualMode = true;
			this.lv_Files.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvfiles_ColumnClick);
			this.lv_Files.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvfiles_RetrieveVirtualItem);
			this.lv_Files.SelectedIndexChanged += new System.EventHandler(this.lvfiles_SelectedIndexChanged);
			this.lv_Files.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bgExtract_OnKeyDown);
			this.lv_Files.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvfiles_DoubleClick);
			// 
			// FileName
			// 
			this.FileName.Text = "FName";
			this.FileName.Width = 222;
			// 
			// FileType
			// 
			this.FileType.Text = "FType";
			this.FileType.Width = 138;
			// 
			// FileSize
			// 
			this.FileSize.Text = "FSize";
			this.FileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.FileSize.Width = 79;
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.lv_Tree);
			this.splitContainer.Panel1MinSize = 160;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.lv_Files);
			this.splitContainer.Size = new System.Drawing.Size(540, 316);
			this.splitContainer.SplitterDistance = 220;
			this.splitContainer.TabIndex = 10;
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(540, 24);
			this.MainMenu.TabIndex = 11;
			this.MainMenu.Text = "menuStrip1";
			this.MainMenu.Visible = false;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.mainmenu_ExtractMenuItem,
            this.toolStripSeparator3,
            this.mainmenu_ShellExecMenuItem,
            this.mainmenu_ViewFolderMenuItem});
			this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.fileToolStripMenuItem.MergeIndex = 1;
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator1.MergeIndex = 4;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
			// 
			// mainmenu_ExtractMenuItem
			// 
			this.mainmenu_ExtractMenuItem.Image = global::HLBox17b.Properties.Resources.folder_export;
			this.mainmenu_ExtractMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.mainmenu_ExtractMenuItem.MergeIndex = 5;
			this.mainmenu_ExtractMenuItem.Name = "mainmenu_ExtractMenuItem";
			this.mainmenu_ExtractMenuItem.Size = new System.Drawing.Size(142, 22);
			this.mainmenu_ExtractMenuItem.Text = "Extract";
			this.mainmenu_ExtractMenuItem.Click += new System.EventHandler(this.menu_ClickExtract);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator3.MergeIndex = 6;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(139, 6);
			// 
			// mainmenu_ShellExecMenuItem
			// 
			this.mainmenu_ShellExecMenuItem.Image = global::HLBox17b.Properties.Resources.lightning;
			this.mainmenu_ShellExecMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.mainmenu_ShellExecMenuItem.MergeIndex = 7;
			this.mainmenu_ShellExecMenuItem.Name = "mainmenu_ShellExecMenuItem";
			this.mainmenu_ShellExecMenuItem.Size = new System.Drawing.Size(142, 22);
			this.mainmenu_ShellExecMenuItem.Text = "Shell Execute";
			this.mainmenu_ShellExecMenuItem.Click += new System.EventHandler(this.menu_ClickShellExec);
			// 
			// mainmenu_ViewFolderMenuItem
			// 
			this.mainmenu_ViewFolderMenuItem.Image = global::HLBox17b.Properties.Resources.folder_explore;
			this.mainmenu_ViewFolderMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.mainmenu_ViewFolderMenuItem.MergeIndex = 8;
			this.mainmenu_ViewFolderMenuItem.Name = "mainmenu_ViewFolderMenuItem";
			this.mainmenu_ViewFolderMenuItem.Size = new System.Drawing.Size(142, 22);
			this.mainmenu_ViewFolderMenuItem.Text = "View Folder";
			this.mainmenu_ViewFolderMenuItem.Click += new System.EventHandler(this.menu_ClickViewFolder);
			// 
			// bgLoader
			// 
			this.bgLoader.WorkerReportsProgress = true;
			this.bgLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgLoader_DoWork);
			this.bgLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgLoader_ProgressChanged);
			this.bgLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgLoader_WorkerCompleted);
			// 
			// imageFiles
			// 
			this.imageFiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageFiles.ImageSize = new System.Drawing.Size(16, 16);
			this.imageFiles.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// bgExtract
			// 
			this.bgExtract.WorkerReportsProgress = true;
			this.bgExtract.WorkerSupportsCancellation = true;
			this.bgExtract.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgExtract_DoWork);
			this.bgExtract.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgExtract_ProgressChanged);
			this.bgExtract.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgExtract_RunWorkerCompleted);
			// 
			// XplMdi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 338);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.MainMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MainMenu;
			this.Name = "XplMdi";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.Shown += new System.EventHandler(this.OnShow);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bgExtract_OnKeyDown);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.contextMenu.ResumeLayout(false);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.ListView lv_Files;
		private System.Windows.Forms.TreeView lv_Tree;
		private System.Windows.Forms.ColumnHeader FileName;
		private System.Windows.Forms.ColumnHeader FileType;
		private System.Windows.Forms.ColumnHeader FileSize;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripProgressBar sb_Progress;
		private System.Windows.Forms.ToolStripStatusLabel sb_Infos;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mainmenu_ExtractMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem ctxmenu_ExtractMenuItem;
		private System.ComponentModel.BackgroundWorker bgLoader;
		private System.Windows.Forms.ImageList imageFolder;
		private System.Windows.Forms.ImageList imageFiles;
		private System.Windows.Forms.ToolStripMenuItem ctxmenu_ShellExecMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mainmenu_ShellExecMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem ctxmenu_ViewFolderMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mainmenu_ViewFolderMenuItem;
		private System.ComponentModel.BackgroundWorker bgExtract;

		}
	}