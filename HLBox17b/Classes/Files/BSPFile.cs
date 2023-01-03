using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using HLBox17b.Classes.Tools;


namespace HLBox17b.Classes.Files
	{
	class BSPFile
		{
		[StructLayout(LayoutKind.Sequential)]
		public struct ModelHeader
			{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public char[] id; // Orriginal header is int, but this is easier
			public int version;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
			public char[] name;
			public int length;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public float[] eyeposition;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public float[] min;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public float[] max;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public float[] bbmin;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public float[] bbmax;
			public int flags;
			public int numbones;
			public int boneindex;
			public int numbonecontrollers;
			public int bonecontrollerindex;
			public int numhitboxes;
			public int hitboxindex;
			public int numseq;
			public int seqindex;
			public int numseqgroups;
			public int seqgroupindex;
			public int numtextures; // Number of textures
			public int textureindex; // Index of texture location - 0 means no textures present
			public int texturedataindex;
			};


		// Header des fichiers BSP pour HL1
		[StructLayout(LayoutKind.Sequential)]
		public struct posinfo
			{
			public uint fileofs;
			public uint filelen;
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct hl1_bsp_header
			{
			public int bsp_version;						// Version
			public posinfo ent_header;							// Position Entités
			public posinfo foo_nocare;							// Foo
			public posinfo tex_header;							// Position Textures
			};
		// Header des fichiers BSP pour HL2
		[StructLayout(LayoutKind.Sequential)]
		public struct lump_t
			{
			public uint fileofs;											// offset into file (bytes)
			public uint filelen;											// length of lump(bytes)
			public uint version;											// lump format version
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public char[] fourCC;										// lump ident code
			};
		[StructLayout(LayoutKind.Sequential)]
		public struct hl2_bsp_header
			{
			public int ident;								// Identifiant
			public int bsp_version;						// Version
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
			public lump_t[] entities;						// Entities
			public int maprev;								// Revision de la map
			};
		// Datas des Textures pour HL1
		[StructLayout(LayoutKind.Sequential)]
		public struct texdata_s
			{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public char[] name;									// Nom de la texture
			public int width;								// Largeur
			public int height;								// Hauteur
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] offsets;								// Offsets
			};


		[StructLayout(LayoutKind.Explicit, Size = 22)]
		public struct ZIPEndOfCentralDirectoryRecord
			{
			[FieldOffset(0)]
			public uint uiSignature; // 4 bytes (0x06054b50)
			[FieldOffset(4)]
			public UInt16 uiNumberOfThisDisk;  // 2 bytes
			[FieldOffset(6)]
			public UInt16 uiNumberOfTheDiskWithStartOfCentralDirectory; // 2 bytes
			[FieldOffset(8)]
			public UInt16 uiCentralDirectoryEntriesThisDisk;	// 2 bytes
			[FieldOffset(10)]
			public UInt16 uiCentralDirectoryEntriesTotal;	// 2 bytes
			[FieldOffset(12)]
			public uint uiCentralDirectorySize; // 4 bytes
			[FieldOffset(16)]
			public uint uiStartOfCentralDirOffset; // 4 bytes
			[FieldOffset(20)]
			public UInt16 uiCommentLength; // 2 bytes
			// zip file comment follows
			};

		[StructLayout(LayoutKind.Explicit, Size = 46)]
		public struct ZIPFileHeader
			{
			[FieldOffset(0)]
			public uint uiSignature; //  4 bytes (0x02014b50) 
			[FieldOffset(4)]
			public UInt16 uiVersionMadeBy; // version made by 2 bytes 
			[FieldOffset(6)]
			public UInt16 uiVersionNeededToExtract; // version needed to extract 2 bytes 
			[FieldOffset(8)]
			public UInt16 uiFlags; // general purpose bit flag 2 bytes 
			[FieldOffset(10)]
			public UInt16 uiCompressionMethod; // compression method 2 bytes 
			[FieldOffset(12)]
			public UInt16 uiLastModifiedTime; // last mod file time 2 bytes 
			[FieldOffset(14)]
			public UInt16 uiLastModifiedDate; // last mod file date 2 bytes 
			[FieldOffset(16)]
			public uint uiCRC32; // crc-32 4 bytes 
			[FieldOffset(20)]
			public uint uiCompressedSize; // compressed size 4 bytes 
			[FieldOffset(24)]
			public uint uiUncompressedSize; // uncompressed size 4 bytes 
			[FieldOffset(28)]
			public UInt16 uiFileNameLength; // file name length 2 bytes 
			[FieldOffset(30)]
			public UInt16 uiExtraFieldLength; // extra field length 2 bytes 
			[FieldOffset(32)]
			public UInt16 uiFileCommentLength; // file comment length 2 bytes 
			[FieldOffset(34)]
			public UInt16 uiDiskNumberStart; // disk number start 2 bytes 
			[FieldOffset(36)]
			public UInt16 uiInternalFileAttribs; // internal file attributes 2 bytes 
			[FieldOffset(38)]
			public uint uiExternalFileAttribs; // external file attributes 4 bytes 
			[FieldOffset(42)]
			public uint uiRelativeOffsetOfLocalHeader; // relative offset of local header 4 bytes 
			// file name (variable size) 
			// extra field (variable size) 
			// file comment (variable size) 
			};



		[StructLayout(LayoutKind.Explicit, Size = 30)]
		public struct ZIPLocalFileHeader
			{
			[FieldOffset(0)]
			public uint uiSignature; //local file header signature 4 bytes (0x04034b50) 
			[FieldOffset(4)]
			public ushort uiVersionNeededToExtract; // version needed to extract 2 bytes 
			[FieldOffset(6)]
			public ushort uiFlags; // general purpose bit flag 2 bytes 
			[FieldOffset(8)]
			public ushort uiCompressionMethod; // compression method 2 bytes 
			[FieldOffset(10)]
			public ushort uiLastModifiedTime; // last mod file time 2 bytes 
			[FieldOffset(12)]
			public ushort uiLastModifiedDate; // last mod file date 2 bytes 
			[FieldOffset(14)]
			public uint uiCRC32; // crc-32 4 bytes 
			[FieldOffset(18)]
			public uint uiCompressedSize; // compressed size 4 bytes 
			[FieldOffset(22)]
			public uint uiUncompressedSize; // uncompressed size 4 bytes 
			[FieldOffset(26)]
			public ushort uiFileNameLength; // file name length 2 bytes 
			[FieldOffset(28)]
			public ushort uiExtraFieldLength; // extra field length 2 bytes 
			// file name (variable size) 
			// extra field (variable size) 
			// file data (variable size) 
			};

		[StructLayout(LayoutKind.Sequential)]
		public struct LMPHeader
			{
			int iLumpOffset;
			int iLumpID;
			int iLumpVersion;
			int iLumpLength;
			int iMapRevision;
			};

		//#defines
		private const int HL1BSPVERSION = 30;
		private const int HL2BSPVERSION = 19;
		private const int HL2BSPVERSION_1 = 17;
		private const int HL2BSPVERSION_2 = 21;
		private const int UNKNOW = 0;
		public const int HL1BSP = 1;
		public const int HL2BSP = 2;
		public const int HL_BSP_MIPMAP_COUNT = 4;

		private const int HL_VBSP_LUMP_ENTITIES = 0;
		private const int HL_VBSP_LUMP_PAKFILE = 40;

		private const int HL_VBSP_ZIP_LOCAL_FILE_HEADER_SIGNATURE = 0x04034b50;
		private const int HL_VBSP_ZIP_FILE_HEADER_SIGNATURE = 0x02014b50;
		private const int HL_VBSP_ZIP_END_OF_CENTRAL_DIRECTORY_RECORD_SIGNATURE = 0x06054b50;
		private const int HL_VBSP_ZIP_CHEKSUM_LENGHT = 0x00008000;

		//Variables
		private string szFullFile;					// Chemin complet du fichier
		public string szRootPath;					// Chemin du répertoire steam
		private string szFileName;					// Nom du fichier
		private string szFileExt;					// Extension du fichier
		private string szMapName;					// Nom du fichier (sans extension ni path)
		private string szFilePath;					// Chemin du fichier
		private FileStream fs;							// FileStream d'accès au fichier
		public int nBspVer;						// Version du BSP
		public int nFilVer;						// Version du BSP
		public int nNumSlots;					// Nombre de Slots de la Map
		public long lFileLen;					// Taille du fichier
		private posinfo EntPos;						// Position des entites
		private posinfo TexLst;						// Position de la Liste des Textures
		private posinfo TexPos;						// Position des textures
		public string szEntBuf;					// Buffer recevant les entitiés
		public List<string> ExtTexs;						// Liste des Textures inclues
		public List<string> IntTexs;						// Liste des Textures inclues
		public List<string> ZipEnts;						// Liste des Entities inclues au format zip
		public List<string> WadsLst;						// Liste des wads nécessaires au fichier
		public List<string> ResrcLst;						// Liste des ressources nécessaires au fichier
		public List<string> CleanLst;						// Liste des ressources nettoyées nécessaires au fichier
		public List<string> FilesLst;						// Liste des fichiers ok
		public List<string> FilesErr;						// Liste des fichiers en erreur
		public List<Dictionary<string, string>> Entities;	// Liste des Entities incluses
		public uint uiTotalTex;								//Nbre Total de Textures
		public uint uiLumpOffset;							//Offset du début de la zone des Textures
		private List<int> TexturesOffsets;					//Tableau d'offset de chaque texture dans le fichier
		public BSPFile(string file)
			{
			szFullFile = file;
			}
		public bool Init()
			{
			//Extension
			string szTmp = Path.GetExtension(szFullFile);
			char[] charsToTrim = { '.' };
			szFileExt = szTmp.TrimStart(charsToTrim);
			//Nom du fichier
			szFileName = Path.GetFileName(szFullFile);
			//Nom de la Map
			szMapName = Path.GetFileNameWithoutExtension(szFullFile);
			//Chemin complet
			szFilePath = Path.GetDirectoryName(szFullFile);
			//Chemin complet
			int nPos = szFilePath.LastIndexOf("\\maps", StringComparison.OrdinalIgnoreCase);
			if (nPos != -1)
				szRootPath = szFilePath.Substring(0, nPos);
			else
				szRootPath = "";
			//Taille du fichier
			lFileLen = 0;
			//Version du BSP
			nBspVer = UNKNOW;
			nFilVer = UNKNOW;
			nNumSlots = 0;
			// Initialisation Listes
			ExtTexs = new List<string>();
			IntTexs = new List<string>();
			ZipEnts = new List<string>();
			WadsLst = new List<string>();
			ResrcLst = new List<string>();
			CleanLst = new List<string>();
			FilesLst = new List<string>();
			FilesErr = new List<string>();
			TexturesOffsets = new List<int>();
			Entities = new List<Dictionary<string, string>>();
			return true;
			}
		public void ThrowError(string szTxt)
			{
			//			if (Program.bVerbose==false)
			//				MessageBox.Show(szTxt, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		public bool Open()
			{
			FileInfo fi = new FileInfo(szFullFile);
			if (fi.Exists)
				{
				try
					{
					fs = new FileStream(szFullFile, FileMode.Open, FileAccess.Read);
					lFileLen = fi.Length;
					}
				catch (Exception ex)
					{
					ThrowError(ex.Message);
					}
				if (fs != null)
					{
					CleanLst.Add(szFullFile);
					return true;
					}
				}
			else
				{
				ThrowError(szFullFile + "\n\nFile not Found");
				}
			return false;
			}
		public bool Close()
			{
			fs.Close();
			return true;
			}

		public bool ReadHeader()
			{
			hl1_bsp_header header1 = StreamTools.ReadStruct<hl1_bsp_header>(fs, 0);

			if (header1.bsp_version != HL1BSPVERSION)
				{
				hl2_bsp_header header2 = StreamTools.ReadStruct<hl2_bsp_header>(fs, 0);

				if ((header2.bsp_version < HL2BSPVERSION_1 || header2.bsp_version > HL2BSPVERSION_2) && header2.bsp_version != 0x00040014)
					{
					ThrowError("Incorrect file version");
					return false;
					}
				nBspVer = HL2BSP;
				nFilVer = header2.bsp_version;
				ReadLumps();
				}
			else
				{
				nBspVer = HL1BSP;
				nFilVer = header1.bsp_version;
				EntPos.fileofs = header1.ent_header.fileofs;
				EntPos.filelen = header1.ent_header.filelen;
				TexPos.fileofs = header1.tex_header.fileofs;
				TexPos.filelen = header1.tex_header.filelen;
				ReadHL1Textures();
				}

			if (ExtTexs.Count() != 0)
				ExtTexs.Sort();

			if (IntTexs.Count() != 0)
				IntTexs.Sort();
			return true;
			}
		/// <summary>
		/// Lecture des textures
		/// </summary>
		/// <returns></returns>
		private void ReadHL1Textures()
			{
			int i = 0;
			int nTexCnt = 0;


			if (TexPos.fileofs < lFileLen)
				{
				fs.Seek(TexPos.fileofs, SeekOrigin.Begin);
				nTexCnt = (int)StreamTools.ReadUInt(fs);
				}
			else
				{
				ThrowError("File Error: Truncated Map File.");
				}


			uiTotalTex = (uint)nTexCnt;
			uiLumpOffset = TexPos.fileofs;

			List<int> TexOffsets = new List<int>();

			for (i = 0; i < nTexCnt; i++)
				{
				TexOffsets.Add((int)StreamTools.ReadUInt(fs));
				}

			uint nPos = 0;
			texdata_s TxDats;
			string name;

			for (i = 0; i < nTexCnt; i++)
				{
				nPos = (uint)TexPos.fileofs + (uint)TexOffsets.ElementAt(i);
				TxDats = StreamTools.ReadStruct<texdata_s>(fs, nPos);
				name = new string(TxDats.name);
				nPos = (uint)name.IndexOf("\0", 0);
				name = name.Substring(0, (int)nPos);
				name = name.Trim();
				if (name != "")
					{
					if (TxDats.offsets[0] == 0 && TxDats.offsets[1] == 0 && TxDats.offsets[2] == 0 && TxDats.offsets[3] == 0)
						{
						if (!ExtTexs.Contains(name))
							{
							ExtTexs.Add(name);
							}
						}
					else
						{
						if (!IntTexs.Contains(name))
							{
							IntTexs.Add(name);
							}
						}
					}
				}
			}


		/// <summary>
		/// Lecture des entités
		/// </summary>
		/// <returns></returns>
		public bool ReadEntities()
			{
			byte[] buffer = new byte[EntPos.filelen];
			long nRetOff = 0;
			int BytesRead = 0;
			szEntBuf = "";
			if (EntPos.fileofs > lFileLen)
				return false;
			nRetOff = fs.Seek(EntPos.fileofs, SeekOrigin.Begin);
			if (nRetOff != EntPos.fileofs)
				return false;
			if ((EntPos.fileofs + EntPos.filelen) > lFileLen)
				return false;
			BytesRead = fs.Read(buffer, 0, (int)EntPos.filelen);
			if (BytesRead != EntPos.filelen)
				return false;
			szEntBuf = System.Text.Encoding.UTF8.GetString(buffer);
			return true;
			}

		public bool ParseDatas()
			{
			string szTmp = "";
			string szKey = "";
			string szVal = "";
			string szResTmp = "";
			string szResExt = "";
			string szOnlyName = "";

			Dictionary<string, string> _strKeys;
			Regex _keyValueRegex = new Regex("{(?<Entitie>[^}]*)}");
			MatchCollection EntsList = _keyValueRegex.Matches(szEntBuf);
			_keyValueRegex = new Regex("\"(?<Key>[^\"]*)\"\\s\"(?<Value>[^\"]*)\"");

			foreach (Match Entitie in EntsList)
				{
				GroupCollection groups = Entitie.Groups;
				szTmp = groups["Entitie"].Value;
				MatchCollection KeysList = _keyValueRegex.Matches(szTmp);
				_strKeys = new Dictionary<string, string>();
				foreach (Match key in KeysList)
					{
					GroupCollection gKeys = key.Groups;
					szKey = gKeys["Key"].Value;
					if (!_strKeys.ContainsKey(szKey))
						{
						szVal = gKeys["Value"].Value;
						szVal = szVal.Replace("/", "\\");
						_strKeys[szKey] = szVal;
						szKey = szKey.ToLower();
						if (szKey == "wad")
							{
							AddWads(szVal);
							continue;
							}
						if (szKey == "skyname")
							{
							AddSkies(szVal);
							continue;
							}

						szResTmp = szVal.ToLower();
						szResExt = FilesTools.GetExtension(szResTmp);

						if (szResTmp == "info_player_start" ||
							szResTmp == "info_player_deathmatch" ||
							szResTmp == "info_player_allies" ||
							szResTmp == "info_player_axis" ||
							szResTmp == "info_initial_player_allies" ||
							szResTmp == "info_initial_player_axis" ||
							szResTmp == "info_player_counterterrorist" ||
							szResTmp == "info_player_terrorist" ||
							szResTmp == "info_player_teamspawn" ||
							szResTmp == "info_survivor_position"
							)
							{
							nNumSlots++;
							}

						if (nBspVer == HL2BSP)
							{
							szOnlyName = FilesTools.GetOnlyName(szVal);

							if (szResTmp.IndexOf("decals/") >= 0)
								{
								szVal = szVal.Replace(".vmt", "");
								AddResrc(szVal, "", ".vmt");
								continue;
								}

							if (szResExt == "spr")
								{
								szOnlyName = "materials\\Sprites\\" + szOnlyName;
								AddResrc(szOnlyName, "", ".vmt");
								continue;
								}

							if (szKey == "texture")
								{
								szVal = szVal.Replace(".vmt", "");
								szOnlyName = "materials";
								if (!szVal.StartsWith("\\"))
									szOnlyName += "\\";
								szOnlyName += szVal;
								AddResrc(szOnlyName, "", ".vmt");
								continue;
								}
							if (szKey == "ropematerial")
								{
								szVal = szVal.Replace(".vmt", "");
								szOnlyName = "materials\\" + szVal;
								AddResrc(szOnlyName, "", ".vmt");
								continue;
								}

							if (szKey.StartsWith("point_hud_icon_"))
								{
								szVal = szVal.Replace(".vmt", "");
								szOnlyName = "materials\\" + szVal;
								AddResrc(szOnlyName, "", ".vmt");
								continue;
								}
							}

						if (szResExt == "bmp" ||
							szResExt == "spr" ||
							szResExt == "tga" ||
							szResExt == "txt" ||
							szResExt == "wav" ||
							szResExt == "mp3" ||
							szResExt == "jpg" ||
							szResExt == "pcx" ||
							szResExt == "nav" ||
							szResExt == "vpk" ||
							szResExt == "vmt" ||
							szResExt == "vtf" ||
							szResExt == "ain" ||
							szResExt == "ani" ||
							szResExt == "nod" ||
							szResExt == "vcs" ||
							szResExt == "ctx" ||
							szResExt == "vtx" ||
							szResExt == "phy" ||
							szResExt == "vvd" ||
							szResExt == "mat" ||
							szResExt == "pxp" ||
							szResExt == "wpt" ||
							szResExt == "mdl" ||
							szResExt == "wd" ||
							szResExt == "bns" ||
							szResExt == "bmz")
							{
							AddResrc(szVal, "", "");
							}

						if (szResExt == "mdl" && nBspVer != HL2BSP)
							{
							CheckModelExtTexture(szVal);
							}
						}
					}
				Entities.Add(_strKeys);
				}
			return true;
			}


		//////////////////////////////////////////////////////////////////////
		// Vérifier models 
		//////////////////////////////////////////////////////////////////////
		void CheckModelExtTexture(string szVal)
			{
			int nPos;
			string szMdlName;
			FileStream fsmdl = null;

			szVal = szVal.Replace('/', '\\');
			nPos = szVal.IndexOf("models\\", StringComparison.OrdinalIgnoreCase);
			if (nPos != -1)
				szVal = szVal.Substring(nPos);
			szMdlName = szRootPath + "\\" + szVal;

			FileInfo fimdl = new FileInfo(szMdlName);

			if (!fimdl.Exists)
				return;

			try
				{
				fsmdl = new FileStream(szMdlName, FileMode.Open, FileAccess.Read);
				}
			catch (Exception)
				{
				return;
				}

			if (fsmdl == null)
				return;

			ModelHeader mdlheader = StreamTools.ReadStruct<ModelHeader>(fsmdl, 0);

			string szId = new string(mdlheader.id);
			if (szId != "IDST")
				{
				fsmdl.Close();
				return;
				}
			if (mdlheader.version != 10)
				{
				fsmdl.Close();
				return;
				}
			if (mdlheader.textureindex != 0)
				{
				fsmdl.Close();
				return;
				}
			fsmdl.Close();

			szMdlName = szVal.Replace(".mdl", "") + "t.mdl";
			AddResrc(szMdlName, "", "");
			return;
			}

		//////////////////////////////////////////////////////////////////////
		// Séparer et ajouter les fichiers wad
		//////////////////////////////////////////////////////////////////////
		private bool AddWads(string Wadlist)
			{
			string szTmp = "";
			Wadlist = Wadlist.TrimEnd(';');
			string[] wads = Wadlist.Split(';');

			foreach (string wad in wads)
				{
				if (FilesTools.HasInvalidChars(wad))
					continue;
				szTmp = wad.Trim();
				szTmp = Path.GetFileName(szTmp).ToLower();
				if (szTmp != "")
					{
					if (!WadsLst.Contains(szTmp))
						{
						AddResrc(szTmp, "", "");
						WadsLst.Add(szTmp);
						}
					}
				}
			return true;
			}

		private bool AddSkies(string szVal)
			{
			string szTmp = "";

			if (nBspVer == HL1BSP)
				{
				szTmp = "gfx\\env\\";
				AddResrc(szVal, szTmp, "up.tga");
				AddResrc(szVal, szTmp, "dn.tga");
				AddResrc(szVal, szTmp, "lf.tga");
				AddResrc(szVal, szTmp, "rt.tga");
				AddResrc(szVal, szTmp, "ft.tga");
				AddResrc(szVal, szTmp, "bk.tga");
				}
			else
				{
				szTmp = "materials\\skybox\\";
				AddResrc(szVal, szTmp, "up.vmt");
				AddResrc(szVal, szTmp, "up.vtf");
				AddResrc(szVal, szTmp, "dn.vmt");
				AddResrc(szVal, szTmp, "dn.vtf");
				AddResrc(szVal, szTmp, "lf.vmt");
				AddResrc(szVal, szTmp, "lf.vtf");
				AddResrc(szVal, szTmp, "rt.vmt");
				AddResrc(szVal, szTmp, "rt.vtf");
				AddResrc(szVal, szTmp, "ft.vmt");
				AddResrc(szVal, szTmp, "ft.vtf");
				AddResrc(szVal, szTmp, "bk.vmt");
				AddResrc(szVal, szTmp, "bk.vtf");
				}
			return true;
			}

		private bool AddResrc(string szVal, string szPrf, string szSuf)
			{
			string stToAdd = szPrf + szVal + szSuf;
			if (stToAdd == "")
				return false;
			if (!ResrcLst.Contains(stToAdd))
				ResrcLst.Add(stToAdd);
			return true;
			}

		public string CheckEntities()
			{
			string szRapport = "";
			string szLine = "";
			int nNumEnt = 0;
			foreach (Dictionary<string, string> Dic in Entities)
				{
				szLine = String.Format("{0}:\r\n", nNumEnt);
				szRapport += szLine;
				foreach (KeyValuePair<string, string> pair in Dic)
					{
					szLine = String.Format("\t{0} => {1}\r\n", pair.Key, pair.Value);
					szRapport += szLine;
					}
				nNumEnt++;
				}
			return szRapport;
			}

		public bool CheckPath()
			{
			string szExt = "";
			string szNewPath = "";
			List<string> arMaps = new List<string>();
			arMaps.Add(szMapName);

			foreach (string szEntity in ResrcLst)
				{
				if (szEntity == "")
					continue;
				szExt = FilesTools.GetExtension(szEntity);
				if (szExt == "wad" && ExtTexs.Count == 0)
					continue;
				if (FilesTools.HasInvalidChars(szEntity))
					continue;
				szNewPath = MiscTools.GetSteamPath(szFileName, arMaps, szEntity);
				if (szNewPath.StartsWith(Program.mRc.szInst_MiscPath + "\\"))
					continue;
				CleanLst.Add(szRootPath + "\\" + szNewPath + Path.GetFileName(szEntity));
				}
			return true;
			}
		//Vérifier et ajouter un fichier ou répertoire à la liste
		public bool CheckAndAddFile(string szSrchFile)
			{
			bool bFound = false;
			string szExt;

			FileInfo fi;
			fi = new FileInfo(szSrchFile);
			if (fi.Exists)
				{
				if (!Directory.Exists(szSrchFile))
					{
					bFound = true;
					CleanLst.Add(szSrchFile);
					}
				return bFound;
				}
			else
				{
				string szTmpFile;
				try
					{
					DirectoryInfo cdi = new DirectoryInfo(szSrchFile);
					if (cdi.Exists)
						{
						FileInfo[] subFiles = cdi.GetFiles();
						if (subFiles.Length > 0)
							{
							foreach (FileInfo subFile in subFiles)
								{
								//on n'ajoute pas les thumb.db
								if (subFile.Name.ToLower() == "thumbs.db")
									continue;
								//on n'ajoute pas non plus les www.17buddies.net
								if (subFile.Name.ToLower() == "www.17buddies.net.txt")
									continue;
								//on n'ajoute pas les .url ni les .html ni les exe ni les dll ni les archives
								szExt = FilesTools.GetExtension(subFile.Name);
								if (szExt == "url" || szExt == "html" || szExt == "exe" || szExt == "dll" || szExt == "rar" || szExt == "zip" || szExt == "7z")
									continue;
								bFound = true;
								byte[] bts = System.Text.Encoding.UTF8.GetBytes(subFile.Name);
								//szTmpFile = subFile.DirectoryName + "\\" + subFile.Name;
								szTmpFile = subFile.DirectoryName + "\\" + System.Text.Encoding.UTF8.GetString(bts);

								CleanLst.Add(szTmpFile);
								}
							}
						return bFound;
						}
					}
				catch (Exception)
					{
					return bFound;
					}
				}
			return bFound;
			}

		public bool DeleteFromCleanList(List<int> IdxesLst)
			{
			foreach (int Idx in IdxesLst)
				{
				CleanLst[Idx] = "";
				}
			return true;
			}

		public void CheckExistingFiles()
			{
			FileInfo fi;
			string szRes = "";

			int nCount = CleanLst.Count;
			int i = 0;

			for (i = nCount; i > 0; i--)
				{
				szRes = CleanLst[i - 1].Trim();
				if (szRes == "")
					{
					CleanLst.RemoveAt(i - 1);
					continue;
					}
				fi = new FileInfo(szRes);
				if (fi.Exists)
					FilesLst.Add(szRes);
				szRes = szRes.Replace(szRootPath + "\\", "");
				CleanLst[i - 1] = szRes.Replace("\\", "/");
				}
			return;
			}

		public bool FindAllOthersFiles()
			{
			string szSrchFile;

			//Recherche dans root
			szSrchFile = szRootPath + "\\" + szMapName + ".txt";
			CheckAndAddFile(szSrchFile);

			//Recherche dans maps
			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".txt";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + "_exclude.lst";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".kv";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".map";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".lin";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".wic";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".rmf";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".p0";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".p1";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".p2";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".p3";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".pts";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".err";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".log";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".lin";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".res";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".vmf";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".prt";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".nav";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".log";
			CheckAndAddFile(szSrchFile);

			//Recherche dans DownloadLists
			szSrchFile = szRootPath + "\\DownloadLists\\" + szMapName + ".lst";
			CheckAndAddFile(szSrchFile);

			//Recherche dans maps\soundcache
			szSrchFile = szRootPath + "\\maps\\soundcache\\" + szMapName + ".cache";
			CheckAndAddFile(szSrchFile);

			//Recherche dans maps\graphs
			szSrchFile = szRootPath + "\\maps\\graphs\\" + szMapName + ".ain";
			CheckAndAddFile(szSrchFile);

			//Recherche dans maps\cfg
			szSrchFile = szRootPath + "\\maps\\cfg\\" + szMapName + ".cfg";
			CheckAndAddFile(szSrchFile);

			//Recherche dans resource
			szSrchFile = szRootPath + "\\resource\\flash\\loading-" + szMapName + ".swf";
			CheckAndAddFile(szSrchFile);
			szSrchFile = szRootPath + "\\resource\\overviews\\" + szMapName + ".txt";
			CheckAndAddFile(szSrchFile);
			szSrchFile = szRootPath + "\\resource\\overviews\\" + szMapName + "_radar.dds";
			CheckAndAddFile(szSrchFile);
			szSrchFile = szRootPath + "\\resource\\overviews\\" + szMapName + "_radar_spectate.dds";
			CheckAndAddFile(szSrchFile);

			//Recherche dans overviews
			szSrchFile = szRootPath + "\\overviews\\" + szMapName + ".txt";
			CheckAndAddFile(szSrchFile);
			szSrchFile = szRootPath + "\\overviews\\" + szMapName + ".bmp";
			CheckAndAddFile(szSrchFile);
			szSrchFile = szRootPath + "\\overviews\\" + szMapName + ".tga";
			CheckAndAddFile(szSrchFile);
			szSrchFile = szRootPath + "\\overviews\\" + szMapName + ".pcx";
			CheckAndAddFile(szSrchFile);

			//Recherche dans scripts
			szSrchFile = szRootPath + "\\scripts\\soundscapes_" + szMapName + ".txt";
			CheckAndAddFile(szSrchFile);

			//Recherche dans waypoints
			szSrchFile = szRootPath + "\\RealBot\\Learned\\rbr\\" + szMapName + ".rbr";
			CheckAndAddFile(szSrchFile);

			//Recherche dans waypoints
			szSrchFile = szRootPath + "\\PODBot\\WPTDefault\\" + szMapName + ".pwf";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\PODBot\\WPTDefault\\" + szMapName + ".pxp";
			CheckAndAddFile(szSrchFile);

			szSrchFile = szRootPath + "\\Sturmbot\\waypoints\\" + szMapName + ".wpt";
			CheckAndAddFile(szSrchFile);

			//Recherche dans répertoire "Misc"
			szSrchFile = szRootPath + "\\" + Program.mRc.szInst_MiscPath + "\\" + szMapName + "\\";
			CheckAndAddFile(szSrchFile);

			//Recherche dans répertoire "Worldcraft"
			szSrchFile = szRootPath + "\\WorldCraft\\" + szMapName + "\\";
			CheckAndAddFile(szSrchFile);

			//Find screenshots
			FindScreenshots();
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Recherche les screenshots pour la map et les intègre
		//////////////////////////////////////////////////////////////////////
		public bool FindScreenshots()
			{
			string szSrchFile;
			string szSrchPath;

			//Search in map path for one jpeg
			szSrchFile = szRootPath + "\\maps\\" + szMapName + ".jpg";
			CheckAndAddFile(szSrchFile);

			Regex reg = new Regex(szMapName + @"\d{4}\.bmp$");
			//Search in root path for bmp
			szSrchPath = szRootPath;
			foreach (string filename in Directory.GetFiles(szSrchPath, "*.bmp"))
				{
				if (reg.IsMatch(filename))
					CleanLst.Add(filename);
				}

			//Search in screenshots path for jpg
			reg = new Regex(@"^" + szMapName + @"\d{4}\.jpg$");
			szSrchPath = szRootPath + "\\screenshots";
			if (FilesTools.CheckDestinationPath(szSrchPath, false))
				{
				foreach (string filename in Directory.GetFiles(szSrchPath, "*.jpg"))
					{
					if (reg.IsMatch(filename))
						CleanLst.Add(filename);
					}
				}
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Création du fichier res
		//////////////////////////////////////////////////////////////////////
		public bool MakeResFile(bool overWrite)
			{
			string szFileRes = szMapName + ".res";
			string szFullRes = szFilePath + "\\" + szFileRes;
			string szRes = null;
			string NewLine = "\r\n";

			if (Program.mRc.bGen_Simulate)
				return true;

			FileInfo fi = new FileInfo(szFullRes);
			if (fi.Exists)
				{
				if (!overWrite)
					return false;
				}

			szRes = Properties.Resources.ResHeader;


			szRes += "// " + szFileRes + NewLine;

			szRes += "// Created: GMT " + DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd HH':'mm':'ss") + NewLine;
			szRes += NewLine;
			if (FilesErr.Count != 0)
				{
				szRes += "// Following files are listed in" + NewLine;
				szRes += "// bsp but couldn't be analysed." + NewLine;
				szRes += "// However they may be necessary:" + NewLine;
				szRes += NewLine;
				foreach (string ErrStr in FilesErr)
					{
					szRes += "// " + ErrStr + NewLine;
					}
				}

			if (nBspVer == HL1BSP)
				{
				szRes += GetResHl1(NewLine);
				}
			else
				{
				szRes += GetResHl2(NewLine);
				}
			try
				{
				var utf8WithoutBom = new System.Text.UTF8Encoding(false);

				TextWriter Rs = new StreamWriter(szFullRes, false, utf8WithoutBom);
				Rs.Write(szRes);
				Rs.Close();
				}
			catch (Exception)
				{
				return false;
				}
			DeleteExistingRes(szFileRes);
			FilesLst.Add(szFullRes);
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Effacer fichier Ressource existant dans liste
		//////////////////////////////////////////////////////////////////////
		void DeleteExistingRes(string szFileRes)
			{
			int i = 0;
			int nCount = FilesLst.Count;

			string szTmp = "";

			for (i = nCount; i > 0; i--)
				{
				szTmp = FilesLst[i - 1];
				if (szTmp.EndsWith(szFileRes, false, null))
					FilesLst.RemoveAt(i - 1);
				}
			}
		//////////////////////////////////////////////////////////////////////
		// Liste des ressources (format HL1)
		//////////////////////////////////////////////////////////////////////
		private string GetResHl1(string NewLine)
			{
			string szRes = "";
			string szExt = null;
			string szCommented;

			foreach (string szResTmp in CleanLst)
				{
				szExt = FilesTools.GetExtension(szResTmp);
				szCommented = "";
				if (Program.mRc.bRes_CommentUnnecessaryFiles)
					{
					if (szExt == "wav" || szExt == "spr" || szExt == "mdl")
						szCommented = "// ";
					}

				if (szExt != "nav"
					&& szExt != "ain"
					&& szExt != "lst"
					&& szExt != "cfg"
					&& szExt != "pwf"
					&& szExt != "pxp"
					&& szExt != "wpt"
					&& szExt != "map"
					&& szExt != "rmf"
					&& szExt != "p0"
					&& szExt != "p1"
					&& szExt != "p2"
					&& szExt != "p3"
					&& szExt != "rmx"
					&& szExt != "lin"
					&& szExt != "pts"
					&& szExt != "wc"
					&& szExt != "loc"
					&& szExt != "log"
					&& szExt != "max"
					&& szExt != "prt"
					&& szExt != "wic"
					&& szExt != "gpk"
					&& szExt != "rfk"
					&& szExt != "bsp"
					&& szExt != "res"
					)
					{
					if (szExt != "txt")
						{
						szRes += szCommented + szResTmp + NewLine;
						}
					else
						{
						string szFileTxt = "maps\\" + szMapName + ".txt";
						if (szFileTxt.ToLower() != szResTmp.ToLower())
							{
							szRes += szCommented + szResTmp + NewLine;
							}
						}
					}
				}
			szRes = szRes.Replace("\\", "/");
			return szRes;
			}
		//////////////////////////////////////////////////////////////////////
		// Liste des ressources (format HL2)
		//////////////////////////////////////////////////////////////////////
		private string GetResHl2(string NewLine)
			{
			string szRes = "";
			string szExt = null;


			foreach (string szResTmp in CleanLst)
				{
				szExt = FilesTools.GetExtension(szResTmp);
				if (szExt != "nav"
					&& szExt != "ain"
					&& szExt != "lst"
					&& szExt != "cfg"
					&& szExt != "pwf"
					&& szExt != "pxp"
					&& szExt != "wpt"
					&& szExt != "map"
					&& szExt != "rmf"
					&& szExt != "p0"
					&& szExt != "p1"
					&& szExt != "p2"
					&& szExt != "p3"
					&& szExt != "rmx"
					&& szExt != "lin"
					&& szExt != "pts"
					&& szExt != "wc"
					&& szExt != "loc"
					&& szExt != "log"
					&& szExt != "max"
					&& szExt != "prt"
					&& szExt != "wic"
					&& szExt != "gpk"
					&& szExt != "rfk"
					&& szExt != "bsp"
					&& szExt != "res"
					)
					{
					if (szExt != "txt")
						{
						szRes += "\t\"" + szResTmp + "\" \"file\"" + NewLine;
						}
					else
						{
						string szFileTxt = "maps\\" + szMapName + ".txt";
						if (szFileTxt.ToLower() != szResTmp.ToLower())
							{
							szRes += "\t\"" + szResTmp + "\" \"file\"" + NewLine;
							}
						}
					}
				}
			if (szRes == "")
				return "";
			string szTot = "";
			szTot += "\"resources\"" + NewLine + "{" + NewLine;
			szRes = szRes.Replace("\\", "/");
			szTot += szRes;
			szTot += "}" + NewLine;
			return szTot;
			}
		//////////////////////////////////////////////////////////////////////
		// Lecture Lumps (pour HL2)
		//////////////////////////////////////////////////////////////////////
		private bool ReadZipDirectory(uint ZipOffset, uint ZipLenght)
			{
			string NameStr = "";
			uint NameOff = 0;
			byte[] buffer;

			if ((uint)Marshal.SizeOf(typeof(ZIPEndOfCentralDirectoryRecord)) <= ZipLenght)
				{
				uint uiTest;
				uint uiOffset = ZipOffset;
				while (uiOffset < ZipOffset + ZipLenght - Marshal.SizeOf(typeof(uint)))
					{
					fs.Seek(uiOffset, SeekOrigin.Begin);
					uiTest = StreamTools.ReadUInt(fs);
					switch (uiTest)
						{
						case HL_VBSP_ZIP_END_OF_CENTRAL_DIRECTORY_RECORD_SIGNATURE:
								{
								ZIPEndOfCentralDirectoryRecord EndOfCentralDirRecord = StreamTools.ReadStruct<ZIPEndOfCentralDirectoryRecord>(fs, uiOffset);

								uint tst1 = ZipOffset + EndOfCentralDirRecord.uiStartOfCentralDirOffset;
								uint tst2 = EndOfCentralDirRecord.uiCentralDirectorySize;
								return true;
								}
						case HL_VBSP_ZIP_FILE_HEADER_SIGNATURE:
								{
								ZIPFileHeader FileHeader = StreamTools.ReadStruct<ZIPFileHeader>(fs, uiOffset);
								NameOff = uiOffset + (uint)Marshal.SizeOf(typeof(ZIPFileHeader));
								fs.Seek(NameOff, SeekOrigin.Begin);
								buffer = new byte[FileHeader.uiFileNameLength];
								fs.Read(buffer, 0, FileHeader.uiFileNameLength);
								NameStr = System.Text.Encoding.UTF8.GetString(buffer);
								NameStr = NameStr.Trim();
								NameStr = NameStr.Replace("/", "\\");
								if (NameStr != "" && !ZipEnts.Contains(NameStr))
									ZipEnts.Add(NameStr);
								uiOffset += (uint)Marshal.SizeOf(typeof(ZIPFileHeader)) + FileHeader.uiFileNameLength + FileHeader.uiExtraFieldLength + FileHeader.uiFileCommentLength;
								break;
								}
						case HL_VBSP_ZIP_LOCAL_FILE_HEADER_SIGNATURE:
								{
								ZIPLocalFileHeader LocalFileHeader = StreamTools.ReadStruct<ZIPLocalFileHeader>(fs, uiOffset);
								NameOff = uiOffset + (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader));
								fs.Seek(NameOff, SeekOrigin.Begin);
								buffer = new byte[LocalFileHeader.uiFileNameLength];
								fs.Read(buffer, 0, LocalFileHeader.uiFileNameLength);
								NameStr = System.Text.Encoding.UTF8.GetString(buffer);
								NameStr = NameStr.Trim();
								NameStr = NameStr.Replace("/", "\\");
								if (NameStr != "" && !ZipEnts.Contains(NameStr))
									ZipEnts.Add(NameStr);
								uiOffset += (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader)) + LocalFileHeader.uiFileNameLength + LocalFileHeader.uiExtraFieldLength + LocalFileHeader.uiCompressedSize;
								break;
								}
						default:
								{
								ThrowError("Invalid file: unknown ZIP section signature " + uiTest);
								return false;
								}
						}
					}
				ThrowError("Invalid file: unexpected end of file while scanning for end of ZIP central directory record.");
				return false;
				}
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Lit et extrait un fichier txt incorporé
		//////////////////////////////////////////////////////////////////////
		private bool ReadEmbeddedTxtFile(string TxtName, uint pkOffset)
			{
			string TxtFile = "";
			uint DataOff = 0;
			byte[] buffer;
			FileStream fw;

			ZIPLocalFileHeader LocalFileHeader = StreamTools.ReadStruct<ZIPLocalFileHeader>(fs, pkOffset);

			if (LocalFileHeader.uiCompressedSize == 0)
				return false;
			if (LocalFileHeader.uiCompressedSize != LocalFileHeader.uiUncompressedSize)
				return false;
			DataOff = pkOffset + (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader)) + LocalFileHeader.uiFileNameLength + LocalFileHeader.uiExtraFieldLength;
			fs.Seek(DataOff, SeekOrigin.Begin);
			buffer = new byte[LocalFileHeader.uiCompressedSize];
			fs.Read(buffer, 0, (int)LocalFileHeader.uiCompressedSize);
			if (buffer.Count() == LocalFileHeader.uiCompressedSize)
				{
				TxtFile = szRootPath + "\\" + TxtName;
				FileInfo f = new FileInfo(TxtFile);
				if (!f.Exists)
					{
					fw = new FileStream(TxtFile, FileMode.Create, FileAccess.Write);
					fw.Write(buffer, 0, (int)LocalFileHeader.uiCompressedSize);
					fw.Close();
					}
				}
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Lecture Lumps (pour HL2)
		//////////////////////////////////////////////////////////////////////
		private bool ReadLumps()
			{
			hl2_bsp_header header2 = StreamTools.ReadStruct<hl2_bsp_header>(fs, 0);

			EntPos.fileofs = header2.entities[0].fileofs;
			EntPos.filelen = header2.entities[0].filelen;
			TexLst.fileofs = header2.entities[44].fileofs;
			TexLst.filelen = header2.entities[44].filelen;
			TexPos.fileofs = header2.entities[43].fileofs;
			TexPos.filelen = header2.entities[43].filelen;
			uiLumpOffset = TexPos.fileofs;
			ReadZipDirectory(header2.entities[HL_VBSP_LUMP_PAKFILE].fileofs, header2.entities[HL_VBSP_LUMP_PAKFILE].filelen);
			ReadHL2Textures();
			return true;
			}

		private void ReadHL2Textures()
			{
			int Offset, ChkLen, nPos;
			byte[] buffer = new byte[128];
			string szTexture;
			string szTex = "";

			ChkLen = 0;
			while (ChkLen < TexLst.filelen)
				{
				if ((TexLst.fileofs + ChkLen) > lFileLen)
					{
					ThrowError("File Error: Truncated Map File.");
					break;
					}
				fs.Seek(TexLst.fileofs + ChkLen, SeekOrigin.Begin);
				Offset = (int)StreamTools.ReadUInt(fs);
				fs.Seek(TexPos.fileofs + Offset, SeekOrigin.Begin);
				fs.Read(buffer, 0, 128);
				szTexture = System.Text.Encoding.UTF8.GetString(buffer);
				nPos = szTexture.IndexOf("\0", 0);
				szTexture = szTexture.Substring(0, nPos);
				szTexture = szTexture.Trim();
				szTexture = szTexture.Trim('/');
				szTexture = szTexture.Trim('\\');
				if (szTexture != "")
					{
					//szTexture = szTexture.ToLower();
					szTexture = szTexture.Replace("/", "\\");
					szTexture = szTexture.Replace(".vmt", "");
					szTex = "materials";
					if (!szTex.StartsWith("\\"))
						szTex += "\\";
					szTex += szTexture + ".vmt";
					if (ZipEnts.Contains(szTex.ToLower()))
						{
						if (!IntTexs.Contains(szTex))
							IntTexs.Add(szTex);
						}
					else
						{
						if (!ExtTexs.Contains(szTex))
							ExtTexs.Add(szTex);
						}
					szTex = szRootPath + "\\materials";
					if (!szTex.StartsWith("\\"))
						szTex += "\\";
					szTex += szTexture + ".vmt";
					if (!CleanLst.Contains(szTex))
						CleanLst.Add(szTex);
					}
				ChkLen += sizeof(int);
				}
			}
		//////////////////////////////////////////////////
		//Récupérer les offsets de chaque zip texture
		//////////////////////////////////////////////////
		private void ReadHL2ZipTexturesOffsets(uint ZipOffset, uint ZipLenght)
			{
			string NameStr = "";
			uint NameOff = 0;
			byte[] buffer;
			string szExt = "";
			uint PkOffset = 0;

			if ((uint)Marshal.SizeOf(typeof(ZIPEndOfCentralDirectoryRecord)) <= ZipLenght)
				{
				uint uiTest;
				uint uiOffset = ZipOffset;
				while (uiOffset < ZipOffset + ZipLenght - Marshal.SizeOf(typeof(uint)))
					{
					fs.Seek(uiOffset, SeekOrigin.Begin);
					uiTest = StreamTools.ReadUInt(fs);
					switch (uiTest)
						{
						case HL_VBSP_ZIP_END_OF_CENTRAL_DIRECTORY_RECORD_SIGNATURE:
								{
								ZIPEndOfCentralDirectoryRecord EndOfCentralDirRecord = StreamTools.ReadStruct<ZIPEndOfCentralDirectoryRecord>(fs, uiOffset);
								uint tst1 = ZipOffset + EndOfCentralDirRecord.uiStartOfCentralDirOffset;
								uint tst2 = EndOfCentralDirRecord.uiCentralDirectorySize;
								return;
								}
						case HL_VBSP_ZIP_FILE_HEADER_SIGNATURE:
								{
								ZIPFileHeader FileHeader = StreamTools.ReadStruct<ZIPFileHeader>(fs, uiOffset);
								NameOff = uiOffset + (uint)Marshal.SizeOf(typeof(ZIPFileHeader));
								fs.Seek(NameOff, SeekOrigin.Begin);
								buffer = new byte[FileHeader.uiFileNameLength];
								fs.Read(buffer, 0, FileHeader.uiFileNameLength);
								NameStr = System.Text.Encoding.UTF8.GetString(buffer);
								NameStr = NameStr.Trim();
								NameStr = NameStr.Replace("/", "\\");
								szExt = FilesTools.GetExtension(NameStr).ToLower();
								if (szExt == "vtf")
									{
									PkOffset = ZipOffset + FileHeader.uiRelativeOffsetOfLocalHeader;
									TexturesOffsets.Add((int)PkOffset);
									}
								uiOffset += (uint)Marshal.SizeOf(typeof(ZIPFileHeader)) + FileHeader.uiFileNameLength + FileHeader.uiExtraFieldLength + FileHeader.uiFileCommentLength;
								break;
								}
						case HL_VBSP_ZIP_LOCAL_FILE_HEADER_SIGNATURE:
								{
								ZIPLocalFileHeader LocalFileHeader = StreamTools.ReadStruct<ZIPLocalFileHeader>(fs, uiOffset);
								NameOff = uiOffset + (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader));
								fs.Seek(NameOff, SeekOrigin.Begin);
								buffer = new byte[LocalFileHeader.uiFileNameLength];
								fs.Read(buffer, 0, LocalFileHeader.uiFileNameLength);
								NameStr = System.Text.Encoding.UTF8.GetString(buffer);
								NameStr = NameStr.Trim();
								NameStr = NameStr.Replace("/", "\\");
								if (NameStr != "" && !ZipEnts.Contains(NameStr))
									ZipEnts.Add(NameStr.ToLower());
								uiOffset += (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader)) + LocalFileHeader.uiFileNameLength + LocalFileHeader.uiExtraFieldLength + LocalFileHeader.uiCompressedSize;
								break;
								}
						default:
								{
								ThrowError("Invalid file: unknown ZIP section signature " + uiTest);
								return;
								}
						}
					}
				ThrowError("Invalid file: unexpected end of file while scanning for end of ZIP central directory record.");
				return;
				}
			return;
			}

		//////////////////////////////////////////////////////////////////////
		// Lis les offsets vers les Textures
		//////////////////////////////////////////////////////////////////////
		public virtual void ReadTexOffsets()
			{
			int i = 0;
			if (nBspVer == HL2BSP)
				{
				hl2_bsp_header header2 = StreamTools.ReadStruct<hl2_bsp_header>(fs, 0);
				if (header2.bsp_version == 21 && header2.bsp_version != 0x00040014)
					{
					uint oldver = 0;

					for (i = 0; i < 64; i++)
						{
						oldver = header2.entities[i].version;
						header2.entities[i].version = header2.entities[i].fileofs;
						header2.entities[i].fileofs = header2.entities[i].filelen;
						header2.entities[i].filelen = oldver;
						}
					}
				ReadHL2ZipTexturesOffsets(header2.entities[HL_VBSP_LUMP_PAKFILE].fileofs, header2.entities[HL_VBSP_LUMP_PAKFILE].filelen);
				uiTotalTex = (uint)TexturesOffsets.Count;
				}
			else if (nBspVer == HL1BSP)
				{
				if (TexPos.fileofs < lFileLen)
					{
					fs.Seek(uiLumpOffset, SeekOrigin.Begin);
					uiTotalTex = StreamTools.ReadUInt(fs);
					for (i = 0; i < uiTotalTex; i++)
						{
						TexturesOffsets.Add((int)StreamTools.ReadUInt(fs));
						}
					}
				}
			else
				{
				return;
				}
			}
		//////////////////////////////////////////////////////////////////////
		// Lire un lump à un endroit spécifique
		//////////////////////////////////////////////////////////////////////
		public virtual Texture ReadFullTex(int nIdx)
			{
			if (nBspVer == HL2BSP)
				{
				return ReadHL2FullTex(nIdx);
				}
			else if (nBspVer == HL1BSP)
				{
				return ReadHL1FullTex(nIdx);
				}
			else
				{
				return null;
				}
			}
		//////////////////////////////////////////////////////////////////////
		// Lire une texture à un emplacement spécifique (HL2)
		//////////////////////////////////////////////////////////////////////
		private Texture ReadHL2FullTex(int nIdx)
			{
			Texture TexInfo = new Texture();

			string NameStr = "";
			uint NameOff = 0;

			uint DataOff = 0;
			byte[] buffer;

			uint pkOffset = (uint)TexturesOffsets.ElementAt(nIdx);

			ZIPLocalFileHeader LocalFileHeader = StreamTools.ReadStruct<ZIPLocalFileHeader>(fs, pkOffset);

			if (LocalFileHeader.uiCompressedSize == 0)
				return null;
			if (LocalFileHeader.uiCompressedSize != LocalFileHeader.uiUncompressedSize)
				return null;

			//Get Name
			NameOff = pkOffset + (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader));
			fs.Seek(NameOff, SeekOrigin.Begin);
			buffer = new byte[LocalFileHeader.uiFileNameLength];
			fs.Read(buffer, 0, LocalFileHeader.uiFileNameLength);
			NameStr = System.Text.Encoding.UTF8.GetString(buffer);
			NameStr = NameStr.Trim();
			NameStr = NameStr.Replace("/", "\\");
			//Get Datas
			DataOff = pkOffset + (uint)Marshal.SizeOf(typeof(ZIPLocalFileHeader)) + LocalFileHeader.uiFileNameLength + LocalFileHeader.uiExtraFieldLength;
			fs.Seek(DataOff, SeekOrigin.Begin);
			buffer = new byte[LocalFileHeader.uiCompressedSize];
			fs.Read(buffer, 0, (int)LocalFileHeader.uiCompressedSize);
			if (buffer.Count() == LocalFileHeader.uiCompressedSize)
				{
				VTFFile vtf = new VTFFile(buffer);
//				vtf.Init();
//				if (vtf.GetHeader() != true)
					return null;
				//vtf.GetMainFramesDatas();
				//TexInfo = vtf.LoadFrame(0);
				}
			TexInfo.szName = Path.GetFileNameWithoutExtension(NameStr);
			TexInfo.iLvwIdx = nIdx;
			TexInfo.type=0x43;
			return TexInfo;
			}
		//////////////////////////////////////////////////////////////////////
		// Lire une texture à un emplacement spécifique (HL1)
		//////////////////////////////////////////////////////////////////////
		private Texture ReadHL1FullTex(int nIdx)
			{
			Texture TexInfo = new Texture();
			texdata_s Lmp;
			string TexName;
			int nPos = 0;
			uint uiNewOffset = 0;
			List<RGBEntry> RgbEntries = new List<RGBEntry>();

			uint TexOrigin = (uint)uiLumpOffset + (uint)TexturesOffsets.ElementAt(nIdx);

			Lmp = StreamTools.ReadStruct<texdata_s>(fs, TexOrigin);

			TexName = new string(Lmp.name);
			nPos = TexName.IndexOf("\0", 0);
			TexInfo.szName = TexName.Substring(0, nPos);
			if (TexInfo.szName == "")
				return null;
			TexInfo.uiWidth = (uint)Lmp.width;
			TexInfo.uiHeight = (uint)Lmp.height;

			int iFullPixelSize = 0;
			int w = Lmp.width;
			int h = Lmp.height;

			bool external = true;
			for (int i = 0; i < HL_BSP_MIPMAP_COUNT; i++)
				{
				if (Lmp.offsets[i] != 0)
					{
					external = false;
					iFullPixelSize += (w * h);
					w /= 2;
					h /= 2;
					}
				}

			if (external == true)
				return null;

			//Initialize bitmap
			Bitmap bmp = new Bitmap((int)TexInfo.uiWidth, (int)TexInfo.uiHeight, PixelFormat.Format8bppIndexed);

			uint uiPixelsSize = TexInfo.uiWidth * TexInfo.uiHeight;
			//Lecture Buffer
			uiNewOffset = TexOrigin + (uint)Lmp.offsets[0];
			fs.Seek(uiNewOffset, SeekOrigin.Begin);
			byte[] pixels = new byte[uiPixelsSize];
			fs.Read(pixels, 0, (int)uiPixelsSize);
			//lecture palette
			uiNewOffset += (uint)iFullPixelSize;
			fs.Seek(uiNewOffset, SeekOrigin.Begin);
			int foo = StreamTools.ReadInt(fs);
			int iPaletteSize = 0x100;
			uiNewOffset += 2;
			ColorPalette pal = bmp.Palette;
			for (uint pl = 0; pl < iPaletteSize; pl++)
				{
				fs.Seek(uiNewOffset, SeekOrigin.Begin);
				RGBEntry RGBe = StreamTools.ReadStruct<RGBEntry>(fs, uiNewOffset);
				pal.Entries[pl] = Color.FromArgb(255, RGBe.cR, RGBe.cG, RGBe.cB);
				uiNewOffset += (uint)Marshal.SizeOf(typeof(RGBEntry));
				}
			bmp.Palette = pal;
			//Lock bitmap for pixel manipulation
			BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, (int)TexInfo.uiWidth, (int)TexInfo.uiHeight), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmd.Scan0, pixels.Length);
			bmp.UnlockBits(bmd);
			TexInfo.uiDiskLength = (uint)(40 + 14 + iPaletteSize * 4 + uiPixelsSize);
			TexInfo.Image = bmp;
			TexInfo.iLvwIdx = nIdx;
			TexInfo.iImgIdx = -1;
			TexInfo.Mipmaps = null;
			TexInfo.rawImg = null;
			TexInfo.rawPal = null;
			TexInfo.type = 0x43;
			return TexInfo;
			}
		}
	}
