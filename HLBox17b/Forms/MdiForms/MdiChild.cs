using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLBox17b.Forms.MdiForms
	{
	public partial class MdiChild : Form
		{
		public string szFullFile;
		public bool bIsModified;
		public bool bIsNew;
		
		public MdiChild()
			{
			bIsModified = false;
			bIsNew = false;
			szFullFile = string.Empty;
			InitializeComponent();
			}

		public virtual void mdichild_UpdateTitle()
			{
			string szTitle = Path.GetFileName(szFullFile);
			szTitle += bIsModified ? "*" : "";
			this.Text = szTitle;
			}

		private void mdichild_OnLoad(object sender, EventArgs e)
			{
			//Hack to redraw mdi icon that doesn't show when creating new child
			this.Icon = Icon.Clone() as Icon;
			}

		}
	}
