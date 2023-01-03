using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;
using System.ComponentModel;

namespace HLBox17b.Classes.Files
	{
	class NCFFile : CCHFile
		{
		[StructLayout(LayoutKind.Sequential)]
		public struct NCFHeader
			{
			public uint uiDummy0;			// Always 0x00000001
			public uint uiMajorVersion;		// Always 0x00000002
			public uint uiMinorVersion;		// NCF version number.
			public uint uiCacheID;
			public uint uiLastVersionPlayed;
			public uint uiDummy3;
			public uint uiDummy4;
			public uint uiFileSize;		// Total size of NCF file in bytes.
			public uint uiBlockSize;		// Size of each data block in bytes.
			public uint uiBlockCount;	// Number of data blocks.
			public uint uiDummy5;
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct NCFDirectoryHeader
			{
			public uint uiDummy0;				// Always 0x00000004
			public uint uiCacheID;				// Cache ID.
			public uint uiLastVersionPlayed;		// NCF file version.
			public uint uiItemCount;				// Number of items in the directory.	
			public uint uiFileCount;				// Number of files in the directory.
			public uint uiChecksumDataLength;	// Always 0x00008000.  Data per checksum?
			public uint uiDirectorySize;			// Size of lpNCFDirectoryEntries & lpNCFDirectoryNames & lpNCFDirectoryInfo1Entries & lpNCFDirectoryInfo2Entries & lpNCFDirectoryCopyEntries & lpNCFDirectoryLocalEntries in bytes.
			public uint uiNameSize;				// Size of the directory names in bytes.
			public uint uiInfo1Count;			// Number of Info1 entires.
			public uint uiCopyCount;				// Number of files to copy.
			public uint uiLocalCount;			// Number of files to keep local.
			public uint uiDummy1;
			public uint uiDummy2;
			public uint uiChecksum;				// Header checksum.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct NCFDirectoryEntry
			{
			public uint uiNameOffset;		// Offset to the directory item name from the end of the directory items.
			public uint uiItemSize;			// Size of the item.  (If file, file size.  If folder, num items.)
			public uint uiChecksumIndex;		// Checksome index. (0xFFFFFFFF == None).
			public uint uiDirectoryFlags;	// Flags for the directory item.  (0x00000000 == Folder).
			public uint uiParentIndex;		// Index of the parent directory item.  (0xFFFFFFFF == None).
			public uint uiNextIndex;			// Index of the next directory item.  (0x00000000 == None).
			public uint uiFirstIndex;		// Index of the first directory item.  (0x00000000 == None).
			};


		public NCFFile(string file, bool OnlyEntries)
			: base(file, OnlyEntries)
			{
			szFullFile = file;
			}

		public override bool Open()
			{
			if (szFullFile == "")
				return false;

			// Vérifier si fichier existe
			try
				{
				FileInfo f = new FileInfo(szFullFile);
				if (!f.Exists)
					return false;
				lgFileSize = f.Length;
				}

			catch (Exception)
				{
				return false;
				}

			try
				{
				fs = new FileStream(szFullFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				}
			catch (Exception ex)
				{
				string str = ex.Message;
				return false;
				}

			if (fs != null)
				return true;
			return false;
			}

		public override bool Close()
			{
			if (fs != null)
				fs.Close();
			return true;
			}
		public override bool MapDataStructure()
			{
			return true;
			}
		public override bool UnMapDataStructure()
			{
			return true;
			}
		public override bool CreateRoot(BackgroundWorker bgworker = null)
			{
			uint uiVersion;
			uint uiHeaderSize = 0;
			uint Idx;
			bool bResult = false;

			//Read Header
			NCFHeader ncfheader = StreamTools.ReadStruct<NCFHeader>(fs, uiHeaderSize);
			if (ncfheader.uiMajorVersion != 2 || ncfheader.uiMinorVersion != 1)
				return bResult;
			uiVersion = ncfheader.uiMinorVersion;
			uiHeaderSize += (uint)Marshal.SizeOf(typeof(NCFHeader));
			// Directory.
			NCFDirectoryHeader ncfDirectoryHeader = StreamTools.ReadStruct<NCFDirectoryHeader>(fs, uiHeaderSize);
			uiHeaderSize += (uint)Marshal.SizeOf(typeof(NCFDirectoryHeader));
			// Entries
			List<NCFDirectoryEntry> ncfDirectoryEntries = new List<NCFDirectoryEntry>();

			for (Idx = 0; Idx < ncfDirectoryHeader.uiItemCount; Idx++)
				{
				ncfDirectoryEntries.Add(StreamTools.ReadStruct<NCFDirectoryEntry>(fs, uiHeaderSize));
				uiHeaderSize += (uint)Marshal.SizeOf(typeof(NCFDirectoryEntry));
				}

			byte[] szNamesBuffer = new byte[ncfDirectoryHeader.uiNameSize];
			fs.Seek(uiHeaderSize, SeekOrigin.Begin);
			fs.Read(szNamesBuffer, 0, (int)ncfDirectoryHeader.uiNameSize);

			string szTmp = null;
			char c = '0';
			bool LineBr = true;
			string pPath = null;
			uint EntIdx = 0;
			uint uiDiskSize = 0;
			string shellType = string.Empty;
			int imgIdx = -1;

			double dTotal = (double)(ncfDirectoryEntries.Count);
			int pgIdx = 0;

			foreach (NCFDirectoryEntry entry in ncfDirectoryEntries)
				{
				if (bgworker != null)
					{
					double dIndex = (double)(pgIdx);
					double dProgressPercentage = (dIndex / dTotal);
					int iProgressPercentage = (int)(dProgressPercentage * 100);
					bgworker.ReportProgress(iProgressPercentage);
					pgIdx++;
					}
				Idx = entry.uiNameOffset;
				LineBr = false;
				szTmp = "";
				while (!LineBr)
					{
					c = (char)szNamesBuffer[Idx++];
					if (c != 0)
						szTmp += c;
					else
						LineBr = true;
					}
				if (szTmp == "")
					szTmp = "Root";
				if (entry.uiParentIndex != 0xffffffff)
					pPath = arEntries[(int)entry.uiParentIndex].FullPath + "\\" + szTmp;
				else
					pPath = szTmp;
				string szExt = "." + FilesTools.GetExtension(szTmp).ToLower();
				shellType = GetFileTypeText(szExt);
				imgIdx = entry.uiDirectoryFlags==0 ? 0 : GetIconImageIndex(szExt);
				arEntries.Add(new CacheEntry(this, EntIdx, entry.uiItemSize, uiDiskSize, entry.uiDirectoryFlags, entry.uiParentIndex, szTmp, pPath, entry.uiFirstIndex, entry.uiNextIndex, 0, 0, shellType, imgIdx,string.Empty));
				EntIdx++;
				}

			if (!bFullEntries)
				{
				foreach (CacheEntry file in arEntries)
					{
					szList.Add(file.FullPath);
					}
				arEntries.Clear();
				}
			return bResult;
			}
		}
	}
