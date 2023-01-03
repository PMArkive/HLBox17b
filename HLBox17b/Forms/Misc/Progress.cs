using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLBox17b.Forms.Misc
	{
	public partial class Progress : Form
		{
		public int ProgressValue
			{
			get { return pgBar.Value; }
			set { pgBar.Value = value; }
			}
		public string Title
			{
			set { this.Text = value; }
			}
		public int Maximum
			{
			get { return pgBar.Maximum; }
			set { pgBar.Maximum=value; }
			}
		public string Information
			{
			set { lblText.Text = value; }
			}

		public Progress()
			{
			InitializeComponent();
			TranslateForm();
			}

		private void TranslateForm()
			{
			btnCancel.Text = Program.appLng._i18n("common_cancel");
			}

		private void progress_OnShown(object sender, EventArgs e)
			{
			if (pgBar == null)
				return;
			pgBar.Maximum = 100;
			pgBar.Step = 10;
			}
		
		public event EventHandler<EventArgs> Canceled;
		private void progress_ClickCancel(object sender, EventArgs e)
			{
			// Create a copy of the event to work with
			EventHandler<EventArgs> ea = Canceled;
			// If there are no subscribers, ea will be null so we need to check to avoid a NullReferenceException.
			if (ea != null)
				ea(this, e);
			}
		}
	}


/*
 					
 * Dans le formulaire appelant:
 * 
				 Form_OnShown()
 *					{
					dlgLoader = new Loader();
					dlgLoader.Canceled += new EventHandler<EventArgs>(Loader_btnCancel_Click);
					dlgLoader.Title = "Loading...";
					dlgLoader.Show();
					bgWork.RunWorkerAsync();
 *					}
 *					
 * 		private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
			{
			BackgroundWorker worker = sender as BackgroundWorker;
			worker.ReportProgress(iProgressPercentage, Texitem);
			if (worker.CancellationPending == true)
				break;
 *			}
 *			
 * 		private void Loader_btnCancel_Click(object sender, EventArgs e)
			{
			if (bgWork.WorkerSupportsCancellation == true)
				{
				bgWork.CancelAsync();
				dlgLoader.Close();
				}
			}

		private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (e.ProgressPercentage != 100)
				{
				dlgLoader.ProgressValue = e.ProgressPercentage;
				dlgLoader.Information = "Loading: Progression " + e.ProgressPercentage.ToString() + "%";
				}
			else
				{
				dlgLoader.ProgressValue = dlgLoader.Maximum;
				dlgLoader.Title = "Parsing...";
				dlgLoader.Information = "Please wait";
				}
			if (e.UserState != null)
				{
				texView.Items.Add((ListViewItem)e.UserState);
				}
			}

		private void BgWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
			{
			this.UseWaitCursor = false;
			if (e.Cancelled == true)
				{
				this.Close();
				return;
				}
			else if (e.Error != null)
				{
				MessageBox.Show("Error: " + e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				this.Close();
				return;
				}
			else
 * 
 * ....
 * 
 * }
 * 
 * * 
*/