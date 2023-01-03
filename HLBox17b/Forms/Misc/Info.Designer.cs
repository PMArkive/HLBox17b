namespace HLBox17b
	{
	partial class Info
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
			this.txt_Info = new System.Windows.Forms.TextBox();
			this.btn_Close = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txt_Info
			// 
			this.txt_Info.Location = new System.Drawing.Point(10, 10);
			this.txt_Info.Multiline = true;
			this.txt_Info.Name = "txt_Info";
			this.txt_Info.ReadOnly = true;
			this.txt_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txt_Info.Size = new System.Drawing.Size(421, 211);
			this.txt_Info.TabIndex = 0;
			this.txt_Info.TabStop = false;
			// 
			// btn_Close
			// 
			this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_Close.Location = new System.Drawing.Point(175, 230);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.btn_Close.Size = new System.Drawing.Size(75, 30);
			this.btn_Close.TabIndex = 1;
			this.btn_Close.Text = "Close";
			this.btn_Close.UseVisualStyleBackColor = true;
			// 
			// Info
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 270);
			this.Controls.Add(this.btn_Close);
			this.Controls.Add(this.txt_Info);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Info";
			this.Text = "Info";
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.TextBox txt_Info;
		private System.Windows.Forms.Button btn_Close;
		}
	}