using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
using SevenZip;
using FreeImageAPI;
using HLBox17b;
using HLBox17b.Classes.Tools;
using HLBox17b.Classes.Files;
using HLBox17b.Forms.Misc;
using HLBox17b.Externals;


namespace HLBox17b.Forms.MdiForms
	{
	public partial class WadMdi : MdiChild
		{
		public Size DefSz;
		List<Image> SavImages;
		private TexturesSorter lvTexturesSorter;
		public string szClipBrdStringId;
		public string szDragNDropGuid;
		public string szDefImgExportFormat;
		private const int MaxNameLength = 16;
		public ImageList texturesListe;
		private List<Texture> VirtualTextures;

		public WadMdi(string szFileName, bool bNew)
			{
			szDefImgExportFormat = "*." + Program.mRc.szTxs_DefImgType;
			szClipBrdStringId = "HLBoxTextureInfoItem";
			szDragNDropGuid = System.Guid.NewGuid().ToString();
			bIsModified = false;
			bIsNew = bNew;
			szFullFile = szFileName;
			if (szFullFile==null || szFullFile==string.Empty)
				this.Close();
			InitializeComponent();
			TranslateForm();

			SavImages = new List<Image>();
			lvTexturesSorter = new TexturesSorter();
			lvTexturesSorter.SortColumn = 0;
			lvTexturesSorter.Order = SortOrder.Ascending;

			VirtualTextures = new List<Texture>();
			texturesListe = new ImageList();
			DefSz = new Size(Program.mRc.nTxs_DefThumbSize, Program.mRc.nTxs_DefThumbSize);
			texturesListe.ImageSize = DefSz;
			texturesListe.ColorDepth = ColorDepth.Depth32Bit; // ColorDepth.Depth8Bit;
			lvTexView.View = View.LargeIcon;
			lvTexView.LargeImageList = texturesListe;
						
			menuItem_Showimages.Checked = true;
			Update_StatBar();
			mdichild_UpdateTitle();
			CheckEditMenusStates();
			statBar.Visible = true;
			Show_Loader(false, null);
			}

		private void TranslateForm()
			{
			//File
			menuMain_File.Text = Program.appLng._i18n("menu_file");
			menuItem_Save.Text = Program.appLng._i18n("waddlg_menu_save");
			menuItem_SaveAs.Text = Program.appLng._i18n("waddlg_menu_saveas");
			menuItem_Export.Text = Program.appLng._i18n("waddlg_menu_export");
			menuItem_Import.Text = Program.appLng._i18n("waddlg_menu_import");

			//Edit
			menuMain_Edit.Text = Program.appLng._i18n("waddlg_menu_edit");
			menuItem_Edit.Text = Program.appLng._i18n("waddlg_menu_edittex");
			menuItem_Show.Text = Program.appLng._i18n("waddlg_menu_showtex");
			menuItem_Copy.Text = Program.appLng._i18n("waddlg_menu_edit_copy");
			menuItem_Paste.Text = Program.appLng._i18n("waddlg_menu_edit_paste");
			menuItem_Cut.Text = Program.appLng._i18n("waddlg_menu_edit_cut");
			menuItem_Delete.Text = Program.appLng._i18n("waddlg_menu_edit_delete");
			menuItem_Delete.ShortcutKeyDisplayString = Program.appLng._i18n("waddlg_menu_edit_delshortcut");
			menuItem_Duplicate.Text = Program.appLng._i18n("waddlg_menu_edit_duplicate");

			//Show
			menuMain_Textures.Text = Program.appLng._i18n("menu_texes");
			menuSub_Show.Text = Program.appLng._i18n("waddlg_menu_view");
			menuItem_Showimages.Text = Program.appLng._i18n("waddlg_menu_view_images");
			menuItem_Showdetails.Text = Program.appLng._i18n("waddlg_menu_view_details");
			menuItem_Showtransparency.Text = Program.appLng._i18n("waddlg_menu_view_transparency");

			//ContextMenu
			contextItem_Edit.Text = Program.appLng._i18n("waddlg_menu_edittex");
			contextItem_Show.Text = Program.appLng._i18n("waddlg_menu_showtex");
			contextItem_Copy.Text = Program.appLng._i18n("waddlg_menu_edit_copy");
			contextItem_Paste.Text = Program.appLng._i18n("waddlg_menu_edit_paste");
			contextItem_Cut.Text = Program.appLng._i18n("waddlg_menu_edit_cut");
			contextItem_Delete.Text = Program.appLng._i18n("waddlg_menu_edit_delete");
			contextItem_Delete.ShortcutKeyDisplayString = Program.appLng._i18n("waddlg_menu_edit_delshortcut");
			contextItem_Duplicate.Text = Program.appLng._i18n("waddlg_menu_edit_duplicate");
			contextItem_Export.Text = Program.appLng._i18n("waddlg_menu_export");

			//Colonnes detail
			lvTexView.Columns[0].Text = Program.appLng._i18n("waddlg_col_header_name");
			lvTexView.Columns[1].Text = Program.appLng._i18n("common_width");
			lvTexView.Columns[2].Text = Program.appLng._i18n("common_height");
			lvTexView.Columns[3].Text = Program.appLng._i18n("common_size");
			}


		public bool CheckClose()
			{
			if (bIsModified)
				{
				string szMsg = Program.appLng._i18n("waddlg_msg_save_mods") + "\"" + Path.GetFileName(szFullFile) + "\" ?";

				DialogResult r = MessageBox.Show(szMsg, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

				if (r == DialogResult.Yes)
					{
					string szFileToSave = szFullFile;
					if (bIsNew || szFileToSave == "")
						{
						szFileToSave = Get_FileToSave();
						if (szFileToSave == "")
							return false;
						}
					if (!SaveWad(szFileToSave))
						return false;
					return true;
					}
				if (r == DialogResult.Cancel)
					{
					return false;
					}
				}
			return true;
			}


		private void wadmdi_OnFormClosing(object sender, FormClosingEventArgs e)
			{
			if (e.Cancel)
				return;
			
			bool Cancelling = false;
			
			if (e.CloseReason == CloseReason.UserClosing)
				{
				if (bIsModified)
					{
					string szMsg = Program.appLng._i18n("waddlg_msg_save_mods") + "\"" + Path.GetFileName(szFullFile) + "\" ?";

					DialogResult r = MessageBox.Show(szMsg, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					switch (r)
						{
						case DialogResult.Yes:
							string szFileToSave = szFullFile;
							if (bIsNew || szFileToSave == "")
								szFileToSave = Get_FileToSave();
							Cancelling = SaveWad(szFileToSave) ? false : true;
							break;
						case DialogResult.Cancel:
							Cancelling = true;
							break;
						default:
							Cancelling = false;
							break;
						}
					}
				}
			e.Cancel = Cancelling;
			}

		private string Get_FileToSave()
			{
			string szFile2Save = "";

			sFileDlg.AddExtension = true;
			sFileDlg.CheckFileExists = false;
			sFileDlg.CheckPathExists = true;
			sFileDlg.ValidateNames = true;
			sFileDlg.SupportMultiDottedExtensions = false;
			sFileDlg.DereferenceLinks = true;
			sFileDlg.Title = Program.appLng._i18n("common_savefile");
			sFileDlg.FilterIndex = 1;
			sFileDlg.DefaultExt = "*.wad";
			sFileDlg.Filter = Program.appLng._i18n("common_wadfiles");
			sFileDlg.InitialDirectory = Program.mRc.szTxs_LastPath;
			sFileDlg.FileName = bIsNew ? "" : szFullFile;
			sFileDlg.RestoreDirectory = true;

			if (sFileDlg.ShowDialog() == DialogResult.OK)
				{
				szFile2Save = sFileDlg.FileName;
				}
			return szFile2Save;
			}

		private bool SaveWad(string szFileName)
			{
			if (szFileName == string.Empty)
				return false;
			this.UseWaitCursor = true;
			Show_Loader(true, Program.appLng._i18n("common_saving"));
			if (WADFile.SaveWad(szFileName, VirtualTextures))
				{
				//Update Status, Cursor and Progress
				Show_Loader(false, Program.appLng._i18n("common_saved"));
				this.UseWaitCursor = false;
				szFullFile = szFileName;
				MainForm MdiPr = (MainForm)this.MdiParent;
				MdiPr.mainform_AddRecents(szFullFile);
				bIsModified = false;
				bIsNew = false;
				mdichild_UpdateTitle();
				return true;
				}
			return false;
			}
		

		private void wadmdi_OnShown(object sender, EventArgs e)
			{
			texturesListe.Images.Clear();
			VirtualTextures.Clear();
			lvTexView.Items.Clear();
			lbTexNames.Items.Clear();

			if (!bIsNew)
				{
				if (bgLoader.IsBusy != true)
					{
					this.UseWaitCursor = true;
					Show_Loader(true, Program.appLng._i18n("common_loading"));
					bgLoader.RunWorkerAsync();
					}
				}
			}

		private void BgLoader_DoWork(object sender, DoWorkEventArgs e)
			{
			BackgroundWorker worker = sender as BackgroundWorker;

			if (szFullFile == "")
				{
				e.Cancel = true;
				return;
				}
			//Initialisation
			int ProgMaxOps = 0;
			int nIdx = 0;
			Texture TxInfo = null;

			WADFile wd = new WADFile(szFullFile);

			if (wd.Open())
				{
				if (wd.ReadHeader())
					{
					ProgMaxOps = (int)wd.uiTotalTex;
					double dTotal = (double)(ProgMaxOps);
					for (nIdx = 0; nIdx < ProgMaxOps; nIdx++)
						{
						double dIndex = (double)(nIdx);
						double dProgressPercentage = (dIndex / dTotal);
						int iProgressPercentage = (int)(dProgressPercentage * 100);
						TxInfo = wd.ReadFullTex(nIdx);
						if (TxInfo.szName != "")
							{
							VirtualTextures.Add(TxInfo);
							worker.ReportProgress(iProgressPercentage);
							}
						}
					}
				wd.Close();
				}
			worker.ReportProgress(100, null);
			int Delay = (ProgMaxOps / 10) * 2;
			System.Threading.Thread.Sleep(Delay);
			}

		private void BgLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (Percent < 100)
				Update_Loader(Percent, String.Format(Program.appLng._i18n("common_progression"),e.ProgressPercentage.ToString()));
			else
				Update_Loader(100, Program.appLng._i18n("common_parsing"));
			}

		private void BgLoader_Completed(object sender, RunWorkerCompletedEventArgs e)
			{
			this.UseWaitCursor = false;
			if (e.Error != null)
				{
				MessageBox.Show("Error: " + e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Show_Loader(false, null);
				this.Close();
				return;
				}
			else
				{
				if (VirtualTextures.Count == 0)
					MessageBox.Show(Program.appLng._i18n("waddlg_msg_no_texes"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				Show_Loader(false, String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"),VirtualTextures.Count));
				lvTexView.BeginUpdate();
				SortTexView();
				lvTexView.VirtualListSize = VirtualTextures.Count;
				lvTexView.EndUpdate();
				bIsModified = false;
				bIsNew = false;
				mdichild_UpdateTitle();
				}
			}

		private void CheckEditMenusStates()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			if (SelectedItems.Count == 0)
				{
				menuItem_Copy.Enabled = false;
				menuItem_Edit.Enabled = false;
				menuItem_Show.Enabled = false;
				menuItem_Copy.Enabled = false;
				menuItem_Cut.Enabled = false;
				menuItem_Delete.Enabled = false;
				menuItem_Duplicate.Enabled = false;
				
				contextItem_Copy.Enabled = false;
				contextItem_Show.Enabled = false;
				contextItem_Edit.Enabled = false;
				contextItem_Cut.Enabled = false;
				contextItem_Delete.Enabled = false;
				contextItem_Duplicate.Enabled = false;

				}
			else
				{
				menuItem_Edit.Enabled = true;
				menuItem_Show.Enabled = true;
				menuItem_Copy.Enabled = true;
				menuItem_Cut.Enabled = true;
				menuItem_Delete.Enabled = true;
				menuItem_Duplicate.Enabled = true;
				
				contextItem_Show.Enabled = true;
				contextItem_Edit.Enabled = true;
				contextItem_Copy.Enabled = true;
				contextItem_Cut.Enabled = true;
				contextItem_Delete.Enabled = true;
				contextItem_Duplicate.Enabled = true;
				}

			if (Clipboard.ContainsData(szClipBrdStringId))
				{
				menuItem_Paste.Enabled = true;
				contextItem_Paste.Enabled = true;
				}
			else
				{
				menuItem_Paste.Enabled = false;
				contextItem_Paste.Enabled = false;
				}

			if (SelectedItems.Count == 1)
				{
				contextItem_Export.Enabled = true;
				menuItem_Export.Enabled = true;
				}
			else
				{
				contextItem_Export.Enabled = false;
				menuItem_Export.Enabled = false;
				}
			}



		private void Show_Loader(bool bState, string szInfo)
			{
			if (bState)
				{
				sb_Progress.Visible = true;
				sb_Infos.Visible = true;
				if (szInfo != null)
					sb_Infos.Text = szInfo;
				else
					sb_Infos.Text = string.Empty;
				sb_Name.Visible = false;
				sb_Size.Visible = false;
				}
			else
				{
				sb_Progress.Visible = false;
				sb_Infos.Visible = true;
				if (szInfo != null)
					sb_Infos.Text = szInfo;
				else
					sb_Infos.Text = string.Empty;
				sb_Name.Visible = true;
				sb_Size.Visible = true;
				}
			}

		private void Update_Loader(int Percent, string szInfo)
			{
			if (Percent > sb_Progress.Maximum)
				Percent = sb_Progress.Maximum;
			sb_Progress.Value = Percent;
			if (szInfo != null)
				sb_Infos.Text = szInfo;
			}

		private void Update_StatBar()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();
			if (SelectedItems.Count != 1)
				{
				sb_Name.Text = "";
				sb_Size.Text = "";
				}
			else
				{
				ListViewItem lvItem = SelectedItems[0];
				Texture txInfos = (Texture)lvItem.Tag;
				sb_Name.Text = txInfos.szName;
				sb_Size.Text = txInfos.uiWidth.ToString() + " x " + txInfos.uiHeight.ToString();
				}
			}

		private void Update_PhaseBar(string szPhase)
			{
			sb_Infos.Text = szPhase;
			}

		private void SortTexView()
			{
			VirtualTextures.Sort(lvTexturesSorter);
			lbTexNames.BeginUpdate();
			lbTexNames.Items.Clear();
			int Idx = 0;
			foreach (Texture TxInfo in VirtualTextures)
				{
				lbTexNames.Items.Add(TxInfo.szName);
				VirtualTextures[Idx].iLvwIdx = Idx;
				}
			lbTexNames.EndUpdate();
			}

		private string lvTexView_RenameItemIfNecessary(string szName)
			{
			if (szName.Length > (MaxNameLength - 1))
				szName = szName.Substring(0, MaxNameLength - 4) + "(0)";
			string szNewName = szName;
			string szPrefix = szName;
			string szIdx = string.Empty;
			List<string> itemsNames = new List<string>();
			foreach (Texture TxInfo in VirtualTextures)
				{
				itemsNames.Add(TxInfo.szName.ToLower());
				}
			bool bFound = true;
			int nCopy = 0;
			while (bFound)
				{
				if (itemsNames.Contains(szNewName.ToLower()))
					{
					nCopy++;
					szIdx = "(" + nCopy.ToString() + ")";
					if ((szPrefix.Length + szIdx.Length) > (MaxNameLength - 1))
						szPrefix = szPrefix.Substring(0, MaxNameLength - 1 - szIdx.Length);
					szNewName = szPrefix + szIdx;
					bFound = true;
					continue;
					}
				bFound = false;
				}
			return szNewName;
			}


		private void lvTexView_AddItem(Texture TxInfo)
			{
			if (this.InvokeRequired)
				{
				Invoke(new MethodInvoker(delegate() { lvTexView_AddItem(TxInfo); }));
				}
			else
				{
				//Modify name if already existing
				string NewStr = lvTexView_RenameItemIfNecessary(TxInfo.szName);
				TxInfo.szName = NewStr;
				TxInfo.iLvwIdx = VirtualTextures.Count;
				TxInfo.iImgIdx = -1;
				VirtualTextures.Add(TxInfo);
				lvTexView.VirtualListSize = VirtualTextures.Count;
				SortTexView();
				}
			}


		private int lbTexNames_GetItemIdx(string szItemTxt)
			{
			if (szItemTxt == null || szItemTxt == string.Empty)
				return -1;
			return lbTexNames.FindString(szItemTxt, -1);
			}

		private int lbTexNames_GetItemIdx(ListViewItem lvItem)
			{
			return lbTexNames_GetItemIdx(lvItem.Text);
			}


		private void lvTexView_RemoveItem(ListViewItem lvItem)
			{
			int ItemIdx = -1;
			//Search index in Virtual list
			Texture TxInfo =  VirtualTextures.Find(x=>x.szName==lvItem.Text);
			ItemIdx = VirtualTextures.IndexOf(TxInfo);
			//Delete in virtual list
			if (ItemIdx==-1)
				return;
			VirtualTextures.RemoveAt(ItemIdx);
			//Change Virtual List Size
			lvTexView.VirtualListSize = VirtualTextures.Count;
			//Recalculate indexes of following items
			for (int i = ItemIdx; i < VirtualTextures.Count; i++)
				{
				VirtualTextures[i].iLvwIdx--;
				}
			//Search index in listbox
			ItemIdx = lbTexNames_GetItemIdx(lvItem.Text);
			if (ItemIdx == -1)
				return;
			//Remove from Names Listbox
			lbTexNames.Items.RemoveAt(ItemIdx);
			//Change virtual list size to redraw
			}

		private List<ListViewItem> lvTexView_GetSelectedIndexes()
			{
			List<ListViewItem> SelectedItems = new List<ListViewItem>();
			for (int i = 0; i < this.lvTexView.SelectedIndices.Count; i++)
				{
				SelectedItems.Add((ListViewItem)lvTexView.Items[lvTexView.SelectedIndices[i]]);
				}
			return SelectedItems;
			}


		#region Menus_Clicks

		private void menuItem_Save_Click(object sender, EventArgs e)
			{
			string szFileToSave = szFullFile;
			if (bIsNew)
				{
				szFileToSave = Get_FileToSave();
				if (szFileToSave == "")
					return;
				}
			SaveWad(szFileToSave);
			}

		private void menuItem_SaveAs_Click(object sender, EventArgs e)
			{
			string szFileToSave = Get_FileToSave();
			if (szFileToSave == "")
				return;
			SaveWad(szFileToSave);
			return;
			}

		private void menuItem_Copy_Click(object sender, EventArgs e)
			{
			DoCopy();
			}

		private void menuItem_Paste_Click(object sender, EventArgs e)
			{
			DoPaste();
			}

		private void menuItem_Cut_Click(object sender, EventArgs e)
			{
			DoCut();
			}

		private void menuItem_Delete_Click(object sender, EventArgs e)
			{
			DoDelete();
			}

		private void menuItem_Duplicate_Click(object sender, EventArgs e)
			{
			DoDuplicate();
			}


		private void menuItem_Addtexture_Click(object sender, EventArgs e)
			{
			AddTexture dDlg = new AddTexture();
			DialogResult dlgRes = dDlg.ShowDialog();
			if (dlgRes == DialogResult.OK)
				{
				Program.mRc.nTxs_LastNewWidth = dDlg.texWidth;
				Program.mRc.nTxs_LastNewHeight = dDlg.texHeight;
				Texture TxInfo = new Texture();
				bool reserveLastClr = (dDlg.TexName.StartsWith("{"));
				Color bgColor1 = reserveLastClr ? Color.Blue : Color.FromArgb(Program.mRc.nTxs_NewColor1);
				Color bgColor2 = reserveLastClr ? Color.Blue : Color.FromArgb(Program.mRc.nTxs_NewColor2);
				Bitmap nulBmp = GraphTools.CreateNullBitmap(dDlg.texWidth, dDlg.texHeight, bgColor1, bgColor2);
				TxInfo = GraphTools.GetTxInfoFromBitmap(nulBmp, reserveLastClr);
				nulBmp.Dispose();
				TxInfo.szName = dDlg.TexName.Trim();
				lvTexView_AddItem(TxInfo);
				bIsModified = true;
				mdichild_UpdateTitle();
				Update_StatBar();
				Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));				
				}
			dDlg.Dispose();
			}

		private void menuItem_ViewImages_Click(object sender, EventArgs e)
			{
			lvTexView.View = View.LargeIcon;
			menuItem_Showdetails.Checked = false;
			menuItem_Showimages.Checked = true;
			}

		private void menuItem_ViewDetails_Click(object sender, EventArgs e)
			{
			lvTexView.View = View.Details;
			menuItem_Showdetails.Checked = true;
			menuItem_Showimages.Checked = false;
			}

		private void menuItem_ViewTransparency_Click(object sender, EventArgs e)
			{
			if (menuItem_Showtransparency.Checked == true)
				{
				lvTexView.LargeImageList.TransparentColor = Color.Transparent;
				menuItem_Showtransparency.Checked = false;
				}
			else
				{
				lvTexView.LargeImageList.TransparentColor = Color.Blue;
				menuItem_Showtransparency.Checked = true;
				}

			//On doit recréer toutes les icones transparentes
			foreach (Texture TxInfo in VirtualTextures)
				{
				//Si la Texture ne dispose pas de transparence on ne fait rien
				if (!(TxInfo.szName.StartsWith("{")))
					continue;
				//La texture n'a pas encore été calculée
				if (TxInfo.iImgIdx == -1)
					continue;
				//On recrée un bitmap basé sur l'existant
				Image img = GraphTools.RescaleImage(TxInfo.Image, DefSz, lvTexView.BackColor);
				texturesListe.Images[TxInfo.iImgIdx] = img;
				lvTexView.RedrawItems((int) TxInfo.iLvwIdx, (int) TxInfo.iLvwIdx,true);
				}
			}


		private void menuItem_Import_Click(object sender, EventArgs e)
			{
			importImgDlg.AddExtension = false;
			importImgDlg.CheckFileExists = true;
			importImgDlg.CheckPathExists = true;
			importImgDlg.ValidateNames = true;
			importImgDlg.SupportMultiDottedExtensions = false;
			importImgDlg.ShowReadOnly = false;
			importImgDlg.DereferenceLinks = true;
			importImgDlg.Title = Program.appLng._i18n("waddlg_menu_import");
			importImgDlg.FilterIndex = 1;
			importImgDlg.Multiselect = true;
			importImgDlg.DefaultExt = "";
			importImgDlg.Filter = Program.appLng._i18n("common_opn_imgfiles");
			importImgDlg.InitialDirectory = Program.mRc.szTxs_LastImportPath;
			if (importImgDlg.ShowDialog() == DialogResult.OK)
				{
				Program.mRc.szTxs_LastImportPath = Path.GetDirectoryName(importImgDlg.FileNames[0]);
				Start_Importation(importImgDlg.FileNames);
				}
			}

		private void menuItem_Export_Click(object sender, EventArgs e)
			{
			DoExport();
			}

		private void menuItem_Edit_Click(object sender, EventArgs e)
			{
			EditFile();
			}

		private void menuItem_Show_Click(object sender, EventArgs e)
			{
			ShowFile();
			}

		#endregion

		#region ListView_Items_Actions

		private void ShowFile()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			if (SelectedItems.Count != 1)
				return;
			int AnimImg = 0;
			List<Texture> arrTextures = new List<Texture>();
			ListViewItem SelItem = SelectedItems[0];

			uint imgW = ((Texture)SelItem.Tag).uiWidth;
			uint imgH = ((Texture)SelItem.Tag).uiHeight;

			string pat = @"\+\d(?<Onlytxt>.*)";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase);
			Match m = r.Match(SelItem.Text);
			if (m.Success)
				{
				string szOnlyName = m.Groups["Onlytxt"].Value;
				szOnlyName = szOnlyName.Replace("(", @"\(");
				szOnlyName = szOnlyName.Replace(")", @"\)");
				szOnlyName = szOnlyName.Replace("[", @"\[");
				szOnlyName = szOnlyName.Replace("]", @"\]");
				szOnlyName = szOnlyName.Replace("+", @"\+");
				szOnlyName = szOnlyName.Replace("-", @"\-");
				szOnlyName = szOnlyName.Replace(".", @"\.");
				pat = @"\+\d" + szOnlyName;
				r = new Regex(pat, RegexOptions.IgnoreCase);
				foreach (Texture TexInfo in VirtualTextures)
					{
					Match m2 = r.Match(TexInfo.szName);
					if (m2.Success)
						{
						uint lvW = TexInfo.uiWidth;
						uint lvH = TexInfo.uiHeight;
						if (lvW == imgW && lvH == imgH)
							{
							if (TexInfo.szName == SelItem.Text)
								AnimImg = arrTextures.Count;
							arrTextures.Add(TexInfo);
							}
						}
					}
				}
			else
				{
				arrTextures.Add((Texture)SelItem.Tag);
				AnimImg = 0;
				}
			Texture TxInfo = (Texture)SelItem.Tag;
			WadView dDlg = new WadView(arrTextures, AnimImg);
			dDlg.ShowDialog();
			}


		private void EditFile()
			{
			if (Program.mRc.szTxs_DefEditor == "")
				{
				MessageBox.Show(Program.appLng._i18n("waddlg_msg_noeditor"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
				}

			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			foreach (ListViewItem itm in SelectedItems)
				{
				if (itm.Index == lbTexNames.SelectedIndex)
					continue;
				itm.Selected = false;
				}

			lvTexView.Items[lbTexNames.SelectedIndex].Focused = true;
			lvTexView.Items[lbTexNames.SelectedIndex].Selected = true;

			ListViewItem item = lvTexView.Items[lbTexNames.SelectedIndex];
			Texture TxInfo = (Texture)item.Tag;


			if (!FilesTools.Create_Temp_Folder())
				return;
			this.UseWaitCursor = true;
			string szImgName = TxInfo.szName;

			string szTmpImgFile = Path.Combine(Program.mRc.szHlBoxTmpPath, szImgName + ".bmp");// + Program.mRc.szTxs_ExtFormat);

			Bitmap PngImg = TxInfo.Image;
			PngImg.Save(szTmpImgFile, ImageFormat.Bmp);
			PngImg.Dispose();
			if (FilesTools.CheckFile(szTmpImgFile))
				{
				DateTime firstWriteTime = File.GetLastWriteTime(szTmpImgFile);
				ProcessStartInfo editor = new ProcessStartInfo();
				editor.FileName = Program.mRc.szTxs_DefEditor;
				editor.Arguments = szTmpImgFile;
				Process p = Process.Start(editor);
				this.UseWaitCursor = false;
				p.WaitForExit();
				DateTime lastWriteTime = File.GetLastWriteTime(szTmpImgFile);
				if (lastWriteTime.Ticks > firstWriteTime.Ticks)
					{
					PngImg = new Bitmap(szTmpImgFile);
					bool reserveLastClr = (item.Text.StartsWith("{"));
					Texture newTxInfo = GraphTools.GetTxInfoFromBitmap(PngImg, reserveLastClr);
					PngImg.Dispose();
					if (newTxInfo.szName == "##!!##")
						{
						MessageBox.Show(Program.appLng._i18n("waddlg_badtexturedimensions"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FilesTools.Delete_Temp_Folder();
						lvTexView.Items[lbTexNames.SelectedIndex].Focused = true;
						lvTexView.Items[lbTexNames.SelectedIndex].Selected = true;
						p.Dispose();
						return;
						}
					newTxInfo.szName = TxInfo.szName;
					newTxInfo.iLvwIdx = item.Index;
					newTxInfo.iImgIdx = TxInfo.iImgIdx;
					VirtualTextures[newTxInfo.iLvwIdx] = newTxInfo;
					if (newTxInfo.iImgIdx != -1)
						{
						Image newImg = GraphTools.RescaleImage(newTxInfo.Image, DefSz, lvTexView.BackColor);
						texturesListe.Images[newTxInfo.iImgIdx] = newImg;
						}
					bIsModified = true;
					mdichild_UpdateTitle();
					Update_StatBar();
					lvTexView.RedrawItems(newTxInfo.iLvwIdx, newTxInfo.iLvwIdx, false);
					}
				FilesTools.Delete_Temp_Folder();
				lvTexView.Items[lbTexNames.SelectedIndex].Focused = true;
				lvTexView.Items[lbTexNames.SelectedIndex].Selected = true;
				p.Dispose();
				}
			}


		private void DoCopy()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			if (SelectedItems.Count == 0)
				return;

			ClipBoardItemsList ClpList = new ClipBoardItemsList();
			foreach (ListViewItem lvItem in SelectedItems)
				{
				Texture TxTmp = (Texture)lvItem.Tag;
				SerialTexture SrTxTmp = new SerialTexture();
				SrTxTmp.szName = TxTmp.szName;
				SrTxTmp.uiDiskLength = TxTmp.uiDiskLength;
				SrTxTmp.uiHeight = TxTmp.uiHeight;
				SrTxTmp.uiWidth = TxTmp.uiWidth;
				SrTxTmp.Image = GraphTools.ImageToByte(TxTmp.Image);
				SrTxTmp.rawImg = TxTmp.rawImg;
				SrTxTmp.Mipmaps = TxTmp.Mipmaps;
				SrTxTmp.rawPal = TxTmp.rawPal;
				SrTxTmp.iLvwIdx = TxTmp.iLvwIdx;
				SrTxTmp.iImgIdx = TxTmp.iImgIdx;
				ClpList.Add(SrTxTmp);
				}
			Clipboard.SetData(szClipBrdStringId, ClpList);
			CheckEditMenusStates();
			}

		private void DoPaste()
			{
			if (Clipboard.ContainsData(szClipBrdStringId))
				{
				ClipBoardItemsList ClpList = (ClipBoardItemsList)Clipboard.GetData(szClipBrdStringId);
				if (ClpList == null)
					return;
				this.UseWaitCursor = true;
				foreach (SerialTexture SrTxTmp in ClpList.ItemsList)
					{
					Texture TxInfo = new Texture();
					TxInfo.szName = SrTxTmp.szName;
					TxInfo.uiDiskLength = SrTxTmp.uiDiskLength;
					TxInfo.uiHeight = SrTxTmp.uiHeight;
					TxInfo.uiWidth = SrTxTmp.uiWidth;
					TxInfo.Image = GraphTools.ByteToImage(SrTxTmp.Image);
					TxInfo.rawImg = SrTxTmp.rawImg;
					TxInfo.Mipmaps = SrTxTmp.Mipmaps;
					TxInfo.rawPal = SrTxTmp.rawPal;
					TxInfo.iLvwIdx = SrTxTmp.iLvwIdx;					
					TxInfo.iImgIdx = SrTxTmp.iImgIdx;
					lvTexView_AddItem(TxInfo);
					}
				this.UseWaitCursor = false;
				CheckEditMenusStates();
				bIsModified = true;
				mdichild_UpdateTitle();
				Update_StatBar();
				Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));
				}
			}

		private void DoCut()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			if (SelectedItems.Count == 0)
				return;

			lvTexView.BeginUpdate();
			lbTexNames.BeginUpdate();
			ClipBoardItemsList ClpList = new ClipBoardItemsList();
			foreach (ListViewItem lvItem in SelectedItems)
				{
				Texture TxTmp = (Texture)lvItem.Tag;
				SerialTexture SrTxTmp = new SerialTexture();
				SrTxTmp.szName = TxTmp.szName;
				SrTxTmp.uiDiskLength = TxTmp.uiDiskLength;
				SrTxTmp.uiHeight = TxTmp.uiHeight;
				SrTxTmp.uiWidth = TxTmp.uiWidth;
				SrTxTmp.Image = GraphTools.ImageToByte(TxTmp.Image);
				SrTxTmp.rawImg = TxTmp.rawImg;
				SrTxTmp.Mipmaps = TxTmp.Mipmaps;
				SrTxTmp.rawPal = TxTmp.rawPal;
				SrTxTmp.iLvwIdx = TxTmp.iLvwIdx;
				SrTxTmp.iImgIdx = TxTmp.iImgIdx;
				ClpList.Add(SrTxTmp);
				lvTexView_RemoveItem(lvItem);
				}
			Clipboard.SetData(szClipBrdStringId, ClpList);
			lvTexView.SelectedIndices.Clear();
			lbTexNames.EndUpdate();
			lvTexView.EndUpdate();
			CheckEditMenusStates();
			bIsModified = true;
			mdichild_UpdateTitle();
			Update_StatBar();
			Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));
			}

		private void DoDuplicate()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			if (SelectedItems.Count == 0)
				return;
			Texture TxInfo = new Texture();
			this.UseWaitCursor = true;
			foreach (ListViewItem lvItem in SelectedItems)
				{
				TxInfo = (Texture)lvItem.Tag;
				lvTexView_AddItem(TxInfo);
				}
			this.UseWaitCursor = false;
			CheckEditMenusStates();
			bIsModified = true;
			mdichild_UpdateTitle();
			Update_StatBar();
			Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));
			}

		private void DoDelete()
			{
			List<ListViewItem> lvItms = new List<ListViewItem>();

			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			foreach (ListViewItem itm in SelectedItems)
				lvItms.Add(itm);

			if (lvItms.Count == 0)
				return;

			string szMsg = "";

			if (lvItms.Count == 1)
				szMsg = Program.appLng._i18n("waddlg_msg_delete_one");
			else
				szMsg = String.Format(Program.appLng._i18n("waddlg_msg_delete_mul"), lvItms.Count);

			DialogResult r = MessageBox.Show(szMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (r == DialogResult.Yes)
				{
				lvTexView.BeginUpdate();
				lbTexNames.BeginUpdate();
				foreach (ListViewItem lvItem in lvItms)
					{
					lvTexView_RemoveItem(lvItem);
					}
				lvTexView.SelectedIndices.Clear();
				lbTexNames.EndUpdate();
				lvTexView.EndUpdate();
				bIsModified = true;
				mdichild_UpdateTitle();
				Update_StatBar();
				Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));
				}
			CheckEditMenusStates();
			}		
#endregion

		#region import_export

		private void DoExport()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			ListViewItem lvItem = SelectedItems[0];

			SaveFileDialog saveImgFileDlg = new SaveFileDialog();
			saveImgFileDlg.Filter = Program.appLng._i18n("common_sav_imgfiles");
			saveImgFileDlg.Title = Program.appLng._i18n("common_savefile");
			saveImgFileDlg.AddExtension = true;
			saveImgFileDlg.CheckFileExists = false;
			saveImgFileDlg.CheckPathExists = true;
			saveImgFileDlg.ValidateNames = true;
			saveImgFileDlg.SupportMultiDottedExtensions = false;
			saveImgFileDlg.DereferenceLinks = true;
			saveImgFileDlg.Title = Program.appLng._i18n("common_savefile");
			saveImgFileDlg.FilterIndex = 1;
			saveImgFileDlg.DefaultExt = szDefImgExportFormat;

			saveImgFileDlg.InitialDirectory = Program.mRc.szTxs_LastExportPath;
			saveImgFileDlg.FileName = lvItem.Text;
			saveImgFileDlg.RestoreDirectory = true;

			if (saveImgFileDlg.ShowDialog() == DialogResult.OK)
				{
				Texture txInfo = (Texture)lvItem.Tag;
				Bitmap bmTexture = new Bitmap(txInfo.Image);
				string szExt = Path.GetExtension(saveImgFileDlg.FileName).ToLower();
				Program.mRc.szTxs_LastExportPath = Path.GetDirectoryName(saveImgFileDlg.FileName);
				switch (szExt)
					{
					case ".tga":
						using (FreeImageBitmap frBmp = new FreeImageBitmap(bmTexture))
							{
							frBmp.Save(saveImgFileDlg.FileName, FREE_IMAGE_FORMAT.FIF_TARGA);
							}
						break;
					case ".bmp":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
						break;
					case ".png":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
						break;
					case ".gif":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Gif);
						break;
					case ".jpg":
					case ".jpeg":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
						break;
					default:
						MessageBox.Show(Program.appLng._i18n("waddlg_msg_bad_image_format"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					}
				bmTexture.Dispose();
				}
			}

		private void DoImport(string szFile)
			{
			if (Directory.Exists(szFile))
				return;
			try
				{
				FileInfo fi = new FileInfo(szFile);

				//Modify name if already existing
				string NewStr = lvTexView_RenameItemIfNecessary(FilesTools.GetOnlyName(szFile));
				//Remplissage Minimum de TxInfo
				Texture TxInfo = new Texture();

				bool reserveLastClr = (NewStr.StartsWith("{"));

				FREE_IMAGE_FORMAT fif = FREE_IMAGE_FORMAT.FIF_UNKNOWN;
				fif = FreeImage.GetFileType(szFile,0);
				if (fif == FREE_IMAGE_FORMAT.FIF_GIF)
					{
					GifDecoder gifDecoder = new GifDecoder();
					gifDecoder.Read(szFile);
					if (gifDecoder.GetFrameCount() >1)
						{
						DoImport_Animated(szFile);
						return;
						}
					}
				using (FreeImageBitmap frBmp = new FreeImageBitmap(szFile))
					{
					Bitmap bit = frBmp.ToBitmap();
					TxInfo = GraphTools.GetTxInfoFromBitmap(bit, reserveLastClr);
					bit.Dispose();
					if (TxInfo.szName == "##!!##")
						{
						MessageBox.Show(Program.appLng._i18n("waddlg_badtexturedimensions"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
						}
					TxInfo.szName = NewStr;
					lvTexView_AddItem(TxInfo);
					}
				}
			catch (Exception exp)
				{
				string szError = Program.appLng._i18n("common_msg_error_opening_file") + ":\n\n" + exp.Message;
				MessageBox.Show(szError, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

		private void DoImport_Animated(string szFile)
			{
			Texture TxInfo = new Texture();

			string TexOnlyName = FilesTools.GetOnlyName(szFile);

			GifDecoder gifDecoder = new GifDecoder();
			gifDecoder.Read(szFile);

			for (int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++)
				{
				Image frame = gifDecoder.GetFrame(i);
				Bitmap bit = new Bitmap(frame);
				frame.Dispose();
				TxInfo = GraphTools.GetTxInfoFromBitmap(bit, false);
				bit.Dispose();
				if (TxInfo.szName == "##!!##")
					{
					MessageBox.Show(Program.appLng._i18n("waddlg_badtexturedimensions"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
					}
				TxInfo.szName = lvTexView_RenameItemIfNecessary("+" + i.ToString() + TexOnlyName);
				lvTexView_AddItem(TxInfo);
				}
			}

		private void Start_Importation(string [] szFiles)
			{
			if (bgImporter.IsBusy != true)
				{
				if (szFiles.Length > 1)
					{
					this.UseWaitCursor = true;
					Show_Loader(true, Program.appLng._i18n("common_loading"));
					}
				bgImporter.RunWorkerAsync(szFiles);
				}
			}

		private void bgImporter_DoWork(object sender, DoWorkEventArgs e)
			{
			BackgroundWorker worker = sender as BackgroundWorker;

			string[] szFiles = (string[])e.Argument;

			int ProgMaxOps = szFiles.Length;
			double dTotal = (double)(ProgMaxOps);

			for (int i = 0; i < szFiles.Length; i++)
				{
				double dIndex = (double)(i);
				double dProgressPercentage = (dIndex / dTotal);
				int iProgressPercentage = (int)(dProgressPercentage * 100);
				worker.ReportProgress(iProgressPercentage);
				DoImport(szFiles[i]);
				}
			worker.ReportProgress(100, null);
			int Delay = (ProgMaxOps / 10) * 2;
			System.Threading.Thread.Sleep(Delay);
			}

		private void bgImporter_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (Percent < 100)
				Update_Loader(Percent, String.Format(Program.appLng._i18n("common_progression"), e.ProgressPercentage.ToString()));
			else
				Update_Loader(100, Program.appLng._i18n("common_parsing"));
			}

		private void bgImporter_Completed(object sender, RunWorkerCompletedEventArgs e)
			{
			this.UseWaitCursor = false;
			Show_Loader(false, null);
			//Update Status
			bIsModified = true;
			mdichild_UpdateTitle();
			Update_StatBar();
			Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));
			}

		#endregion

		#region lvTexView_events

		private void lvTexView_OnKeyDown(object sender, KeyEventArgs e)
			{
			switch (e.KeyCode)
				{
				case Keys.C:
					if (e.Control)
						DoCopy();
					break;
				case Keys.V:
					if (e.Control)
						DoPaste();
					break;
				case Keys.X:
					if (e.Control)
						DoCut();
					break;
				case Keys.D:
					if (e.Control)
						DoDuplicate();
					break;
				case Keys.Delete:
					DoDelete();
					break;
				case Keys.F2:
					lvTexView_OnBeginLabelEdit();
					break;
				}
			}

		private void lvTexView_OnBeginLabelEdit()
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			if (SelectedItems.Count != 1)
				return;
			ListViewItem lvItem = SelectedItems[0];
			lvItem.BeginEdit();
			}

		private void lvTexView_OnAfterLabelEdit(object sender, LabelEditEventArgs e)
			{
			//Pas de modifs
			if (e.Label == null)
				return;
			//Entrée d'une chaine nulle
			string NewStr = e.Label;
			if (NewStr.Length == 0)
				{
				MessageBox.Show(Program.appLng._i18n("waddlg_msg_cantbeempty"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.CancelEdit = true;
				return;
				}
			//Caractères non autorisés
			string InvalidCharsStr = FilesTools.InitInvalidChars();
			string wadNameinvalids = InvalidCharsStr + "\\\\" + ":";
			if (Regex.IsMatch(NewStr, InvalidCharsStr))
				{
				MessageBox.Show(Program.appLng._i18n("waddlg_msg_illegalchars"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.CancelEdit = true;
				return;
				}
			NewStr = NewStr.Replace(" ", "_");
			//sauvegarde du texte original
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();
			string szOriginalName = SelectedItems[0].Text;
			if (NewStr.Length > (MaxNameLength - 1))
				{
				NewStr = NewStr.Substring(0, (MaxNameLength - 1));
				MessageBox.Show(Program.appLng._i18n("waddlg_msg_truncateto15"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				SelectedItems[0].Text = NewStr;
				e.CancelEdit = true;
				}
			//Entrée déjà existante
			foreach (ListViewItem itm in lvTexView.Items)
				{
				if (NewStr.Equals(itm.Text, StringComparison.OrdinalIgnoreCase))
					{
					if (itm.Index != e.Item)
						{
						MessageBox.Show(Program.appLng._i18n("waddlg_msg_existing_name"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						e.CancelEdit = true;
						SelectedItems[0].Text = szOriginalName;
						return;
						}
					}
				}
			ListViewItem lvItem = SelectedItems[0];
			Texture txInfos = (Texture)lvItem.Tag;
			txInfos.szName = NewStr;
			lvItem.Tag = txInfos;
			//Change Name in Listbox
			lbTexNames.Items[lvItem.Index] = NewStr;
			////////////////////////
			bIsModified = true;
			mdichild_UpdateTitle();
			Update_StatBar();
			CheckEditMenusStates();
			}

		private void lvTexView_OnMouseClick(object sender, MouseEventArgs e)
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			lbTexNames.BeginUpdate();
			lbTexNames.ClearSelected();
			foreach (ListViewItem lvItem in SelectedItems)
				{
				lbTexNames.SetSelected(lvItem.Index, true);
				}

			lbTexNames.EndUpdate();
			Update_StatBar();
			CheckEditMenusStates();
			}

		private void lvTexView_OnDoubleClick(object sender, EventArgs e)
			{
			ShowFile();
			}

		private void lvTexView_OnColumnClick(object sender, ColumnClickEventArgs e)
			{
			if (e.Column == lvTexturesSorter.SortColumn)
				{
				if (lvTexturesSorter.Order == SortOrder.Ascending)
					lvTexturesSorter.Order = SortOrder.Descending;
				else
					lvTexturesSorter.Order = SortOrder.Ascending;
				}
			else
				{
				lvTexturesSorter.SortColumn = e.Column;
				lvTexturesSorter.Order = SortOrder.Ascending;
				}
			lvTexView.BeginUpdate();
			SortTexView();
			lvTexView.VirtualListSize = VirtualTextures.Count;
			lvTexView.EndUpdate();
			}

		private void lvTexView_OnSelectedIndexChanged(object sender, EventArgs e)
			{
			CheckEditMenusStates();
			}

		private void lvTexView_OnItemDrag(object sender, ItemDragEventArgs e)
			{
			MainForm MdiPr = (MainForm)this.MdiParent;
			MdiPr.szDragSource = szDragNDropGuid;
			MdiPr.bDragOnListClient = true;

			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();

			ListViewItem[] myItemsT = new ListViewItem[SelectedItems.Count];
			int i = 0;
			foreach (ListViewItem myItemT in SelectedItems)
				{
				myItemsT[i] = myItemT;
				i = i + 1;
				}
			this.DoDragDrop(new DataObject("System.Windows.Forms.ListViewItem()", myItemsT), DragDropEffects.All);
			}

		private void lvTexView_OnDragDrop(object sender, DragEventArgs e)
			{
			List<string> FilesToImport = new List<string>();
			MainForm MdiPr = (MainForm)this.MdiParent;

			if (MdiPr.szDragSource != "")
				{
				if (MdiPr.szDragSource != szDragNDropGuid)
					{
					Texture TxInfo = new Texture();
					MdiPr.szDragSource = "";
					MdiPr.bDragOnListClient = false;
					ListViewItem[] myItemsT = (ListViewItem[])e.Data.GetData("System.Windows.Forms.ListViewItem()", false);
					foreach (ListViewItem lvItem in myItemsT)
						{
						TxInfo = (Texture)lvItem.Tag;
						lvTexView_AddItem(TxInfo);
						}
					bIsModified = true;
					mdichild_UpdateTitle();
					Update_StatBar();
					Update_PhaseBar(String.Format(Program.appLng._i18n("waddlg_msg_totalimgs"), lvTexView.Items.Count));
					}
				}
			else
				{
				string[] handles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				foreach (string s in handles)
					{
					if (File.Exists(s))
						{
						int bType = 0;
						//Search for tex files
						string lookfortexs = ".wad;.bsp;.vtf;.vmt";
						string[] extensions_texs = lookfortexs.Split(new char[] { ';' });
						foreach (string szX in extensions_texs)
							{
							if (string.Compare(Path.GetExtension(s), szX, true) == 0)
								{
								bType = 1;
								break;
								}
							}
						//Search for img files
						if (bType == 0)
							{
							string lookforimgs = ".png;.jpg;.jpeg;.gif;.bmp;.tga";
							string[] extensions_imgs = lookforimgs.Split(new char[] { ';' });
							foreach (string szX in extensions_imgs)
								{
								if (string.Compare(Path.GetExtension(s), szX, true) == 0)
									{
									bType = 2;
									break;
									}
								}
							}
						switch (bType)
							{
							case 1:
								MdiPr.mainform_DropDownFile(s);
								break;
							case 2:
								FilesToImport.Add(s);
								break;
							default:
								break;
							}
						}
					else if (Directory.Exists(s))
						{
						string lookforimgspattern = "*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tga";
						string[] extensions_imgspattern = lookforimgspattern.Split(new char[] { ';' });
						DirectoryInfo di = new DirectoryInfo(s);
						foreach (string srext in extensions_imgspattern)
							{
							FileInfo[] files = di.GetFiles(srext);
							foreach (FileInfo fiInfo in files)
								{
								FilesToImport.Add(fiInfo.FullName);
								}
							}
						}
					}
				}
			if (FilesToImport.Count != 0)
				{
				Start_Importation(FilesToImport.ToArray());
				}
			}

		private void lvTexView_OnDragEnter(object sender, DragEventArgs e)
			{
			MainForm MdiPr = (MainForm)this.MdiParent;
			MdiPr.bDragOnListClient = true;

			if (MdiPr.szDragSource == szDragNDropGuid)
				MdiPr.bDragOnListClient = false;
			}

		private void lvTexView_OnDragLeave(object sender, EventArgs e)
			{
			MainForm MdiPr = (MainForm)this.MdiParent;
			MdiPr.bDragOnListClient = false;
			}

		private void lvTexView_OnDragOver(object sender, DragEventArgs e)
			{
			MainForm MdiPr = (MainForm)this.MdiParent;
			if (MdiPr.szDragSource != "")
				{
				if (e.Data.GetDataPresent("System.Windows.Forms.ListViewItem()"))
					{
					if ((e.KeyState & 4) == 4 && (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
						e.Effect = DragDropEffects.Move;
					else
						e.Effect = DragDropEffects.Copy;
					}
				else
					{
					e.Effect = DragDropEffects.None;
					}
				}
			else
				{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
					e.Effect = DragDropEffects.Copy;
				}
			}

#endregion

		#region lbTexNames_events

		private void lbTexNames_OnDoubleClick(object sender, MouseEventArgs e)
			{
			EditFile();
			}

		private void lbTexNames_OnClick(object sender, MouseEventArgs e)
			{
			List<ListViewItem> SelectedItems = lvTexView_GetSelectedIndexes();
			//Récupération des Noms sélectionnés
			List<int> lbSelIndexes = new List<int>();
			foreach (int idx in lbTexNames.SelectedIndices)
				{
				lbSelIndexes.Add(idx);
				}
			foreach (ListViewItem itm in SelectedItems)
				{
				if (!lbSelIndexes.Contains(itm.Index))
					{
					itm.Selected = false;
					}
				}
			foreach (int nIdx in lbSelIndexes)
				{
				lvTexView.Items[nIdx].Focused = true;
				lvTexView.Items[nIdx].Selected = true;
				if (lbSelIndexes.Count == 1)
					lvTexView.EnsureVisible(nIdx);
				}
			lvTexView.Focus();
			}
		#endregion

		private ListViewItem[] myCache; //array to cache items for the virtual list 
		private int firstItem; //stores the index of the first item in the cache

		private void lvTexView_OnRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
			{
			//Caching is not required but improves performance on large sets. 
			//To leave out caching, don't connect the CacheVirtualItems event  
			//and make sure myCache is null. 

			//check to see if the requested item is currently in the cache 
			if (myCache != null && e.ItemIndex >= firstItem && e.ItemIndex < firstItem + myCache.Length)
				{
				//A cache hit, so get the ListViewItem from the cache instead of making a new one.
				e.Item = myCache[e.ItemIndex - firstItem];
				}
			else
				{
				if (e.Item == null)
					{
					Texture TxInfo = VirtualTextures[e.ItemIndex];
					ListViewItem Texitem = new ListViewItem();
					Texitem.Tag = TxInfo;
					Texitem.Text = TxInfo.szName;
					Texitem.SubItems.Add(TxInfo.uiWidth.ToString());
					Texitem.SubItems.Add(TxInfo.uiHeight.ToString());
					Texitem.SubItems.Add(TxInfo.uiDiskLength.ToString());
					Texitem.ImageIndex = TxInfo.iImgIdx;
					if (TxInfo.iImgIdx == -1)
						{
						VirtualTextures[e.ItemIndex].iLvwIdx = e.ItemIndex;
						VirtualTextures[e.ItemIndex].iImgIdx = texturesListe.Images.Count;
						Texitem.ImageIndex = VirtualTextures[e.ItemIndex].iImgIdx;
						Bitmap bmp = GraphTools.RescaleImage(TxInfo.Image, DefSz, lvTexView.BackColor);
						texturesListe.Images.Add(bmp);
						}
					e.Item = Texitem;
					}
				}
			}

		//Manages the cache.  ListView calls this when it might need a  
		//cache refresh. 
		void lvTexView_OnCacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
			{
			if (myCache != null && e.StartIndex >= firstItem && e.EndIndex <= firstItem + myCache.Length)
				{
				return;
				}
			//Now we need to rebuild the cache.
			firstItem = e.StartIndex;
			int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
			myCache = new ListViewItem[length];

			//Fill the cache with the appropriate ListViewItems. 
			for (int i = 0;i <length; i++)
				{
				Texture TxInfo = VirtualTextures[i + firstItem];
				ListViewItem Texitem = new ListViewItem();
				Texitem.Tag = TxInfo;
				Texitem.Text = TxInfo.szName;
				Texitem.SubItems.Add(TxInfo.uiWidth.ToString());
				Texitem.SubItems.Add(TxInfo.uiHeight.ToString());
				Texitem.SubItems.Add(TxInfo.uiDiskLength.ToString());
				Texitem.ImageIndex = TxInfo.iImgIdx;
				if (TxInfo.iImgIdx == -1)
					{
					VirtualTextures[i + firstItem].iLvwIdx = i + firstItem;
					VirtualTextures[i + firstItem].iImgIdx = texturesListe.Images.Count;
					Texitem.ImageIndex = VirtualTextures[i + firstItem].iImgIdx;
					Bitmap bmp = GraphTools.RescaleImage(TxInfo.Image, DefSz, lvTexView.BackColor);
					texturesListe.Images.Add(bmp);
					}
				myCache[i] = Texitem;
				}

			}

		private void wadmdi_OnActivated(object sender, EventArgs e)
			{
			CheckEditMenusStates();
			}

		}
	}



public class TexturesSorter : IComparer <Texture>
	{
	private int ColumnToSort;
	private SortOrder OrderOfSort;

	public TexturesSorter()
		{
		ColumnToSort = 0;
		OrderOfSort = SortOrder.None;
		}
	public int Compare(Texture x, Texture y)
		{
		if (OrderOfSort == SortOrder.None)
			return 0;
		int compareResult;
		Texture TextureX, TextureY;

		TextureX = (Texture)x;
		TextureY = (Texture)y;

		if (ColumnToSort==0)
			{
			string szColX = TextureX.szName;
			string szColY = TextureY.szName;

			if (szColX.StartsWith("!") && !szColY.StartsWith("!"))
				return ((OrderOfSort == SortOrder.Ascending) ? -1 : 1);
			if (!szColX.StartsWith("!") && szColY.StartsWith("!"))
				return ((OrderOfSort == SortOrder.Ascending) ? 1 : -1);

			if (szColX.StartsWith("{") && !szColY.StartsWith("{"))
				return ((OrderOfSort == SortOrder.Ascending) ? -1 : 1);
			if (!szColX.StartsWith("{") && szColY.StartsWith("{"))
				return ((OrderOfSort == SortOrder.Ascending) ? 1 : -1);

			if (szColX.StartsWith("-") && !szColY.StartsWith("-"))
				return ((OrderOfSort == SortOrder.Ascending) ? -1 : 1);
			if (!szColX.StartsWith("-") && szColY.StartsWith("-"))
				return ((OrderOfSort == SortOrder.Ascending) ? 1 : -1);

			string pat = @"[\+\-](?<nIdx>\d)(?<Onlytxt>.*)";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase);
			Match m1 = r.Match(szColX);
			Match m2 = r.Match(szColY);
			if (m1.Success && m2.Success)
				{
				szColX = m1.Groups["Onlytxt"].Value;
				szColY = m2.Groups["Onlytxt"].Value;
				if (szColX == szColY)
					{
					szColX = m1.Groups["nIdx"].Value + szColX;
					szColY = m2.Groups["nIdx"].Value + szColY;
					}
				}

			compareResult = String.Compare(szColX, szColY);

			if (OrderOfSort == SortOrder.Ascending)
				{
				return compareResult;
				}
			else if (OrderOfSort == SortOrder.Descending)
				{
				return (-compareResult);
				}
			else
				{
				return 0;
				}
			}
		else
			{
			uint szColX = 0;
			uint szColY = 0;
			uint szAltX = 0;
			uint szAltY = 0;

			switch (ColumnToSort)
				{
				case 1:
					szColX = TextureX.uiWidth;
					szColY = TextureY.uiWidth;
					szAltX = TextureX.uiHeight;
					szAltY = TextureY.uiHeight;
					break;
				case 2:
					szColX = TextureX.uiHeight;
					szColY = TextureY.uiHeight;
					szAltX = TextureX.uiWidth;
					szAltY = TextureY.uiWidth;
					break;
				case 3:
					szColX = TextureX.uiDiskLength;
					szColY = TextureY.uiDiskLength;
					break;
				}

			if (szColX > szColY)
				{
				if (OrderOfSort == SortOrder.Ascending)
					return 1;
				else
					return -1;
				}
			else if (szColX == szColY)
				{
				if (ColumnToSort==3)
					return 0;
				if (szAltX > szAltY)
					return OrderOfSort == SortOrder.Ascending ? 1 : -1;
				else if (szAltX < szAltY)
					return OrderOfSort == SortOrder.Ascending ? -1 : 1;
				else
					return 0;
				}
			else
				{
				if (OrderOfSort == SortOrder.Ascending)
					return -1;
				else
					return 1;
				}
			}
		}
	public int SortColumn { set { ColumnToSort = value; } get { return ColumnToSort; } }
	public SortOrder Order { set { OrderOfSort = value; } get { return OrderOfSort; } }
	}


[Serializable]
public class ClipBoardItemsList
	{
	public List<SerialTexture> ItemsList;
	public ClipBoardItemsList()
		{
		ItemsList = new List<SerialTexture>();
		}
	public void Add(SerialTexture lvItem)
		{
		ItemsList.Add(lvItem);
		}
	}