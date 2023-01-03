using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLBox17b
	{
	public struct VersionInfo
		{
		public string Company;
		public string Filename;
		public string Product;
		public string Version;
		public string Comments;
		public bool Patched;
		public string Copyright;
		public string Trademark;
		public string Description;
		public string InternalName;
		}

	public class myVerInfo
		{
		public myVerInfo()
			{
			}

		public VersionInfo GetInfos()
			{
			VersionInfo vi;

			vi.Company = "";
			vi.Filename = "";
			vi.Product = "";
			vi.Version = "";
			vi.Comments = "";
			vi.Patched = false;
			vi.Copyright = "";
			vi.Trademark = "";
			vi.Description = "";
			vi.InternalName = "";

			try
				{
				string appExe = Application.ExecutablePath;

				System.Diagnostics.FileVersionInfo fileVersInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(appExe);

				vi.Company = fileVersInfo.CompanyName;
				vi.Filename = fileVersInfo.FileName;
				vi.Product = fileVersInfo.ProductName;
				vi.Version = fileVersInfo.FileVersion;
				vi.Comments = fileVersInfo.Comments;
				vi.Patched = fileVersInfo.IsPatched;
				vi.Copyright = fileVersInfo.LegalCopyright;
				vi.Trademark = fileVersInfo.LegalTrademarks;
				vi.Description = fileVersInfo.FileDescription;
				vi.InternalName = fileVersInfo.InternalName;
				}
			catch (Exception e)
				{
				string szError = "CSTLANG.LNG:\n\n" + e.Message;
				MessageBox.Show(szError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Application.Exit();
				}
			return vi;
			}
		}
	}
