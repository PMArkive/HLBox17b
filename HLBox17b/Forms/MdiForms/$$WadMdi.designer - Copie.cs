namespace HLBox17b.Forms.MdiForms
	{
	partial class WadMdi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WadMdi));
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.menuMain_File = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.menuMain_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Paste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_Cut = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_Duplicate = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMain_Textures = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Addtexture = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.menuSub_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Showimages = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Showdetails = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_Showtransparency = new System.Windows.Forms.ToolStripMenuItem();
			this.sFileDlg = new System.Windows.Forms.SaveFileDialog();
			this.lvTexView = new System.Windows.Forms.ListView();
			this.szName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nWidth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nHeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ctxtMenuChild = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.contextItem_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.contextItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.contextItem_Paste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.contextItem_Cut = new System.Windows.Forms.ToolStripMenuItem();
			this.contextItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.contextItem_Duplicate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.contextItem_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.statBar = new System.Windows.Forms.StatusStrip();
			this.sb_Progress = new System.Windows.Forms.ToolStripProgressBar();
			this.sb_Infos = new System.Windows.Forms.ToolStripStatusLabel();
			this.sb_Name = new System.Windows.Forms.ToolStripStatusLabel();
			this.sb_Size = new System.Windows.Forms.ToolStripStatusLabel();
			this.importImgDlg = new System.Windows.Forms.OpenFileDialog();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.lbTexNames = new System.Windows.Forms.ListBox();
			this.bgLoader = new System.ComponentModel.BackgroundWorker();
			this.bgImporter = new System.ComponentModel.BackgroundWorker();
			this.ctxtMenuChild.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(540, 24);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "mnuMdiChild";
			this.MainMenu.Visible = false;
			// 
			// menuMain_File
			// 
			this.menuMain_File.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.menuMain_File.MergeIndex = 1;
			this.menuMain_File.Name = "menuMain_File";
			this.menuMain_File.Size = new System.Drawing.Size(37, 20);
			this.menuMain_File.Text = "File";
			// 
			// menuItem_Save
			// 
			this.menuItem_Save.Image = global::HLBox17b.Properties.Resources.mnuSave;
			this.menuItem_Save.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuItem_Save.MergeIndex = 2;
			this.menuItem_Save.Name = "menuItem_Save";
			this.menuItem_Save.ShortcutKeyDisplayString = "";
			this.menuItem_Save.Size = new System.Drawing.Size(152, 22);
			this.menuItem_Save.Text = "Save";
			this.menuItem_Save.Click += new System.EventHandler(this.menuItem_Save_Click);
			// 
			// menuItem_SaveAs
			// 
			this.menuItem_SaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuItem_SaveAs.MergeIndex = 3;
			this.menuItem_SaveAs.Name = "menuItem_SaveAs";
			this.menuItem_SaveAs.Size = new System.Drawing.Size(152, 22);
			this.menuItem_SaveAs.Text = "SaveAs";
			this.menuItem_SaveAs.Click += new System.EventHandler(this.menuItem_SaveAs_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator1.MergeIndex = 4;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// menuItem_Import
			// 
			this.menuItem_Import.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuItem_Import.MergeIndex = 5;
			this.menuItem_Import.Name = "menuItem_Import";
			this.menuItem_Import.Size = new System.Drawing.Size(152, 22);
			this.menuItem_Import.Text = "Import";
			this.menuItem_Import.Click += new System.EventHandler(this.menuItem_Import_Click);
			// 
			// menuItem_Export
			// 
			this.menuItem_Export.Enabled = false;
			this.menuItem_Export.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuItem_Export.MergeIndex = 6;
			this.menuItem_Export.Name = "menuItem_Export";
			this.menuItem_Export.Size = new System.Drawing.Size(152, 22);
			this.menuItem_Export.Text = "Export";
			this.menuItem_Export.Click += new System.EventHandler(this.menuItem_Export_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator8.MergeIndex = 7;
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
			// 
			// menuMain_Edit
			// 
			this.menuMain_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Edit,
            this.menuItem_Show,
            this.toolStripSeparator10,
            this.menuItem_Copy,
            this.menuItem_Paste,
            this.toolStripSeparator5,
            this.menuItem_Cut,
            this.menuItem_Delete,
            this.toolStripSeparator6,
            this.menuItem_Duplicate});
			this.menuMain_Edit.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuMain_Edit.MergeIndex = 1;
			this.menuMain_Edit.Name = "menuMain_Edit";
			this.menuMain_Edit.Size = new System.Drawing.Size(39, 20);
			this.menuMain_Edit.Text = "Edit";
			// 
			// menuItem_Edit
			// 
			this.menuItem_Edit.Image = global::HLBox17b.Properties.Resources.image__pencil;
			this.menuItem_Edit.Name = "menuItem_Edit";
			this.menuItem_Edit.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Edit.Text = "Editer";
			this.menuItem_Edit.Click += new System.EventHandler(this.menuItem_Edit_Click);
			// 
			// menuItem_Show
			// 
			this.menuItem_Show.Image = global::HLBox17b.Properties.Resources.FullScreen;
			this.menuItem_Show.Name = "menuItem_Show";
			this.menuItem_Show.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Show.Text = "Show";
			this.menuItem_Show.Click += new System.EventHandler(this.menuItem_Show_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(163, 6);
			// 
			// menuItem_Copy
			// 
			this.menuItem_Copy.Image = global::HLBox17b.Properties.Resources.CopyHS;
			this.menuItem_Copy.Name = "menuItem_Copy";
			this.menuItem_Copy.ShortcutKeyDisplayString = "Ctrl+C";
			this.menuItem_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.menuItem_Copy.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Copy.Text = "Copy";
			this.menuItem_Copy.Click += new System.EventHandler(this.menuItem_Copy_Click);
			// 
			// menuItem_Paste
			// 
			this.menuItem_Paste.Image = global::HLBox17b.Properties.Resources.PasteHS;
			this.menuItem_Paste.Name = "menuItem_Paste";
			this.menuItem_Paste.ShortcutKeyDisplayString = "Ctrl+V";
			this.menuItem_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.menuItem_Paste.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Paste.Text = "Paste";
			this.menuItem_Paste.Click += new System.EventHandler(this.menuItem_Paste_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(163, 6);
			// 
			// menuItem_Cut
			// 
			this.menuItem_Cut.Image = global::HLBox17b.Properties.Resources.CutHS;
			this.menuItem_Cut.Name = "menuItem_Cut";
			this.menuItem_Cut.ShortcutKeyDisplayString = "Ctrl+X";
			this.menuItem_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.menuItem_Cut.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Cut.Text = "Cut";
			this.menuItem_Cut.Click += new System.EventHandler(this.menuItem_Cut_Click);
			// 
			// menuItem_Delete
			// 
			this.menuItem_Delete.Image = global::HLBox17b.Properties.Resources.delete;
			this.menuItem_Delete.Name = "menuItem_Delete";
			this.menuItem_Delete.ShortcutKeyDisplayString = "Suppr";
			this.menuItem_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.menuItem_Delete.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Delete.Text = "Delete";
			this.menuItem_Delete.Click += new System.EventHandler(this.menuItem_Delete_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(163, 6);
			// 
			// menuItem_Duplicate
			// 
			this.menuItem_Duplicate.Name = "menuItem_Duplicate";
			this.menuItem_Duplicate.ShortcutKeyDisplayString = "Ctrl+D";
			this.menuItem_Duplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.menuItem_Duplicate.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Duplicate.Text = "Duplicate";
			this.menuItem_Duplicate.Click += new System.EventHandler(this.menuItem_Duplicate_Click);
			// 
			// menuMain_Textures
			// 
			this.menuMain_Textures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Addtexture,
            this.toolStripSeparator13,
            this.menuSub_Show});
			this.menuMain_Textures.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.menuMain_Textures.MergeIndex = 1;
			this.menuMain_Textures.Name = "menuMain_Textures";
			this.menuMain_Textures.Size = new System.Drawing.Size(63, 20);
			this.menuMain_Textures.Text = "Textures";
			// 
			// menuItem_Addtexture
			// 
			this.menuItem_Addtexture.Image = global::HLBox17b.Properties.Resources.image__plus;
			this.menuItem_Addtexture.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuItem_Addtexture.MergeIndex = 2;
			this.menuItem_Addtexture.Name = "menuItem_Addtexture";
			this.menuItem_Addtexture.Size = new System.Drawing.Size(165, 22);
			this.menuItem_Addtexture.Text = "Add New Texture";
			this.menuItem_Addtexture.Click += new System.EventHandler(this.menuItem_Addtexture_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator13.MergeIndex = 3;
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(162, 6);
			// 
			// menuSub_Show
			// 
			this.menuSub_Show.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Showimages,
            this.menuItem_Showdetails,
            this.toolStripSeparator11,
            this.menuItem_Showtransparency});
			this.menuSub_Show.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuSub_Show.MergeIndex = 4;
			this.menuSub_Show.Name = "menuSub_Show";
			this.menuSub_Show.Size = new System.Drawing.Size(165, 22);
			this.menuSub_Show.Text = "View";
			// 
			// menuItem_Showimages
			// 
			this.menuItem_Showimages.Image = global::HLBox17b.Properties.Resources.application_view_tile;
			this.menuItem_Showimages.Name = "menuItem_Showimages";
			this.menuItem_Showimages.Size = new System.Drawing.Size(145, 22);
			this.menuItem_Showimages.Text = "Images";
			this.menuItem_Showimages.Click += new System.EventHandler(this.menuItem_ViewImages_Click);
			// 
			// menuItem_Showdetails
			// 
			this.menuItem_Showdetails.Image = global::HLBox17b.Properties.Resources.application_view_detail;
			this.menuItem_Showdetails.Name = "menuItem_Showdetails";
			this.menuItem_Showdetails.Size = new System.Drawing.Size(145, 22);
			this.menuItem_Showdetails.Text = "Details";
			this.menuItem_Showdetails.Click += new System.EventHandler(this.menuItem_ViewDetails_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(142, 6);
			// 
			// menuItem_Showtransparency
			// 
			this.menuItem_Showtransparency.Name = "menuItem_Showtransparency";
			this.menuItem_Showtransparency.Size = new System.Drawing.Size(145, 22);
			this.menuItem_Showtransparency.Text = "Transparency";
			this.menuItem_Showtransparency.Click += new System.EventHandler(this.menuItem_ViewTransparency_Click);
			// 
			// sFileDlg
			// 
			this.sFileDlg.DefaultExt = "*.wad";
			// 
			// lvTexView
			// 
			this.lvTexView.AllowColumnReorder = true;
			this.lvTexView.AllowDrop = true;
			this.lvTexView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.szName,
            this.nWidth,
            this.nHeight,
            this.nSize});
			this.lvTexView.ContextMenuStrip = this.ctxtMenuChild;
			this.lvTexView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvTexView.FullRowSelect = true;
			this.lvTexView.GridLines = true;
			this.lvTexView.LabelEdit = true;
			this.lvTexView.Location = new System.Drawing.Point(0, 0);
			this.lvTexView.Name = "lvTexView";
			this.lvTexView.Size = new System.Drawing.Size(376, 316);
			this.lvTexView.TabIndex = 0;
			this.lvTexView.UseCompatibleStateImageBehavior = false;
			this.lvTexView.VirtualMode = true;
			this.lvTexView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvTexView_OnAfterLabelEdit);
			this.lvTexView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvTexView_OnColumnClick);
			this.lvTexView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvTexView_OnItemDrag);
			this.lvTexView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvTexView_OnRetrieveVirtualItem);
			this.lvTexView.SelectedIndexChanged += new System.EventHandler(this.lvTexView_OnSelectedIndexChanged);
			this.lvTexView.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvTexView_OnDragDrop);
			this.lvTexView.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvTexView_OnDragEnter);
			this.lvTexView.DragOver += new System.Windows.Forms.DragEventHandler(this.lvTexView_OnDragOver);
			this.lvTexView.DragLeave += new System.EventHandler(this.lvTexView_OnDragLeave);
			this.lvTexView.DoubleClick += new System.EventHandler(this.lvTexView_OnDoubleClick);
			this.lvTexView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvTexView_OnKeyDown);
			this.lvTexView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvTexView_OnMouseClick);
			// 
			// szName
			// 
			this.szName.Text = "szName";
			this.szName.Width = 200;
			// 
			// nWidth
			// 
			this.nWidth.Text = "Width";
			this.nWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nWidth.Width = 100;
			// 
			// nHeight
			// 
			this.nHeight.Text = "Height";
			this.nHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nHeight.Width = 100;
			// 
			// nSize
			// 
			this.nSize.Text = "Size";
			this.nSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nSize.Width = 100;
			// 
			// ctxtMenuChild
			// 
			this.ctxtMenuChild.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextItem_Edit,
            this.contextItem_Show,
            this.toolStripSeparator7,
            this.contextItem_Copy,
            this.contextItem_Paste,
            this.toolStripSeparator2,
            this.contextItem_Cut,
            this.contextItem_Delete,
            this.toolStripSeparator3,
            this.contextItem_Duplicate,
            this.toolStripSeparator4,
            this.contextItem_Export});
			this.ctxtMenuChild.Name = "contextMenuStrip1";
			this.ctxtMenuChild.Size = new System.Drawing.Size(167, 204);
			// 
			// contextItem_Edit
			// 
			this.contextItem_Edit.Image = global::HLBox17b.Properties.Resources.image__pencil;
			this.contextItem_Edit.Name = "contextItem_Edit";
			this.contextItem_Edit.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Edit.Text = "Editer";
			this.contextItem_Edit.Click += new System.EventHandler(this.menuItem_Edit_Click);
			// 
			// contextItem_Show
			// 
			this.contextItem_Show.Image = global::HLBox17b.Properties.Resources.FullScreen;
			this.contextItem_Show.Name = "contextItem_Show";
			this.contextItem_Show.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Show.Text = "Show";
			this.contextItem_Show.Click += new System.EventHandler(this.menuItem_Show_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(163, 6);
			// 
			// contextItem_Copy
			// 
			this.contextItem_Copy.Image = global::HLBox17b.Properties.Resources.CopyHS;
			this.contextItem_Copy.Name = "contextItem_Copy";
			this.contextItem_Copy.ShortcutKeyDisplayString = "Ctrl+C";
			this.contextItem_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.contextItem_Copy.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Copy.Text = "Copy";
			this.contextItem_Copy.Click += new System.EventHandler(this.menuItem_Copy_Click);
			// 
			// contextItem_Paste
			// 
			this.contextItem_Paste.Image = global::HLBox17b.Properties.Resources.PasteHS;
			this.contextItem_Paste.Name = "contextItem_Paste";
			this.contextItem_Paste.ShortcutKeyDisplayString = "Ctrl+V";
			this.contextItem_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.contextItem_Paste.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Paste.Text = "Paste";
			this.contextItem_Paste.Click += new System.EventHandler(this.menuItem_Paste_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(163, 6);
			// 
			// contextItem_Cut
			// 
			this.contextItem_Cut.Image = global::HLBox17b.Properties.Resources.CutHS;
			this.contextItem_Cut.Name = "contextItem_Cut";
			this.contextItem_Cut.ShortcutKeyDisplayString = "Ctrl+X";
			this.contextItem_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.contextItem_Cut.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Cut.Text = "Cut";
			this.contextItem_Cut.Click += new System.EventHandler(this.menuItem_Cut_Click);
			// 
			// contextItem_Delete
			// 
			this.contextItem_Delete.Image = global::HLBox17b.Properties.Resources.delete;
			this.contextItem_Delete.Name = "contextItem_Delete";
			this.contextItem_Delete.ShortcutKeyDisplayString = "Suppr";
			this.contextItem_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.contextItem_Delete.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Delete.Text = "Delete";
			this.contextItem_Delete.Click += new System.EventHandler(this.menuItem_Delete_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(163, 6);
			// 
			// contextItem_Duplicate
			// 
			this.contextItem_Duplicate.Name = "contextItem_Duplicate";
			this.contextItem_Duplicate.ShortcutKeyDisplayString = "Ctrl+D";
			this.contextItem_Duplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.contextItem_Duplicate.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Duplicate.Text = "Duplicate";
			this.contextItem_Duplicate.Click += new System.EventHandler(this.menuItem_Duplicate_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(163, 6);
			// 
			// contextItem_Export
			// 
			this.contextItem_Export.Name = "contextItem_Export";
			this.contextItem_Export.Size = new System.Drawing.Size(166, 22);
			this.contextItem_Export.Text = "Export";
			this.contextItem_Export.Click += new System.EventHandler(this.menuItem_Export_Click);
			// 
			// statBar
			// 
			this.statBar.Location = new System.Drawing.Point(0, 316);
			this.statBar.Name = "statBar";
			this.statBar.Size = new System.Drawing.Size(540, 22);
			this.statBar.SizingGrip = false;
			this.statBar.TabIndex = 1;
			this.statBar.Text = "statusBar";
			// 
			// sb_Progress
			// 
			this.sb_Progress.Name = "sb_Progress";
			this.sb_Progress.Size = new System.Drawing.Size(200, 16);
			// 
			// sb_Infos
			// 
			this.sb_Infos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.sb_Infos.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.sb_Infos.Name = "sb_Infos";
			this.sb_Infos.Size = new System.Drawing.Size(73, 17);
			this.sb_Infos.Spring = true;
			this.sb_Infos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sb_Name
			// 
			this.sb_Name.AutoSize = false;
			this.sb_Name.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.sb_Name.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.sb_Name.Name = "sb_Name";
			this.sb_Name.Size = new System.Drawing.Size(150, 17);
			this.sb_Name.Text = "Name";
			// 
			// sb_Size
			// 
			this.sb_Size.AutoSize = false;
			this.sb_Size.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.sb_Size.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.sb_Size.Name = "sb_Size";
			this.sb_Size.Size = new System.Drawing.Size(100, 17);
			this.sb_Size.Text = "Size";
			// 
			// importImgDlg
			// 
			this.importImgDlg.FileName = "openFileDialog1";
			this.importImgDlg.Multiselect = true;
			this.importImgDlg.RestoreDirectory = true;
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
			this.splitContainer.Panel1.Controls.Add(this.lbTexNames);
			this.splitContainer.Panel1MinSize = 150;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.lvTexView);
			this.splitContainer.Size = new System.Drawing.Size(540, 316);
			this.splitContainer.SplitterDistance = 160;
			this.splitContainer.TabIndex = 2;
			// 
			// lbTexNames
			// 
			this.lbTexNames.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTexNames.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbTexNames.FormattingEnabled = true;
			this.lbTexNames.HorizontalScrollbar = true;
			this.lbTexNames.ItemHeight = 15;
			this.lbTexNames.Location = new System.Drawing.Point(0, 0);
			this.lbTexNames.Name = "lbTexNames";
			this.lbTexNames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbTexNames.Size = new System.Drawing.Size(160, 316);
			this.lbTexNames.TabIndex = 0;
			this.lbTexNames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbTexNames_OnClick);
			this.lbTexNames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbTexNames_OnDoubleClick);
			// 
			// bgLoader
			// 
			this.bgLoader.WorkerReportsProgress = true;
			this.bgLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgLoader_DoWork);
			this.bgLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgLoader_ProgressChanged);
			this.bgLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgLoader_Completed);
			// 
			// bgImporter
			// 
			this.bgImporter.WorkerReportsProgress = true;
			this.bgImporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgImporter_DoWork);
			this.bgImporter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgImporter_ProgressChanged);
			this.bgImporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgImporter_Completed);
			// 
			// WadMdi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 338);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.statBar);
			this.Controls.Add(this.MainMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MainMenu;
			this.Name = "WadMdi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wadmdi_OnFormClosing);
			this.Load += new System.EventHandler(this.wadmdi_OnLoad);
			this.Shown += new System.EventHandler(this.wadmdi_OnShown);
			this.ctxtMenuChild.ResumeLayout(false);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.SaveFileDialog sFileDlg;
		private System.Windows.Forms.ToolStripMenuItem menuMain_File;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Save;
		private System.Windows.Forms.ToolStripMenuItem menuItem_SaveAs;
		private System.Windows.Forms.ToolStripMenuItem menuMain_Edit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ListView lvTexView;
		private System.Windows.Forms.StatusStrip statBar;
		private System.Windows.Forms.ContextMenuStrip ctxtMenuChild;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Copy;
		private System.Windows.Forms.ColumnHeader szName;
		private System.Windows.Forms.ColumnHeader nWidth;
		private System.Windows.Forms.ColumnHeader nHeight;
		private System.Windows.Forms.ColumnHeader nSize;
		private System.Windows.Forms.ToolStripStatusLabel sb_Name;
		private System.Windows.Forms.ToolStripStatusLabel sb_Size;
		private System.Windows.Forms.ToolStripStatusLabel sb_Infos;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Paste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Cut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Duplicate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Delete;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Copy;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Paste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Cut;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Delete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Duplicate;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Import;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Export;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Export;
		private System.Windows.Forms.OpenFileDialog importImgDlg;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ListBox lbTexNames;
		private System.ComponentModel.BackgroundWorker bgLoader;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Show;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Show;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripProgressBar sb_Progress;
		private System.Windows.Forms.ToolStripMenuItem menuMain_Textures;
		private System.Windows.Forms.ToolStripMenuItem menuSub_Show;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Showimages;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Showdetails;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Showtransparency;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Addtexture;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.ComponentModel.BackgroundWorker bgImporter;
		}
	}