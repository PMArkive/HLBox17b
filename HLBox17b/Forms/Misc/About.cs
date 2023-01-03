using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HLBox17b
	{
	public partial class About : Form
		{
		private Label lblCopyright;
		private IContainer components;
		private LinkLabel lnk_Changelog;
		private LinkLabel lnk_Disclaimer;
		private Button btn_Close;
		private Timer theTimer;
		private Label lblVersion;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public About(bool SplashMode)
			{
			InitializeComponent();
			myVerInfo vi = new myVerInfo();
			VersionInfo stvi = vi.GetInfos();
			string szVer = stvi.Version;
			string szCop = stvi.Copyright;
			lblVersion.Text = Program.appLng._i18n("Version_txt") + " " + szVer;
			lblCopyright.Text = Program.appLng._i18n("Copyrht_txt") + " " + szCop;
			if (SplashMode)
				{
				this.ControlBox = false;
				this.Cursor = Cursors.AppStarting;
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
				this.ShowIcon = false;
				this.Height = 189;
				lnk_Disclaimer.Visible = false;
				lnk_Changelog.Visible = false;
				InitTimer();
				}
			TranslateForm();
			}
		private void TranslateForm()
			{
			this.Text = Program.appLng._i18n("dlg_about");
			lnk_Disclaimer.Text = Program.appLng._i18n("disclaimer");
			lnk_Changelog.Text = Program.appLng._i18n("changelog");
			btn_Close.Text = Program.appLng._i18n("common_close");
			}

		private void InitTimer()
			{
			theTimer.Interval = 2000;
			theTimer.Start();
			}

		private void theTimer_Tick(object sender, EventArgs e)
			{
			theTimer.Stop();
			CloseSplash();
			}

		private void CloseSplash()
			{
			this.Close();
			}

		private void InitializeComponent()
			{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lnk_Changelog = new System.Windows.Forms.LinkLabel();
			this.lnk_Disclaimer = new System.Windows.Forms.LinkLabel();
			this.btn_Close = new System.Windows.Forms.Button();
			this.theTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// lblCopyright
			// 
			this.lblCopyright.BackColor = System.Drawing.SystemColors.Window;
			this.lblCopyright.Location = new System.Drawing.Point(167, 162);
			this.lblCopyright.Margin = new System.Windows.Forms.Padding(0);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(310, 22);
			this.lblCopyright.TabIndex = 3;
			this.lblCopyright.Text = "Copyright";
			this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.SystemColors.Window;
			this.lblVersion.Location = new System.Drawing.Point(170, 140);
			this.lblVersion.Margin = new System.Windows.Forms.Padding(0);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(307, 22);
			this.lblVersion.TabIndex = 2;
			this.lblVersion.Text = "Version";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lnk_Changelog
			// 
			this.lnk_Changelog.AutoSize = true;
			this.lnk_Changelog.BackColor = System.Drawing.SystemColors.Window;
			this.lnk_Changelog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lnk_Changelog.LinkColor = System.Drawing.Color.DarkRed;
			this.lnk_Changelog.Location = new System.Drawing.Point(10, 145);
			this.lnk_Changelog.Name = "lnk_Changelog";
			this.lnk_Changelog.Size = new System.Drawing.Size(58, 13);
			this.lnk_Changelog.TabIndex = 2;
			this.lnk_Changelog.TabStop = true;
			this.lnk_Changelog.Text = "Changelog";
			this.lnk_Changelog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnk_Changelog.VisitedLinkColor = System.Drawing.Color.DarkRed;
			this.lnk_Changelog.Click += new System.EventHandler(this.Changelog_Click);
			// 
			// lnk_Disclaimer
			// 
			this.lnk_Disclaimer.AutoSize = true;
			this.lnk_Disclaimer.BackColor = System.Drawing.SystemColors.Window;
			this.lnk_Disclaimer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lnk_Disclaimer.LinkColor = System.Drawing.Color.DarkRed;
			this.lnk_Disclaimer.Location = new System.Drawing.Point(10, 165);
			this.lnk_Disclaimer.Name = "lnk_Disclaimer";
			this.lnk_Disclaimer.Size = new System.Drawing.Size(55, 13);
			this.lnk_Disclaimer.TabIndex = 3;
			this.lnk_Disclaimer.TabStop = true;
			this.lnk_Disclaimer.Text = "Disclaimer";
			this.lnk_Disclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnk_Disclaimer.VisitedLinkColor = System.Drawing.Color.DarkRed;
			this.lnk_Disclaimer.Click += new System.EventHandler(this.Disclaimer_Click);
			// 
			// btn_Close
			// 
			this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_Close.Location = new System.Drawing.Point(205, 200);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.btn_Close.Size = new System.Drawing.Size(75, 30);
			this.btn_Close.TabIndex = 1;
			this.btn_Close.Text = "Close";
			this.btn_Close.UseVisualStyleBackColor = true;
			// 
			// theTimer
			// 
			this.theTimer.Tick += new System.EventHandler(this.theTimer_Tick);
			// 
			// About
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = global::HLBox17b.Properties.Resources.Splash;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(486, 238);
			this.Controls.Add(this.btn_Close);
			this.Controls.Add(this.lnk_Changelog);
			this.Controls.Add(this.lnk_Disclaimer);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblVersion);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		private void Changelog_Click(object sender, EventArgs e)
			{
			Info inf = new Info();
			inf.SetTitle(Program.appLng._i18n("changelog"));
			inf.SetContentFile("Changelog");
			inf.ShowDialog();
			}

		private void Disclaimer_Click(object sender, EventArgs e)
			{
			Info inf = new Info();
			inf.SetTitle(Program.appLng._i18n("disclaimer"));
			inf.SetContentFile("Readme");
			inf.ShowDialog();
			}
		}
	}
