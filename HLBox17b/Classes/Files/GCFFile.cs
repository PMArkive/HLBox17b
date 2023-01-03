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
	class GCFFile : CCHFile
		{
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFHeader
			{
			public uint uiDummy0;		// Always 0x00000001
			public uint uiMajorVersion;	// Always 0x00000001
			public uint uiMinorVersion;	// GCF version number.
			public uint uiCacheID;
			public uint uiLastVersionPlayed;
			public uint uiDummy1;
			public uint uiDummy2;
			public uint uiFileSize;		// Total size of GCF file in bytes.
			public uint uiBlockSize;		// Size of each data block in bytes.
			public uint uiBlockCount;	// Number of data blocks.
			public uint uiDummy3;
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFBlockEntryHeader
			{
			public uint uiBlockCount;	// Number of data blocks.
			public uint uiBlocksUsed;	// Number of data blocks that point to data.
			public uint uiDummy0;
			public uint uiDummy1;
			public uint uiDummy2;
			public uint uiDummy3;
			public uint uiDummy4;
			public uint uiChecksum;		// Header checksum.
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFBlockEntry
			{
			public uint uiEntryFlags;				// Flags for the block entry.  0x200F0000 == Not used.
			public uint uiFileDataOffset;			// The offset for the data contained in this block entry in the file.
			public uint uiFileDataSize;				// The length of the data in this block entry.
			public uint uiFirstDataBlockIndex;		// The index to the first data block of this block entry's data.
			public uint uiNextBlockEntryIndex;		// The next block entry in the series.  (N/A if == BlockCount.)
			public uint uiPreviousBlockEntryIndex;	// The previous block entry in the series.  (N/A if == BlockCount.)
			public uint uiDirectoryIndex;			// The index of the block entry in the directory.
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFFragmentationMapHeader
			{
			public uint uiBlockCount;		// Number of data blocks.
			public uint uiFirstUnusedEntry;	// The index of the first unused fragmentation map entry.
			public uint uiTerminator;		// The block entry terminator; 0 = 0x0000ffff or 1 = 0xffffffff.
			public uint uiChecksum;			// Header checksum.
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFFragmentationMap
			{
			public uint uiNextDataBlockIndex;		// The index of the next data block.
			};
		// The below section is part of version 5 but not version 6.
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFBlockEntryMapHeader
			{
			public uint uiBlockCount;			// Number of data blocks.	
			public uint uiFirstBlockEntryIndex;	// Index of the first block entry.
			public uint uiLastBlockEntryIndex;	// Index of the last block entry.
			public uint uiDummy0;
			public uint uiChecksum;				// Header checksum.
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFBlockEntryMap
			{
			public uint uiPreviousBlockEntryIndex;	// The previous block entry.  (N/A if == BlockCount.)
			public uint uiNextBlockEntryIndex;		// The next block entry.  (N/A if == BlockCount.)
			};
		// End section.
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryHeader
			{
			public uint uiDummy0;				// Always 0x00000004
			public uint uiCacheID;				// Cache ID.
			public uint uiLastVersionPlayed;		// GCF file version.
			public uint uiItemCount;				// Number of items in the directory.	
			public uint uiFileCount;				// Number of files in the directory.
			public uint uiDummy1;				// Always 0x00008000.  Data per checksum?
			public uint uiDirectorySize;			// Size of lpGCFDirectoryEntries & lpGCFDirectoryNames & lpGCFDirectoryInfo1Entries & lpGCFDirectoryInfo2Entries & lpGCFDirectoryCopyEntries & lpGCFDirectoryLocalEntries in bytes.
			public uint uiNameSize;				// Size of the directory names in bytes.
			public uint uiInfo1Count;			// Number of Info1 entires.
			public uint uiCopyCount;				// Number of files to copy.
			public uint uiLocalCount;			// Number of files to keep local.
			public uint uiDummy2;
			public uint uiDummy3;
			public uint uiChecksum;				// Header checksum.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryEntry
			{
			public uint uiNameOffset;		// Offset to the directory item name from the end of the directory items.
			public uint uiItemSize;			// Size of the item.  (If file, file size.  If folder, num items.)
			public uint uiChecksumIndex;		// Checksome index. (0xFFFFFFFF == None).
			public uint uiDirectoryFlags;	// Flags for the directory item.  (0x00000000 == Folder).
			public uint uiParentIndex;		// Index of the parent directory item.  (0xFFFFFFFF == None).
			public uint uiNextIndex;			// Index of the next directory item.  (0x00000000 == None).
			public uint uiFirstIndex;		// Index of the first directory item.  (0x00000000 == None).
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryMapHeader
			{
			public uint uiDummy0;			// Always 0x00000001
			public uint uiDummy1;			// Always 0x00000000
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryMapEntry
			{
			public uint uiFirstBlockIndex;	// Index of the first data block. (N/A if == BlockCount.)
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFChecksumHeader
			{
			public uint uiDummy0;			// Always 0x00000001
			public uint uiChecksumSize;		// Size of LPGCFCHECKSUMHEADER & LPGCFCHECKSUMMAPHEADER & in bytes.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFChecksumMapHeader
			{
			public uint uiDummy0;			// Always 0x14893721
			public uint uiDummy1;			// Always 0x00000001
			public uint uiItemCount;			// Number of items.
			public uint uiChecksumCount;		// Number of checksums.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFChecksumMapEntry
			{
			public uint uiChecksumCount;			// Number of checksums.
			public uint uiFirstChecksumIndex;	// Index of first checksum.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFChecksumEntry
			{
			public ulong uiChecksum;				// Checksum.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDataBlockHeader
			{
			public uint uiLastVersionPlayed;		// GCF file version.  This field is not part of all file versions.
			public uint uiBlockCount;			// Number of data blocks.
			public uint uiBlockSize;				// Size of each data block in bytes.
			public uint uiFirstBlockOffset;		// Offset to first data block.
			public uint uiBlocksUsed;			// Number of data blocks that contain data.
			public uint uiChecksum;				// Header checksum.
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryInfo1Entry
			{
			public uint uiDummy0;			// Always 0x00000001
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryInfo2Entry
			{
			public uint uiDummy0;			// Always 0x00000001
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryCopyEntry
			{
			public uint uiDirectoryIndex;			// Always 0x00000001
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct GCFDirectoryLocalEntry
			{
			public uint uiDirectoryIndex;			// Always 0x00000001
			};

		public GCFFile(string file, bool OnlyEntries)
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
			fs = null;
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// Mappage de la Structure
		/////////////////////////////////////////////////////////////////////////////////
		public override bool MapDataStructure()
			{
			uint uiVersion;
			uint uiHeaderSize = 0;

			//Read Header
			pHeader = uiHeaderSize;
			GCFHeader gcfheader = StreamTools.ReadStruct<GCFHeader>(fs, uiHeaderSize);
			if (gcfheader.uiMajorVersion != 1 || (gcfheader.uiMinorVersion != 3 && gcfheader.uiMinorVersion != 5 && gcfheader.uiMinorVersion != 6))
				return false;
			uiCacheID = gcfheader.uiCacheID;
			uiVersion = gcfheader.uiMinorVersion;
			gBlocksCount = gcfheader.uiBlockCount;
			gBlocksSize = gcfheader.uiBlockSize;
			uiHeaderSize += pHeader + (uint)Marshal.SizeOf(typeof(GCFHeader));
			// Block entries.
			pBlockEntryHeader = uiHeaderSize;
			GCFBlockEntryHeader gcfBlocEntryHeader = StreamTools.ReadStruct<GCFBlockEntryHeader>(fs, uiHeaderSize);
			pBlockEntries = pBlockEntryHeader + (uint)Marshal.SizeOf(typeof(GCFBlockEntryHeader));
			uiHeaderSize += (uint)Marshal.SizeOf(typeof(GCFBlockEntryHeader)) + (uint)(gcfBlocEntryHeader.uiBlockCount * (uint)Marshal.SizeOf(typeof(GCFBlockEntry)));
			// Fragmentation map.
			pFragmentationMapHeader = uiHeaderSize;
			GCFFragmentationMapHeader gcfFragmentationMapHeader = StreamTools.ReadStruct<GCFFragmentationMapHeader>(fs, uiHeaderSize);
			gTerminator = gcfFragmentationMapHeader.uiTerminator == 0 ? 0x0000ffff : 0xffffffff;
			pFragmentationMap = pFragmentationMapHeader + (uint)Marshal.SizeOf(typeof(GCFFragmentationMapHeader));
			uiHeaderSize += (uint)Marshal.SizeOf(typeof(GCFFragmentationMapHeader)) + (uint)(gcfFragmentationMapHeader.uiBlockCount * (uint)Marshal.SizeOf(typeof(GCFFragmentationMap)));
			// Block entry map.
			if (uiVersion < 6)
				{
				pBlockEntryMapHeader = pFragmentationMap + (uint)(gcfFragmentationMapHeader.uiBlockCount * (uint)Marshal.SizeOf(typeof(GCFFragmentationMap)));
				GCFBlockEntryMapHeader gcfBlocEntryMapHeader = StreamTools.ReadStruct<GCFBlockEntryMapHeader>(fs, uiHeaderSize);
				pBlockEntryMap = pBlockEntryMapHeader + (uint)Marshal.SizeOf(typeof(GCFBlockEntryMapHeader));
				uiHeaderSize += (uint)Marshal.SizeOf(typeof(GCFBlockEntryMapHeader)) + (uint)(gcfBlocEntryMapHeader.uiBlockCount * (uint)Marshal.SizeOf(typeof(GCFBlockEntryMap)));
				}
			else
				{
				pBlockEntryMapHeader = 0;
				pBlockEntryMap = 0;
				}
			// Directory.
			pDirectoryHeader = uiHeaderSize;
			GCFDirectoryHeader gcfDirectoryHeader = StreamTools.ReadStruct<GCFDirectoryHeader>(fs, uiHeaderSize);
			uiHeaderSize += (uint)Marshal.SizeOf(typeof(GCFDirectoryHeader));
			pDirectoryEntries = uiHeaderSize;
			gItemsCount = gcfDirectoryHeader.uiItemCount;
			// Entries
			pDirectoryNames = pDirectoryEntries + (uint)Marshal.SizeOf(typeof(GCFDirectoryEntry)) * gItemsCount;
			//Misc
			pDirectoryInfo1Entries = pDirectoryNames + gcfDirectoryHeader.uiNameSize;
			pDirectoryInfo2Entries = pDirectoryInfo1Entries + (uint)Marshal.SizeOf(typeof(GCFDirectoryInfo1Entry)) * gcfDirectoryHeader.uiInfo1Count;
			pDirectoryCopyEntries = pDirectoryInfo2Entries + (uint)Marshal.SizeOf(typeof(GCFDirectoryInfo2Entry)) * gcfDirectoryHeader.uiItemCount;
			pDirectoryLocalEntries = pDirectoryCopyEntries + (uint)Marshal.SizeOf(typeof(GCFDirectoryCopyEntry)) * gcfDirectoryHeader.uiCopyCount;
			//Map Idx
			if (uiVersion < 5)
				{
				pDirectoryMapHeader = 0;
				pDirectoryMapEntries = pDirectoryHeader + gcfDirectoryHeader.uiDirectorySize;
				}
			else
				{
				pDirectoryMapHeader = pDirectoryHeader + gcfDirectoryHeader.uiDirectorySize;
				pDirectoryMapEntries = pDirectoryMapHeader + (uint)Marshal.SizeOf(typeof(GCFDirectoryMapHeader));
				}
			//CheckSum
			pChecksumHeader = pDirectoryMapEntries + (uint)Marshal.SizeOf(typeof(GCFDirectoryMapEntry)) * gItemsCount;
			GCFChecksumHeader gcfCheckSumHeader = StreamTools.ReadStruct<GCFChecksumHeader>(fs, pChecksumHeader);
			pChecksumMapHeader = pChecksumHeader + (uint)Marshal.SizeOf(typeof(GCFChecksumHeader));
			GCFChecksumMapHeader gcfCheckSumMapHeader = StreamTools.ReadStruct<GCFChecksumMapHeader>(fs, pChecksumMapHeader);
			pChecksumMapEntries = pChecksumMapHeader + (uint)Marshal.SizeOf(typeof(GCFChecksumMapHeader));
			pChecksumEntries = pChecksumMapEntries + (uint)Marshal.SizeOf(typeof(GCFChecksumMapEntry)) * gcfCheckSumMapHeader.uiItemCount;
			// Datas Blocks
			if (uiVersion < 5)
				pDataBlockHeader = pChecksumMapHeader + gcfCheckSumHeader.uiChecksumSize - sizeof(uint);
			else
				pDataBlockHeader = pChecksumMapHeader + gcfCheckSumHeader.uiChecksumSize;
			GCFDataBlockHeader DataBlockHeader = StreamTools.ReadStruct<GCFDataBlockHeader>(fs, pDataBlockHeader);
			pFirstDataBlockOffset = DataBlockHeader.uiFirstBlockOffset;
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// Un-Mappage de la Structure
		/////////////////////////////////////////////////////////////////////////////////
		public override bool UnMapDataStructure()
			{
			gItemsCount = 0;
			gTerminator = 0;
			gBlocksCount = 0;
			gBlocksSize = 0;
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
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// Création de l'arborescence du gcf
		/////////////////////////////////////////////////////////////////////////////////
		public override bool CreateRoot(BackgroundWorker bgworker = null)
			{
			uint uiNOffset = 0;
			uint uiItemCount = StreamTools.ReadStruct<GCFDirectoryHeader>(fs, pDirectoryHeader).uiItemCount;
			uint uiNamesSize = StreamTools.ReadStruct<GCFDirectoryHeader>(fs, pDirectoryHeader).uiNameSize;
			bool LineBr;
			string szTmp, pPath;
			char c;
			uint EntIdx = 0;
			uint uiDiskSize = 0;
			string shellType = string.Empty;
			int imgIdx = -1;


			byte[] szNamesBuffer = new byte[uiNamesSize];
			fs.Seek(pDirectoryNames, SeekOrigin.Begin);
			fs.Read(szNamesBuffer, 0, (int)uiNamesSize);

			double dTotal = (double)(uiItemCount);
			for (int Entry = 0; Entry < uiItemCount; Entry++)
				{
				if (bgworker != null)
					{
					double dIndex = (double)(Entry);
					double dProgressPercentage = (dIndex / dTotal);
					int iProgressPercentage = (int)(dProgressPercentage * 100);
					bgworker.ReportProgress(iProgressPercentage);
					}
				GCFDirectoryEntry entry = StreamTools.GetAt<GCFDirectoryEntry>(fs, Entry, pDirectoryEntries);
				uiNOffset = entry.uiNameOffset;
				LineBr = false;
				szTmp = "";
				while (!LineBr)
					{
					c = (char)szNamesBuffer[uiNOffset++];
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
				uiDiskSize = gBlocksSize * (uint)Math.Ceiling((double)(entry.uiItemSize / gBlocksSize));

				string szExt = "."+FilesTools.GetExtension(szTmp).ToLower();
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
			return true;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// Récupération de l'index d'un fichier dans un gcf
		/////////////////////////////////////////////////////////////////////////////////
		public override int GetFileIdx(string szFileName)
			{
			int WadIdx = -1;
			uint uiNOffset = 0;
			uint uiItemCount = StreamTools.ReadStruct<GCFDirectoryHeader>(fs, pDirectoryHeader).uiItemCount;
			uint uiNamesSize = StreamTools.ReadStruct<GCFDirectoryHeader>(fs, pDirectoryHeader).uiNameSize;
			bool LineBr;
			string szTmp;
			char c;

			byte[] szNamesBuffer = new byte[uiNamesSize];
			fs.Seek(pDirectoryNames, SeekOrigin.Begin);
			fs.Read(szNamesBuffer, 0, (int)uiNamesSize);

			for (int Entry = 0; Entry < uiItemCount; Entry++)
				{
				if (StreamTools.GetAt<GCFDirectoryEntry>(fs, Entry, pDirectoryEntries).uiDirectoryFlags == 0)
					continue;
				uiNOffset = StreamTools.GetAt<GCFDirectoryEntry>(fs, Entry, pDirectoryEntries).uiNameOffset;
				LineBr = false;
				szTmp = "";
				while (!LineBr)
					{
					c = (char)szNamesBuffer[uiNOffset++];
					if (c != 0)
						szTmp += c;
					else
						LineBr = true;
					}
				if (szTmp.ToLower() == szFileName.ToLower())
					return (int)Entry;
				}
			return WadIdx;
			}
		///////////////////////////////////////////////////////////
		// Taux de Fragmentation global du fichier
		///////////////////////////////////////////////////////////		
		public override bool GetFragmentationRate()
			{
			uint uiFilesTotal = 0;
			long uiBytesTotal = 0;
			uint uiItemCount = StreamTools.ReadStruct<GCFDirectoryHeader>(fs, pDirectoryHeader).uiItemCount;
			uint i, flag;


			for (i = 0; i < uiItemCount; i++)
				{
				flag = StreamTools.GetAt<GCFDirectoryEntry>(fs, (int)i, pDirectoryEntries).uiDirectoryFlags;
				if (flag != 0)
					{
					FragmInfo frmInfo = GetItemFragmentation(i);
					uiBlocksFragmented += frmInfo.uiBlocksFragmented;
					uiBlocksUsed += frmInfo.uiBlocksUsed;
					uiFilesTotal++;
					uiBytesTotal += (frmInfo.uiBlocksUsed * gBlocksSize);
					}
				}
			if (uiBlocksFragmented == 0 || uiBlocksUsed == 0)
				return true;
			return false;
			}
		///////////////////////////////////////////////////////////
		// Taux de Fragmentation d'un item
		///////////////////////////////////////////////////////////		
		public override FragmInfo GetItemFragmentation(uint uiDirectoryItemIndex)
			{
			FragmInfo frmInfo = new FragmInfo();

			if (StreamTools.GetAt<GCFDirectoryEntry>(fs, (int)uiDirectoryItemIndex, pDirectoryEntries).uiDirectoryFlags == 0)
				{
				uiDirectoryItemIndex = StreamTools.GetAt<GCFDirectoryEntry>(fs, (int)uiDirectoryItemIndex, pDirectoryEntries).uiFirstIndex;
				while (uiDirectoryItemIndex != 0 && uiDirectoryItemIndex != 0xffffffff)
					{
					FragmInfo newfrmInfo = GetItemFragmentation(uiDirectoryItemIndex);
					frmInfo.uiBlocksFragmented = newfrmInfo.uiBlocksFragmented;
					frmInfo.uiBlocksUsed = newfrmInfo.uiBlocksUsed;
					uiDirectoryItemIndex = StreamTools.GetAt<GCFDirectoryEntry>(fs, (int)uiDirectoryItemIndex, pDirectoryEntries).uiNextIndex;
					}
				}
			else
				{
				uint uiLastDataBlockIndex = gBlocksCount;
				uint uiBlockEntryIndex = StreamTools.GetAt<GCFDirectoryMapEntry>(fs, (int)uiDirectoryItemIndex, pDirectoryMapEntries).uiFirstBlockIndex;

				while (uiBlockEntryIndex != gBlocksCount)
					{
					uint uiBlockEntrySize = 0;
					uint uiBlockDataSizeTot = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFileDataSize;
					uint uiDataBlockIndex = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFirstDataBlockIndex;
					while (uiDataBlockIndex < gTerminator && uiBlockEntrySize < uiBlockDataSizeTot)
						{
						if (uiLastDataBlockIndex != gBlocksCount && uiLastDataBlockIndex + 1 != uiDataBlockIndex)
							frmInfo.uiBlocksFragmented++;
						frmInfo.uiBlocksUsed++;
						uiLastDataBlockIndex = uiDataBlockIndex;
						uiDataBlockIndex = StreamTools.GetAt<GCFFragmentationMap>(fs, (int)uiDataBlockIndex, pFragmentationMap).uiNextDataBlockIndex;
						uiBlockEntrySize += gBlocksSize;
						}
					uiBlockEntryIndex = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiNextBlockEntryIndex;
					}
				}
			return frmInfo;
			}
		///////////////////////////////////////////////////////////
		// Extraction  de l'entrée Idx dans le Flux fsDest
		///////////////////////////////////////////////////////////
		public override bool ExtractItem(uint Idx, FileStream fsDest, ref double dExtractedSize, BackgroundWorker bgworker = null, double dTotalSize = 1)
			{
			GCFBlockEntry temp;
			uint uiFGcfDataOffset = 0;    //Offset réel dans le fichier GCF
			uint uiBlockDataLength = 0;   //Longueur réelle du bloc à écrire

			double dProgressPercentage = 0;
			int iProgressPercentage = 0;

			uint OverallDataBytes = 0;

			uint uiBlockEntryIndex = StreamTools.GetAt<GCFDirectoryMapEntry>(fs, (int)Idx, pDirectoryMapEntries).uiFirstBlockIndex;
			uint uiDataBlockIndex = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFirstDataBlockIndex;
			uint uiFileDataOffset = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFileDataOffset;
			uint uiBlockDataSizeTot = 0;
			uint uiItemSize = StreamTools.GetAt<GCFDirectoryEntry>(fs, (int)Idx, pDirectoryEntries).uiItemSize;

			uint uiDataBlockOffset = 0;

			byte[] buffer = new byte[gBlocksSize];

			//Remplissage du Fichier de Destination (utile pour le Fseek uiFileDataOffset ci-dessous)
			fsDest.SetLength((long)(uiItemSize - 1));

			while ((uiBlockEntryIndex != gBlocksCount) && (OverallDataBytes < uiItemSize))
				{
				temp = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries);
				uiBlockDataSizeTot = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFileDataSize;
				OverallDataBytes += uiBlockDataSizeTot;
				uiDataBlockOffset = 0;
				//Offset dans le fichier de destination
				try { fsDest.Seek((long)uiFileDataOffset, SeekOrigin.Begin); }
				catch (Exception) { }
				//Parcourir Datas du bloc en cours
				while ((uiDataBlockIndex != gTerminator) && (uiDataBlockOffset < uiBlockDataSizeTot))
					{
					uiFGcfDataOffset = pFirstDataBlockOffset + uiDataBlockIndex * gBlocksSize;
					fs.Seek(uiFGcfDataOffset, SeekOrigin.Begin);
					fs.Read(buffer, 0, (int)gBlocksSize);
					uiBlockDataLength = uiDataBlockOffset + gBlocksSize > uiBlockDataSizeTot ? uiBlockDataSizeTot - uiDataBlockOffset : gBlocksSize;
					uiDataBlockOffset += uiBlockDataLength;
					if (bgworker != null)
						{
						dExtractedSize += uiBlockDataLength;
						dProgressPercentage = (dExtractedSize / dTotalSize);
						iProgressPercentage = (int)(dProgressPercentage * 100);
						bgworker.ReportProgress(iProgressPercentage);
						}
					fsDest.Write(buffer, 0, (int)uiBlockDataLength);
					uiDataBlockIndex = StreamTools.GetAt<GCFFragmentationMap>(fs, (int)uiDataBlockIndex, pFragmentationMap).uiNextDataBlockIndex;
					}
				//Changement de Bloc, Passage au bloc suivant
				if (uiDataBlockOffset >= uiBlockDataSizeTot)
					{
					uiBlockEntryIndex = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiNextBlockEntryIndex;
					if (uiBlockEntryIndex != gBlocksCount)
						{
						uiDataBlockIndex = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFirstDataBlockIndex;
						uiFileDataOffset = StreamTools.GetAt<GCFBlockEntry>(fs, (int)uiBlockEntryIndex, pBlockEntries).uiFileDataOffset;
						}
					}
				}
			return true;
			}
		}
	}
