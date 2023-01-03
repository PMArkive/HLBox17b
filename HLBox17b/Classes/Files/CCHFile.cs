using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using HLBox17b.Classes.Tools;
using System.Collections;

namespace HLBox17b.Classes.Files
	{
	public class CCHFile
		{
		//Variables
		public string szFullFile;						// Chemin complet du fichier
		public long lgFileSize;							// Taille du fichier
		public bool bFullEntries;
		public FileStream fs;							// FileStream d'accès au fichier
		public List<string> szList; // Liste des fichiers
		public List<CacheEntry> arEntries; // Liste des entree cache

		public uint gItemsCount;
		public uint gTerminator;
		public uint gBlocksCount;
		public uint gBlocksSize;
		public uint uiBlocksFragmented;
		public uint uiBlocksUsed;
		public uint uiCacheID;
		public uint pDirectoryItems;
		public uint pHeader;
		public uint pBlockEntryHeader;
		public uint pBlockEntries;
		public uint pFragmentationMapHeader;
		public uint pFragmentationMap;
		public uint pBlockEntryMapHeader;
		public uint pBlockEntryMap;
		public uint pDirectoryHeader;
		public uint pDirectoryEntries;
		public uint pDirectoryNames;
		public uint pDirectoryInfo1Entries;
		public uint pDirectoryInfo2Entries;
		public uint pDirectoryCopyEntries;
		public uint pDirectoryLocalEntries;
		public uint pDirectoryMapHeader;
		public uint pDirectoryMapEntries;
		public uint pChecksumHeader;
		public uint pChecksumMapHeader;
		public uint pChecksumMapEntries;
		public uint pChecksumEntries;
		public uint pDataBlockHeader;
		public uint pFirstDataBlockOffset;
		private Hashtable systemTypes;
		private string szFolderType;
		public ImageList iconListe;

		public CCHFile(string file, bool FullEntries)
			{
			szFullFile = file;
			lgFileSize = 0;
			bFullEntries = FullEntries;
			szList = new List<string>();
			arEntries = new List<CacheEntry>();
			gItemsCount = 0;
			gTerminator = 0;
			gBlocksCount = 0;
			gBlocksSize = 0;
			uiBlocksFragmented = 0;
			uiBlocksUsed = 0;
			uiCacheID = 0;
			pDirectoryItems = 0;
			pHeader = 0;
			pBlockEntryHeader = 0;
			pBlockEntries = 0;
			pFragmentationMapHeader = 0;
			pFragmentationMap = 0;
			pBlockEntryMapHeader = 0;
			pBlockEntryMap = 0;
			pDirectoryHeader = 0;
			pDirectoryEntries = 0;
			pDirectoryNames = 0;
			pDirectoryInfo1Entries = 0;
			pDirectoryInfo2Entries = 0;
			pDirectoryCopyEntries = 0;
			pDirectoryLocalEntries = 0;
			pDirectoryMapHeader = 0;
			pDirectoryMapEntries = 0;
			pChecksumHeader = 0;
			pChecksumMapHeader = 0;
			pChecksumMapEntries = 0;
			pChecksumEntries = 0;
			pDataBlockHeader = 0;
			pFirstDataBlockOffset = 0;
			systemTypes = new Hashtable();
			iconListe = new ImageList();
			iconListe.ImageSize = new System.Drawing.Size(16, 16); ;
			iconListe.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			iconListe.TransparentColor = System.Drawing.Color.Transparent;

			string SamplePath = Path.GetDirectoryName(Application.ExecutablePath);

			Icon Icn = ShellAPI.GetFolderIcon(SamplePath);
			iconListe.Images.Add(Icn);

			szFolderType = ShellAPI.GetFolderType(SamplePath);
			}
		public virtual bool Open()
			{
			return false;
			}

		public virtual bool Close()
			{
			return false;
			}
		public virtual bool MapDataStructure()
			{

			return true;
			}
		public virtual bool UnMapDataStructure()
			{
			return true;
			}
		public virtual bool CreateLightRoot(string szPrefix)
			{
			return false;
			}
		public virtual bool CreateRoot(BackgroundWorker bgworker = null)
			{
			return false;
			}
		public virtual int GetFileIdx(string szFileName)
			{
			return -1;
			}
		public virtual bool ExtractItem(uint Idx, FileStream Fw, ref double dExtractedSize, BackgroundWorker bgworker = null, double dTotalSize = 1)
			{
			return true;
			}
		public virtual bool GetFragmentationRate()
			{
			return true;
			}
		public virtual FragmInfo GetItemFragmentation(uint uiDirectoryItemIndex)
			{
			FragmInfo frmInfo = new FragmInfo();
			return frmInfo;
			}

		public virtual string GetFileTypeText(string szExt)
			{
			if (systemTypes.ContainsKey(szExt) == false)
				{
				string szType = ShellAPI.GetFileType(szExt);
				systemTypes.Add(szExt, szType);
				}
			return (string)systemTypes[szExt];
			}


		public virtual int GetIconImageIndex(string szExt)
			{
			if (iconListe.Images.ContainsKey(szExt) == false)
				{
				Icon Icn = ShellAPI.GetSmallIcon(szExt);
				iconListe.Images.Add(szExt, Icn);
				}
			return (int)iconListe.Images.IndexOfKey(szExt);
			}
		}

	public class CacheEntry : IComparable<CacheEntry>
		{
		public uint Idx;
		public CCHFile Parent;
		public uint ItemSize;
		public uint DiskSize;
		public uint Flags;
		public uint ParentIndex;
		public string Name;
		public bool isDir;
		public string FullPath;
		public string SteamPath;
		public uint FirstIndex;
		public uint NextIndex;
		public long DirOffset;
		public int PreloadBytes;
		public string ShellType;
		public int imgIdx;

		public CacheEntry(CCHFile Parent, uint Idx, uint iSize, uint dSize, uint dType, uint pIndex, string szName, string szPath, uint fstidx, uint nxtidx, long FileOff, int Preload,string shellType,int imageIdx,string stmPath)
			{
			this.Idx = Idx;
			this.Parent = Parent;
			this.ItemSize = iSize;
			this.DiskSize = dSize;
			this.ParentIndex = pIndex;
			this.Name = szName;
			this.Flags = dType;
			this.isDir = (dType == 0) ? true : false;
			this.FullPath = szPath;
			this.FirstIndex = fstidx;
			this.NextIndex = nxtidx;
			this.DirOffset = FileOff;
			this.PreloadBytes = Preload;
			this.ShellType = shellType;
			this.imgIdx = imageIdx;
			this.SteamPath = stmPath;
			}
		public List<CacheEntry> GetDirectories()
			{
			List<CacheEntry> Direct = new List<CacheEntry>();
			foreach (CacheEntry entry in Parent.arEntries)
				{
				if (entry.ParentIndex == this.Idx && entry.isDir)
					Direct.Add(entry);
				}
			return Direct;
			}

		public List<CacheEntry> GetFiles()
			{
			List<CacheEntry> Direct = new List<CacheEntry>();
			foreach (CacheEntry entry in Parent.arEntries)
				{
				if (entry.ParentIndex == this.Idx && !entry.isDir)
					Direct.Add(entry);
				}
			return Direct;
			}

		public int CompareTo(CacheEntry other)
			{
			return string.Compare(this.Name, other.Name);
			}
		}

	public class FolderInfo
		{
		public uint ItemSize;
		public uint ItemCount;
		public uint ItemType;

		public FolderInfo()
			{
			this.ItemSize = 0;
			this.ItemCount = 0;
			this.ItemType = 0;
			}
		}

	public class FragmInfo
		{
		public uint uiBlocksFragmented;
		public uint uiBlocksUsed;

		public FragmInfo()
			{
			this.uiBlocksFragmented = 0;
			this.uiBlocksUsed = 0;
			}
		}

	public class WadFileInfo
		{
		public string WadName;
		public string WadFull;
		public string SysName;
		public bool IsSys;
		public bool Exist;
		public bool Parsed;
		public List<string> TexList;

		public WadFileInfo(string szName, string szFullPath, bool bSysFile, bool Exists)
			{
			this.SysName = szName;
			this.WadName = Path.GetFileName(this.SysName).ToLower();
			this.WadFull = szFullPath;
			this.IsSys = bSysFile;
			this.Exist = Exists;
			TexList = new List<string>();
			this.Parsed = false;
			}
		public void SetTextures(List<string> NewTextures)
			{
			TexList.Clear();
			TexList.AddRange(NewTextures);
			}
		}
	}
