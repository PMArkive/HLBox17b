using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using HLBox17b.Classes.Tools;
using System.Windows.Forms;
using System.ComponentModel;

namespace HLBox17b.Classes.Files
	{
	class MFSTFile : CCHFile
		{
		//#defines
		private const int MSGID_PAYLOAD = 0x71F617D0;
		private const int MSGID_METADATA = 0x1F4812BE;
		private const int MSGID_SIGNATURE = 0x1B81B817;
		private const int MSGID_FILETERMINATOR = 0x32C415AB;

		//Variables
		private BinaryReader b;
		public List<PackageEntry> pkEntries;

		public MFSTFile(string file, bool OnlyEntries)
			: base(file, OnlyEntries)
			{
			szFullFile = file;
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
				b = new BinaryReader(fs);
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
			UInt32 MsgID = 0;
			UInt32 MsgSize = 0;

			try
				{
				//PAYLOAD SECTION
				MsgID = b.ReadUInt32();
				if (MsgID != MSGID_PAYLOAD)
					return false;
				MsgSize = b.ReadUInt32();
				b.ReadBytes((int)MsgSize);
				//METADATA SECTION
				MsgID = b.ReadUInt32();
				if (MsgID != MSGID_METADATA)
					return false;
				MsgSize = b.ReadUInt32();
				b.ReadBytes((int)MsgSize);
				//SIGNATURE SECTION
				MsgID = b.ReadUInt32();
				if (MsgID != MSGID_SIGNATURE)
					return false;
				MsgSize = b.ReadUInt32();
				b.ReadBytes((int)MsgSize);
				//TERMINATOR SECTION
				MsgID = b.ReadUInt32();
				if (MsgID != MSGID_FILETERMINATOR)
					return false;
				//Rewind
				fs.Seek(0, SeekOrigin.Begin);
				return true;
				}
			catch (Exception)
				{
				return false;
				}
			}

		public override bool UnMapDataStructure()
			{
			return true;
			}

		/////////////////////////////////////////////////////////////////////////////////
		/// Création de l'arborescence du gcf
		/////////////////////////////////////////////////////////////////////////////////
		public override bool CreateRoot(BackgroundWorker bgworker = null)
			{
			bool CheckSizes = false;
			string szInstallDir = "";
			double dLastUpdate = 0;
			string szTmpFile = "";
			FileInfo f;
			PackageEntry OldEntry = new PackageEntry();
			PackageEntry oParent = new PackageEntry();

			ACFFile acFile = this.GetInstallDir();
			if (acFile != null)
				{
				szInstallDir = acFile.GetAppInstallDir();
				dLastUpdate = acFile.GetlastUpdate();
				}

			if (szInstallDir != "")
				{
				DirectoryInfo cdi = new DirectoryInfo(szInstallDir);
				if (cdi.Exists)
					CheckSizes = true;
				else
					szInstallDir = "";
				}

			AddChild("Root", null, szInstallDir);

			List<ManifestFileInfo> arFilesInfos = GetAllFilesInfo();

			double dTotal = (double)(arFilesInfos.Count*2);

			int nIdx = 0;

			foreach (ManifestFileInfo mInfo in arFilesInfos)
				{
				if (bgworker != null)
					{
					double dIndex = (double)(nIdx);
					double dProgressPercentage = (dIndex / dTotal);
					int iProgressPercentage = (int)(dProgressPercentage * 100);
					bgworker.ReportProgress(iProgressPercentage);
					nIdx++;
					}
				PackageEntry Entry = new PackageEntry();
				if (mInfo.IsDir)
					{
					Entry.FileExt = "";
					Entry.DirectoryName = "Root\\" + Path.GetDirectoryName(mInfo.szName);
					Entry.FileName = "";
					Entry.isDir = true;
					oParent = GetParent(Entry,szInstallDir);
					continue;
					}
				Entry.FileExt = FilesTools.GetExtension(mInfo.szName);
				Entry.DirectoryName = "Root\\" + Path.GetDirectoryName(mInfo.szName);
				Entry.FileName = Path.GetFileName(mInfo.szName);
				Entry.uiCRC = 0;
				Entry.usPreloadBytes = 0;
				Entry.usArchiveIndex = 0;
				Entry.uiEntryOffset = 0;
				Entry.uiEntryTotSize = 0;
				Entry.uiEntryLength = 0;
				//Récupérer taille du fichier
				if (CheckSizes)
					{
					szTmpFile = szInstallDir + "\\" + mInfo.szName;
					f = new FileInfo(szTmpFile);
					if (f.Exists)
						{
						Entry.szSteamPath = szTmpFile;
						Entry.uiEntryTotSize = (uint)f.Length;
						Entry.uiEntryLength = Entry.uiEntryTotSize;
						}
					}
				Entry.usTerminator = 0;
				Entry.isDir = false;
				Entry.Flags = 0x4000;

				Entry.szOnlyName = Entry.FileName;
				Entry.szFullPath = Entry.DirectoryName + "\\" + Entry.szOnlyName;
				oParent = GetParent(Entry, szInstallDir);
				Entry.iParentIdx = oParent.iIdx;
				Entry.FileOffset = fs.Position;
				Entry.iIdx = pkEntries.Count;
				pkEntries.Add(Entry);
				}

			string shellType = string.Empty;
			int imgIdx = -1;
			foreach (PackageEntry Entry in pkEntries)
				{
				if (bgworker != null)
					{
					double dIndex = (double)(nIdx);
					double dProgressPercentage = (dIndex / dTotal);
					int iProgressPercentage = (int)(dProgressPercentage * 100);
					bgworker.ReportProgress(iProgressPercentage);
					nIdx++;
					}
				string szExt = "." + FilesTools.GetExtension(Entry.szOnlyName).ToLower();
				shellType = GetFileTypeText(szExt);
				imgIdx = Entry.Flags==0 ? 0 : GetIconImageIndex(szExt);
				arEntries.Add(new CacheEntry(this, (uint)Entry.iIdx, Entry.uiEntryTotSize, Entry.uiEntryLength, Entry.Flags, (uint)Entry.iParentIdx, Entry.szOnlyName, Entry.szFullPath, (uint)Entry.usArchiveIndex, Entry.uiEntryOffset, Entry.FileOffset, Entry.usPreloadBytes, shellType, imgIdx,Entry.szSteamPath));
				}
			pkEntries.Clear();
			return true;
			}

		private ACFFile GetInstallDir()
			{
			//Search for AppId
			List<int> AppsIds = ModsTools.GetAllAppsIDs();

			bool bFound = false;

			string szFileName = Path.GetFileName(szFullFile).ToLower();

			foreach (int appID in AppsIds)
				{
				if (bFound)
					break;
				//Open appID manifest file
				ACFFile acfile = new ACFFile(appID);
				List<string> ManifestFiles = acfile.GetDepotsFiles();
				foreach (string manifest in ManifestFiles)
					{
					if (Path.GetFileName(manifest).ToLower() == szFileName)
						return acfile;
					}
				}
			return null;
			}


		private List<ManifestFileInfo> GetAllFilesInfo()
			{
			List<ManifestFileInfo> FilesInfo = new List<ManifestFileInfo>();
			UInt32 MsgID = 0;
			UInt32 PayLoadSize = 0;
			int TotalBytesRead = 0;
			string szPayLoadBuffer = "";
			byte nFoo = 0;
			byte nCtrl1 = 0;
			byte nCtrl2 = 0;
			int nDataSize = 0;
			int nFilenameLength = 0;
			long SavedPos = 0;
			UInt16 flags = 0;
			bool IsDir = false;
			DateTime Tm = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();


			//Début de la lecture;
			b.BaseStream.Seek(0, SeekOrigin.Begin);
			MsgID = b.ReadUInt32();
			if (MsgID == MSGID_PAYLOAD)
				{
				PayLoadSize = b.ReadUInt32();

				while (TotalBytesRead < PayLoadSize)
					{
					nFoo = b.ReadByte();
					TotalBytesRead += 1;
					nDataSize = b.ReadByte();
					TotalBytesRead += 1;
					nCtrl1 = b.ReadByte();
					TotalBytesRead += 1;
					if (nCtrl1 != 0x0a)
						{
						nDataSize += (nCtrl1 - 1) * 128;
						b.ReadByte();
						TotalBytesRead += 1;
						}
					else
						{
						SavedPos = b.BaseStream.Position;
						nFilenameLength = b.ReadByte();
						b.ReadBytes(nFilenameLength);
						nCtrl2 = b.ReadByte();
						b.BaseStream.Seek(SavedPos, SeekOrigin.Begin);
						if (nCtrl2 != 0x10)
							{
							nDataSize += (nCtrl1 - 1) * 128;
							b.ReadByte();
							TotalBytesRead += 1;
							}
						}
					nFilenameLength = b.ReadByte();
					TotalBytesRead += 1;
					byte[] buffer = new byte[nFilenameLength];
					buffer = b.ReadBytes(nFilenameLength);
					szPayLoadBuffer = System.Text.Encoding.UTF8.GetString(buffer).Replace("/", "\\");
					TotalBytesRead += nFilenameLength;
					SavedPos = b.BaseStream.Position;
					//On cherche les flags
					IsDir = false;
					nFoo = b.ReadByte(); //0x10
					flags = b.ReadUInt16();
					if (flags == 0x1800)
						{
						IsDir = true;
						}
					////Creation Package Entry
					FilesInfo.Add(new ManifestFileInfo(szPayLoadBuffer, IsDir, 0, Tm));
					b.BaseStream.Seek(SavedPos, SeekOrigin.Begin);
					b.ReadBytes(nDataSize - 2 - nFilenameLength);
					TotalBytesRead += nDataSize - 2 - nFilenameLength;
					}
				}
			return FilesInfo;
			}

		public PackageEntry AddChild(string szFolderName, PackageEntry pkeParent, string szInstallDir)
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

			string szName = szFolderName.Replace("Root","");
			string szTmpFile = szInstallDir + szName;
			if (FilesTools.CheckDestinationPath(szTmpFile))
				pkE.szSteamPath = szTmpFile;
			pkEntries.Add(pkE);
			return pkE;
			}

		public PackageEntry GetParent(PackageEntry Entry,string szInstallDir)
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
					pkeChild = AddChild(szFolder, pkeParent,szInstallDir);
					pkeParent.uiEntryLength++;
					}
				pkeParent.ChildNodes[szFolder] = pkeChild;
				pkeParent = pkeChild;
				}
			return pkeParent;
			}

		/////////////////////////////////////////////////////////////////////////////////
		/// Création de l'arborescence du gcf
		/////////////////////////////////////////////////////////////////////////////////
		public List<string> CreateFullRoot(string sModInstallDir, string sIdTmp)
			{
			string szFileName = string.Empty;
			string szIdent = string.Empty;
			string szGauche = string.Empty;
			List<string> LstVpkFiles = new List<string>();
			List<string> FilesNames = new List<string>();
			List<ManifestFileInfo> arFilesInfos = GetAllFilesInfo();

			if (sIdTmp.Trim() != string.Empty)
				szIdent = sIdTmp.Trim().ToLower() + "\\";

			foreach (ManifestFileInfo mInfo in arFilesInfos)
				{
				if (mInfo.IsDir)
					continue;

				if (FilesTools.GetExtension(mInfo.szName) == "vpk")
					{
					if (mInfo.szName.Length > 8)
						{
						if (mInfo.szName.Substring(mInfo.szName.Length - 8).ToLower() == "_dir.vpk")
							{
							LstVpkFiles = GetVpkFileContent(mInfo.szName, sModInstallDir);
							if (LstVpkFiles != null)
								FilesNames.AddRange(LstVpkFiles);
							}
						}
					}
				else
					{
					string szPrefix = ModsTools.Get_Full_Common_From_Mod(mInfo.szName, sModInstallDir, true);
					szFileName = szPrefix + "\\" + Path.GetFileName(mInfo.szName);
					FilesNames.Add(szFileName);
					}
				}
			return FilesNames;
			}



		private List<string> GetVpkFileContent(string vpkpakfile, string sModInstallDir)
			{
			List<string> vpkFiles = new List<string>();

			CCHFile myCacheFile;
			if (sModInstallDir == "")
				return null;

			string szPrefix = ModsTools.Get_Full_Common_From_Mod(vpkpakfile, sModInstallDir, true);

			string vpkfile = sModInstallDir + "\\" + vpkpakfile;

			myCacheFile = (VPKFile)new VPKFile(vpkfile, true);

			if (myCacheFile.Open())
				{
				if (!myCacheFile.MapDataStructure())
					{
					myCacheFile.Close();
					return null;
					}
				myCacheFile.CreateLightRoot(szPrefix);
				myCacheFile.Close();
				return myCacheFile.szList;
				}
			return null;
			}

		}



	public class ManifestFileInfo
		{
		public string szName;
		public bool IsDir;
		public long FileLength;
		public DateTime FileDate;

		public ManifestFileInfo(string name, bool dir, long FlLg, DateTime FlDt)
			{
			szName = name;
			IsDir = dir;
			FileLength = FlLg;
			FileDate = new DateTime();
			FileDate = FlDt;
			}
		}
	}
