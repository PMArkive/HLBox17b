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

namespace HLBox17b.Classes.Files
	{
	class WADFile
		{
		public List<WADLump> LumpsInfo { get; private set; }
		public string szFullFile { get; private set; }
		public string szWadName;
		public List<string> wadTexes;
		public List<string> usedTextures;
		public uint uiTotalTex;
		public uint uiLumpOffset;
		public long lFileSize;

		//Private members
		private const int MaxPaletteColors = 256;
		private const int MaxNameLength = 16;
		private const int LumpSize = 32;
		private const int MaxTextureWidth = 2048;
		private const int MaxTextureHeight = 2048;
		private const int QCharWidth = 16;
		private const int QNumbOfGlyphs = 256;
		private readonly static byte[] WadHeaderId = { 0x57, 0x41, 0x44, 0x33 }; //WAD3

		private WADHeader header;
		private BinaryReader binReader;
		private FileStream fs;

		private long pixelsBlockPos = 0;

		private ColorPalette palGrey;

		public WADFile(string szFile)
			{
			szFullFile = szFile;
			szWadName = Path.GetFileName(szFile);
			wadTexes = new List<string>();
			uiTotalTex = 0;
			uiLumpOffset = 0;
			lFileSize = 0;
			usedTextures = new List<string>();
			LumpsInfo = new List<WADLump>();
			palGrey = CreateGrayPal();
			}
		public void ThrowError(string szTxt)
			{
			MessageBox.Show(szTxt, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		public bool Open()
			{
			FileInfo fi = new FileInfo(szFullFile);

			if (fi.Exists)
				{
				try
					{
					lFileSize = fi.Length;
					fs = new FileStream(szFullFile, FileMode.Open, FileAccess.Read);
					binReader = new BinaryReader(fs);
					}
				catch (Exception ex)
					{
					ThrowError(ex.Message);
					}
				if (fs != null)
					return true;
				}
			else
				{
				ThrowError(szFullFile + "\n\nFile not Found");
				}
			return false;
			}
		public bool Close()
			{
			if (binReader != null)
				binReader.Close();
			if (fs != null)
				fs.Close();
			binReader = null;
			fs = null;
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Lire les textures du wad
		//////////////////////////////////////////////////////////////////////
		public virtual bool ReadTexes()
			{
			return ReadTexes(true);
			}
		//////////////////////////////////////////////////////////////////////
		// Lire les textures du wad
		//////////////////////////////////////////////////////////////////////
		public virtual bool ReadTexes(bool bThrow)
			{
			header = new WADHeader();

			header.Id = binReader.ReadChars(4);
			//Check header
			string magic = new string(header.Id);
			if (magic != System.Text.Encoding.ASCII.GetString(WadHeaderId))
				{
				Close();
				return false;
				}

			header.LumpCount = binReader.ReadUInt32();
			header.LumpOffset = binReader.ReadUInt32();
			if (!LoadLumpsInfo())
				return false;
			return true;
			}

		/// <summary>
		/// Load basic lumps data.
		/// </summary>
		private bool LoadLumpsInfo()
			{
			//Seek to first lump
			binReader.BaseStream.Seek(header.LumpOffset, SeekOrigin.Begin);

			//Iterate all lumps, insert every lump to array
			for (int i = 0; i < header.LumpCount; i++)
				{
				WADLump lump = new WADLump();
				lump.Offset = binReader.ReadUInt32();
				lump.CompressedLength = binReader.ReadUInt32();
				lump.FullLength = binReader.ReadUInt32();
				lump.Type = binReader.ReadByte();
				lump.Compression = binReader.ReadByte();
				//Padding, 2-bytes
				binReader.BaseStream.Seek(2, SeekOrigin.Current);
				lump.Name = new string(binReader.ReadChars(MaxNameLength)).TrimEnd('\0');
				if (!wadTexes.Contains(lump.Name))
					wadTexes.Add(lump.Name);
				LumpsInfo.Add(lump);
				}
			if (wadTexes.Count == 0)
				return false;
			wadTexes.Sort();
			return true;
			}

		//////////////////////////////////////////////////////////////////////
		// Controler les textures utilisées dans un wad
		//////////////////////////////////////////////////////////////////////
		public virtual bool CheckUsedTextures(List<string> arTexes)
			{
			string szTmp;
			foreach (string tex in arTexes)
				{
				szTmp = tex.ToLower();
				if (wadTexes.Contains(szTmp))
					{
					if (!usedTextures.Contains(szTmp))
						usedTextures.Add(szTmp);
					}
				}
			return true;
			}

		//////////////////////////////////////////////////////////////////////
		// Lire les textures du wad
		//////////////////////////////////////////////////////////////////////
		public virtual bool ReadHeader()
			{
			binReader.BaseStream.Seek(0, SeekOrigin.Begin);
			header = new WADHeader();

			header.Id = binReader.ReadChars(4);
			//Check header
			string magic = new string(header.Id);
			if (magic != System.Text.Encoding.ASCII.GetString(WadHeaderId))
				{
				Close();
				return false;
				}
			header.LumpCount = binReader.ReadUInt32();
			header.LumpOffset = binReader.ReadUInt32();
			uiTotalTex = header.LumpCount;
			uiLumpOffset = header.LumpOffset;
			if (uiTotalTex == 0)
				return false;
			if (uiLumpOffset == 0 || uiLumpOffset > lFileSize)
				return false;
			if (!LoadLumpsInfo())
				return false;
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Lire un lump à un endroit spécifique
		//////////////////////////////////////////////////////////////////////
		public virtual Texture ReadFullTex(int nIdx, bool transparent = false)
			{
			Texture retVal = null;
			byte[] palBytes = new byte[MaxPaletteColors * 3];

			if (nIdx > -1 && nIdx < LumpsInfo.Count)
				{
				byte type = LumpsInfo[nIdx].Type;
				//0x40 - tempdecal.wad
				//0x42 - cached.wad
				//0x43 - normald wads
				//0x46 - fonts 
				if (type == 0x40 || type == 0x42 || type == 0x43 || type == 0x46) //Supported types
					{
					//Go to lump
					binReader.BaseStream.Seek(LumpsInfo[nIdx].Offset, SeekOrigin.Begin);
					if (type == 0x40 || type == 0x43)
						{
						//Skip lump name
						binReader.BaseStream.Seek(MaxNameLength, SeekOrigin.Current);
						}
					//Read texture size
					uint width = binReader.ReadUInt32();
					uint height = binReader.ReadUInt32();
					if (width > MaxTextureWidth || height > MaxTextureHeight)
						return null;
					if (width == 0 || height == 0)
						return null;
					//If QFont
					if (type == 0x46)
						{
						//width = width * QCharWidth;
						width = 256;
						uint RowCount = binReader.ReadUInt32();
						uint RowHeight = binReader.ReadUInt32();
						CharInfo[] FontInfo = new CharInfo[QNumbOfGlyphs];
						for (int i = 0; i < QNumbOfGlyphs; i++)
							{
							FontInfo[i].StartOffset = binReader.ReadUInt16();
							FontInfo[i].CharWidth = binReader.ReadUInt16();
							}
						}
					//Initialize bitmap
					Bitmap bmp = new Bitmap((int)width, (int)height, PixelFormat.Format8bppIndexed);

					//Read pixel offset, skip MIPMAPS offsets
					uint pixelOffset = 0;
					if (type == 0x40 || type == 0x43)
						{
						//Not used, but needed
						pixelOffset = binReader.ReadUInt32();
						//Skip MIPMAPS offsets, not needed
						binReader.BaseStream.Seek(12, SeekOrigin.Current);
						}
					//Read RAW pixels
					uint pixelSize = width * height;
					pixelsBlockPos = binReader.BaseStream.Position;
					byte[] pixels = binReader.ReadBytes((int)pixelSize);
					//Read MIPMAPS
					TextureMipmaps mipmaps = null;
					if (type == 0x40 || type == 0x43)
						{
						mipmaps = new TextureMipmaps();
						mipmaps.Mipmap1 = binReader.ReadBytes((int)((width / 2) * (height / 2)));
						mipmaps.Mipmap2 = binReader.ReadBytes((int)((width / 4) * (height / 4)));
						mipmaps.Mipmap3 = binReader.ReadBytes((int)((width / 8) * (height / 8)));
						}
					//Padding 2-bytes
					binReader.BaseStream.Seek(2, SeekOrigin.Current);
					if (type != 0x46)
						{
						if (type == 0x40)
							{
							bmp.Palette = palGrey;
							}
						else
							{
							//Prepare new palette for bitmap
							ColorPalette pal = bmp.Palette;
							//Read palette bytes from file into array
							palBytes = binReader.ReadBytes(MaxPaletteColors * 3);

							for (int i = 0, j = 0; i < MaxPaletteColors; i++)
								{
								pal.Entries[i] = Color.FromArgb(palBytes[j], palBytes[j + 1], palBytes[j + 2]);
								j += 3;
								}
							bmp.Palette = pal;
							}
						//Lock bitmap for pixel manipulation
						BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, (int)width, (int)height), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
						System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmd.Scan0, pixels.Length);
						bmp.UnlockBits(bmd);
						}
					retVal = new Texture();
					retVal.szName = LumpsInfo[nIdx].Name;
					retVal.uiWidth = width;
					retVal.uiHeight = height;
					retVal.uiDiskLength = LumpsInfo[nIdx].CompressedLength;
					retVal.Image = bmp;
					retVal.Mipmaps = mipmaps;
					retVal.rawImg = pixels;
					retVal.rawPal = palBytes;
					retVal.iLvwIdx = nIdx;
					retVal.iImgIdx = -1;
					retVal.type = type;
					}
				else
					{
					return null;
					}
				}
			else
				{
				return null;
				}
			return retVal;
			}
		//////////////////////////////////////////////////////////////////////
		// Créer palette greyscale
		//////////////////////////////////////////////////////////////////////
		private ColorPalette CreateGrayPal()
			{
			Bitmap TmpBMP = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
			ColorPalette GreyPal = TmpBMP.Palette;
			for (int i = 0; i < MaxPaletteColors; i++)
				{
				GreyPal.Entries[i] = Color.FromArgb(i, i, i);
				}
			TmpBMP.Dispose();
			return GreyPal;
			}
		//////////////////////////////////////////////////////////////////////
		// Sauvegarder un wad
		//////////////////////////////////////////////////////////////////////
		public static bool SaveWad(string szFileName, List<Texture> VirtualTextures)
			{
			if (szFileName == "")
				return false;

			List<string> names = new List<string>();

			int itmCounts = VirtualTextures.Count;

			using (FileStream fs = new FileStream(szFileName, FileMode.Create))
			using (BinaryWriter bw = new BinaryWriter(fs))
				{
				uint[] offsets = new uint[itmCounts];
				uint[] sizes = new uint[itmCounts];
				byte[] types = new byte[itmCounts];

				//WAD header
				bw.Write(WadHeaderId);
				bw.Write(itmCounts);
				bw.Write(0); //This will be changed later

				int nItmIdx = 0;
				foreach (Texture TxInfo in VirtualTextures)
					{
					//Image size
					int imgWidth = (int)TxInfo.uiWidth;
					int imgHeight = (int)TxInfo.uiHeight;
					//Sauvegarde de l'offset texture
					uint posTextureStart = (uint)bw.BaseStream.Position;
					offsets[nItmIdx] = posTextureStart;
					types[nItmIdx] = TxInfo.type;
					//Texture name
					names.Add(TxInfo.szName);
					byte[] name = CreateTextureName(TxInfo.szName);
					bw.Write(name, 0, name.Length);
					//Texture dimensions
					bw.Write(imgWidth);
					bw.Write(imgHeight);
					//Offsets
					uint posImage = (uint)(bw.BaseStream.Position - posTextureStart);
					bw.Write(posImage + 16); //image
					int pixelSize = (imgWidth * imgHeight);
					int m1 = ((imgWidth / 2) * (imgHeight / 2));
					int m2 = ((imgWidth / 4) * (imgHeight / 4));
					int m3 = ((imgWidth / 8) * (imgHeight / 8));
					bw.Write((uint)(posImage + pixelSize + 16)); //mipmap1
					bw.Write((uint)(posImage + pixelSize + m1 + 16)); //mipmap2
					bw.Write((uint)(posImage + pixelSize + m1 + m2 + 16)); //mipmap3		
					bw.Write(TxInfo.rawImg);
					bw.Write(TxInfo.Mipmaps.Mipmap1);
					bw.Write(TxInfo.Mipmaps.Mipmap2);
					bw.Write(TxInfo.Mipmaps.Mipmap3);
					bw.Write(new byte[] { 0x00, 0x01 });
					bw.Write(TxInfo.rawPal);
					//Padding
					bw.Write(new byte[] { 0x00, 0x00 });
					sizes[nItmIdx] = (uint)bw.BaseStream.Position - posTextureStart;
					nItmIdx++;
					}
				//Update_Progress(itmCounts);
				long posLumps = bw.BaseStream.Position;
				bw.Seek(8, SeekOrigin.Begin);
				bw.Write((uint)posLumps);
				bw.Seek((int)posLumps, SeekOrigin.Begin);
				//Write Lumps infos
				for (int i = 0; i < itmCounts; i++)
					{
					bw.Write(offsets[i]);
					bw.Write(sizes[i]);
					bw.Write(sizes[i]);
					bw.Write(types[i]);
					bw.Write((byte)0);
					bw.Write(new byte[] { 0x00, 0x00 });
					byte[] name = CreateTextureName(names[i]);
					bw.Write(name, 0, name.Length);
					}
				}
			return true;
			}
		//////////////////////////////////////////////////////////////////////
		// Créer nom de texture 
		//////////////////////////////////////////////////////////////////////
		private static byte[] CreateTextureName(string text)
			{
			byte[] newName = new byte[MaxNameLength];
			byte[] b = System.Text.Encoding.ASCII.GetBytes(text);
			b.CopyTo(newName, 0);
			newName[MaxNameLength - 1] = 0;
			return newName;
			}

		}
	}

