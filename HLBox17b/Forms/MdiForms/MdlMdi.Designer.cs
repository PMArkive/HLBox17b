namespace HLBox17b.Forms.MdiForms
	{
	partial class MdlMdi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdlMdi));
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.statBar = new System.Windows.Forms.StatusStrip();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pictureBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(639, 363);
			this.panel1.TabIndex = 1;
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBox.BackgroundImage = global::HLBox17b.Properties.Resources.transparent_tile;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(639, 363);
			this.pictureBox.TabIndex = 2;
			this.pictureBox.TabStop = false;
			// 
			// statBar
			// 
			this.statBar.Location = new System.Drawing.Point(0, 363);
			this.statBar.Name = "statBar";
			this.statBar.Size = new System.Drawing.Size(639, 22);
			this.statBar.TabIndex = 0;
			this.statBar.Text = "statusStrip1";
			// 
			// MdlMdi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(639, 385);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MdlMdi";
			this.Shown += new System.EventHandler(this.mdlmdi_OnShown);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.StatusStrip statBar;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox;
		}
	}
