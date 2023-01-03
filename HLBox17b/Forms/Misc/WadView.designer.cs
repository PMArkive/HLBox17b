namespace HLBox17b
	{
	partial class WadView
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
			System.Windows.Forms.ToolStripStatusLabel toolStripVoid;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WadView));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_View = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_ViewNormal = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_ViewMipmaps = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_ViewTiled = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem_ViewAnimated = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripColor = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripSize = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripZoom = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox = new HLBox17b.Externals.NearestPictureBox();
			this.bgWork = new System.ComponentModel.BackgroundWorker();
			this.timerAni = new System.Windows.Forms.Timer(this.components);
			this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
			toolStripVoid = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStripVoid
			// 
			toolStripVoid.Name = "toolStripVoid";
			toolStripVoid.Size = new System.Drawing.Size(7, 17);
			toolStripVoid.Spring = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_File,
            this.toolStripMenuItem1,
            this.menuItem_View});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(262, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuItem_File
			// 
			this.menuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Close,
            this.menuItem_Export});
			this.menuItem_File.Name = "menuItem_File";
			this.menuItem_File.Size = new System.Drawing.Size(37, 20);
			this.menuItem_File.Text = "File";
			// 
			// menuItem_Close
			// 
			this.menuItem_Close.Name = "menuItem_Close";
			this.menuItem_Close.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Close.Text = "Close";
			this.menuItem_Close.Click += new System.EventHandler(this.menuItem_Close_OnClick);
			// 
			// menuItem_Export
			// 
			this.menuItem_Export.Image = global::HLBox17b.Properties.Resources.film_save;
			this.menuItem_Export.Name = "menuItem_Export";
			this.menuItem_Export.Size = new System.Drawing.Size(166, 22);
			this.menuItem_Export.Text = "Export Animation";
			this.menuItem_Export.Click += new System.EventHandler(this.menuItem_Export_OnClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
			// 
			// menuItem_View
			// 
			this.menuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_ViewNormal,
            this.menuItem_ViewMipmaps,
            this.menuItem_ViewTiled,
            this.menuItem_ViewAnimated});
			this.menuItem_View.Name = "menuItem_View";
			this.menuItem_View.Size = new System.Drawing.Size(44, 20);
			this.menuItem_View.Text = "View";
			// 
			// menuItem_ViewNormal
			// 
			this.menuItem_ViewNormal.Image = global::HLBox17b.Properties.Resources.layout;
			this.menuItem_ViewNormal.Name = "menuItem_ViewNormal";
			this.menuItem_ViewNormal.Size = new System.Drawing.Size(126, 22);
			this.menuItem_ViewNormal.Text = "Normal";
			this.menuItem_ViewNormal.Click += new System.EventHandler(this.menustrip_ViewNormalClick);
			// 
			// menuItem_ViewMipmaps
			// 
			this.menuItem_ViewMipmaps.Image = global::HLBox17b.Properties.Resources.layout_3_mix;
			this.menuItem_ViewMipmaps.Name = "menuItem_ViewMipmaps";
			this.menuItem_ViewMipmaps.Size = new System.Drawing.Size(126, 22);
			this.menuItem_ViewMipmaps.Text = "Mipmaps";
			this.menuItem_ViewMipmaps.Click += new System.EventHandler(this.menustrip_ViewMipmapsClick);
			// 
			// menuItem_ViewTiled
			// 
			this.menuItem_ViewTiled.Image = global::HLBox17b.Properties.Resources.layout_4;
			this.menuItem_ViewTiled.Name = "menuItem_ViewTiled";
			this.menuItem_ViewTiled.Size = new System.Drawing.Size(126, 22);
			this.menuItem_ViewTiled.Text = "Tiled";
			this.menuItem_ViewTiled.Click += new System.EventHandler(this.menustrip_ViewTiledClick);
			// 
			// menuItem_ViewAnimated
			// 
			this.menuItem_ViewAnimated.Image = global::HLBox17b.Properties.Resources.film;
			this.menuItem_ViewAnimated.Name = "menuItem_ViewAnimated";
			this.menuItem_ViewAnimated.Size = new System.Drawing.Size(126, 22);
			this.menuItem_ViewAnimated.Text = "Animated";
			this.menuItem_ViewAnimated.Click += new System.EventHandler(this.menuStrip_ViewAnimatedClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripVoid,
            this.toolStripColor,
            this.toolStripSize,
            this.toolStripZoom});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(262, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripColor
			// 
			this.toolStripColor.AutoSize = false;
			this.toolStripColor.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripColor.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripColor.Name = "toolStripColor";
			this.toolStripColor.Size = new System.Drawing.Size(100, 17);
			this.toolStripColor.Text = "Name";
			// 
			// toolStripSize
			// 
			this.toolStripSize.AutoSize = false;
			this.toolStripSize.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripSize.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSize.Name = "toolStripSize";
			this.toolStripSize.Size = new System.Drawing.Size(90, 17);
			this.toolStripSize.Text = "Size";
			// 
			// toolStripZoom
			// 
			this.toolStripZoom.AutoSize = false;
			this.toolStripZoom.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripZoom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripZoom.Name = "toolStripZoom";
			this.toolStripZoom.Size = new System.Drawing.Size(50, 17);
			this.toolStripZoom.Text = "Zoom";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(262, 262);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(262, 284);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanelVisible = false;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.pictureBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(262, 262);
			this.panel1.TabIndex = 0;
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(260, 260);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picturebox_OnMouseMove);
			// 
			// bgWork
			// 
			this.bgWork.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkStart);
			this.bgWork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkEnd);
			// 
			// timerAni
			// 
			this.timerAni.Interval = 500;
			this.timerAni.Tick += new System.EventHandler(this.timerAni_Tick);
			// 
			// saveFileDlg
			// 
			this.saveFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.wadview_ExportAnimated);
			// 
			// WadView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(262, 308);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WadView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WadView";
			this.Load += new System.EventHandler(this.OnLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItem_View;
		private System.Windows.Forms.ToolStripMenuItem menuItem_ViewNormal;
		private System.Windows.Forms.ToolStripMenuItem menuItem_ViewTiled;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripSize;
		private System.Windows.Forms.ToolStripStatusLabel toolStripZoom;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripMenuItem menuItem_ViewAnimated;
		private System.ComponentModel.BackgroundWorker bgWork;
		private System.Windows.Forms.Timer timerAni;
		private System.Windows.Forms.ToolStripMenuItem menuItem_File;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Close;
		private System.Windows.Forms.ToolStripMenuItem menuItem_Export;
		private System.Windows.Forms.SaveFileDialog saveFileDlg;
		private Externals.NearestPictureBox pictureBox;
		private System.Windows.Forms.ToolStripStatusLabel toolStripColor;
		private System.Windows.Forms.ToolStripMenuItem menuItem_ViewMipmaps;
		}
	}