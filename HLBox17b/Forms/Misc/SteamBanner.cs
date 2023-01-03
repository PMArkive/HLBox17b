using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;

namespace HLBox17b.Forms.Misc
	{
	public partial class SteamBanner : Form
		{
		public SteamBanner()
			{
			InitializeComponent();
			TranslateForm();
			}

		private void TranslateForm()
			{
			//Set Tooltips for buttons
			ToolTip ttip = new ToolTip();
			ttip.AutoPopDelay = 4000;
			ttip.InitialDelay = 1000;
			ttip.ReshowDelay = 500;
			ttip.ShowAlways = true;
			this.Text = Program.appLng._i18n("sbn_steambannertitle");
			gbNewBanner.Text = Program.appLng._i18n("sbn_newbanner");
			LoadingPanel.Text = Program.appLng._i18n("sbn_loadingurl");
			ttip.SetToolTip(btnDef17b, Program.appLng._i18n("sbn_default17b"));
			ttip.SetToolTip(btnCheck, Program.appLng._i18n("sbn_refreshurl"));
			}

		private void steambanner_UpdateUrl()
			{
			this.UseWaitCursor = true;
			steambanner_ShowLoading(true);
			if (tbUrl.Text.Trim() == string.Empty)
				return;
			//wbUrl.Url = new Uri(tbUrl.Text);
			wbUrl.Navigate(new Uri(tbUrl.Text));
			}

		private void steambanner_TimerTick(object sender, EventArgs e)
			{
			theTimer.Stop();
			steambanner_UpdateUrl();
			}


		private void steambanner_OnShow(object sender, EventArgs e)
			{
			tbUrl.Text = RegTools.GetSteamProvidedByUrl();

			Point locationOnForm = panelWeb.FindForm().PointToClient(panelWeb.Parent.PointToScreen(panelWeb.Location));
			LoadingPanel.Left = locationOnForm.X;
			LoadingPanel.Top = locationOnForm.Y;
			LoadingPanel.Width = panelWeb.Width;
			LoadingPanel.Height = panelWeb.Height;
			panelWeb.Visible = false;
			LoadingPanel.BringToFront();
			LoadingPanel.Visible = true;
			theTimer.Start();
			}

		private void steambanner_ClickbtnDef(object sender, EventArgs e)
			{
			tbUrl.Text = Program.mRc.szBan_defaultUrl;
			steambanner_UpdateUrl();
			}

		private void steambanner_ClickbtnCheck(object sender, EventArgs e)
			{
			steambanner_UpdateUrl();
			}

		private void steambanner_ClickBtnOk(object sender, EventArgs e)
			{
			if (tbUrl.Text.Trim() == string.Empty)
				return;
			DialogResult r = MessageBox.Show(Program.appLng._i18n("sbn_changeurl"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (r == DialogResult.No)
				return;
			RegTools.ChangeSteamProvidedByUrl(tbUrl.Text);
			}

		private void steambanner_ShowLoading(bool bShow)
			{
			if (bShow)
				{
				panelWeb.Visible = false;
				LoadingPanel.BringToFront();
				LoadingPanel.Visible = true;
				}
			else
				{
				LoadingPanel.Visible = false;
				panelWeb.BringToFront();
				panelWeb.Visible = true;
				}
			}

		private void steambanner_wbProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
			{
			if (e.CurrentProgress >= e.MaximumProgress)
				{
				steambanner_ShowLoading(false);
				this.UseWaitCursor = false;
				}
			}

		private void steambanner_tbUrlTextChanged(object sender, EventArgs e)
			{
			if (tbUrl.Text.Trim() == string.Empty)
				{
				btnCheck.Enabled = false;
				btnOk.Enabled = false;
				}
			else
				{
				if (Uri.IsWellFormedUriString(tbUrl.Text,UriKind.Absolute))
					{
					btnCheck.Enabled = true;
					btnOk.Enabled = true;
					}
				else
					{
					btnCheck.Enabled = false;
					btnOk.Enabled = false;
					}
				}
			}










		}
	}
