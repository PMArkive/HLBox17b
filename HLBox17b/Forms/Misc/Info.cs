using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace HLBox17b
	{
	public partial class Info : Form
		{
		public Info()
			{
			InitializeComponent();
			TranslateForm();
			}
		private void TranslateForm()
			{
			btn_Close.Text = Program.appLng._i18n("common_close");
			}
		public void SetTitle(string szTitle)
			{
			this.Text = szTitle;
			}
		public void SetContentFile(string szFile)
			{
			string myString = string.Empty;
			try
				{
				string szReadmeFile = Path.GetDirectoryName(Application.ExecutablePath) + @"\"+szFile+".txt";
				System.IO.StreamReader myFile = new System.IO.StreamReader(szReadmeFile);
				myString = myFile.ReadToEnd();
				myFile.Close();
				}
			catch (Exception)
				{
				myString = "File "+szFile+".txt not found";
				}
			txt_Info.Text = myString;
			}
		}
	}
