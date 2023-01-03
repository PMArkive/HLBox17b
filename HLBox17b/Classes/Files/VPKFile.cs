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
	public class PackageEntry
		{
		public string FileName;
		public string DirectoryName;
		public string FileExt;
		public uint uiCRC;
		public int usPreloadBytes;
		public int usArchiveIndex;
		public long FileOffset;
		public uint uiEntryOffset;
		public uint uiEntryLength;
		public uint uiEntryTotSize;
		public int usTerminator;
		public int iParentIdx;
		public int iIdx;
		public bool isDir;
		public uint Flags;
		public string szOnlyName;
		public string szFullPath;
		public string szSteamPath;
		public IDictionary<string, PackageEntry> ChildNodes;

		public PackageEntry()
			{
			FileExt = "";
			FileName = "";
			DirectoryName = "";
			szOnlyName = "";
			szFullPath = "";
			uiCRC = 0;
			usPreloadBytes = 0;
			usArchiveIndex = 0;
			uiEntryOffset = 0;
			uiEntryLength = 0;
			uiEntryTotSize = 0;
			usTerminator = 0;
			isDir = true;
			Flags = 0;
			iParentIdx = -1;
			iIdx = -1;
			ChildNodes = new Dictionary<string, PackageEntry>();
			FileOffset = 0;
			szSteamPath = "";
			}
		}

	class VPKFile : CCHFile
		{
		[StructLayout(LayoutKind.Sequential)]
		public struct VPKHeader
			{
			public uint uiSignature;		// Always 0x55aa1234
			public uint uiVersion;	// Always 0x00000001
			public uint uiDirectoryLength;
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct VPKHeader2
			{
			public uint uiSignature;		// Always 0x55aa1234
			public uint uiVersion;	// Always 0x00000002
			public uint uiDirectoryLength;
			public int Unknown1; // 0 in CSGO
			public uint FooterLength;
			public int Unknown3; // 48 in CSGO
			public int Unknown4; // 0 in CSGO
			};

		private uint uiDirectoryLength;
		public List<PackageEntry> pkEntries;
		public string szVpkFile;

		public VPKFile(string file, bool FullEntries)
			: base(file, FullEntries)
			{
			szFullFile = file;
			szVpkFile = Path.GetFileName(szFullFile);
			uiDirectoryLength = 0;
			pkEntries = new List<PackageEntry>();
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

		public override bool MapDataStructure()
			{
			uint uiHeaderSize = 0;
			VPKHeader vpkheader = StreamTools.ReadStruct<VPKHeader>(fs, uiHeaderSize);
			if (vpkheader.uiSignature != 0x55aa1234)
				return false;

			if (vpkheader.uiVersion != 1 && vpkheader.uiVersion != 2)
				return false;

			if (vpkheader.uiVersion == 1)
				{
				uiDirectoryLength = vpkheader.uiDirectoryLength;
				}
			else //Version 2
				{
				VPKHeader2 vpkheader2 = StreamTools.ReadStruct<VPKHeader2>(fs, uiHeaderSize);
				uiDirectoryLength = vpkheader2.uiDirectoryLength;
				}

			uiDirectoryLength = vpkheader.uiDirectoryLength;
			if (uiDirectoryLength == 0)
				return false;
			return true;
			}

		public override bool CreateLightRoot(string szTmpPref)
			{
			uint uiCRC = 0;
			int usPreloadBytes = 0;
			int usArchiveIndex = 0;
			uint uiEntryOffset = 0;
			uint uiEntryLength = 0;
			int usTerminator = 0;

			string FileName = string.Empty;
			string DirectoryName = string.Empty;
			string FileExt = string.Empty;
			string OldDirectoryName = string.Empty;
			string OldFileExt = string.Empty;
			string szFileNameTmp = string.Empty;
			string szPrefix = string.Empty;

			long ActPos = fs.Position;
			long SavPos = 0;
			int skipnext = 2;
			long end = ActPos + uiDirectoryLength - 1;

			string szCompleteFileName = "";

			if (szTmpPref.Trim() != string.Empty)
				szPrefix = szTmpPref.Trim() + "\\";


			while (ActPos < end)
				{
				if (skipnext > 1)
					{
					FileExt = StreamTools.ReadNullTerminatedString(fs).Trim();
					OldFileExt = FileExt;
					}
				else
					{
					FileExt = OldFileExt;
					}

				if (skipnext > 0)
					{
					string Name = StreamTools.ReadNullTerminatedString(fs).Trim();
					DirectoryName = Name.Replace("/", "\\");
					OldDirectoryName = DirectoryName;
					}
				else
					{
					DirectoryName = OldDirectoryName;
					}
				SavPos = fs.Position;
				szFileNameTmp = StreamTools.ReadNullTerminatedString(fs);
				if (szFileNameTmp == "")
					{
					fs.Seek(SavPos, SeekOrigin.Begin);
					skipnext = StreamTools.ReadNullChars(fs);
					ActPos = fs.Position;
					continue;
					}
				FileName = szFileNameTmp.Trim();
				uiCRC = StreamTools.ReadUInt(fs);
				usPreloadBytes = StreamTools.ReadInt(fs);
				usArchiveIndex = StreamTools.ReadInt(fs);
				uiEntryOffset = StreamTools.ReadUInt(fs);
				uiEntryLength = StreamTools.ReadUInt(fs);
				usTerminator = StreamTools.ReadInt(fs);
				szCompleteFileName = szPrefix;
				if (DirectoryName != string.Empty)
					szCompleteFileName += DirectoryName + "\\";
				szCompleteFileName += FileName + "." + FileExt;
				szList.Add(szCompleteFileName);
				fs.Seek(usPreloadBytes, SeekOrigin.Current);
				skipnext = StreamTools.ReadNullChars(fs);
				ActPos = fs.Position;
				}
			return true;
			}


		public override bool CreateRoot(BackgroundWorker bgworker = null)
			{
			if (bFullEntries)
				return CreateFullRoot(bgworker);

			uint uiCRC = 0;
			int usPreloadBytes = 0;
			int usArchiveIndex = 0;
			uint uiEntryOffset = 0;
			uint uiEntryLength = 0;
			int usTerminator = 0;

			string FileName = "";
			string DirectoryName = "";
			string FileExt = "";
			string OldDirectoryName = "";
			string OldFileExt = "";
			string szFileNameTmp = "";

			long ActPos = fs.Position;
			long SavPos = 0;
			int skipnext = 2;
			long end = ActPos + uiDirectoryLength - 1;

			string szCompleteFileName = "";

			while (ActPos < end)
				{
				if (skipnext > 1)
					{
					FileExt = StreamTools.ReadNullTerminatedString(fs).Trim();
					OldFileExt = FileExt;
					}
				else
					{
					FileExt = OldFileExt;
					}

				if (skipnext > 0)
					{
					string Name = StreamTools.ReadNullTerminatedString(fs).Trim();
					DirectoryName = Name.Replace("/", "\\");
					OldDirectoryName = DirectoryName;
					}
				else
					{
					DirectoryName = OldDirectoryName;
					}
				SavPos = fs.Position;
				szFileNameTmp = StreamTools.ReadNullTerminatedString(fs);
				if (szFileNameTmp == "")
					{
					fs.Seek(SavPos, SeekOrigin.Begin);
					skipnext = StreamTools.ReadNullChars(fs);
					ActPos = fs.Position;
					continue;
					}
				FileName = szFileNameTmp.Trim();
				uiCRC = StreamTools.ReadUInt(fs);
				usPreloadBytes = StreamTools.ReadInt(fs);
				usArchiveIndex = StreamTools.ReadInt(fs);
				uiEntryOffset = StreamTools.ReadUInt(fs);
				uiEntryLength = StreamTools.ReadUInt(fs);
				usTerminator = StreamTools.ReadInt(fs);
				szCompleteFileName = "Root\\" + DirectoryName + "\\" + FileName + "." + FileExt;
				szList.Add(szCompleteFileName);
				fs.Seek(usPreloadBytes, SeekOrigin.Current);
				skipnext = StreamTools.ReadNullChars(fs);
				ActPos = fs.Position;
				}
			return true;
			}

		public bool CreateFullRoot(BackgroundWorker bgworker = null)
			{
			PackageEntry OldEntry = new PackageEntry();
			PackageEntry oParent = new PackageEntry();
			long ActPos = fs.Position;
			long SavPos = 0;
			int skipnext = 2;
			long end = ActPos + uiDirectoryLength - 1;
			string szCompleteFileName = "";
			string szFileNameTmp = "";

			AddChild("Root", null);

			double dTotal = (double)(end);

			while (ActPos < end)
				{
				if (bgworker != null)
					{
					double dIndex = (double)(ActPos);
					double dProgressPercentage = (dIndex / dTotal);
					int iProgressPercentage = (int)(dProgressPercentage * 100);
					bgworker.ReportProgress(iProgressPercentage);
					}
				PackageEntry Entry = new PackageEntry();
				if (skipnext > 1)
					{
					Entry.FileExt = StreamTools.ReadNullTerminatedString(fs).Trim();
					OldEntry.FileExt = Entry.FileExt;
					}
				else
					{
					Entry.FileExt = OldEntry.FileExt;
					}

				if (skipnext > 0)
					{
					string Name = StreamTools.ReadNullTerminatedString(fs).Trim();
					Entry.DirectoryName = "Root\\" + Name.Replace("/", "\\");
					OldEntry.DirectoryName = Entry.DirectoryName;
					}
				else
					{
					Entry.DirectoryName = OldEntry.DirectoryName;
					}
				SavPos = fs.Position;
				szFileNameTmp = StreamTools.ReadNullTerminatedString(fs);
				if (szFileNameTmp == "")
					{
					oParent = GetParent(Entry);
					fs.Seek(SavPos, SeekOrigin.Begin);
					skipnext = StreamTools.ReadNullChars(fs);
					ActPos = fs.Position;
					continue;
					}
				Entry.FileName = szFileNameTmp.Trim();
				Entry.uiCRC = StreamTools.ReadUInt(fs);
				Entry.usPreloadBytes = StreamTools.ReadInt(fs);
				Entry.usArchiveIndex = StreamTools.ReadInt(fs);
				Entry.uiEntryOffset = StreamTools.ReadUInt(fs);
				if (Entry.usArchiveIndex == 0x7fff)
					Entry.uiEntryOffset += (uiDirectoryLength + (uint)Marshal.SizeOf(typeof(VPKHeader)));
				Entry.uiEntryLength = StreamTools.ReadUInt(fs);
				Entry.uiEntryTotSize = Entry.uiEntryLength + (uint)Entry.usPreloadBytes;
				Entry.usTerminator = StreamTools.ReadInt(fs);
				Entry.isDir = false;
				Entry.Flags = 0x4000;

				Entry.szOnlyName = Entry.FileName;
				if (Entry.FileExt != "")
					Entry.szOnlyName += ("." + Entry.FileExt);

				szCompleteFileName = Entry.DirectoryName + "\\" + Entry.szOnlyName;
				Entry.szFullPath = szCompleteFileName;
				oParent = GetParent(Entry);
				Entry.iParentIdx = oParent.iIdx;
				Entry.FileOffset = fs.Position;
				Entry.iIdx = pkEntries.Count;
				pkEntries.Add(Entry);
				fs.Seek(Entry.usPreloadBytes, SeekOrigin.Current);
				skipnext = StreamTools.ReadNullChars(fs);
				ActPos = fs.Position;
				}

			string shellType = string.Empty;
			int imgIdx = -1;
			foreach (PackageEntry Entry in pkEntries)
				{
				string szExt = "." + FilesTools.GetExtension(Entry.szOnlyName).ToLower();
				shellType = GetFileTypeText(szExt);
				imgIdx = Entry.Flags ==0 ? 0 : GetIconImageIndex(szExt);
				arEntries.Add(new CacheEntry(this, (uint)Entry.iIdx, Entry.uiEntryTotSize, Entry.uiEntryLength, Entry.Flags, (uint)Entry.iParentIdx, Entry.szOnlyName, Entry.szFullPath, (uint)Entry.usArchiveIndex, Entry.uiEntryOffset, Entry.FileOffset, Entry.usPreloadBytes, shellType, imgIdx,string.Empty));
				}
			pkEntries.Clear();
			return true;
			}

		public PackageEntry AddChild(string szFolderName, PackageEntry pkeParent)
			{
			PackageEntry pkE = new PackageEntry();
			pkE.DirectoryName = szFolderName;
			string[] parts = szFolderName.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
			pkE.szOnlyName = parts[parts.Count() - 1];
			pkE.szFullPath = szFolderName;
			pkE.isDir = true;
			if (pkeParent != null)
				pkE.iParentIdx = pkeParent.iIdx;
			pkE.iIdx = pkEntries.Count;
			pkEntries.Add(pkE);
			return pkE;
			}

		public PackageEntry GetParent(PackageEntry Entry)
			{
			PackageEntry pkeParent;
			PackageEntry pkeChild;
			string[] parts = Entry.DirectoryName.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
			string szFolder = "";
			pkeParent = pkEntries[0];
			foreach (string part in parts)
				{
				pkeChild = new PackageEntry();
				if (szFolder != "")
					szFolder += "\\";
				szFolder += part;
				if (szFolder == "Root")
					continue;
				if (!pkeParent.ChildNodes.TryGetValue(szFolder, out pkeChild))
					{
					pkeChild = AddChild(szFolder, pkeParent);
					pkeParent.uiEntryLength++;
					}
				pkeParent.ChildNodes[szFolder] = pkeChild;
				pkeParent = pkeChild;
				}
			return pkeParent;
			}

		public override bool ExtractItem(uint Idx, FileStream fsDest, ref double dExtractedSize, BackgroundWorker bgworker = null, double dTotalSize = 1)
			{
			CacheEntry cchEntry = arEntries.ElementAt((int)Idx);
			FileStream fsSrc = null;
			double dProgressPercentage = 0;
			int iProgressPercentage = 0;

			//Si on a des preloads on recherche dans le vpk_dir
			if (cchEntry.PreloadBytes != 0)
				{
				byte[] bufferPreload = new byte[cchEntry.PreloadBytes];
				fsSrc = new FileStream(this.szFullFile, FileMode.Open, FileAccess.Read);
				fsSrc.Seek(cchEntry.DirOffset, SeekOrigin.Begin);
				fsSrc.Read(bufferPreload, 0, (int)cchEntry.PreloadBytes);
				fsDest.Write(bufferPreload, 0, cchEntry.PreloadBytes);
				fsSrc.Close();
				}
			//Si tout le fichier est contenu dans le preload on retourne
			if (cchEntry.DiskSize == 0)
				return true;
			byte[] bufferEntry = new byte[cchEntry.DiskSize];
			//Une autre partie est contenue ailleurs
			string szArchiveFile = "";
			//Contenu dans le dir ou dans une archive à part
			if (cchEntry.FirstIndex == 0x7fff)
				{
				szArchiveFile = this.szFullFile;
				}
			else
				{
				string pat = @"(.*)_dir.vpk$";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match m = r.Match(this.szVpkFile);

				if (m.Success)
					{
					if (m.Groups[1].Value != null && m.Groups[1].Value != "")
						szArchiveFile = Path.GetDirectoryName(this.szFullFile) + "\\" + m.Groups[1].Value + "_" + String.Format("{0:D3}", cchEntry.FirstIndex) + ".vpk";
					}
				}
			if (!FilesTools.CheckFile(szArchiveFile))
				return false;
			//On ouvre le fichier
			fsSrc = new FileStream(szArchiveFile, FileMode.Open, FileAccess.Read);
			fsSrc.Seek(cchEntry.NextIndex, SeekOrigin.Begin);
			int DestPos = (int)fsDest.Position;
			fsSrc.Read(bufferEntry, 0, (int)cchEntry.DiskSize);
			fsDest.Write(bufferEntry, DestPos, (int)cchEntry.DiskSize);
			fsSrc.Close();
			if (bgworker != null)
				{
				dExtractedSize += cchEntry.DiskSize;
				dProgressPercentage = (dExtractedSize / dTotalSize);
				iProgressPercentage = (int)(dProgressPercentage * 100);
				bgworker.ReportProgress(iProgressPercentage);
				}
			return true;
			}
		}
	}
