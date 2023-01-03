using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace HLBox17b.Classes.Tools
	{
	class WebTools
		{
		//// Variables Globales
		static private bool bInformUser = false;	//Informer utilisateur qu'il dispose de la dernière version
		static private bool bVerChecking = false;	//Téléchargement des données de version en cours
		/////////////////////////////////////////////////////////////////////////////////		 
		/////////////////////////////////////////////////////////////////////////////////
		/// Accès à l'aide sur Textures
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Accès à l'aide textures
		///</summary>
		public static void ViewTexturesHelp()
			{
			Process.Start(Program.mRc.szGen_17bTexesHelp);
			}
		/////////////////////////////////////////////////////////////////////////////////		 
		/////////////////////////////////////////////////////////////////////////////////
		/// Accès au Web
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Accès au site 17Buddies
		///</summary>
		public static void Goto17bWebSite()
			{
			Process.Start(Program.mRc.szGen_17bUrl);
			}
		/////////////////////////////////////////////////////////////////////////////////		 
		/////////////////////////////////////////////////////////////////////////////////
		/// Verification Version
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Verification de la version du logiciel
		/// Appel de la fonction de téléchargement asynchrone
		///</summary>
		/////////////////////////////////////////////////////////////////////////////////
		public static void NewVersion_StartCheck(bool binform)
			{
			bInformUser = binform;
			if (!bVerChecking)
				{
				bVerChecking = true;
				string remoteUri = Program.mRc.szGen_17bUpdateUrl;
				WebClient client = new WebClient();
				client.DownloadDataCompleted += NewVersion_CheckCompleted;
				try
					{
					client.DownloadDataAsync(new Uri(remoteUri));
					}
				catch (UriFormatException ex)
					{
					MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					client.Dispose();
					}
				catch (WebException ex)
					{
					MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					client.Dispose();
					}
				}
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Verification de la version du logiciel
		/// Fonction de retour après téléchargement des données
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////////
		public static void NewVersion_CheckCompleted(object sender, DownloadDataCompletedEventArgs e)
			{
			try
				{
				// If the request was not canceled and did not throw
				// an exception, display the resource.
				if (!e.Cancelled && e.Error == null)
					{
					myVerInfo vi = new myVerInfo();
					VersionInfo stvi = vi.GetInfos();
					string myVer = stvi.Version;
					byte[] data = (byte[])e.Result;
					string actVer = System.Text.Encoding.UTF8.GetString(data);
					if (myVer != actVer)
						{
						DialogResult r = MessageBox.Show(Program.appLng._i18n("version_new_available"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
						if (r == DialogResult.Yes)
							{
							WebTools.Goto17bWebSite();
							}
						}
					else if (bInformUser)
						{
						MessageBox.Show(Program.appLng._i18n("version_lastest_ok"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
						}
					}
				else
					{
					string szError = "Check Version:\n\n" + e.Error;
					MessageBox.Show(szError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			catch
				{
				string szError = "Check Version:\n\n" + e.Error;
				MessageBox.Show(szError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			bVerChecking = false;
			}

		}
	}
