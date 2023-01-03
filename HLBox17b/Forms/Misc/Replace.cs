using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLBox17b.Classes;
using HLBox17b.Classes.Tools;

namespace HLBox17b
	{
	public partial class Replace : Form
		{
		public bool bReplaceAll;
		public bool bReplaceNone;
		public Replace(string flname, DateTime time1, DateTime time2, long size1, long size2)
			{
			InitializeComponent();
			bReplaceAll = false;
			bReplaceNone=false;
			flname1.Text = FilesTools.TruncatePath(flname,90);
			flname2.Text = FilesTools.TruncatePath(flname, 90);
			flsize1.Text = String.Format("{0:0,0}", size1);
			flsize2.Text = String.Format("{0:0,0}", size2);
			fltime1.Text = String.Format("{0:G}", time1);
			fltime2.Text = (time2 == DateTime.MinValue) ? "n/a" : String.Format("{0:G}", time2);
			TranslateForm();
			}

		private void TranslateForm()
			{
			//Title
			this.Text = Program.appLng._i18n("rpl_title");
			//Buttons
			btnYes.Text = Program.appLng._i18n("common_yes");
			btnNo.Text = Program.appLng._i18n("common_no");
			btnAll.Text = Program.appLng._i18n("common_all");
			btnNone.Text = Program.appLng._i18n("common_none");	
			//GroupBox
			gb1.Text = Program.appLng._i18n("rpl_existingfile");
			gb2.Text = Program.appLng._i18n("rpl_newfile");
			//Labels
			size1.Text = Program.appLng._i18n("common_size")+":";
			date1.Text = Program.appLng._i18n("common_date")+":";
			size2.Text = Program.appLng._i18n("common_size") + ":";
			date2.Text = Program.appLng._i18n("common_date") + ":";
			}


		private void replace_ClickAll(object sender, EventArgs e)
			{
			bReplaceAll = true;
			bReplaceNone = false;			
			this.DialogResult = DialogResult.Yes;
			this.Close();
			}

		private void replace_ClickNone(object sender, EventArgs e)
			{
			bReplaceAll = false;
			bReplaceNone = true;			
			this.DialogResult = DialogResult.No;
			this.Close();
			}
		}
	}
