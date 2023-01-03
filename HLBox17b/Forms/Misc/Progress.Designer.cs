namespace HLBox17b.Forms.Misc
	{
	partial class Progress
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
			this.pgBar = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pgBar
			// 
			this.pgBar.Cursor = System.Windows.Forms.Cursors.Default;
			this.pgBar.Location = new System.Drawing.Point(15, 35);
			this.pgBar.Name = "pgBar";
			this.pgBar.Size = new System.Drawing.Size(345, 23);
			this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pgBar.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(150, 70);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.progress_ClickCancel);
			// 
			// lblText
			// 
			this.lblText.Location = new System.Drawing.Point(15, 10);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(345, 17);
			this.lblText.TabIndex = 0;
			this.lblText.Text = "Information";
			this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Loader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(376, 110);
			this.ControlBox = false;
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pgBar);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Loader";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Title";
			this.Shown += new System.EventHandler(this.progress_OnShown);
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ProgressBar pgBar;
		public System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Label lblText;
		}
	}