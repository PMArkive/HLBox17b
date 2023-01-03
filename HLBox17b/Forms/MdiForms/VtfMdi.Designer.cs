namespace HLBox17b.Forms.MdiForms
	{
	partial class VtfMdi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VtfMdi));
			this.ctxtMenuChild = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.contextItem_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.contextItem_View = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxitem_ViewNormal = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxitem_ViewTiled = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxitem_ViewAnimated = new System.Windows.Forms.ToolStripMenuItem();
			this.vtfImagesList = new System.Windows.Forms.ImageList(this.components);
			this.iconListe = new System.Windows.Forms.ImageList(this.components);
			this.bgLoader = new System.ComponentModel.BackgroundWorker();
			this.timerAni = new System.Windows.Forms.Timer(this.components);
			this.bgAnimator = new System.ComponentModel.BackgroundWorker();
			this.bgBrowser = new System.ComponentModel.BackgroundWorker();
			this.splitContainer0 = new System.Windows.Forms.SplitContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.gbZoom = new System.Windows.Forms.GroupBox();
			this.trackbar_Zoom = new System.Windows.Forms.TrackBar();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabImage = new System.Windows.Forms.TabPage();
			this.gbAnimation = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.trackBar_Frame = new System.Windows.Forms.TrackBar();
			this.btn_PlayStopAnim = new System.Windows.Forms.Button();
			this.lbl_Speed = new System.Windows.Forms.Label();
			this.dt_Speed = new System.Windows.Forms.NumericUpDown();
			this.gbSelectImage = new System.Windows.Forms.GroupBox();
			this.lbl_SliceLimits = new System.Windows.Forms.Label();
			this.lbl_FaceLimits = new System.Windows.Forms.Label();
			this.lbl_MipmapsLimits = new System.Windows.Forms.Label();
			this.lbl_FrameLimits = new System.Windows.Forms.Label();
			this.lbl_ActSlice = new System.Windows.Forms.Label();
			this.dt_ActSlice = new System.Windows.Forms.NumericUpDown();
			this.dt_ActFrame = new System.Windows.Forms.NumericUpDown();
			this.lbl_ActFrame = new System.Windows.Forms.Label();
			this.lbl_ActFace = new System.Windows.Forms.Label();
			this.dt_ActFace = new System.Windows.Forms.NumericUpDown();
			this.lbl_ActMipmap = new System.Windows.Forms.Label();
			this.dt_ActMipmap = new System.Windows.Forms.NumericUpDown();
			this.tabInfos = new System.Windows.Forms.TabPage();
			this.gbLowRes = new System.Windows.Forms.GroupBox();
			this.dt_FormatLow = new System.Windows.Forms.Label();
			this.lbl_FormatLow = new System.Windows.Forms.Label();
			this.dt_DimsLow = new System.Windows.Forms.Label();
			this.lbl_DimsLow = new System.Windows.Forms.Label();
			this.gbHighRes = new System.Windows.Forms.GroupBox();
			this.dt_FormatHigh = new System.Windows.Forms.Label();
			this.lbl_FormatHigh = new System.Windows.Forms.Label();
			this.dt_DimsHigh = new System.Windows.Forms.Label();
			this.lbl_DimsHigh = new System.Windows.Forms.Label();
			this.gbLayout = new System.Windows.Forms.GroupBox();
			this.dt_Bpp = new System.Windows.Forms.Label();
			this.lbl_Bpp = new System.Windows.Forms.Label();
			this.dt_Depth = new System.Windows.Forms.Label();
			this.lbl_Depth = new System.Windows.Forms.Label();
			this.dt_Startframe = new System.Windows.Forms.Label();
			this.dt_Reflectivity = new System.Windows.Forms.Label();
			this.lbl_Reflectivity = new System.Windows.Forms.Label();
			this.lbl_Startframe = new System.Windows.Forms.Label();
			this.dt_Bumpmap = new System.Windows.Forms.Label();
			this.lbl_Bumpmap = new System.Windows.Forms.Label();
			this.dt_Mipmaps = new System.Windows.Forms.Label();
			this.lbl_Mipmaps = new System.Windows.Forms.Label();
			this.dt_Slices = new System.Windows.Forms.Label();
			this.lbl_Slices = new System.Windows.Forms.Label();
			this.dt_Faces = new System.Windows.Forms.Label();
			this.lbl_Faces = new System.Windows.Forms.Label();
			this.dt_Frames = new System.Windows.Forms.Label();
			this.lbl_Frames = new System.Windows.Forms.Label();
			this.gbGeneral = new System.Windows.Forms.GroupBox();
			this.dt_Filesize = new System.Windows.Forms.Label();
			this.lbl_Filesize = new System.Windows.Forms.Label();
			this.dt_Version = new System.Windows.Forms.Label();
			this.lbl_Version = new System.Windows.Forms.Label();
			this.tabFlags = new System.Windows.Forms.TabPage();
			this.listbox_Flags = new System.Windows.Forms.CheckedListBox();
			this.tabBrowse = new System.Windows.Forms.TabPage();
			this.dirsTreeView = new System.Windows.Forms.TreeView();
			this.statBar = new System.Windows.Forms.StatusStrip();
			this.sb_Progress = new System.Windows.Forms.ToolStripProgressBar();
			this.sb_Infos = new System.Windows.Forms.ToolStripStatusLabel();
			this.sb_Zoom = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.menuMain_File = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMain_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMain_Textures = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItem_View = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_ViewNormal = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_ViewTiled = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_ViewAnimated = new System.Windows.Forms.ToolStripMenuItem();
			this.tabContent = new HLBox17b.Externals.TablessControl();
			this.tabPicture = new System.Windows.Forms.TabPage();
			this.picturePanel = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.tabVmt = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.tabViewBrowse = new System.Windows.Forms.TabPage();
			this.lvw_Browse = new System.Windows.Forms.ListView();
			this.ctxtMenuChild.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer0)).BeginInit();
			this.splitContainer0.Panel1.SuspendLayout();
			this.splitContainer0.Panel2.SuspendLayout();
			this.splitContainer0.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.gbZoom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackbar_Zoom)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabImage.SuspendLayout();
			this.gbAnimation.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_Frame)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_Speed)).BeginInit();
			this.gbSelectImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActSlice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActFrame)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActFace)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActMipmap)).BeginInit();
			this.tabInfos.SuspendLayout();
			this.gbLowRes.SuspendLayout();
			this.gbHighRes.SuspendLayout();
			this.gbLayout.SuspendLayout();
			this.gbGeneral.SuspendLayout();
			this.tabFlags.SuspendLayout();
			this.tabBrowse.SuspendLayout();
			this.statBar.SuspendLayout();
			this.MainMenu.SuspendLayout();
			this.tabContent.SuspendLayout();
			this.tabPicture.SuspendLayout();
			this.picturePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.tabVmt.SuspendLayout();
			this.tabViewBrowse.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxtMenuChild
			// 
			this.ctxtMenuChild.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextItem_Copy,
            this.contextItem_Export,
            this.toolStripSeparator2,
            this.contextItem_View});
			this.ctxtMenuChild.Name = "contextMenuStrip1";
			this.ctxtMenuChild.Size = new System.Drawing.Size(145, 76);
			// 
			// contextItem_Copy
			// 
			this.contextItem_Copy.Image = global::HLBox17b.Properties.Resources.CopyHS;
			this.contextItem_Copy.Name = "contextItem_Copy";
			this.contextItem_Copy.ShortcutKeyDisplayString = "Ctrl+C";
			this.contextItem_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.contextItem_Copy.Size = new System.Drawing.Size(144, 22);
			this.contextItem_Copy.Text = "Copy";
			this.contextItem_Copy.Click += new System.EventHandler(this.menuItem_Copy_Click);
			// 
			// contextItem_Export
			// 
			this.contextItem_Export.Name = "contextItem_Export";
			this.contextItem_Export.Size = new System.Drawing.Size(144, 22);
			this.contextItem_Export.Text = "Export";
			this.contextItem_Export.Click += new System.EventHandler(this.menuItem_Export_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(141, 6);
			// 
			// contextItem_View
			// 
			this.contextItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxitem_ViewNormal,
            this.ctxitem_ViewTiled,
            this.ctxitem_ViewAnimated});
			this.contextItem_View.Name = "contextItem_View";
			this.contextItem_View.Size = new System.Drawing.Size(144, 22);
			this.contextItem_View.Text = "View";
			// 
			// ctxitem_ViewNormal
			// 
			this.ctxitem_ViewNormal.Checked = true;
			this.ctxitem_ViewNormal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctxitem_ViewNormal.Image = global::HLBox17b.Properties.Resources.layout;
			this.ctxitem_ViewNormal.Name = "ctxitem_ViewNormal";
			this.ctxitem_ViewNormal.Size = new System.Drawing.Size(126, 22);
			this.ctxitem_ViewNormal.Text = "Normal";
			this.ctxitem_ViewNormal.Click += new System.EventHandler(this.menuitem_ClickViewNormal);
			// 
			// ctxitem_ViewTiled
			// 
			this.ctxitem_ViewTiled.Image = global::HLBox17b.Properties.Resources.layout_4;
			this.ctxitem_ViewTiled.Name = "ctxitem_ViewTiled";
			this.ctxitem_ViewTiled.Size = new System.Drawing.Size(126, 22);
			this.ctxitem_ViewTiled.Text = "Tiled";
			this.ctxitem_ViewTiled.Click += new System.EventHandler(this.menuitem_ClickViewTiled);
			// 
			// ctxitem_ViewAnimated
			// 
			this.ctxitem_ViewAnimated.Image = global::HLBox17b.Properties.Resources.film;
			this.ctxitem_ViewAnimated.Name = "ctxitem_ViewAnimated";
			this.ctxitem_ViewAnimated.Size = new System.Drawing.Size(126, 22);
			this.ctxitem_ViewAnimated.Text = "Animated";
			this.ctxitem_ViewAnimated.Click += new System.EventHandler(this.menuitem_ClickViewAnimated);
			// 
			// vtfImagesList
			// 
			this.vtfImagesList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.vtfImagesList.ImageSize = new System.Drawing.Size(128, 128);
			this.vtfImagesList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// iconListe
			// 
			this.iconListe.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.iconListe.ImageSize = new System.Drawing.Size(16, 16);
			this.iconListe.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// bgLoader
			// 
			this.bgLoader.WorkerReportsProgress = true;
			this.bgLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgLoader_DoWork);
			this.bgLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgLoader_ProgressChanged);
			this.bgLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgLoader_Completed);
			// 
			// timerAni
			// 
			this.timerAni.Tick += new System.EventHandler(this.timerAni_Tick);
			// 
			// bgAnimator
			// 
			this.bgAnimator.WorkerSupportsCancellation = true;
			this.bgAnimator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bganim_DoWork);
			this.bgAnimator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bganim_Completed);
			// 
			// bgBrowser
			// 
			this.bgBrowser.WorkerReportsProgress = true;
			this.bgBrowser.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgbrowser_DoWork);
			this.bgBrowser.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgbrowser_ProgressChanged);
			this.bgBrowser.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgbrowser_WorkerCompleted);
			// 
			// splitContainer0
			// 
			this.splitContainer0.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer0.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer0.Location = new System.Drawing.Point(0, 0);
			this.splitContainer0.Name = "splitContainer0";
			// 
			// splitContainer0.Panel1
			// 
			this.splitContainer0.Panel1.Controls.Add(this.splitContainer1);
			this.splitContainer0.Panel1MinSize = 200;
			// 
			// splitContainer0.Panel2
			// 
			this.splitContainer0.Panel2.Controls.Add(this.tabContent);
			this.splitContainer0.Size = new System.Drawing.Size(540, 449);
			this.splitContainer0.SplitterDistance = 280;
			this.splitContainer0.TabIndex = 2;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gbZoom);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl);
			this.splitContainer1.Size = new System.Drawing.Size(280, 449);
			this.splitContainer1.SplitterDistance = 60;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 0;
			// 
			// gbZoom
			// 
			this.gbZoom.Controls.Add(this.trackbar_Zoom);
			this.gbZoom.Location = new System.Drawing.Point(10, 2);
			this.gbZoom.Name = "gbZoom";
			this.gbZoom.Size = new System.Drawing.Size(235, 55);
			this.gbZoom.TabIndex = 4;
			this.gbZoom.TabStop = false;
			this.gbZoom.Text = "Zoom";
			// 
			// trackbar_Zoom
			// 
			this.trackbar_Zoom.AutoSize = false;
			this.trackbar_Zoom.Location = new System.Drawing.Point(10, 15);
			this.trackbar_Zoom.Name = "trackbar_Zoom";
			this.trackbar_Zoom.Size = new System.Drawing.Size(220, 35);
			this.trackbar_Zoom.TabIndex = 0;
			this.trackbar_Zoom.ValueChanged += new System.EventHandler(this.trackbarzoom_ValueChanged);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabImage);
			this.tabControl.Controls.Add(this.tabInfos);
			this.tabControl.Controls.Add(this.tabFlags);
			this.tabControl.Controls.Add(this.tabBrowse);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(280, 388);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.maintabs_ChangeTab);
			// 
			// tabImage
			// 
			this.tabImage.Controls.Add(this.gbAnimation);
			this.tabImage.Controls.Add(this.gbSelectImage);
			this.tabImage.Location = new System.Drawing.Point(4, 22);
			this.tabImage.Name = "tabImage";
			this.tabImage.Padding = new System.Windows.Forms.Padding(3);
			this.tabImage.Size = new System.Drawing.Size(272, 362);
			this.tabImage.TabIndex = 0;
			this.tabImage.Text = "Image";
			this.tabImage.UseVisualStyleBackColor = true;
			// 
			// gbAnimation
			// 
			this.gbAnimation.Controls.Add(this.panel2);
			this.gbAnimation.Controls.Add(this.btn_PlayStopAnim);
			this.gbAnimation.Controls.Add(this.lbl_Speed);
			this.gbAnimation.Controls.Add(this.dt_Speed);
			this.gbAnimation.Location = new System.Drawing.Point(10, 165);
			this.gbAnimation.Name = "gbAnimation";
			this.gbAnimation.Size = new System.Drawing.Size(235, 105);
			this.gbAnimation.TabIndex = 3;
			this.gbAnimation.TabStop = false;
			this.gbAnimation.Text = "Animation";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Control;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.trackBar_Frame);
			this.panel2.Location = new System.Drawing.Point(10, 55);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(150, 40);
			this.panel2.TabIndex = 2;
			// 
			// trackBar_Frame
			// 
			this.trackBar_Frame.AutoSize = false;
			this.trackBar_Frame.Location = new System.Drawing.Point(1, 3);
			this.trackBar_Frame.Name = "trackBar_Frame";
			this.trackBar_Frame.Size = new System.Drawing.Size(144, 35);
			this.trackBar_Frame.TabIndex = 1;
			this.trackBar_Frame.ValueChanged += new System.EventHandler(this.trackbarframe_ValueChanged);
			// 
			// btn_PlayStopAnim
			// 
			this.btn_PlayStopAnim.Image = global::HLBox17b.Properties.Resources.control;
			this.btn_PlayStopAnim.Location = new System.Drawing.Point(190, 60);
			this.btn_PlayStopAnim.Name = "btn_PlayStopAnim";
			this.btn_PlayStopAnim.Size = new System.Drawing.Size(35, 30);
			this.btn_PlayStopAnim.TabIndex = 10;
			this.btn_PlayStopAnim.UseVisualStyleBackColor = true;
			this.btn_PlayStopAnim.Click += new System.EventHandler(this.btnplaystop_Click);
			// 
			// lbl_Speed
			// 
			this.lbl_Speed.Location = new System.Drawing.Point(10, 25);
			this.lbl_Speed.Name = "lbl_Speed";
			this.lbl_Speed.Size = new System.Drawing.Size(110, 20);
			this.lbl_Speed.TabIndex = 9;
			this.lbl_Speed.Text = "Speed";
			this.lbl_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Speed
			// 
			this.dt_Speed.Location = new System.Drawing.Point(165, 25);
			this.dt_Speed.Name = "dt_Speed";
			this.dt_Speed.Size = new System.Drawing.Size(60, 20);
			this.dt_Speed.TabIndex = 8;
			this.dt_Speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dt_Speed.ValueChanged += new System.EventHandler(this.dtspeed_ChangeAnimationSpeed);
			// 
			// gbSelectImage
			// 
			this.gbSelectImage.Controls.Add(this.lbl_SliceLimits);
			this.gbSelectImage.Controls.Add(this.lbl_FaceLimits);
			this.gbSelectImage.Controls.Add(this.lbl_MipmapsLimits);
			this.gbSelectImage.Controls.Add(this.lbl_FrameLimits);
			this.gbSelectImage.Controls.Add(this.lbl_ActSlice);
			this.gbSelectImage.Controls.Add(this.dt_ActSlice);
			this.gbSelectImage.Controls.Add(this.dt_ActFrame);
			this.gbSelectImage.Controls.Add(this.lbl_ActFrame);
			this.gbSelectImage.Controls.Add(this.lbl_ActFace);
			this.gbSelectImage.Controls.Add(this.dt_ActFace);
			this.gbSelectImage.Controls.Add(this.lbl_ActMipmap);
			this.gbSelectImage.Controls.Add(this.dt_ActMipmap);
			this.gbSelectImage.Location = new System.Drawing.Point(10, 10);
			this.gbSelectImage.Name = "gbSelectImage";
			this.gbSelectImage.Size = new System.Drawing.Size(235, 145);
			this.gbSelectImage.TabIndex = 2;
			this.gbSelectImage.TabStop = false;
			this.gbSelectImage.Text = "Select Image";
			// 
			// lbl_SliceLimits
			// 
			this.lbl_SliceLimits.Location = new System.Drawing.Point(130, 115);
			this.lbl_SliceLimits.Name = "lbl_SliceLimits";
			this.lbl_SliceLimits.Size = new System.Drawing.Size(35, 20);
			this.lbl_SliceLimits.TabIndex = 11;
			this.lbl_SliceLimits.Text = "0-12";
			this.lbl_SliceLimits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_FaceLimits
			// 
			this.lbl_FaceLimits.Location = new System.Drawing.Point(130, 85);
			this.lbl_FaceLimits.Name = "lbl_FaceLimits";
			this.lbl_FaceLimits.Size = new System.Drawing.Size(35, 20);
			this.lbl_FaceLimits.TabIndex = 10;
			this.lbl_FaceLimits.Text = "0-12";
			this.lbl_FaceLimits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_MipmapsLimits
			// 
			this.lbl_MipmapsLimits.Location = new System.Drawing.Point(130, 55);
			this.lbl_MipmapsLimits.Name = "lbl_MipmapsLimits";
			this.lbl_MipmapsLimits.Size = new System.Drawing.Size(35, 20);
			this.lbl_MipmapsLimits.TabIndex = 9;
			this.lbl_MipmapsLimits.Text = "0-12";
			this.lbl_MipmapsLimits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_FrameLimits
			// 
			this.lbl_FrameLimits.Location = new System.Drawing.Point(130, 25);
			this.lbl_FrameLimits.Name = "lbl_FrameLimits";
			this.lbl_FrameLimits.Size = new System.Drawing.Size(35, 20);
			this.lbl_FrameLimits.TabIndex = 8;
			this.lbl_FrameLimits.Text = "0-12";
			this.lbl_FrameLimits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_ActSlice
			// 
			this.lbl_ActSlice.Location = new System.Drawing.Point(10, 115);
			this.lbl_ActSlice.Name = "lbl_ActSlice";
			this.lbl_ActSlice.Size = new System.Drawing.Size(110, 20);
			this.lbl_ActSlice.TabIndex = 7;
			this.lbl_ActSlice.Text = "Slice";
			this.lbl_ActSlice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_ActSlice
			// 
			this.dt_ActSlice.Location = new System.Drawing.Point(165, 115);
			this.dt_ActSlice.Name = "dt_ActSlice";
			this.dt_ActSlice.Size = new System.Drawing.Size(60, 20);
			this.dt_ActSlice.TabIndex = 6;
			this.dt_ActSlice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dt_ActSlice.ValueChanged += new System.EventHandler(this.vtfmdi_actslice_ValueChanged);
			// 
			// dt_ActFrame
			// 
			this.dt_ActFrame.Location = new System.Drawing.Point(165, 25);
			this.dt_ActFrame.Name = "dt_ActFrame";
			this.dt_ActFrame.Size = new System.Drawing.Size(60, 20);
			this.dt_ActFrame.TabIndex = 0;
			this.dt_ActFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dt_ActFrame.ValueChanged += new System.EventHandler(this.vtfmdi_actframe_ValueChanged);
			// 
			// lbl_ActFrame
			// 
			this.lbl_ActFrame.Location = new System.Drawing.Point(10, 25);
			this.lbl_ActFrame.Name = "lbl_ActFrame";
			this.lbl_ActFrame.Size = new System.Drawing.Size(100, 20);
			this.lbl_ActFrame.TabIndex = 1;
			this.lbl_ActFrame.Text = "Frame";
			this.lbl_ActFrame.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_ActFace
			// 
			this.lbl_ActFace.Location = new System.Drawing.Point(10, 85);
			this.lbl_ActFace.Name = "lbl_ActFace";
			this.lbl_ActFace.Size = new System.Drawing.Size(110, 20);
			this.lbl_ActFace.TabIndex = 5;
			this.lbl_ActFace.Text = "Face";
			this.lbl_ActFace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_ActFace
			// 
			this.dt_ActFace.Location = new System.Drawing.Point(165, 85);
			this.dt_ActFace.Name = "dt_ActFace";
			this.dt_ActFace.Size = new System.Drawing.Size(60, 20);
			this.dt_ActFace.TabIndex = 4;
			this.dt_ActFace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dt_ActFace.ValueChanged += new System.EventHandler(this.vtfmdi_actface_ValueChanged);
			// 
			// lbl_ActMipmap
			// 
			this.lbl_ActMipmap.Location = new System.Drawing.Point(10, 55);
			this.lbl_ActMipmap.Name = "lbl_ActMipmap";
			this.lbl_ActMipmap.Size = new System.Drawing.Size(110, 20);
			this.lbl_ActMipmap.TabIndex = 3;
			this.lbl_ActMipmap.Text = "MipMap";
			this.lbl_ActMipmap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_ActMipmap
			// 
			this.dt_ActMipmap.Location = new System.Drawing.Point(165, 55);
			this.dt_ActMipmap.Name = "dt_ActMipmap";
			this.dt_ActMipmap.Size = new System.Drawing.Size(60, 20);
			this.dt_ActMipmap.TabIndex = 2;
			this.dt_ActMipmap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dt_ActMipmap.ValueChanged += new System.EventHandler(this.vtfmdi_actmipmap_ValueChanged);
			// 
			// tabInfos
			// 
			this.tabInfos.AutoScroll = true;
			this.tabInfos.Controls.Add(this.gbLowRes);
			this.tabInfos.Controls.Add(this.gbHighRes);
			this.tabInfos.Controls.Add(this.gbLayout);
			this.tabInfos.Controls.Add(this.gbGeneral);
			this.tabInfos.Location = new System.Drawing.Point(4, 22);
			this.tabInfos.Name = "tabInfos";
			this.tabInfos.Padding = new System.Windows.Forms.Padding(3);
			this.tabInfos.Size = new System.Drawing.Size(272, 358);
			this.tabInfos.TabIndex = 1;
			this.tabInfos.Text = "Infos";
			this.tabInfos.UseVisualStyleBackColor = true;
			// 
			// gbLowRes
			// 
			this.gbLowRes.Controls.Add(this.dt_FormatLow);
			this.gbLowRes.Controls.Add(this.lbl_FormatLow);
			this.gbLowRes.Controls.Add(this.dt_DimsLow);
			this.gbLowRes.Controls.Add(this.lbl_DimsLow);
			this.gbLowRes.Location = new System.Drawing.Point(10, 345);
			this.gbLowRes.Name = "gbLowRes";
			this.gbLowRes.Size = new System.Drawing.Size(235, 62);
			this.gbLowRes.TabIndex = 5;
			this.gbLowRes.TabStop = false;
			this.gbLowRes.Text = "LowRes";
			// 
			// dt_FormatLow
			// 
			this.dt_FormatLow.Location = new System.Drawing.Point(125, 40);
			this.dt_FormatLow.Name = "dt_FormatLow";
			this.dt_FormatLow.Size = new System.Drawing.Size(105, 15);
			this.dt_FormatLow.TabIndex = 3;
			this.dt_FormatLow.Text = "10 (DXT5)";
			this.dt_FormatLow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_FormatLow
			// 
			this.lbl_FormatLow.Location = new System.Drawing.Point(5, 40);
			this.lbl_FormatLow.Name = "lbl_FormatLow";
			this.lbl_FormatLow.Size = new System.Drawing.Size(105, 15);
			this.lbl_FormatLow.TabIndex = 2;
			this.lbl_FormatLow.Text = "Format";
			this.lbl_FormatLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_DimsLow
			// 
			this.dt_DimsLow.Location = new System.Drawing.Point(140, 20);
			this.dt_DimsLow.Name = "dt_DimsLow";
			this.dt_DimsLow.Size = new System.Drawing.Size(90, 15);
			this.dt_DimsLow.TabIndex = 1;
			this.dt_DimsLow.Text = "16 x 16";
			this.dt_DimsLow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_DimsLow
			// 
			this.lbl_DimsLow.Location = new System.Drawing.Point(5, 20);
			this.lbl_DimsLow.Name = "lbl_DimsLow";
			this.lbl_DimsLow.Size = new System.Drawing.Size(105, 15);
			this.lbl_DimsLow.TabIndex = 0;
			this.lbl_DimsLow.Text = "Dimensions";
			this.lbl_DimsLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gbHighRes
			// 
			this.gbHighRes.Controls.Add(this.dt_FormatHigh);
			this.gbHighRes.Controls.Add(this.lbl_FormatHigh);
			this.gbHighRes.Controls.Add(this.dt_DimsHigh);
			this.gbHighRes.Controls.Add(this.lbl_DimsHigh);
			this.gbHighRes.Location = new System.Drawing.Point(10, 280);
			this.gbHighRes.Name = "gbHighRes";
			this.gbHighRes.Size = new System.Drawing.Size(235, 62);
			this.gbHighRes.TabIndex = 4;
			this.gbHighRes.TabStop = false;
			this.gbHighRes.Text = "HighRes ";
			// 
			// dt_FormatHigh
			// 
			this.dt_FormatHigh.Location = new System.Drawing.Point(125, 40);
			this.dt_FormatHigh.Name = "dt_FormatHigh";
			this.dt_FormatHigh.Size = new System.Drawing.Size(105, 15);
			this.dt_FormatHigh.TabIndex = 3;
			this.dt_FormatHigh.Text = "10 (DXT5)";
			this.dt_FormatHigh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_FormatHigh
			// 
			this.lbl_FormatHigh.Location = new System.Drawing.Point(5, 40);
			this.lbl_FormatHigh.Name = "lbl_FormatHigh";
			this.lbl_FormatHigh.Size = new System.Drawing.Size(105, 15);
			this.lbl_FormatHigh.TabIndex = 2;
			this.lbl_FormatHigh.Text = "Format";
			this.lbl_FormatHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_DimsHigh
			// 
			this.dt_DimsHigh.Location = new System.Drawing.Point(140, 20);
			this.dt_DimsHigh.Name = "dt_DimsHigh";
			this.dt_DimsHigh.Size = new System.Drawing.Size(90, 15);
			this.dt_DimsHigh.TabIndex = 1;
			this.dt_DimsHigh.Text = "2048 x 2048";
			this.dt_DimsHigh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_DimsHigh
			// 
			this.lbl_DimsHigh.Location = new System.Drawing.Point(5, 20);
			this.lbl_DimsHigh.Name = "lbl_DimsHigh";
			this.lbl_DimsHigh.Size = new System.Drawing.Size(105, 15);
			this.lbl_DimsHigh.TabIndex = 0;
			this.lbl_DimsHigh.Text = "Dimensions";
			this.lbl_DimsHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gbLayout
			// 
			this.gbLayout.Controls.Add(this.dt_Bpp);
			this.gbLayout.Controls.Add(this.lbl_Bpp);
			this.gbLayout.Controls.Add(this.dt_Depth);
			this.gbLayout.Controls.Add(this.lbl_Depth);
			this.gbLayout.Controls.Add(this.dt_Startframe);
			this.gbLayout.Controls.Add(this.dt_Reflectivity);
			this.gbLayout.Controls.Add(this.lbl_Reflectivity);
			this.gbLayout.Controls.Add(this.lbl_Startframe);
			this.gbLayout.Controls.Add(this.dt_Bumpmap);
			this.gbLayout.Controls.Add(this.lbl_Bumpmap);
			this.gbLayout.Controls.Add(this.dt_Mipmaps);
			this.gbLayout.Controls.Add(this.lbl_Mipmaps);
			this.gbLayout.Controls.Add(this.dt_Slices);
			this.gbLayout.Controls.Add(this.lbl_Slices);
			this.gbLayout.Controls.Add(this.dt_Faces);
			this.gbLayout.Controls.Add(this.lbl_Faces);
			this.gbLayout.Controls.Add(this.dt_Frames);
			this.gbLayout.Controls.Add(this.lbl_Frames);
			this.gbLayout.Location = new System.Drawing.Point(10, 75);
			this.gbLayout.Name = "gbLayout";
			this.gbLayout.Size = new System.Drawing.Size(235, 200);
			this.gbLayout.TabIndex = 4;
			this.gbLayout.TabStop = false;
			this.gbLayout.Text = "Layout";
			// 
			// dt_Bpp
			// 
			this.dt_Bpp.Location = new System.Drawing.Point(175, 180);
			this.dt_Bpp.Name = "dt_Bpp";
			this.dt_Bpp.Size = new System.Drawing.Size(55, 15);
			this.dt_Bpp.TabIndex = 17;
			this.dt_Bpp.Text = "1";
			this.dt_Bpp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Bpp
			// 
			this.lbl_Bpp.Location = new System.Drawing.Point(5, 180);
			this.lbl_Bpp.Name = "lbl_Bpp";
			this.lbl_Bpp.Size = new System.Drawing.Size(105, 15);
			this.lbl_Bpp.TabIndex = 16;
			this.lbl_Bpp.Text = "Bytes Per Pixel";
			this.lbl_Bpp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Depth
			// 
			this.dt_Depth.Location = new System.Drawing.Point(175, 160);
			this.dt_Depth.Name = "dt_Depth";
			this.dt_Depth.Size = new System.Drawing.Size(55, 15);
			this.dt_Depth.TabIndex = 15;
			this.dt_Depth.Text = "1";
			this.dt_Depth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Depth
			// 
			this.lbl_Depth.Location = new System.Drawing.Point(5, 160);
			this.lbl_Depth.Name = "lbl_Depth";
			this.lbl_Depth.Size = new System.Drawing.Size(105, 15);
			this.lbl_Depth.TabIndex = 14;
			this.lbl_Depth.Text = "Depth";
			this.lbl_Depth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Startframe
			// 
			this.dt_Startframe.Location = new System.Drawing.Point(180, 40);
			this.dt_Startframe.Name = "dt_Startframe";
			this.dt_Startframe.Size = new System.Drawing.Size(50, 15);
			this.dt_Startframe.TabIndex = 13;
			this.dt_Startframe.Text = "0";
			this.dt_Startframe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dt_Reflectivity
			// 
			this.dt_Reflectivity.Location = new System.Drawing.Point(125, 140);
			this.dt_Reflectivity.Name = "dt_Reflectivity";
			this.dt_Reflectivity.Size = new System.Drawing.Size(105, 15);
			this.dt_Reflectivity.TabIndex = 11;
			this.dt_Reflectivity.Text = "0.125/0.303/0.147";
			this.dt_Reflectivity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Reflectivity
			// 
			this.lbl_Reflectivity.Location = new System.Drawing.Point(5, 140);
			this.lbl_Reflectivity.Name = "lbl_Reflectivity";
			this.lbl_Reflectivity.Size = new System.Drawing.Size(105, 15);
			this.lbl_Reflectivity.TabIndex = 10;
			this.lbl_Reflectivity.Text = "Reflectivity";
			this.lbl_Reflectivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_Startframe
			// 
			this.lbl_Startframe.Location = new System.Drawing.Point(5, 40);
			this.lbl_Startframe.Name = "lbl_Startframe";
			this.lbl_Startframe.Size = new System.Drawing.Size(105, 15);
			this.lbl_Startframe.TabIndex = 12;
			this.lbl_Startframe.Text = "Start Frame";
			this.lbl_Startframe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Bumpmap
			// 
			this.dt_Bumpmap.Location = new System.Drawing.Point(175, 120);
			this.dt_Bumpmap.Name = "dt_Bumpmap";
			this.dt_Bumpmap.Size = new System.Drawing.Size(55, 15);
			this.dt_Bumpmap.TabIndex = 9;
			this.dt_Bumpmap.Text = "1";
			this.dt_Bumpmap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Bumpmap
			// 
			this.lbl_Bumpmap.Location = new System.Drawing.Point(5, 120);
			this.lbl_Bumpmap.Name = "lbl_Bumpmap";
			this.lbl_Bumpmap.Size = new System.Drawing.Size(105, 15);
			this.lbl_Bumpmap.TabIndex = 8;
			this.lbl_Bumpmap.Text = "Bumpmap";
			this.lbl_Bumpmap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Mipmaps
			// 
			this.dt_Mipmaps.Location = new System.Drawing.Point(175, 100);
			this.dt_Mipmaps.Name = "dt_Mipmaps";
			this.dt_Mipmaps.Size = new System.Drawing.Size(55, 15);
			this.dt_Mipmaps.TabIndex = 7;
			this.dt_Mipmaps.Text = "1";
			this.dt_Mipmaps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Mipmaps
			// 
			this.lbl_Mipmaps.Location = new System.Drawing.Point(5, 100);
			this.lbl_Mipmaps.Name = "lbl_Mipmaps";
			this.lbl_Mipmaps.Size = new System.Drawing.Size(105, 15);
			this.lbl_Mipmaps.TabIndex = 6;
			this.lbl_Mipmaps.Text = "Mipmaps";
			this.lbl_Mipmaps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Slices
			// 
			this.dt_Slices.Location = new System.Drawing.Point(175, 80);
			this.dt_Slices.Name = "dt_Slices";
			this.dt_Slices.Size = new System.Drawing.Size(55, 15);
			this.dt_Slices.TabIndex = 5;
			this.dt_Slices.Text = "1";
			this.dt_Slices.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Slices
			// 
			this.lbl_Slices.Location = new System.Drawing.Point(5, 80);
			this.lbl_Slices.Name = "lbl_Slices";
			this.lbl_Slices.Size = new System.Drawing.Size(105, 15);
			this.lbl_Slices.TabIndex = 4;
			this.lbl_Slices.Text = "Z Slices";
			this.lbl_Slices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Faces
			// 
			this.dt_Faces.Location = new System.Drawing.Point(175, 60);
			this.dt_Faces.Name = "dt_Faces";
			this.dt_Faces.Size = new System.Drawing.Size(55, 15);
			this.dt_Faces.TabIndex = 3;
			this.dt_Faces.Text = "1";
			this.dt_Faces.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Faces
			// 
			this.lbl_Faces.Location = new System.Drawing.Point(5, 60);
			this.lbl_Faces.Name = "lbl_Faces";
			this.lbl_Faces.Size = new System.Drawing.Size(105, 15);
			this.lbl_Faces.TabIndex = 2;
			this.lbl_Faces.Text = "Faces";
			this.lbl_Faces.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Frames
			// 
			this.dt_Frames.Location = new System.Drawing.Point(180, 20);
			this.dt_Frames.Name = "dt_Frames";
			this.dt_Frames.Size = new System.Drawing.Size(50, 15);
			this.dt_Frames.TabIndex = 1;
			this.dt_Frames.Text = "10";
			this.dt_Frames.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Frames
			// 
			this.lbl_Frames.Location = new System.Drawing.Point(5, 20);
			this.lbl_Frames.Name = "lbl_Frames";
			this.lbl_Frames.Size = new System.Drawing.Size(105, 15);
			this.lbl_Frames.TabIndex = 0;
			this.lbl_Frames.Text = "Frames";
			this.lbl_Frames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gbGeneral
			// 
			this.gbGeneral.Controls.Add(this.dt_Filesize);
			this.gbGeneral.Controls.Add(this.lbl_Filesize);
			this.gbGeneral.Controls.Add(this.dt_Version);
			this.gbGeneral.Controls.Add(this.lbl_Version);
			this.gbGeneral.Location = new System.Drawing.Point(10, 10);
			this.gbGeneral.Name = "gbGeneral";
			this.gbGeneral.Size = new System.Drawing.Size(235, 62);
			this.gbGeneral.TabIndex = 2;
			this.gbGeneral.TabStop = false;
			this.gbGeneral.Text = "General";
			// 
			// dt_Filesize
			// 
			this.dt_Filesize.Location = new System.Drawing.Point(125, 40);
			this.dt_Filesize.Name = "dt_Filesize";
			this.dt_Filesize.Size = new System.Drawing.Size(105, 15);
			this.dt_Filesize.TabIndex = 3;
			this.dt_Filesize.Text = "2 930 Kb";
			this.dt_Filesize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Filesize
			// 
			this.lbl_Filesize.Location = new System.Drawing.Point(5, 40);
			this.lbl_Filesize.Name = "lbl_Filesize";
			this.lbl_Filesize.Size = new System.Drawing.Size(105, 15);
			this.lbl_Filesize.TabIndex = 2;
			this.lbl_Filesize.Text = "File Size";
			this.lbl_Filesize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dt_Version
			// 
			this.dt_Version.Location = new System.Drawing.Point(180, 20);
			this.dt_Version.Name = "dt_Version";
			this.dt_Version.Size = new System.Drawing.Size(50, 15);
			this.dt_Version.TabIndex = 1;
			this.dt_Version.Text = "3.2";
			this.dt_Version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_Version
			// 
			this.lbl_Version.Location = new System.Drawing.Point(5, 20);
			this.lbl_Version.Name = "lbl_Version";
			this.lbl_Version.Size = new System.Drawing.Size(105, 15);
			this.lbl_Version.TabIndex = 0;
			this.lbl_Version.Text = "Version";
			this.lbl_Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabFlags
			// 
			this.tabFlags.Controls.Add(this.listbox_Flags);
			this.tabFlags.Location = new System.Drawing.Point(4, 22);
			this.tabFlags.Name = "tabFlags";
			this.tabFlags.Size = new System.Drawing.Size(272, 358);
			this.tabFlags.TabIndex = 2;
			this.tabFlags.Text = "Flags";
			this.tabFlags.UseVisualStyleBackColor = true;
			// 
			// listbox_Flags
			// 
			this.listbox_Flags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listbox_Flags.FormattingEnabled = true;
			this.listbox_Flags.Location = new System.Drawing.Point(0, 0);
			this.listbox_Flags.Name = "listbox_Flags";
			this.listbox_Flags.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listbox_Flags.Size = new System.Drawing.Size(272, 358);
			this.listbox_Flags.TabIndex = 0;
			// 
			// tabBrowse
			// 
			this.tabBrowse.Controls.Add(this.dirsTreeView);
			this.tabBrowse.Location = new System.Drawing.Point(4, 22);
			this.tabBrowse.Name = "tabBrowse";
			this.tabBrowse.Padding = new System.Windows.Forms.Padding(5);
			this.tabBrowse.Size = new System.Drawing.Size(272, 358);
			this.tabBrowse.TabIndex = 3;
			this.tabBrowse.Text = "Browse";
			this.tabBrowse.UseVisualStyleBackColor = true;
			// 
			// dirsTreeView
			// 
			this.dirsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dirsTreeView.Location = new System.Drawing.Point(5, 5);
			this.dirsTreeView.Name = "dirsTreeView";
			this.dirsTreeView.Size = new System.Drawing.Size(262, 348);
			this.dirsTreeView.TabIndex = 0;
			this.dirsTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.dirsTreeView_BeforeExpand);
			this.dirsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dirsTreeView_AfterSelect);
			// 
			// statBar
			// 
			this.statBar.AutoSize = false;
			this.statBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sb_Progress,
            this.sb_Infos,
            this.sb_Zoom});
			this.statBar.Location = new System.Drawing.Point(0, 449);
			this.statBar.Name = "statBar";
			this.statBar.Size = new System.Drawing.Size(540, 26);
			this.statBar.SizingGrip = false;
			this.statBar.TabIndex = 1;
			this.statBar.Text = "statusBar";
			// 
			// sb_Progress
			// 
			this.sb_Progress.AutoSize = false;
			this.sb_Progress.Name = "sb_Progress";
			this.sb_Progress.Size = new System.Drawing.Size(200, 20);
			// 
			// sb_Infos
			// 
			this.sb_Infos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.sb_Infos.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.sb_Infos.Name = "sb_Infos";
			this.sb_Infos.Size = new System.Drawing.Size(192, 21);
			this.sb_Infos.Spring = true;
			this.sb_Infos.Text = "Infos";
			this.sb_Infos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sb_Zoom
			// 
			this.sb_Zoom.AutoSize = false;
			this.sb_Zoom.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.sb_Zoom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.sb_Zoom.Name = "sb_Zoom";
			this.sb_Zoom.Size = new System.Drawing.Size(100, 21);
			this.sb_Zoom.Text = "Zoom";
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMain_File,
            this.menuMain_Edit,
            this.menuMain_Textures});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(540, 24);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "mnuMdiChild";
			this.MainMenu.Visible = false;
			// 
			// menuMain_File
			// 
			this.menuMain_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator9,
            this.menuItem_Export});
			this.menuMain_File.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.menuMain_File.MergeIndex = 1;
			this.menuMain_File.Name = "menuMain_File";
			this.menuMain_File.Size = new System.Drawing.Size(37, 20);
			this.menuMain_File.Text = "File";
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator9.MergeIndex = 4;
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(104, 6);
			// 
			// menuItem_Export
			// 
			this.menuItem_Export.Enabled = false;
			this.menuItem_Export.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.menuItem_Export.MergeIndex = 5;
			this.menuItem_Export.Name = "menuItem_Export";
			this.menuItem_Export.Size = new System.Drawing.Size(107, 22);
			this.menuItem_Export.Text = "Export";
			this.menuItem_Export.Click += new System.EventHandler(this.menuItem_Export_Click);
			// 
			// menuMain_Edit
			// 
			this.menuMain_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Copy});
			this.menuMain_Edit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.menuMain_Edit.MergeIndex = 1;
			this.menuMain_Edit.Name = "menuMain_Edit";
			this.menuMain_Edit.Size = new System.Drawing.Size(39, 20);
			this.menuMain_Edit.Text = "Edit";
			// 
			// menuItem_Copy
			// 
			this.menuItem_Copy.Image = global::HLBox17b.Properties.Resources.CopyHS;
			this.menuItem_Copy.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.menuItem_Copy.Name = "menuItem_Copy";
			this.menuItem_Copy.ShortcutKeyDisplayString = "Ctrl+C";
			this.menuItem_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.menuItem_Copy.Size = new System.Drawing.Size(144, 22);
			this.menuItem_Copy.Text = "Copy";
			this.menuItem_Copy.Click += new System.EventHandler(this.menuItem_Copy_Click);
			// 
			// menuMain_Textures
			// 
			this.menuMain_Textures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.menuItem_View});
			this.menuMain_Textures.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.menuMain_Textures.MergeIndex = 3;
			this.menuMain_Textures.Name = "menuMain_Textures";
			this.menuMain_Textures.Size = new System.Drawing.Size(63, 20);
			this.menuMain_Textures.Text = "Textures";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(96, 6);
			// 
			// menuItem_View
			// 
			this.menuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_ViewNormal,
            this.menuitem_ViewTiled,
            this.menuitem_ViewAnimated});
			this.menuItem_View.Name = "menuItem_View";
			this.menuItem_View.Size = new System.Drawing.Size(99, 22);
			this.menuItem_View.Text = "View";
			// 
			// menuitem_ViewNormal
			// 
			this.menuitem_ViewNormal.Checked = true;
			this.menuitem_ViewNormal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.menuitem_ViewNormal.Image = global::HLBox17b.Properties.Resources.layout;
			this.menuitem_ViewNormal.Name = "menuitem_ViewNormal";
			this.menuitem_ViewNormal.Size = new System.Drawing.Size(126, 22);
			this.menuitem_ViewNormal.Text = "Normal";
			this.menuitem_ViewNormal.Click += new System.EventHandler(this.menuitem_ClickViewNormal);
			// 
			// menuitem_ViewTiled
			// 
			this.menuitem_ViewTiled.Image = global::HLBox17b.Properties.Resources.layout_4;
			this.menuitem_ViewTiled.Name = "menuitem_ViewTiled";
			this.menuitem_ViewTiled.Size = new System.Drawing.Size(126, 22);
			this.menuitem_ViewTiled.Text = "Tiled";
			this.menuitem_ViewTiled.Click += new System.EventHandler(this.menuitem_ClickViewTiled);
			// 
			// menuitem_ViewAnimated
			// 
			this.menuitem_ViewAnimated.Image = global::HLBox17b.Properties.Resources.film;
			this.menuitem_ViewAnimated.Name = "menuitem_ViewAnimated";
			this.menuitem_ViewAnimated.Size = new System.Drawing.Size(126, 22);
			this.menuitem_ViewAnimated.Text = "Animated";
			this.menuitem_ViewAnimated.Click += new System.EventHandler(this.menuitem_ClickViewAnimated);
			// 
			// tabContent
			// 
			this.tabContent.Controls.Add(this.tabPicture);
			this.tabContent.Controls.Add(this.tabVmt);
			this.tabContent.Controls.Add(this.tabViewBrowse);
			this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabContent.Location = new System.Drawing.Point(0, 0);
			this.tabContent.Name = "tabContent";
			this.tabContent.SelectedIndex = 0;
			this.tabContent.Size = new System.Drawing.Size(256, 449);
			this.tabContent.TabIndex = 0;
			// 
			// tabPicture
			// 
			this.tabPicture.Controls.Add(this.picturePanel);
			this.tabPicture.Location = new System.Drawing.Point(4, 22);
			this.tabPicture.Name = "tabPicture";
			this.tabPicture.Padding = new System.Windows.Forms.Padding(3);
			this.tabPicture.Size = new System.Drawing.Size(248, 423);
			this.tabPicture.TabIndex = 0;
			this.tabPicture.Text = "Picture";
			this.tabPicture.UseVisualStyleBackColor = true;
			// 
			// picturePanel
			// 
			this.picturePanel.AutoScroll = true;
			this.picturePanel.BackColor = System.Drawing.SystemColors.Window;
			this.picturePanel.ContextMenuStrip = this.ctxtMenuChild;
			this.picturePanel.Controls.Add(this.pictureBox);
			this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picturePanel.Location = new System.Drawing.Point(3, 3);
			this.picturePanel.Name = "picturePanel";
			this.picturePanel.Size = new System.Drawing.Size(242, 417);
			this.picturePanel.TabIndex = 0;
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBox.BackgroundImage = global::HLBox17b.Properties.Resources.transparent_tile;
			this.pictureBox.Location = new System.Drawing.Point(5, 5);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(70, 65);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// tabVmt
			// 
			this.tabVmt.Controls.Add(this.richTextBox1);
			this.tabVmt.Location = new System.Drawing.Point(4, 22);
			this.tabVmt.Name = "tabVmt";
			this.tabVmt.Padding = new System.Windows.Forms.Padding(3);
			this.tabVmt.Size = new System.Drawing.Size(248, 419);
			this.tabVmt.TabIndex = 1;
			this.tabVmt.Text = "VMT";
			this.tabVmt.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(242, 413);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// tabViewBrowse
			// 
			this.tabViewBrowse.Controls.Add(this.lvw_Browse);
			this.tabViewBrowse.Location = new System.Drawing.Point(4, 22);
			this.tabViewBrowse.Name = "tabViewBrowse";
			this.tabViewBrowse.Size = new System.Drawing.Size(248, 419);
			this.tabViewBrowse.TabIndex = 2;
			this.tabViewBrowse.Text = "Browser";
			this.tabViewBrowse.UseVisualStyleBackColor = true;
			// 
			// lvw_Browse
			// 
			this.lvw_Browse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvw_Browse.LargeImageList = this.vtfImagesList;
			this.lvw_Browse.Location = new System.Drawing.Point(0, 0);
			this.lvw_Browse.Name = "lvw_Browse";
			this.lvw_Browse.Size = new System.Drawing.Size(248, 419);
			this.lvw_Browse.TabIndex = 0;
			this.lvw_Browse.UseCompatibleStateImageBehavior = false;
			this.lvw_Browse.VirtualMode = true;
			this.lvw_Browse.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvwbrowse_RetrieveVirtualItem);
			this.lvw_Browse.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwbrowse_OnDoubleClick);
			// 
			// VtfMdi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 475);
			this.Controls.Add(this.splitContainer0);
			this.Controls.Add(this.statBar);
			this.Controls.Add(this.MainMenu);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.MainMenu;
			this.Name = "VtfMdi";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.vtfmdi_FormClosing);
			this.Shown += new System.EventHandler(this.vtfmdi_OnShown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vtfmdi_OnKeyDown);
			this.ctxtMenuChild.ResumeLayout(false);
			this.splitContainer0.Panel1.ResumeLayout(false);
			this.splitContainer0.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer0)).EndInit();
			this.splitContainer0.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.gbZoom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackbar_Zoom)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabImage.ResumeLayout(false);
			this.gbAnimation.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBar_Frame)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_Speed)).EndInit();
			this.gbSelectImage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dt_ActSlice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActFrame)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActFace)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_ActMipmap)).EndInit();
			this.tabInfos.ResumeLayout(false);
			this.gbLowRes.ResumeLayout(false);
			this.gbHighRes.ResumeLayout(false);
			this.gbLayout.ResumeLayout(false);
			this.gbGeneral.ResumeLayout(false);
			this.tabFlags.ResumeLayout(false);
			this.tabBrowse.ResumeLayout(false);
			this.statBar.ResumeLayout(false);
			this.statBar.PerformLayout();
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.tabContent.ResumeLayout(false);
			this.tabPicture.ResumeLayout(false);
			this.picturePanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.tabVmt.ResumeLayout(false);
			this.tabViewBrowse.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuMain_File;
		private System.Windows.Forms.ToolStripMenuItem menuMain_Edit;
		private System.Windows.Forms.StatusStrip statBar;
		private System.Windows.Forms.ContextMenuStrip ctxtMenuChild;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Copy;
		private System.Windows.Forms.ToolStripStatusLabel sb_Zoom;
		private System.Windows.Forms.ToolStripStatusLabel sb_Infos;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Copy;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Export;
		private System.Windows.Forms.ToolStripMenuItem contextItem_Export;
		private System.Windows.Forms.SplitContainer splitContainer0;
		private System.ComponentModel.BackgroundWorker bgLoader;
		private System.Windows.Forms.ToolStripProgressBar sb_Progress;
		private System.Windows.Forms.Panel picturePanel;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem menuMain_Textures;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuItem_View;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ViewNormal;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ViewTiled;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ViewAnimated;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem contextItem_View;
		private System.Windows.Forms.ToolStripMenuItem ctxitem_ViewNormal;
		private System.Windows.Forms.ToolStripMenuItem ctxitem_ViewTiled;
		private System.Windows.Forms.ToolStripMenuItem ctxitem_ViewAnimated;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox gbZoom;
		private System.Windows.Forms.TrackBar trackbar_Zoom;
		private System.Windows.Forms.Timer timerAni;
		private System.ComponentModel.BackgroundWorker bgAnimator;
		private Externals.TablessControl tabContent;
		private System.Windows.Forms.TabPage tabPicture;
		private System.Windows.Forms.TabPage tabVmt;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TabPage tabViewBrowse;
		private System.Windows.Forms.ListView lvw_Browse;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabImage;
		private System.Windows.Forms.GroupBox gbAnimation;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TrackBar trackBar_Frame;
		private System.Windows.Forms.Button btn_PlayStopAnim;
		private System.Windows.Forms.Label lbl_Speed;
		private System.Windows.Forms.NumericUpDown dt_Speed;
		private System.Windows.Forms.GroupBox gbSelectImage;
		private System.Windows.Forms.Label lbl_SliceLimits;
		private System.Windows.Forms.Label lbl_FaceLimits;
		private System.Windows.Forms.Label lbl_MipmapsLimits;
		private System.Windows.Forms.Label lbl_FrameLimits;
		private System.Windows.Forms.Label lbl_ActSlice;
		private System.Windows.Forms.NumericUpDown dt_ActSlice;
		private System.Windows.Forms.NumericUpDown dt_ActFrame;
		private System.Windows.Forms.Label lbl_ActFrame;
		private System.Windows.Forms.Label lbl_ActFace;
		private System.Windows.Forms.NumericUpDown dt_ActFace;
		private System.Windows.Forms.Label lbl_ActMipmap;
		private System.Windows.Forms.NumericUpDown dt_ActMipmap;
		private System.Windows.Forms.TabPage tabInfos;
		private System.Windows.Forms.GroupBox gbLowRes;
		private System.Windows.Forms.Label dt_FormatLow;
		private System.Windows.Forms.Label lbl_FormatLow;
		private System.Windows.Forms.Label dt_DimsLow;
		private System.Windows.Forms.Label lbl_DimsLow;
		private System.Windows.Forms.GroupBox gbHighRes;
		private System.Windows.Forms.Label dt_FormatHigh;
		private System.Windows.Forms.Label lbl_FormatHigh;
		private System.Windows.Forms.Label dt_DimsHigh;
		private System.Windows.Forms.Label lbl_DimsHigh;
		private System.Windows.Forms.GroupBox gbLayout;
		private System.Windows.Forms.Label dt_Bpp;
		private System.Windows.Forms.Label lbl_Bpp;
		private System.Windows.Forms.Label dt_Depth;
		private System.Windows.Forms.Label lbl_Depth;
		private System.Windows.Forms.Label dt_Startframe;
		private System.Windows.Forms.Label dt_Reflectivity;
		private System.Windows.Forms.Label lbl_Reflectivity;
		private System.Windows.Forms.Label lbl_Startframe;
		private System.Windows.Forms.Label dt_Bumpmap;
		private System.Windows.Forms.Label lbl_Bumpmap;
		private System.Windows.Forms.Label dt_Mipmaps;
		private System.Windows.Forms.Label lbl_Mipmaps;
		private System.Windows.Forms.Label dt_Slices;
		private System.Windows.Forms.Label lbl_Slices;
		private System.Windows.Forms.Label dt_Faces;
		private System.Windows.Forms.Label lbl_Faces;
		private System.Windows.Forms.Label dt_Frames;
		private System.Windows.Forms.Label lbl_Frames;
		private System.Windows.Forms.GroupBox gbGeneral;
		private System.Windows.Forms.Label dt_Filesize;
		private System.Windows.Forms.Label lbl_Filesize;
		private System.Windows.Forms.Label dt_Version;
		private System.Windows.Forms.Label lbl_Version;
		private System.Windows.Forms.TabPage tabFlags;
		private System.Windows.Forms.CheckedListBox listbox_Flags;
		private System.Windows.Forms.TabPage tabBrowse;
		private System.Windows.Forms.ImageList iconListe;
		private System.Windows.Forms.TreeView dirsTreeView;
		private System.ComponentModel.BackgroundWorker bgBrowser;
		private System.Windows.Forms.ImageList vtfImagesList;
		}
	}