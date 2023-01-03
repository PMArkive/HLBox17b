namespace HLBox17b
	{
	partial class DownDlg
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
			System.Windows.Forms.GroupBox groupBox1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownDlg));
			this.edtModRootFolder = new System.Windows.Forms.TextBox();
			this.root_folder_mod = new System.Windows.Forms.Label();
			this.btnbrowsefolder = new System.Windows.Forms.Button();
			this.btnFinalAction = new System.Windows.Forms.Button();
			this.btnCloseCancel = new System.Windows.Forms.Button();
			this.destination_mod = new System.Windows.Forms.Label();
			this.ctl_tools_lstmods = new System.Windows.Forms.ComboBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.bgWork1 = new System.ComponentModel.BackgroundWorker();
			this.bgWork2 = new System.ComponentModel.BackgroundWorker();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(this.edtModRootFolder);
			groupBox1.Controls.Add(this.root_folder_mod);
			groupBox1.Controls.Add(this.btnbrowsefolder);
			groupBox1.Controls.Add(this.btnFinalAction);
			groupBox1.Controls.Add(this.btnCloseCancel);
			groupBox1.Controls.Add(this.destination_mod);
			groupBox1.Controls.Add(this.ctl_tools_lstmods);
			groupBox1.Controls.Add(this.progressBar);
			groupBox1.Location = new System.Drawing.Point(5, 5);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			groupBox1.Size = new System.Drawing.Size(430, 150);
			groupBox1.TabIndex = 27;
			groupBox1.TabStop = false;
			// 
			// edtModRootFolder
			// 
			this.edtModRootFolder.Location = new System.Drawing.Point(10, 70);
			this.edtModRootFolder.Name = "edtModRootFolder";
			this.edtModRootFolder.ReadOnly = true;
			this.edtModRootFolder.Size = new System.Drawing.Size(365, 20);
			this.edtModRootFolder.TabIndex = 34;
			this.edtModRootFolder.TabStop = false;
			this.edtModRootFolder.TextChanged += new System.EventHandler(this.edtTextChanged);
			// 
			// root_folder_mod
			// 
			this.root_folder_mod.Location = new System.Drawing.Point(10, 50);
			this.root_folder_mod.Name = "root_folder_mod";
			this.root_folder_mod.Size = new System.Drawing.Size(300, 13);
			this.root_folder_mod.TabIndex = 33;
			this.root_folder_mod.Text = "root_folder_mod";
			// 
			// btnbrowsefolder
			// 
			this.btnbrowsefolder.Image = global::HLBox17b.Properties.Resources.Explorer;
			this.btnbrowsefolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnbrowsefolder.Location = new System.Drawing.Point(385, 65);
			this.btnbrowsefolder.Name = "btnbrowsefolder";
			this.btnbrowsefolder.Size = new System.Drawing.Size(33, 29);
			this.btnbrowsefolder.TabIndex = 31;
			this.btnbrowsefolder.UseVisualStyleBackColor = true;
			this.btnbrowsefolder.Click += new System.EventHandler(this.btnbrowsefolder_Click);
			// 
			// btnFinalAction
			// 
			this.btnFinalAction.Location = new System.Drawing.Point(255, 110);
			this.btnFinalAction.Name = "btnFinalAction";
			this.btnFinalAction.Size = new System.Drawing.Size(78, 33);
			this.btnFinalAction.TabIndex = 30;
			this.btnFinalAction.Text = "btnFinalAction";
			this.btnFinalAction.UseVisualStyleBackColor = true;
			this.btnFinalAction.Visible = false;
			this.btnFinalAction.Click += new System.EventHandler(this.btnLastAction_Click);
			// 
			// btnCloseCancel
			// 
			this.btnCloseCancel.Location = new System.Drawing.Point(340, 110);
			this.btnCloseCancel.Name = "btnCloseCancel";
			this.btnCloseCancel.Size = new System.Drawing.Size(78, 33);
			this.btnCloseCancel.TabIndex = 1;
			this.btnCloseCancel.Text = "btnclosecancel";
			this.btnCloseCancel.UseVisualStyleBackColor = true;
			this.btnCloseCancel.Click += new System.EventHandler(this.Down_BtnClickCancel);
			// 
			// destination_mod
			// 
			this.destination_mod.Location = new System.Drawing.Point(10, 100);
			this.destination_mod.Name = "destination_mod";
			this.destination_mod.Size = new System.Drawing.Size(300, 13);
			this.destination_mod.TabIndex = 29;
			this.destination_mod.Text = "destination_mod";
			// 
			// ctl_tools_lstmods
			// 
			this.ctl_tools_lstmods.FormattingEnabled = true;
			this.ctl_tools_lstmods.Location = new System.Drawing.Point(10, 120);
			this.ctl_tools_lstmods.Name = "ctl_tools_lstmods";
			this.ctl_tools_lstmods.Size = new System.Drawing.Size(235, 21);
			this.ctl_tools_lstmods.TabIndex = 28;
			this.ctl_tools_lstmods.Text = "ctl_tools_lstmods";
			this.ctl_tools_lstmods.SelectedIndexChanged += new System.EventHandler(this.ctlLstMods_ChangeSelectedIndex);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(10, 20);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(410, 23);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 0;
			// 
			// bgWork1
			// 
			this.bgWork1.WorkerReportsProgress = true;
			this.bgWork1.WorkerSupportsCancellation = true;
			this.bgWork1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWork1_DoWork);
			this.bgWork1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWork1_ProgressChanged);
			this.bgWork1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWork1_Completed);
			// 
			// bgWork2
			// 
			this.bgWork2.WorkerReportsProgress = true;
			this.bgWork2.WorkerSupportsCancellation = true;
			this.bgWork2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWork2_DoWork);
			this.bgWork2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWork2_ProgressChanged);
			this.bgWork2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWork2_Completed);
			// 
			// DownDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(440, 162);
			this.Controls.Add(groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DownDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DownDlg";
			this.Shown += new System.EventHandler(this.Down_OnShow);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar;
		public System.Windows.Forms.ComboBox ctl_tools_lstmods;
		private System.Windows.Forms.Button btnCloseCancel;
		private System.Windows.Forms.Label destination_mod;
		private System.ComponentModel.BackgroundWorker bgWork1;
		private System.ComponentModel.BackgroundWorker bgWork2;
		private System.Windows.Forms.Button btnFinalAction;
		private System.Windows.Forms.Button btnbrowsefolder;
		private System.Windows.Forms.Label root_folder_mod;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox edtModRootFolder;
		}
	}