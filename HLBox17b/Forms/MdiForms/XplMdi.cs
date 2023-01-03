using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLBox17b.Classes.Files;
using HLBox17b.Classes.Tools;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Collections;
using System.Diagnostics;


namespace HLBox17b.Forms.MdiForms
	{
	public partial class XplMdi : MdiChild
		{
		private CCHFile myCacheFile;
		private bool bIsManifest;
		public static myTreeListItems myTrItems;

		private FilesSorter lvFilesSorter;

		private bool bYesForAllReplace;
		private bool bNoForAllReplace;
		private double dExtractedSize;
		private List<CacheEntry> ExtractedItems;
		private List<CacheEntry> VirtualCacheEntries;
		TreeNode oldSelected;


		public XplMdi(string szFileName)
			{
			InitializeComponent();
			szFullFile = szFileName;
			bIsModified = false;
			bIsNew = false;
			oldSelected = null;
			myTrItems = new myTreeListItems();

			TranslateForm();

			lvFilesSorter = new FilesSorter();
			lvFilesSorter.SortColumn = 0;
			lvFilesSorter.Order = SortOrder.Ascending;
			VirtualCacheEntries = new List<CacheEntry>();
			ExtractedItems = new List<CacheEntry>();

			string SamplePath = Path.GetDirectoryName(Application.ExecutablePath);

			Icon Icn = ShellAPI.GetFolderIcon(SamplePath);
			imageFolder.Images.Add(Icn);

			bIsManifest = false;

			bYesForAllReplace = false;
			bNoForAllReplace = false;
			dExtractedSize = 0;

			sb_Infos.Text = "";
			sb_Infos.Visible = true;
			sb_Progress.Visible = false;
			}

		private void TranslateForm()
			{
			//Title
			this.Text = szFullFile;
			lv_Files.Columns[0].Text = Program.appLng._i18n("xpl_filename");
			lv_Files.Columns[1].Text = Program.appLng._i18n("xpl_filetype");
			lv_Files.Columns[2].Text = Program.appLng._i18n("xpl_filesize");
			mainmenu_ExtractMenuItem.Text = Program.appLng._i18n("xpl_extract");
			ctxmenu_ExtractMenuItem.Text = Program.appLng._i18n("xpl_extract");
			mainmenu_ShellExecMenuItem.Text = Program.appLng._i18n("xpl_shellexecute");
			mainmenu_ViewFolderMenuItem.Text = Program.appLng._i18n("xpl_viewfolder");
			ctxmenu_ShellExecMenuItem.Text = Program.appLng._i18n("xpl_shellexecute");
			ctxmenu_ViewFolderMenuItem.Text = Program.appLng._i18n("xpl_viewfolder");
			}


		private void OnShow(object sender, EventArgs e)
			{
			if (bgLoader.IsBusy != true)
				{
				this.UseWaitCursor = true;
				xplorer_ShowLoader(true, Program.appLng._i18n("common_loading"));
				bgLoader.RunWorkerAsync();
				}
			}

		#region loading file

		private void BgLoader_DoWork(object sender, DoWorkEventArgs e)
			{
			string szExt;

			szExt = FilesTools.GetExtension(szFullFile).ToLower();

			switch (szExt)
				{
				case "vpk":
					myCacheFile = (VPKFile)new VPKFile(szFullFile, true);
					bIsManifest = false;
					break;
				case "gcf":
					myCacheFile = (GCFFile)new GCFFile(szFullFile,true);
					bIsManifest = false;
					break;
				case "ncf":
					myCacheFile = (NCFFile)new NCFFile(szFullFile,true);
					bIsManifest = true;
					break;
				case "manifest":
					myCacheFile = (MFSTFile)new MFSTFile(szFullFile, true);
					bIsManifest = true;
					break;
				default:
					return;
				}

			if (myCacheFile.Open())
				{
				if (!myCacheFile.MapDataStructure())
					{
					MessageBox.Show(Program.appLng._i18n("common_msg_invalid_file_format"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					myCacheFile.Close();
					this.Close();
					return;
					}
				myCacheFile.CreateRoot(bgLoader);
				myCacheFile.GetFragmentationRate();
				myCacheFile.Close();
				}
			imageFiles = myCacheFile.iconListe;
			bgLoader.ReportProgress(100, null);
			int Delay = 100;
			System.Threading.Thread.Sleep(Delay);
			}

		private void BgLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (Percent < 100)
				xplorer_UpdateLoader(Percent, String.Format(Program.appLng._i18n("common_progression"), e.ProgressPercentage.ToString()));
			else
				xplorer_UpdateLoader(100, Program.appLng._i18n("common_parsing"));
			}

		private void BgLoader_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
			{
			lv_Files.SmallImageList = imageFiles;
			lvtree_Populate();
			this.UseWaitCursor = false;
			xplorer_ShowLoader(false, null);
			lvtree_SelectFirst(true);
			xplorer_CheckMenuStates();
			}
		
		#endregion


		#region lvtree_events

		private void lvtree_Populate()
			{
			lv_Tree.Nodes.Clear();
			lv_Tree.BeginUpdate();
			TreeNode rootNode;
			CacheEntry Root = myCacheFile.arEntries[0];
			rootNode = new TreeNode(Root.Name);
			rootNode.Tag = Root;
			lvtree_GetDirectories(Root.GetDirectories(), rootNode);
			lv_Tree.Nodes.Add(rootNode);
			rootNode.Expand();
			lv_Tree.Sort();
			lv_Tree.EndUpdate();
			}

		private void lvtree_SelectFirst(bool Select)
			{
			if (Select)
				{
				TreeNode rootNode = lv_Tree.Nodes[0];
				lv_Tree.SelectedNode = rootNode;
				CacheEntry nodeDirInfo = (CacheEntry)rootNode.Tag;
				lvfiles_Populate(nodeDirInfo);
				}
			sb_Infos.Text = szFullFile;
			if (myCacheFile.uiCacheID !=0)
				sb_Infos.Text += " (Id=" + myCacheFile.uiCacheID + ")";
			sb_Infos.Text += ": " + FilesTools.formatSize(myCacheFile.lgFileSize);

			if (myCacheFile.uiBlocksUsed > 0)
				{
				sb_Infos.Text += " - " + Program.appLng._i18n("xpl_blocks") + ": ";
				sb_Infos.Text += myCacheFile.uiBlocksUsed + "/" + myCacheFile.gBlocksCount;
				decimal ratio = Math.Floor((decimal)(10000 * myCacheFile.uiBlocksFragmented / myCacheFile.uiBlocksUsed)) / 100;
				sb_Infos.Text += " - " + Program.appLng._i18n("xpl_fragmentation") + ": " + ratio.ToString() + " %";
				}
			}



		private void lvtree_GetDirectories(List<CacheEntry> subDirs, TreeNode nodeToAddTo)
			{
			TreeNode aNode;
			List<CacheEntry> subSubDirs;
			subDirs.Sort();
			foreach (CacheEntry subDir in subDirs)
				{
				if (!subDir.isDir)
					continue;
				aNode = new TreeNode(subDir.Name, 0, 0);
				aNode.Tag = subDir;
				subSubDirs = subDir.GetDirectories();
				if (subSubDirs.Count != 0)
					{
					lvtree_GetDirectories(subSubDirs, aNode);
					}
				nodeToAddTo.Nodes.Add(aNode);
				}
			}

		private void lvtree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
			{
			List<CacheEntry> itemsList = new List<CacheEntry>();

			var hitTest = lv_Tree.HitTest(e.Location);
			if (hitTest.Location == TreeViewHitTestLocations.PlusMinus)
				return;
			lv_Tree.SelectedNode = e.Node;
			TreeNode newSelected = e.Node;
			if (oldSelected != null && oldSelected == e.Node)
				return;
			oldSelected = e.Node;

			CacheEntry nodeDirInfo = (CacheEntry)newSelected.Tag;
			itemsList.Add(nodeDirInfo);
			lvfiles_Populate(nodeDirInfo);
			if (nodeDirInfo.Idx == 0)
				lvtree_SelectFirst(false);
			else
				xplorer_SetTextInfo(itemsList);
			xplorer_CheckMenuStates();
			}

		private TreeNode lvtree_GetSelectedTreeNode(CacheEntry itemInfo)
			{
			TreeNode trSelected = lv_Tree.SelectedNode;

			foreach (TreeNode trChild in trSelected.Nodes)
				{
				CacheEntry trInfo = (CacheEntry) trChild.Tag;
				if (trInfo.Idx == itemInfo.Idx)
					return trChild;
				}
			return trSelected;
			}

		#endregion


		#region lvfiles_events

		private void lvfiles_Populate(CacheEntry nodeDirInfo)
			{
			lv_Files.BeginUpdate();
			lv_Files.SelectedIndices.Clear();
			VirtualCacheEntries.Clear();

			List<CacheEntry> EntryTmp = new List<CacheEntry>();
			EntryTmp = nodeDirInfo.GetDirectories();
			foreach (CacheEntry dir in EntryTmp)
				{
				VirtualCacheEntries.Add(dir);
				}
			EntryTmp.Clear();
			EntryTmp = nodeDirInfo.GetFiles();
			foreach (CacheEntry file in EntryTmp)
				{
				VirtualCacheEntries.Add(file);
				}
			lvfiles_SortCacheEntries();
			lv_Files.EndUpdate();
			}


		private void lvfiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
			{
			if (e.Item == null)
				{
				CacheEntry Entry = VirtualCacheEntries[e.ItemIndex];
				ListViewItem Fileitem = new ListViewItem();
				Fileitem.Tag = Entry;
				Fileitem.Text = Entry.Name;
				Fileitem.SubItems.Add(Entry.ShellType);
				if (Entry.isDir)
					Fileitem.SubItems.Add("");
				else
					Fileitem.SubItems.Add(FilesTools.formatSize(Entry.ItemSize));
				Fileitem.ImageIndex = Entry.imgIdx;
				e.Item = Fileitem;
				}
			}



		private void lvfiles_DoubleClick(object sender, MouseEventArgs e)
			{
			List<ListViewItem> SelectedItems = lvfiles_GetSelectedIndexes();

			if (SelectedItems.Count == 0)
				return;

			ListViewItem lvItem = SelectedItems[0];
			CacheEntry itemInfo = (CacheEntry)lvItem.Tag;

			if (itemInfo.isDir)
				{
				lvfiles_Populate(itemInfo);
				lv_Tree.SelectedNode = lvtree_GetSelectedTreeNode(itemInfo);
				}
			else
				{
				if (bIsManifest)
					{
					if (itemInfo.SteamPath == string.Empty)
						return;
					Process.Start(itemInfo.SteamPath);
					}
				else
					{
					List<CacheEntry> itemsList = new List<CacheEntry>();
					itemsList.Add(itemInfo);
					Extract_GetFolderAndStart(itemsList);
					}
				}
			xplorer_CheckMenuStates();
			}

		private List<ListViewItem> lvfiles_GetSelectedIndexes()
			{
			List<ListViewItem> SelectedItems = new List<ListViewItem>();
			for (int i = 0; i < this.lv_Files.SelectedIndices.Count; i++)
				{
				SelectedItems.Add((ListViewItem)lv_Files.Items[lv_Files.SelectedIndices[i]]);
				}
			return SelectedItems;
			}

		private void lvfiles_SelectedIndexChanged(object sender, EventArgs e)
			{
			List<ListViewItem> SelectedItems = lvfiles_GetSelectedIndexes();

			List<CacheEntry> itemsList = new List<CacheEntry>();
			foreach (ListViewItem lvItem in SelectedItems)
				{
				CacheEntry itemInfo = (CacheEntry)lvItem.Tag;
				itemsList.Add(itemInfo);
				}
			xplorer_SetTextInfo(itemsList);
			xplorer_CheckMenuStates();
			}

		private void lvfiles_ColumnClick(object sender, ColumnClickEventArgs e)
			{
			if (e.Column == lvFilesSorter.SortColumn)
				{
				if (lvFilesSorter.Order == SortOrder.Ascending)
					lvFilesSorter.Order = SortOrder.Descending;
				else
					lvFilesSorter.Order = SortOrder.Ascending;
				}
			else
				{
				lvFilesSorter.SortColumn = e.Column;
				lvFilesSorter.Order = SortOrder.Ascending;
				}
			lvfiles_SortCacheEntries();
			}

		private void lvfiles_SortCacheEntries()
			{
			lv_Files.BeginUpdate();
			VirtualCacheEntries.Sort(lvFilesSorter);
			lv_Files.VirtualListSize = VirtualCacheEntries.Count;
			lv_Files.EndUpdate();
			}

		#endregion

		#region manifest specials cmd

		private void menu_ClickShellExec(object sender, EventArgs e)
			{
			List<ListViewItem> SelectedItems = lvfiles_GetSelectedIndexes();

			if (SelectedItems.Count == 0)
				return;

			ListViewItem lvItem = SelectedItems[0];
			CacheEntry itemInfo = (CacheEntry)lvItem.Tag;
			if (itemInfo.SteamPath == string.Empty)
				return;
			Process.Start(itemInfo.SteamPath);
			}

		private void menu_ClickViewFolder(object sender, EventArgs e)
			{
			string itemPath = string.Empty;

			List<ListViewItem> SelectedItems = lvfiles_GetSelectedIndexes();
			if (SelectedItems.Count == 1)
				{
				CacheEntry itemInfo = (CacheEntry)SelectedItems[0].Tag;
				if (itemInfo.isDir)
					itemPath = itemInfo.SteamPath;
				}
			else
				{
				TreeNode trSelected = lv_Tree.SelectedNode;
				CacheEntry itemInfo = (CacheEntry)trSelected.Tag;
				if (itemInfo.isDir)
					itemPath = itemInfo.SteamPath;
				}

			if (itemPath == string.Empty)
				return;
			Process.Start(itemPath);
			}

		#endregion

		#region Informations

		private void xplorer_ShowLoader(bool bState, string szInfo,bool bInit=false)
			{
			if (bState)
				{
				sb_Progress.Visible = true;
				if (szInfo != null)
					sb_Infos.Text = szInfo;
				else
					sb_Infos.Text = string.Empty;
				}
			else
				{
				sb_Progress.Visible = false;
				sb_Infos.Visible = true;
				if (szInfo != null)
					sb_Infos.Text = szInfo;
				else
					sb_Infos.Text = string.Empty;
				}
			if (bInit)
				sb_Progress.Value = 0;
			}

		private void xplorer_UpdateLoader(int Percent, string szInfo)
			{
			if (Percent > sb_Progress.Maximum)
				Percent = sb_Progress.Maximum;
			sb_Progress.Value = Percent;
			if (szInfo != null)
				sb_Infos.Text = szInfo;
			}

		private void xplorer_SetTextInfo(List<CacheEntry> itemsList)
			{
			string szSelectedType = "";
			FolderInfo fInfo = new FolderInfo();
			fInfo = Extract_GetTotalSize(itemsList);

			string szFormatSize = FilesTools.formatSize(fInfo.ItemSize);
			uint NumSelected = fInfo.ItemCount;

			switch (fInfo.ItemType)
				{
				case 1:
					szSelectedType = Program.appLng._i18n("xpl_files");
					break;
				case 2:
					szSelectedType = Program.appLng._i18n("xpl_folders");
					break;
				default:
					szSelectedType = Program.appLng._i18n("xpl_elements");
					break;
				}

			if (itemsList.Count == 1)
				{
				CacheEntry itemInfo = itemsList[0];
				if (itemInfo.isDir)
					{
					sb_Infos.Text = Program.appLng._i18n("xpl_selection") + ": " + itemsList.Count.ToString() + " " + szSelectedType + ": " + NumSelected.ToString() + " " + Program.appLng._i18n("xpl_items") + " : " + szFormatSize;
					return;
					}
				}

			sb_Infos.Text = Program.appLng._i18n("xpl_selection") + ": " + itemsList.Count.ToString() + " " + szSelectedType + ": " + NumSelected.ToString() + " " + Program.appLng._i18n("xpl_items") + " : " + szFormatSize;
			}

		private void xplorer_CheckMenuStates()
			{
			//On recherche d'abord dans la listview files
			List<ListViewItem> SelectedItems = lvfiles_GetSelectedIndexes();
			if (SelectedItems.Count != 0)
				{
				if (bIsManifest)
					{
					mainmenu_ExtractMenuItem.Enabled = false;
					ctxmenu_ExtractMenuItem.Enabled = false;
					if (SelectedItems.Count > 1)
						{
						mainmenu_ShellExecMenuItem.Enabled = false;
						ctxmenu_ShellExecMenuItem.Enabled = false;
						mainmenu_ViewFolderMenuItem.Enabled = false;
						ctxmenu_ViewFolderMenuItem.Enabled = false;
						}
					else
						{
						CacheEntry itemInfo = (CacheEntry)SelectedItems[0].Tag;
						mainmenu_ShellExecMenuItem.Enabled = itemInfo.isDir ? false : true;
						ctxmenu_ShellExecMenuItem.Enabled = itemInfo.isDir ? false : true;
						mainmenu_ViewFolderMenuItem.Enabled = itemInfo.isDir ? true : false;
						ctxmenu_ViewFolderMenuItem.Enabled = itemInfo.isDir ? true : false;
						}
					}
				else
					{
					mainmenu_ShellExecMenuItem.Enabled = false;
					ctxmenu_ShellExecMenuItem.Enabled = false;
					mainmenu_ViewFolderMenuItem.Enabled = false;
					ctxmenu_ViewFolderMenuItem.Enabled = false;
					mainmenu_ExtractMenuItem.Enabled = true;
					ctxmenu_ExtractMenuItem.Enabled = true;
					}
				return;
				}
			//Rien n'est sélectionné dans la listview files: on recherche dans le treeview
			if (lv_Tree.SelectedNode == null)
				{
				mainmenu_ExtractMenuItem.Enabled = false;
				ctxmenu_ExtractMenuItem.Enabled = false;
				mainmenu_ShellExecMenuItem.Enabled = false;
				mainmenu_ViewFolderMenuItem.Enabled = false;
				ctxmenu_ShellExecMenuItem.Enabled = false;
				ctxmenu_ViewFolderMenuItem.Enabled = false;
				}

			TreeNode SelectedFolder = lv_Tree.SelectedNode;
			CacheEntry nodeDirInfo = (CacheEntry)SelectedFolder.Tag;

			if (nodeDirInfo.Idx == 0)
				{
				mainmenu_ExtractMenuItem.Enabled = false;
				ctxmenu_ExtractMenuItem.Enabled = false;
				mainmenu_ShellExecMenuItem.Enabled = false;
				mainmenu_ViewFolderMenuItem.Enabled = false;
				ctxmenu_ShellExecMenuItem.Enabled = false;
				ctxmenu_ViewFolderMenuItem.Enabled = false;
				}
			else
				{
				mainmenu_ExtractMenuItem.Enabled = bIsManifest ? false : true;
				ctxmenu_ExtractMenuItem.Enabled = bIsManifest ? false : true;
				mainmenu_ShellExecMenuItem.Enabled = false;
				ctxmenu_ShellExecMenuItem.Enabled = false;
				mainmenu_ViewFolderMenuItem.Enabled = bIsManifest ? true : false;
				ctxmenu_ViewFolderMenuItem.Enabled = bIsManifest ? true : false;
				}
			}

		#endregion


		#region extract

		private void menu_ClickExtract(object sender, EventArgs e)
			{
			List<CacheEntry> itemsList = new List<CacheEntry>();

			List<ListViewItem> SelectedItems = lvfiles_GetSelectedIndexes();
			if (SelectedItems.Count != 0)
				{
				foreach (ListViewItem lvItem in SelectedItems)
					{
					CacheEntry itemInfo = (CacheEntry)lvItem.Tag;
					itemsList.Add(itemInfo);
					}
				}
			else
				{
				TreeNode trSelected = lv_Tree.SelectedNode;
				CacheEntry itemInfo = (CacheEntry)trSelected.Tag;
				itemsList.Add(itemInfo);
				}
			if (itemsList.Count == 0)
				return;
			Extract_GetFolderAndStart(itemsList);
			}

		private bool Extract_ShowReplaceDlg(string flname, DateTime time1, long size1, long size2)
			{
			bool bResult = false;
			if (this.InvokeRequired)
				{
				Invoke(new MethodInvoker(delegate() { bResult = Extract_ShowReplaceDlg(flname, time1, size1, size2); }));
				}
			else
				{
				DateTime time2 = DateTime.MinValue;
				Replace tmpWnd = new Replace(flname, time1, time2, size1, size2);

				if (tmpWnd.ShowDialog(this) == DialogResult.Yes)
					{
					if (tmpWnd.bReplaceAll)
						{
						bYesForAllReplace = true;
						bNoForAllReplace = false;
						}
					bResult = true;
					}
				else
					{
					if (tmpWnd.bReplaceNone)
						{
						bYesForAllReplace = false;
						bNoForAllReplace = true;
						}
					bResult = false;
					}
				}
			return bResult;
			}

		private bool Extract_CanReplace(string filename, long filesize)
			{
			bool bReplace = true;

			FileInfo f = new FileInfo(filename);

			if (!bYesForAllReplace)
				{
				if (f.Exists)
					{
					if (bNoForAllReplace)
						{
						bReplace = false;
						}
					else
						{
						long sizeext = f.Length;
						DateTime timeext = f.LastWriteTime;
						if (!Extract_ShowReplaceDlg(filename, timeext, sizeext, filesize))
							{
							bReplace = false;
							}
						}
					}
				}
			return bReplace;
			}

		private FolderInfo Extract_GetTotalSize(List<CacheEntry> itemsList)
			{
			FolderInfo fInfo = new FolderInfo();
			uint IsOnlyFolder = 1;
			uint IsOnlyFile = 1;
			foreach (CacheEntry item in itemsList)
				{
				if (item.isDir)
					{
					IsOnlyFile = 0;
					List<CacheEntry> SubFiles = new List<CacheEntry>();
					foreach (CacheEntry folder in item.GetDirectories())
						{
						SubFiles.Add(folder);
						}
					foreach (CacheEntry file in item.GetFiles())
						{
						SubFiles.Add(file);
						}
					FolderInfo newFolder = Extract_GetTotalSize(SubFiles);
					fInfo.ItemSize += newFolder.ItemSize;
					fInfo.ItemCount += newFolder.ItemCount;
					continue;
					}
				IsOnlyFolder = 0;
				fInfo.ItemSize += item.ItemSize;
				fInfo.ItemCount++;
				}
			if (IsOnlyFolder == 1 && IsOnlyFile == 1)
				fInfo.ItemType = 3;
			else if (IsOnlyFolder == 0 && IsOnlyFile == 0)
				fInfo.ItemType = 3;
			else if (IsOnlyFolder == 0)
				fInfo.ItemType = 1;
			else if (IsOnlyFile == 0)
				fInfo.ItemType = 2;
			else
				fInfo.ItemType = 3;
			return fInfo;
			}

		private void Extract_GetFolderAndStart(List<CacheEntry> itemsList)
			{
			string szDstpath = "";
			folderBrowserDlg.SelectedPath = Program.mRc.szXpl_LastExtractPath;
			folderBrowserDlg.Description = Program.appLng._i18n("xpl_destfolder");

			DialogResult dlgRes = folderBrowserDlg.ShowDialog();

			if (dlgRes != DialogResult.OK)
				return;
			szDstpath = folderBrowserDlg.SelectedPath;

			if (!FilesTools.CheckDestinationPath(szDstpath, false))
				{
				MessageBox.Show(Program.appLng._i18n("common_msg_invalid_folder"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
				}
			Program.mRc.szXpl_LastExtractPath = szDstpath;

			if (!myCacheFile.Open())
				return;

			this.UseWaitCursor = true;
			//Calcul de la taille totale à extraire
			double dTotalSize = (double) Extract_GetTotalSize(itemsList).ItemSize;

			List<object> arguments = new List<object>();
                    arguments.Add(szDstpath);
                    arguments.Add(itemsList);
					arguments.Add(dTotalSize);

			if (bgExtract.IsBusy != true)
				{
				this.UseWaitCursor = true;
				xplorer_ShowLoader(true, Program.appLng._i18n("xpl_startextract"),true);
				bgExtract.RunWorkerAsync(arguments);
				}
			}

		private void bgExtract_DoWork(object sender, DoWorkEventArgs e)
			{
			List<object> argsList = e.Argument as List<object>;

			string szDstpath = (string) argsList[0];
			ExtractedItems.Clear();
			ExtractedItems.AddRange((List<CacheEntry>) argsList[1]);
			double dTotalSize = (double)argsList[2];
			Extract_StartExtraction(szDstpath, ExtractedItems, dTotalSize);
			bgExtract.ReportProgress(100, null);
			int Delay = 100;
			System.Threading.Thread.Sleep(Delay);
			}

		private void bgExtract_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (Percent < 100)
				{
				string szProgTxt = String.Format(Program.appLng._i18n("common_progression"), e.ProgressPercentage.ToString());
				szProgTxt += " (" + Program.appLng._i18n("common_esctocancel")+")";
				xplorer_UpdateLoader(Percent, szProgTxt );
				}
			}

		private void bgExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
			{
			myCacheFile.Close();
			bYesForAllReplace = false;
			bNoForAllReplace = false;
			xplorer_ShowLoader(false, null);
			this.UseWaitCursor = false;
			xplorer_SetTextInfo(ExtractedItems);
			ExtractedItems.Clear();
			}

		private void bgExtract_OnKeyDown(object sender, KeyEventArgs e)
			{
			if (e.KeyCode == Keys.Escape)
				{
				if (bgExtract.WorkerSupportsCancellation == true)
					bgExtract.CancelAsync();
				}
			}

		private bool Extract_StartExtraction(string szPath, List<CacheEntry> itemsList, double dTotalSize)
			{
			string szFullPath = "";
			double dProgressPercentage = 0;
			int iProgressPercentage = 0;
 
			foreach (CacheEntry item in itemsList)
				{
				if (bgExtract.CancellationPending == true)
					break;
				szFullPath = szPath + "\\" + item.Name;
				if (item.isDir)
					{
					List<CacheEntry> SubFiles = new List<CacheEntry>();
					FilesTools.CreateFolder(szFullPath);
					foreach (CacheEntry folder in item.GetDirectories())
						{
						SubFiles.Add(folder);
						}
					foreach (CacheEntry file in item.GetFiles())
						{
						SubFiles.Add(file);
						}
					Extract_StartExtraction(szFullPath, SubFiles, dTotalSize);
					continue;
					}

				bool bReplace = Extract_CanReplace(szFullPath, item.ItemSize);
				if (!bReplace)
					{
					dExtractedSize += item.ItemSize;
					dProgressPercentage = (dExtractedSize / dTotalSize);
					iProgressPercentage = (int)(dProgressPercentage * 100);
					bgExtract.ReportProgress(iProgressPercentage);
					continue;
					}
				//Ouverture fichier
				FileStream fw;
				try
					{
					fw = new FileStream(szFullPath, FileMode.Create, FileAccess.Write, FileShare.None);
					if (fw == null)
						return false;
					}
				catch (Exception ex)
					{
					MessageBox.Show(ex.Message);
					return false;
					}
				if (!myCacheFile.ExtractItem(item.Idx, fw, ref dExtractedSize, bgExtract, dTotalSize))
					{
					string szMsg = item.Name + " : \n" + Program.appLng._i18n("xpl_extraction_error");
					MessageBox.Show(szMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				fw.Close();
				}
			return true;
			}

		#endregion


		}


	public class FilesSorter : IComparer<CacheEntry>
		{
		private int ColumnToSort;
		private SortOrder OrderOfSort;

		public FilesSorter()
			{
			ColumnToSort = 0;
			OrderOfSort = SortOrder.None;
			}
		public int Compare(CacheEntry x, CacheEntry y)
			{
			if (OrderOfSort == SortOrder.None)
				return 0;
			
			int compareOnNameResult;

			CacheEntry CacheX, CacheY;

			CacheX = (CacheEntry)x;
			CacheY = (CacheEntry)y;

			//Si un des deux est un répertoire mais pas l'autre le tri est automatique
			if (CacheX.isDir && !CacheY.isDir)
				return ((OrderOfSort == SortOrder.Ascending) ? -1 : 1);
			if (!CacheX.isDir && CacheY.isDir)
				return ((OrderOfSort == SortOrder.Ascending) ? 1 : -1);


			//Première comparaison sur le nom
			compareOnNameResult = String.Compare(CacheX.Name, CacheY.Name);

			if (ColumnToSort == 0)
				{
				return ((OrderOfSort == SortOrder.Ascending) ? compareOnNameResult : -compareOnNameResult);
				}
			else if (ColumnToSort == 1)
				{
				int compareOnShType = String.Compare(CacheX.ShellType, CacheY.ShellType);
				if (compareOnShType != 0)
					return ((OrderOfSort == SortOrder.Ascending) ? compareOnShType : -compareOnShType);
				return ((OrderOfSort == SortOrder.Ascending) ? compareOnNameResult : -compareOnNameResult);
				}
			else
				{
				uint nColX = CacheX.ItemSize;
				uint nColY = CacheY.ItemSize;

				if (nColX > nColY)
					{
					return ((OrderOfSort == SortOrder.Ascending) ? 1 : -1);
					}
				else if (nColX == nColY)
					{
					return ((OrderOfSort == SortOrder.Ascending) ? compareOnNameResult : -compareOnNameResult);
					}
				else
					{
					return ((OrderOfSort == SortOrder.Ascending) ? -1 : 1);
					}
				}
			}
		public int SortColumn { set { ColumnToSort = value; } get { return ColumnToSort; } }
		public SortOrder Order { set { OrderOfSort = value; } get { return OrderOfSort; } }
		}

	}

