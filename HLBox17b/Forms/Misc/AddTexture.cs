using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;

namespace HLBox17b
	{
	public partial class AddTexture : Form
		{
		private int nWidth;
		private int nHeight;

		public int texWidth {get { return nWidth; } set {nWidth = value; }}
		public int texHeight { get { return nHeight; } set { nHeight = value; } }
		public string TexName { get { return textbox_TexName.Text; } set { textbox_TexName.Text = value; } }

		public AddTexture()
			{
			InitializeComponent();
			nWidth = Program.mRc.nTxs_LastNewWidth;
			nHeight = Program.mRc.nTxs_LastNewHeight;
			textbox_TexWidth.Text = nWidth.ToString();
			textbox_TexHeight.Text = nHeight.ToString();
			TranslateForm();
			}
		private void TranslateForm()
			{
			ToolTip ttip = new ToolTip();
			ttip.AutoPopDelay = 4000;
			ttip.InitialDelay = 1000;
			ttip.ReshowDelay = 500;
			ttip.ShowAlways = true;
			this.Text = Program.appLng._i18n("waddlg_addtexture_hdr");
			gb_TexName.Text = Program.appLng._i18n("waddlg_gb_texname");
			label_texWidth.Text = Program.appLng._i18n("common_width");
			label_texHeight.Text = Program.appLng._i18n("common_height");
			ttip.SetToolTip(button_HelpTexes, Program.appLng._i18n("waddlg_texeshelp"));
			}

		private void trackbar_Width_OnValueChanged(object sender, EventArgs e)
			{
			nWidth = ((int)(trackbar_TexWidth.Value/16)) * 16;
			textbox_TexWidth.Text = nWidth.ToString();
			}

		private void trackbar_Height_OnValueChanged(object sender, EventArgs e)
			{
			nHeight = ((int)(trackbar_TexHeight.Value/16)) * 16;
			textbox_TexHeight.Text = nHeight.ToString();
			}

		private void button_HelpTexes_OnClick(object sender, EventArgs e)
			{
			WebTools.ViewTexturesHelp();
			}

		private void addtexture_BtnOk_OnCLick(object sender, EventArgs e)
			{
			if (TexName.Trim() == string.Empty)
				{
				MessageBox.Show(Program.appLng._i18n("waddlg_msg_cantbeempty"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
				}
			}

		private void addtexture_OnShown(object sender, EventArgs e)
			{
			textbox_TexName.Focus();
			}
		}
	}
