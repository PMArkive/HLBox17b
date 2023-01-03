namespace HLBox17b
	{
	partial class AddTexture
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTexture));
			this.textbox_TexName = new System.Windows.Forms.TextBox();
			this.trackbar_TexWidth = new System.Windows.Forms.TrackBar();
			this.label_texWidth = new System.Windows.Forms.Label();
			this.label_texHeight = new System.Windows.Forms.Label();
			this.trackbar_TexHeight = new System.Windows.Forms.TrackBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.textbox_TexHeight = new System.Windows.Forms.TextBox();
			this.textbox_TexWidth = new System.Windows.Forms.TextBox();
			this.gb_TexName = new System.Windows.Forms.GroupBox();
			this.button_HelpTexes = new System.Windows.Forms.Button();
			this.gb_TexDims = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.trackbar_TexWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackbar_TexHeight)).BeginInit();
			this.gb_TexName.SuspendLayout();
			this.gb_TexDims.SuspendLayout();
			this.SuspendLayout();
			// 
			// textbox_TexName
			// 
			this.textbox_TexName.Location = new System.Drawing.Point(15, 20);
			this.textbox_TexName.MaxLength = 15;
			this.textbox_TexName.Name = "textbox_TexName";
			this.textbox_TexName.Size = new System.Drawing.Size(120, 20);
			this.textbox_TexName.TabIndex = 1;
			// 
			// trackbar_TexWidth
			// 
			this.trackbar_TexWidth.AutoSize = false;
			this.trackbar_TexWidth.LargeChange = 16;
			this.trackbar_TexWidth.Location = new System.Drawing.Point(5, 30);
			this.trackbar_TexWidth.Maximum = 512;
			this.trackbar_TexWidth.Minimum = 16;
			this.trackbar_TexWidth.Name = "trackbar_TexWidth";
			this.trackbar_TexWidth.Size = new System.Drawing.Size(180, 30);
			this.trackbar_TexWidth.SmallChange = 16;
			this.trackbar_TexWidth.TabIndex = 3;
			this.trackbar_TexWidth.TickFrequency = 64;
			this.trackbar_TexWidth.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackbar_TexWidth.Value = 64;
			this.trackbar_TexWidth.ValueChanged += new System.EventHandler(this.trackbar_Width_OnValueChanged);
			// 
			// label_texWidth
			// 
			this.label_texWidth.Location = new System.Drawing.Point(5, 10);
			this.label_texWidth.Name = "label_texWidth";
			this.label_texWidth.Size = new System.Drawing.Size(125, 20);
			this.label_texWidth.TabIndex = 0;
			this.label_texWidth.Text = "Width";
			this.label_texWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_texHeight
			// 
			this.label_texHeight.Location = new System.Drawing.Point(5, 70);
			this.label_texHeight.Name = "label_texHeight";
			this.label_texHeight.Size = new System.Drawing.Size(120, 20);
			this.label_texHeight.TabIndex = 0;
			this.label_texHeight.Text = "Height";
			this.label_texHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// trackbar_TexHeight
			// 
			this.trackbar_TexHeight.AutoSize = false;
			this.trackbar_TexHeight.LargeChange = 16;
			this.trackbar_TexHeight.Location = new System.Drawing.Point(5, 90);
			this.trackbar_TexHeight.Maximum = 512;
			this.trackbar_TexHeight.Minimum = 16;
			this.trackbar_TexHeight.Name = "trackbar_TexHeight";
			this.trackbar_TexHeight.Size = new System.Drawing.Size(180, 30);
			this.trackbar_TexHeight.SmallChange = 16;
			this.trackbar_TexHeight.TabIndex = 4;
			this.trackbar_TexHeight.TickFrequency = 64;
			this.trackbar_TexHeight.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackbar_TexHeight.Value = 64;
			this.trackbar_TexHeight.ValueChanged += new System.EventHandler(this.trackbar_Height_OnValueChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(125, 210);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(25, 210);
			this.btnOk.Name = "btnOk";
			this.btnOk.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			this.btnOk.Size = new System.Drawing.Size(75, 30);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.addtexture_BtnOk_OnCLick);
			// 
			// textbox_TexHeight
			// 
			this.textbox_TexHeight.Cursor = System.Windows.Forms.Cursors.Default;
			this.textbox_TexHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textbox_TexHeight.Location = new System.Drawing.Point(135, 70);
			this.textbox_TexHeight.Name = "textbox_TexHeight";
			this.textbox_TexHeight.ReadOnly = true;
			this.textbox_TexHeight.Size = new System.Drawing.Size(45, 20);
			this.textbox_TexHeight.TabIndex = 0;
			this.textbox_TexHeight.TabStop = false;
			this.textbox_TexHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textbox_TexWidth
			// 
			this.textbox_TexWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textbox_TexWidth.Location = new System.Drawing.Point(135, 10);
			this.textbox_TexWidth.Name = "textbox_TexWidth";
			this.textbox_TexWidth.ReadOnly = true;
			this.textbox_TexWidth.Size = new System.Drawing.Size(45, 20);
			this.textbox_TexWidth.TabIndex = 0;
			this.textbox_TexWidth.TabStop = false;
			this.textbox_TexWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// gb_TexName
			// 
			this.gb_TexName.Controls.Add(this.button_HelpTexes);
			this.gb_TexName.Controls.Add(this.textbox_TexName);
			this.gb_TexName.Location = new System.Drawing.Point(15, 10);
			this.gb_TexName.Name = "gb_TexName";
			this.gb_TexName.Size = new System.Drawing.Size(195, 55);
			this.gb_TexName.TabIndex = 0;
			this.gb_TexName.TabStop = false;
			this.gb_TexName.Text = "Texture Name";
			// 
			// button_HelpTexes
			// 
			this.button_HelpTexes.Image = global::HLBox17b.Properties.Resources.help;
			this.button_HelpTexes.Location = new System.Drawing.Point(150, 17);
			this.button_HelpTexes.Name = "button_HelpTexes";
			this.button_HelpTexes.Size = new System.Drawing.Size(30, 25);
			this.button_HelpTexes.TabIndex = 2;
			this.button_HelpTexes.UseVisualStyleBackColor = true;
			this.button_HelpTexes.Click += new System.EventHandler(this.button_HelpTexes_OnClick);
			// 
			// gb_TexDims
			// 
			this.gb_TexDims.Controls.Add(this.trackbar_TexWidth);
			this.gb_TexDims.Controls.Add(this.textbox_TexHeight);
			this.gb_TexDims.Controls.Add(this.label_texWidth);
			this.gb_TexDims.Controls.Add(this.textbox_TexWidth);
			this.gb_TexDims.Controls.Add(this.trackbar_TexHeight);
			this.gb_TexDims.Controls.Add(this.label_texHeight);
			this.gb_TexDims.Location = new System.Drawing.Point(15, 65);
			this.gb_TexDims.Name = "gb_TexDims";
			this.gb_TexDims.Size = new System.Drawing.Size(195, 130);
			this.gb_TexDims.TabIndex = 9;
			this.gb_TexDims.TabStop = false;
			// 
			// AddTexture
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(222, 253);
			this.Controls.Add(this.gb_TexDims);
			this.Controls.Add(this.gb_TexName);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddTexture";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddTexture";
			this.Shown += new System.EventHandler(this.addtexture_OnShown);
			((System.ComponentModel.ISupportInitialize)(this.trackbar_TexWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackbar_TexHeight)).EndInit();
			this.gb_TexName.ResumeLayout(false);
			this.gb_TexName.PerformLayout();
			this.gb_TexDims.ResumeLayout(false);
			this.gb_TexDims.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.TextBox textbox_TexName;
		private System.Windows.Forms.TrackBar trackbar_TexWidth;
		private System.Windows.Forms.Label label_texWidth;
		private System.Windows.Forms.Label label_texHeight;
		private System.Windows.Forms.TrackBar trackbar_TexHeight;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox textbox_TexHeight;
		private System.Windows.Forms.TextBox textbox_TexWidth;
		private System.Windows.Forms.GroupBox gb_TexName;
		private System.Windows.Forms.GroupBox gb_TexDims;
		private System.Windows.Forms.Button button_HelpTexes;
		}
	}