namespace HLBox17b.Forms.Misc
{
    partial class SteamBanner
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamBanner));
			this.gbNewBanner = new System.Windows.Forms.GroupBox();
			this.panelWeb = new System.Windows.Forms.Panel();
			this.wbUrl = new System.Windows.Forms.WebBrowser();
			this.btnCheck = new System.Windows.Forms.Button();
			this.tbUrl = new System.Windows.Forms.TextBox();
			this.btnDef17b = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.LoadingPanel = new System.Windows.Forms.Label();
			this.theTimer = new System.Windows.Forms.Timer(this.components);
			this.gbNewBanner.SuspendLayout();
			this.panelWeb.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbNewBanner
			// 
			this.gbNewBanner.Controls.Add(this.panelWeb);
			this.gbNewBanner.Controls.Add(this.btnCheck);
			this.gbNewBanner.Controls.Add(this.tbUrl);
			this.gbNewBanner.Controls.Add(this.btnDef17b);
			resources.ApplyResources(this.gbNewBanner, "gbNewBanner");
			this.gbNewBanner.Name = "gbNewBanner";
			this.gbNewBanner.TabStop = false;
			// 
			// panelWeb
			// 
			this.panelWeb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelWeb.Controls.Add(this.wbUrl);
			resources.ApplyResources(this.panelWeb, "panelWeb");
			this.panelWeb.Name = "panelWeb";
			// 
			// wbUrl
			// 
			this.wbUrl.AllowWebBrowserDrop = false;
			resources.ApplyResources(this.wbUrl, "wbUrl");
			this.wbUrl.IsWebBrowserContextMenuEnabled = false;
			this.wbUrl.Name = "wbUrl";
			this.wbUrl.ScriptErrorsSuppressed = true;
			this.wbUrl.ScrollBarsEnabled = false;
			this.wbUrl.TabStop = false;
			this.wbUrl.WebBrowserShortcutsEnabled = false;
			this.wbUrl.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.steambanner_wbProgressChanged);
			// 
			// btnCheck
			// 
			resources.ApplyResources(this.btnCheck, "btnCheck");
			this.btnCheck.Name = "btnCheck";
			this.btnCheck.UseVisualStyleBackColor = true;
			this.btnCheck.Click += new System.EventHandler(this.steambanner_ClickbtnCheck);
			// 
			// tbUrl
			// 
			resources.ApplyResources(this.tbUrl, "tbUrl");
			this.tbUrl.Name = "tbUrl";
			this.tbUrl.TextChanged += new System.EventHandler(this.steambanner_tbUrlTextChanged);
			// 
			// btnDef17b
			// 
			this.btnDef17b.Image = global::HLBox17b.Properties.Resources.Ico17b;
			resources.ApplyResources(this.btnDef17b, "btnDef17b");
			this.btnDef17b.Name = "btnDef17b";
			this.btnDef17b.UseVisualStyleBackColor = true;
			this.btnDef17b.Click += new System.EventHandler(this.steambanner_ClickbtnDef);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.btnOk, "btnOk");
			this.btnOk.Name = "btnOk";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.steambanner_ClickBtnOk);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// LoadingPanel
			// 
			this.LoadingPanel.BackColor = System.Drawing.SystemColors.Window;
			this.LoadingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.LoadingPanel, "LoadingPanel");
			this.LoadingPanel.Name = "LoadingPanel";
			// 
			// theTimer
			// 
			this.theTimer.Interval = 500;
			this.theTimer.Tick += new System.EventHandler(this.steambanner_TimerTick);
			// 
			// SteamBanner
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LoadingPanel);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.gbNewBanner);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SteamBanner";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Shown += new System.EventHandler(this.steambanner_OnShow);
			this.gbNewBanner.ResumeLayout(false);
			this.gbNewBanner.PerformLayout();
			this.panelWeb.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox gbNewBanner;
		private System.Windows.Forms.TextBox tbUrl;
		private System.Windows.Forms.Button btnDef17b;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnCheck;
		private System.Windows.Forms.WebBrowser wbUrl;
		private System.Windows.Forms.Panel panelWeb;
		private System.Windows.Forms.Label LoadingPanel;
		private System.Windows.Forms.Timer theTimer;
    }
}

