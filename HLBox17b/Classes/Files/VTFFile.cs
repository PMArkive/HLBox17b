using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using HLBox17b.Classes.Tools;
using System.ComponentModel;
using Tao.DevIl;

namespace HLBox17b.Classes.Files
	{
	class VTFFile
		{
		public enum vtfFormat : int
			{
			IMAGE_FORMAT_NONE = -1,
			IMAGE_FORMAT_RGBA8888 = 0,					//0
			IMAGE_FORMAT_ABGR8888,						//1
			IMAGE_FORMAT_RGB888,						//2
			IMAGE_FORMAT_BGR888,						//3
			IMAGE_FORMAT_RGB565,						//4
			IMAGE_FORMAT_I8,							//5
			IMAGE_FORMAT_IA88,							//6
			IMAGE_FORMAT_P8,							//7
			IMAGE_FORMAT_A8,							//8
			IMAGE_FORMAT_RGB888_BLUESCREEN,				//9
			IMAGE_FORMAT_BGR888_BLUESCREEN,				//10
			IMAGE_FORMAT_ARGB8888,						//11
			IMAGE_FORMAT_BGRA8888,						//12
			IMAGE_FORMAT_DXT1,							//13
			IMAGE_FORMAT_DXT3,							//14
			IMAGE_FORMAT_DXT5,							//15
			IMAGE_FORMAT_BGRX8888,						//16
			IMAGE_FORMAT_BGR565,						//17
			IMAGE_FORMAT_BGRX5551,						//18
			IMAGE_FORMAT_BGRA4444,						//19
			IMAGE_FORMAT_DXT1_ONEBITALPHA,				//20
			IMAGE_FORMAT_BGRA5551,						//21
			IMAGE_FORMAT_UV88,							//22
			IMAGE_FORMAT_UVWQ8888,						//23
			IMAGE_FORMAT_RGBA16161616F,					//24
			IMAGE_FORMAT_RGBA16161616,					//25
			IMAGE_FORMAT_UVLX8888,						//26
			IMAGE_FORMAT_R32F,							//27
			IMAGE_FORMAT_RGB323232F,					//28
			IMAGE_FORMAT_RGBA32323232F,					//29
			IMAGE_FORMAT_nVidia_DST16,					//30
			IMAGE_FORMAT_nVidia_DST24,					//31
			IMAGE_FORMAT_nVidia_INTZ,					//32
			IMAGE_FORMAT_nVidia_RAWZ,					//33
			IMAGE_FORMAT_ATI_DST16,						//34
			IMAGE_FORMAT_ATI_DST24,						//35
			IMAGE_FORMAT_nVidia_NULL,					//36
			IMAGE_FORMAT_ATI1N,							//37
			IMAGE_FORMAT_ATI2N							//38
			};

		public class SVTFImageFormatInfo
			{
			public string szName;					//!< Enumeration text equivalent.
			public uint uiBitsPerPixel;			//!< Format bits per pixel.
			public uint uiBytesPerPixel;			//!< Format bytes per pixel.
			public uint uiRedBitsPerPixel;			//!< Format red bits per pixel.  0 for N/A.
			public uint uiGreenBitsPerPixel;		//!< Format green bits per pixel.  0 for N/A.
			public uint uiBlueBitsPerPixel;		//!< Format blue bits per pixel.  0 for N/A.
			public uint uiAlphaBitsPerPixel;		//!< Format alpha bits per pixel.  0 for N/A.
			public bool bIsCompressed;				//!< Format is compressed (DXT).
			public bool bIsSupported;				//!< Format is supported by VTFLib.
			public SVTFImageFormatInfo(string szNam,uint bipp, uint bypp, uint R, uint G, uint B, uint A , bool comp, bool sup)
				{
				szName = szNam;
				uiBitsPerPixel = bipp;
				uiBytesPerPixel = bypp;
				uiRedBitsPerPixel = R;
				uiGreenBitsPerPixel = G;
				uiBlueBitsPerPixel = B;
				uiAlphaBitsPerPixel = A;
				bIsCompressed = comp;
				bIsSupported = sup;
				}
			};

		private SVTFImageFormatInfo[] VTFImageFormatInfo = new SVTFImageFormatInfo[]
			{
			new SVTFImageFormatInfo ( "RGBA8888",			 32,  4,  8,  8,  8,  8, false,  true ),		// IMAGE_FORMAT_RGBA8888,
			new SVTFImageFormatInfo ( "ABGR8888",			 32,  4,  8,  8,  8,  8, false,  true ),		// IMAGE_FORMAT_ABGR8888, 
			new SVTFImageFormatInfo ( "RGB888",				 24,  3,  8,  8,  8,  0, false,  true ),		// IMAGE_FORMAT_RGB888,
			new SVTFImageFormatInfo ( "BGR888",				 24,  3,  8,  8,  8,  0, false,  true ),		// IMAGE_FORMAT_BGR888,
			new SVTFImageFormatInfo ( "RGB565",				 16,  2,  5,  6,  5,  0, false,  true ),		// IMAGE_FORMAT_RGB565, 
			new SVTFImageFormatInfo ( "I8",					  8,  1,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_I8,
			new SVTFImageFormatInfo ( "IA88",				 16,  2,  0,  0,  0,  8, false,  true ),		// IMAGE_FORMAT_IA88
			new SVTFImageFormatInfo ( "P8",					  8,  1,  0,  0,  0,  0, false, false ),		// IMAGE_FORMAT_P8
			new SVTFImageFormatInfo ( "A8",					  8,  1,  0,  0,  0,  8, false,  true ),		// IMAGE_FORMAT_A8
			new SVTFImageFormatInfo ( "RGB888 Bluescreen",	 24,  3,  8,  8,  8,  0, false,  true ),		// IMAGE_FORMAT_RGB888_BLUESCREEN
			new SVTFImageFormatInfo ( "BGR888 Bluescreen",	 24,  3,  8,  8,  8,  0, false,  true ),		// IMAGE_FORMAT_BGR888_BLUESCREEN
			new SVTFImageFormatInfo ( "ARGB8888",			 32,  4,  8,  8,  8,  8, false,  true ),		// IMAGE_FORMAT_ARGB8888
			new SVTFImageFormatInfo ( "BGRA8888",			 32,  4,  8,  8,  8,  8, false,  true ),		// IMAGE_FORMAT_BGRA8888
			new SVTFImageFormatInfo ( "DXT1",				  4,  0,  0,  0,  0,  0,  true,  true ),		// IMAGE_FORMAT_DXT1
			new SVTFImageFormatInfo ( "DXT3",				  8,  0,  0,  0,  0,  8,  true,  true ),		// IMAGE_FORMAT_DXT3
			new SVTFImageFormatInfo ( "DXT5",				  8,  0,  0,  0,  0,  8,  true,  true ),		// IMAGE_FORMAT_DXT5
			new SVTFImageFormatInfo ( "BGRX8888",			 32,  4,  8,  8,  8,  0, false,  true ),		// IMAGE_FORMAT_BGRX8888
			new SVTFImageFormatInfo ( "BGR565",				 16,  2,  5,  6,  5,  0, false,  true ),		// IMAGE_FORMAT_BGR565
			new SVTFImageFormatInfo ( "BGRX5551",			 16,  2,  5,  5,  5,  0, false,  true ),		// IMAGE_FORMAT_BGRX5551
			new SVTFImageFormatInfo ( "BGRA4444",			 16,  2,  4,  4,  4,  4, false,  true ),		// IMAGE_FORMAT_BGRA4444
			new SVTFImageFormatInfo ( "DXT1 One Bit Alpha",	  4,  0,  0,  0,  0,  1,  true,  true ),		// IMAGE_FORMAT_DXT1_ONEBITALPHA
			new SVTFImageFormatInfo ( "BGRA5551",			 16,  2,  5,  5,  5,  1, false,  true ),		// IMAGE_FORMAT_BGRA5551
			new SVTFImageFormatInfo ( "UV88",				 16,  2,  8,  8,  0,  0, false,  true ),		// IMAGE_FORMAT_UV88
			new SVTFImageFormatInfo ( "UVWQ8888",			 32,  4,  8,  8,  8,  8, false,  true ),		// IMAGE_FORMAT_UVWQ8899
			new SVTFImageFormatInfo ( "RGBA16161616F",	     64,  8, 16, 16, 16, 16, false,  true ),		// IMAGE_FORMAT_RGBA16161616F
			new SVTFImageFormatInfo ( "RGBA16161616",	     64,  8, 16, 16, 16, 16, false,  true ),		// IMAGE_FORMAT_RGBA16161616
			new SVTFImageFormatInfo ( "UVLX8888",			 32,  4,  8,  8,  8,  8, false,  true ),		// IMAGE_FORMAT_UVLX8888
			new SVTFImageFormatInfo ( "R32F",				 32,  4, 32,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_R32F
			new SVTFImageFormatInfo ( "RGB323232F",			 96, 12, 32, 32, 32,  0, false,  true ),		// IMAGE_FORMAT_RGB323232F
			new SVTFImageFormatInfo ( "RGBA32323232F",		128, 16, 32, 32, 32, 32, false,  true ),		// IMAGE_FORMAT_RGBA32323232F
			new SVTFImageFormatInfo ( "nVidia DST16",		 16,  2,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_NV_DST16
			new SVTFImageFormatInfo ( "nVidia DST24",		 24,  3,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_NV_DST24
			new SVTFImageFormatInfo ( "nVidia INTZ",		 32,  4,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_NV_INTZ
			new SVTFImageFormatInfo ( "nVidia RAWZ",		 32,  4,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_NV_RAWZ
			new SVTFImageFormatInfo ( "ATI DST16",			 16,  2,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_ATI_DST16
			new SVTFImageFormatInfo ( "ATI DST24",			 24,  3,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_ATI_DST24
			new SVTFImageFormatInfo ( "nVidia NULL",		 32,  4,  0,  0,  0,  0, false,  true ),		// IMAGE_FORMAT_NV_NULL
			new SVTFImageFormatInfo ( "ATI1N",				  4,  0,  0,  0,  0,  0,  true,  true ),		// IMAGE_FORMAT_ATI1N
			new SVTFImageFormatInfo ( "ATI2N",				  8,  0,  0,  0,  0,  0,  true,  true )
			};


		public enum vtfFlags : uint
			{
			// Flags from the *.txt config file
			TEXTUREFLAGS_POINTSAMPLE = 0x00000001,
			TEXTUREFLAGS_TRILINEAR = 0x00000002,
			TEXTUREFLAGS_CLAMPS = 0x00000004,
			TEXTUREFLAGS_CLAMPT = 0x00000008,
			TEXTUREFLAGS_ANISOTROPIC = 0x00000010,
			TEXTUREFLAGS_HINT_DXT5 = 0x00000020,
			TEXTUREFLAGS_PWL_CORRECTED = 0x00000040,
			TEXTUREFLAGS_NORMAL = 0x00000080,
			TEXTUREFLAGS_NOMIP = 0x00000100,
			TEXTUREFLAGS_NOLOD = 0x00000200,
			TEXTUREFLAGS_ALL_MIPS = 0x00000400,
			TEXTUREFLAGS_PROCEDURAL = 0x00000800,

			// These are automatically generated by vtex from the texture data.
			TEXTUREFLAGS_ONEBITALPHA = 0x00001000,
			TEXTUREFLAGS_EIGHTBITALPHA = 0x00002000,

			// Newer flags from the *.txt config file
			TEXTUREFLAGS_ENVMAP = 0x00004000,
			TEXTUREFLAGS_RENDERTARGET = 0x00008000,
			TEXTUREFLAGS_DEPTHRENDERTARGET = 0x00010000,
			TEXTUREFLAGS_NODEBUGOVERRIDE = 0x00020000,
			TEXTUREFLAGS_SINGLECOPY = 0x00040000,
			TEXTUREFLAGS_PRE_SRGB = 0x00080000,

			TEXTUREFLAGS_UNUSED_00100000 = 0x00100000,
			TEXTUREFLAGS_UNUSED_00200000 = 0x00200000,
			TEXTUREFLAGS_UNUSED_00400000 = 0x00400000,

			TEXTUREFLAGS_NODEPTHBUFFER = 0x00800000,

			TEXTUREFLAGS_UNUSED_01000000 = 0x01000000,

			TEXTUREFLAGS_CLAMPU = 0x02000000,
			TEXTUREFLAGS_VERTEXTEXTURE = 0x04000000,
			TEXTUREFLAGS_SSBUMP = 0x08000000,

			TEXTUREFLAGS_UNUSED_10000000 = 0x10000000,

			TEXTUREFLAGS_BORDER = 0x20000000,

			TEXTUREFLAGS_UNUSED_40000000 = 0x40000000,
			TEXTUREFLAGS_UNUSED_80000000 = 0x80000000,
			};




		[StructLayout(LayoutKind.Sequential)]
		public struct tagVTFHEADER
			{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public char[] signature;		// File signature ("VTF\0").
			public uint version0;		// version[0].version[1] (currently 7.2).
			public uint version1;		// version[0].version[1] (currently 7.2).
			public uint headerSize;		// Size of the header struct (16 byte aligned; currently 80 bytes).
			public UInt16 width;			// Width of the largest mipmap in pixels. Must be a power of 2.
			public UInt16 height;			// Height of the largest mipmap in pixels. Must be a power of 2.
			public uint flags;			// VTF flags.
			public UInt16 frames;			// Number of frames, if animated (1 for no animation).
			public UInt16 firstFrame;		// First frame in animation (0 based).
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] padding0;		// reflectivity padding (16 byte alignment).
			public float reflectivity1;	// reflectivity vector.
			public float reflectivity2;	// reflectivity vector.
			public float reflectivity3;	// reflectivity vector.
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] padding1;		// reflectivity padding (16 byte alignment).
			public float bumpmapScale;		// Bumpmap scale.
			public uint highResImageFormat;	// High resolution image format.
			public byte nummips;		// Number of mipmaps.
			public int lowResImageFormat;	// Low resolution image format (always DXT1).
			public byte lowResImageWidth;	// Low resolution image width.
			public byte lowResImageHeight;	// Low resolution image height.
			public byte depth;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public byte[] padding2;		// reflectivity padding (16 byte alignment).
			public int ResourceCount;	// Resource Count.
			public UInt16 faces;
			};

		public class FrameEntry
			{
			public int fridx;
			public int froffset;
			public int frsize;
			public FrameEntry(int frame, int Offset, int Size)
				{
				fridx = frame;
				froffset = Offset;
				frsize = Size;
				}
			}

		private string szFullFile;					// Chemin complet du fichier
		public long lFileSize;
		int nMode;
		private uint uiImageBufferSize;
		private tagVTFHEADER header;
		private MemoryStream memStream;
		private BinaryReader binReader;
		private bool bParsedHeader;
		private byte[] ImageDatas;

		///////////////////////////////////////////////////////////////////////////////////////
		// Déclaration classe sur string
		///////////////////////////////////////////////////////////////////////////////////////			
		public VTFFile(string szFile)
			{
			szFullFile = szFile;
			uiImageBufferSize = 0;
			lFileSize = 0;
			nMode = 0;
			bParsedHeader = false;
			ImageDatas = null;
			}
		///////////////////////////////////////////////////////////////////////////////////////
		// Déclaration classe sur buffer
		///////////////////////////////////////////////////////////////////////////////////////			
		public VTFFile(byte[] buffer)
			{
			szFullFile = string.Empty;
			uiImageBufferSize = 0;
			lFileSize = 0;
			nMode = 1;
			memStream = new MemoryStream(buffer);
			bParsedHeader = false;
			ImageDatas = null;
			}
		///////////////////////////////////////////////////////////////////////////////////////
		// Ouverture fichier
		///////////////////////////////////////////////////////////////////////////////////////			
		public bool Open()
			{
			if (nMode == 0)
				{
				FileInfo fi = new FileInfo(szFullFile);

				if (fi.Exists)
					{
					try
						{
						lFileSize = fi.Length;
						memStream = new MemoryStream(File.ReadAllBytes(szFullFile));
						binReader = new BinaryReader(memStream);
						}
					catch (Exception)
						{
						return false;
						}
					if (binReader != null)
						return true;
					}
				}
			else
				{
				if (memStream != null)
					{
					if (memStream.Length != 0)
						{
						binReader = new BinaryReader(memStream);
						if (binReader != null)
							return true;
						}
					}
				}
			return false;
			}
		///////////////////////////////////////////////////////////////////////////////////////
		// Fermeture fichier
		///////////////////////////////////////////////////////////////////////////////////////		
		public bool Close()
			{
			if (binReader != null)
				binReader.Close();
			if (memStream != null)
				memStream.Close();
			binReader = null;
			memStream = null;
			return true;
			}
		///////////////////////////////////////////////////////////////////////////////////////
		// Lecture entête
		///////////////////////////////////////////////////////////////////////////////////////	
		public virtual bool ReadHeader()
			{
			binReader.BaseStream.Seek(0, SeekOrigin.Begin);
			header = new tagVTFHEADER();

			header.signature = binReader.ReadChars(4);
			//Check header
			string magic = new string(header.signature);
			if (magic != "VTF\0")
				{
				Close();
				return false;
				}

			header.version0 = binReader.ReadUInt32();
			header.version1 = binReader.ReadUInt32();
			header.headerSize = binReader.ReadUInt32();

			if (header.version0 != 7)
				return false;

			if (header.version1 < 0 || header.version1 > 5)
				return false;

			if (header.headerSize != 80 && header.headerSize != 96 && header.headerSize != 104 && header.headerSize != 64 && header.headerSize != 88)
				return false;

			header.width = binReader.ReadUInt16();
			header.height = binReader.ReadUInt16();
			header.flags = binReader.ReadUInt32();
			header.frames = binReader.ReadUInt16();
			header.firstFrame = binReader.ReadUInt16();
			if (header.firstFrame == 0xffff)
				header.firstFrame = 0;
			header.padding0 = binReader.ReadBytes(4);
			header.reflectivity1 = binReader.ReadSingle();
			header.reflectivity2 = binReader.ReadSingle();
			header.reflectivity3 = binReader.ReadSingle();
			header.padding1 = binReader.ReadBytes(4);
			header.bumpmapScale = binReader.ReadSingle();
			header.highResImageFormat = binReader.ReadUInt32();
			header.nummips = binReader.ReadByte();
			header.lowResImageFormat = binReader.ReadInt32();
			header.lowResImageWidth = binReader.ReadByte();
			header.lowResImageHeight = binReader.ReadByte();
			if (header.version1 > 1)
				header.depth = binReader.ReadByte();
			if (header.version1 > 2)
				{
				header.padding2 = binReader.ReadBytes(3);
				header.ResourceCount = binReader.ReadInt32();
				}
			if (header.version1 > 3)
				{
				header.padding2 = binReader.ReadBytes(8);
				}

			//Width et Height ne peuvent etre nuls
			if (header.width == 0 || header.height == 0)
				return false;
			//Width et Height doivent être multiples de 2			
			if ((header.width % 2) != 0 || (header.height % 2) != 0)
				return false;
			//Le LowRes image peut etre nul
			if ((header.lowResImageWidth != 0) && (header.lowResImageHeight != 0))
				{
				if ((header.lowResImageWidth % 2) != 0 || (header.lowResImageHeight % 2) != 0)
					return false;
				if ((header.lowResImageWidth > 16) || (header.lowResImageHeight > 16))
					return false;
				}
			if ((header.lowResImageWidth > header.width) || (header.lowResImageHeight > header.height))
				return false;
			if (header.lowResImageFormat != (int)vtfFormat.IMAGE_FORMAT_DXT1 && header.lowResImageFormat != -1)
				return false;

			if ((header.flags & (uint)vtfFlags.TEXTUREFLAGS_ENVMAP) != 0)
				header.faces = (UInt16)(header.firstFrame == 0xFFFF ? 6 : 7);
			else
				header.faces = 1;

			uiImageBufferSize = ComputeImageSize(header.width, header.height, header.nummips, (vtfFormat) header.highResImageFormat) * header.frames * header.faces;
			if (uiImageBufferSize == 0)
				return false;
			ImageDatas = new byte[uiImageBufferSize];
			ImageDatas = binReader.ReadBytes((int)uiImageBufferSize);
			bParsedHeader = true;
			return true;
			}
		///////////////////////////////////////////////////////////////////
		// 
		///////////////////////////////////////////////////////////////////		
		byte[] GetData(uint uiFrame, uint uiFace, uint uiSlice, uint uiMipmapLevel)
			{
			if (!bParsedHeader)
				return null;
			if (ImageDatas == null)
				return null;
			MemoryStream imgStream = new MemoryStream(ImageDatas);
			BinaryReader binTmp = new BinaryReader(imgStream);
			uint buffersize = ComputeMipmapSize(header.width, header.height, 1, uiMipmapLevel, (vtfFormat)header.highResImageFormat);
			uint dataoffset = ComputeDataOffset(uiFrame, uiFace, uiSlice, uiMipmapLevel, (vtfFormat)header.highResImageFormat);			
			byte[] buffer = new byte[buffersize];
			binTmp.BaseStream.Seek(dataoffset, SeekOrigin.Begin);
			buffer = binTmp.ReadBytes((int) buffersize);
			binTmp.Close();
			binTmp.Dispose();
			imgStream.Close();
			imgStream.Dispose();
			return buffer; 
			}
		///////////////////////////////////////////////////////////////////
		// 
		///////////////////////////////////////////////////////////////////		
		private void UpdateVtf(uint uiFrame, uint uiFace, uint uiSlice, uint uiMipLevel)
			{
			uint uiWidth = 0;
			uint uiHeight = 0;
			uint uiDepth = 0;
			uint uiBufferSize = 0;

			this.ComputeMipmapDimensions(header.width, header.height, header.depth, uiMipLevel, ref uiWidth, ref uiHeight, ref uiDepth);

			if (uiSlice >= uiDepth)
				uiSlice = uiDepth - 1;
			
			uint numSlice_Value = uiSlice;
			uint numSlice_Maximum = uiDepth;

			uiBufferSize = ComputeImageSize(uiWidth, uiHeight, 1, vtfFormat.IMAGE_FORMAT_RGBA8888);

			byte [] lpBuffer = new byte[uiBufferSize];

			ConvertToRGBA8888(GetData(uiFrame, uiFace, uiSlice, uiMipLevel), ref lpBuffer, uiWidth, uiHeight, (vtfFormat) header.highResImageFormat);


			}
		///////////////////////////////////////////////////////////////////
		// Récupérer l'adresse d'une image à un emplacement donné
		///////////////////////////////////////////////////////////////////		
		private uint ComputeDataOffset(uint uiFrame, uint uiFace, uint uiSlice, uint uiMipLevel, vtfFormat ImageFormat)
			{
			if (!bParsedHeader)
				return 0;

			uint uiOffset = 0;

			uint uiFrameCount = this.GetFrames();
			uint uiFaceCount = this.GetFaces();
			uint uiSliceCount = this.GetSlices();
			uint uiMipCount = this.GetNumMipmaps();

			if (uiFrame > uiFrameCount)
				uiFrame = uiFrameCount - 1;
			if (uiFace > uiFaceCount)
				uiFace = uiFaceCount - 1;
			if (uiSlice > uiSliceCount)
				uiSlice = uiSliceCount - 1;
			if (uiMipLevel > uiMipCount)
				uiMipLevel = uiMipCount - 1;

			// Transverse past all frames and faces of each mipmap (up to the requested one).
			for (uint  i = uiMipCount - 1; i > uiMipLevel; i--)
				{
				uiOffset += ComputeMipmapSize(this.header.width, this.header.height, this.header.depth, i, ImageFormat) * uiFrameCount * uiFaceCount;
				}
			uint uiTemp1 = ComputeMipmapSize(this.header.width, this.header.height, this.header.depth, uiMipLevel, ImageFormat);
			uint uiTemp2 = ComputeMipmapSize(this.header.width, this.header.height, 1, uiMipLevel, ImageFormat);
			// Transverse past requested frames and faces of requested mipmap.
			uiOffset += uiTemp1 * uiFrame * uiFaceCount * uiSliceCount;
			uiOffset += uiTemp1 * uiFace * uiSliceCount;
			uiOffset += uiTemp2 * uiSlice;

			if (uiOffset > uiImageBufferSize)
				return 0;
			return uiOffset;
			}
		///////////////////////////////////////////////////////////////////
		// Récupérer informations d'un format d'image
		///////////////////////////////////////////////////////////////////	
		SVTFImageFormatInfo GetImageFormatInfo(vtfFormat ImageFormat)
			{
			if ((int)ImageFormat < 0 || (int)ImageFormat > VTFImageFormatInfo.Length - 1)
				return null;
			return VTFImageFormatInfo[(int) ImageFormat];
			}
		///////////////////////////////////////////////////////////////////
		// Calculer la taille en bits d'une image en fonction de sa taille et de son type
		///////////////////////////////////////////////////////////////////		
		private uint ComputeImageSize(uint uiWidth, uint uiHeight, uint uiDepth, vtfFormat ImageFormat)
			{
			switch(ImageFormat)
				{
				case vtfFormat.IMAGE_FORMAT_DXT1:
				case vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
					if(uiWidth < 4 && uiWidth > 0)
						uiWidth = 4;
					if(uiHeight < 4 && uiHeight > 0)
						uiHeight = 4;
					return ((uiWidth + 3) / 4) * ((uiHeight + 3) / 4) * 8 * uiDepth;
				case vtfFormat.IMAGE_FORMAT_DXT3:
				case vtfFormat.IMAGE_FORMAT_DXT5:
					if(uiWidth < 4 && uiWidth > 0)
						uiWidth = 4;
					if(uiHeight < 4 && uiHeight > 0)
						uiHeight = 4;
					return ((uiWidth + 3) / 4) * ((uiHeight + 3) / 4) * 16 * uiDepth;
				default:
					SVTFImageFormatInfo tmpImgInfo = GetImageFormatInfo(ImageFormat);
					if (tmpImgInfo == null)
						return 0;
					return uiWidth * uiHeight * uiDepth * tmpImgInfo.uiBytesPerPixel;
				}
			}
		///////////////////////////////////////////////////////////////////
		//  Calculer la taille en bits d'une image en fonction de sa taille, de son type et du nombre de mipmaps
		///////////////////////////////////////////////////////////////////		
		private uint ComputeImageSize(uint uiWidth, uint uiHeight, uint uiDepth,uint uiMipmaps, vtfFormat ImageFormat)
			{
			uint uiImageSize = 0;

			if (uiWidth == 0 && uiHeight == 0 && uiDepth == 0)
				return 0;

			for (uint i = 0; i < uiMipmaps; i++)
				{
				uiImageSize += ComputeImageSize(uiWidth, uiHeight, uiDepth, ImageFormat);
				uiWidth >>= 1;
				uiHeight >>= 1;
				uiDepth >>= 1;
				if (uiWidth < 1)
					uiWidth = 1;
				if (uiHeight < 1)
					uiHeight = 1;
				if (uiDepth < 1)
					uiDepth = 1;
				}
			return uiImageSize;
			}
		///////////////////////////////////////////////////////////////////
		// Calculer la taille (width, height, depth) d'un mipmap à un niveau donné
		///////////////////////////////////////////////////////////////////		
		private void ComputeMipmapDimensions(uint uiWidth, uint uiHeight, uint uiDepth, uint uiMipmapLevel, ref uint uiMipmapWidth, ref uint uiMipmapHeight, ref uint uiMipmapDepth)
			{
			uiMipmapWidth = uiWidth >> (int) uiMipmapLevel;
			uiMipmapHeight = uiHeight >> (int) uiMipmapLevel;
			uiMipmapDepth = uiDepth >> (int) uiMipmapLevel;
			// stop the dimension being less than 1 x 1
			if(uiMipmapWidth < 1)
				uiMipmapWidth = 1;

			if(uiMipmapHeight < 1)
				uiMipmapHeight = 1;

			if(uiMipmapDepth < 1)
				uiMipmapDepth = 1;
			}
		///////////////////////////////////////////////////////////////////
		// Calculer la taille mémoire d'un mipmap en fonction de sa taille de son niveau et de son format
		///////////////////////////////////////////////////////////////////		
		private uint ComputeMipmapSize(uint width, uint height, uint depth, uint MipLevel, vtfFormat ImageFormat)
			{
			uint uiMipmapWidth=0, uiMipmapHeight=0, uiMipmapDepth=0;
			ComputeMipmapDimensions(width, height, depth, MipLevel, ref uiMipmapWidth, ref uiMipmapHeight, ref uiMipmapDepth);
			return ComputeImageSize(uiMipmapWidth, uiMipmapHeight, uiMipmapDepth, ImageFormat);
			}

		///////////////////////////////////////////////////////////////////////////////////////
		// Renvoyer les datas du header
		///////////////////////////////////////////////////////////////////////////////////////			
		public string GetSignature()
			{
			return new string(header.signature);
			}
		public string GetVersion()
			{
			return header.version0.ToString() + "." + header.version1.ToString();
			}
		public UInt16 GetWidth()
			{
			return header.width;
			}
		public UInt16 GetHeight()
			{
			return header.height;
			}
		public uint GetFlags()
			{
			return header.flags;
			}
		public UInt16 GetFrames()
			{
			return header.frames;
			}
		public UInt16 GetFirstFrame()
			{
			return header.firstFrame;
			}
		public UInt16 GetFaces()
			{
			return header.faces;
			}
		public UInt16 GetSlices()
			{
			return 1;
			}
		public float GetReflectivity1()
			{
			return header.reflectivity1;
			}
		public float GetReflectivity2()
			{
			return header.reflectivity2;
			}
		public float GetReflectivity3()
			{
			return header.reflectivity3;
			}
		public float GetBumpScale()
			{
			return header.bumpmapScale;
			}
		public uint GetHighResImageFormat()
			{
			return header.highResImageFormat;
			}
		public string GetHihResFormatString()
			{
			return GetFormatString(header.highResImageFormat);
			}
		public UInt16 GetNumMipmaps()
			{
			return header.nummips;
			}
		public int GetLowResImageFormat()
			{
			return header.lowResImageFormat;
			}
		public string GetLowResFormatString()
			{
			return GetFormatString((uint) header.lowResImageFormat);
			}
		public UInt16 GetLowResImageWidth()
			{
			return header.lowResImageWidth;
			}
		public UInt16 GetLowResImageHeigt()
			{
			return header.lowResImageHeight;
			}
		public UInt16 GetImageDepth()
			{
			return header.depth;
			}
		public int GetResourceCount()
			{
			return header.ResourceCount;
			}
		public UInt16 GetBytesperPixel()
			{
			return GetBytesPerPixel();
			}
		///////////////////////////////////////////////////////////////////////////////////////
		// Renvoie le nombre de bits par pixel
		///////////////////////////////////////////////////////////////////////////////////////			
		private UInt16 GetBytesPerPixel()
			{
			switch (header.highResImageFormat)
				{
				case (int)vtfFormat.IMAGE_FORMAT_RGBA16161616F:
				case (int)vtfFormat.IMAGE_FORMAT_RGBA16161616:
					return 8;
				case (int)vtfFormat.IMAGE_FORMAT_RGBA8888:
				case (int)vtfFormat.IMAGE_FORMAT_ABGR8888:
				case (int)vtfFormat.IMAGE_FORMAT_ARGB8888:
				case (int)vtfFormat.IMAGE_FORMAT_BGRA8888:
				case (int)vtfFormat.IMAGE_FORMAT_BGRX8888:
				case (int)vtfFormat.IMAGE_FORMAT_UVWQ8888:
				case (int)vtfFormat.IMAGE_FORMAT_UVLX8888:
					return 4;
				case (int)vtfFormat.IMAGE_FORMAT_RGB888:
				case (int)vtfFormat.IMAGE_FORMAT_BGR888:
				case (int)vtfFormat.IMAGE_FORMAT_RGB888_BLUESCREEN:
				case (int)vtfFormat.IMAGE_FORMAT_BGR888_BLUESCREEN:
					return 3;
				case (int)vtfFormat.IMAGE_FORMAT_RGB565:
				case (int)vtfFormat.IMAGE_FORMAT_IA88:
				case (int)vtfFormat.IMAGE_FORMAT_BGR565:
				case (int)vtfFormat.IMAGE_FORMAT_BGRX5551:
				case (int)vtfFormat.IMAGE_FORMAT_BGRA4444:
				case (int)vtfFormat.IMAGE_FORMAT_BGRA5551:
				case (int)vtfFormat.IMAGE_FORMAT_UV88:
					return 2;
				case (int)vtfFormat.IMAGE_FORMAT_I8:
				case (int)vtfFormat.IMAGE_FORMAT_P8:
				case (int)vtfFormat.IMAGE_FORMAT_A8:
					return 1;
				case (int)vtfFormat.IMAGE_FORMAT_DXT1:
				case (int)vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
				case (int)vtfFormat.IMAGE_FORMAT_DXT3:
				case (int)vtfFormat.IMAGE_FORMAT_DXT5:
					return 0;
				default:
					return 0;
				}
			}
		///////////////////////////////////////////////////////////////////////////////////////
		// Renvoie la chaine de caractère du format
		///////////////////////////////////////////////////////////////////////////////////////			
		private string GetFormatString(uint formatId)
			{
			switch (formatId)
				{
				case (int)vtfFormat.IMAGE_FORMAT_RGBA16161616F:
					return "RGBA16161616F";
				case (int)vtfFormat.IMAGE_FORMAT_RGBA16161616:
					return "RGBA16161616";
				case (int)vtfFormat.IMAGE_FORMAT_RGBA8888:
					return "RGBA8888";
				case (int)vtfFormat.IMAGE_FORMAT_ABGR8888:
					return "ABGR8888";
				case (int)vtfFormat.IMAGE_FORMAT_ARGB8888:
					return "ARGB8888";
				case (int)vtfFormat.IMAGE_FORMAT_BGRA8888:
					return "BGRA8888";
				case (int)vtfFormat.IMAGE_FORMAT_BGRX8888:
					return "BGRX8888";
				case (int)vtfFormat.IMAGE_FORMAT_UVWQ8888:
					return "UVWQ8888";
				case (int)vtfFormat.IMAGE_FORMAT_UVLX8888:
					return "UVLX8888";
				case (int)vtfFormat.IMAGE_FORMAT_RGB888:
					return "RGB888";
				case (int)vtfFormat.IMAGE_FORMAT_BGR888:
					return "BGR888";
				case (int)vtfFormat.IMAGE_FORMAT_RGB888_BLUESCREEN:
					return "RGB888_BLUESCREEN";
				case (int)vtfFormat.IMAGE_FORMAT_BGR888_BLUESCREEN:
					return "BGR888_BLUESCREEN";
				case (int)vtfFormat.IMAGE_FORMAT_RGB565:
					return "RGB565";
				case (int)vtfFormat.IMAGE_FORMAT_IA88:
					return "IA88";
				case (int)vtfFormat.IMAGE_FORMAT_BGR565:
					return "BGR565";
				case (int)vtfFormat.IMAGE_FORMAT_BGRX5551:
					return "BGRX5551";
				case (int)vtfFormat.IMAGE_FORMAT_BGRA4444:
					return "BGRA4444";
				case (int)vtfFormat.IMAGE_FORMAT_BGRA5551:
					return "BGRA5551";
				case (int)vtfFormat.IMAGE_FORMAT_UV88:
					return "UV88";
				case (int)vtfFormat.IMAGE_FORMAT_I8:
					return "I8";
				case (int)vtfFormat.IMAGE_FORMAT_P8:
					return "P8";
				case (int)vtfFormat.IMAGE_FORMAT_A8:
					return "A8";
				case (int)vtfFormat.IMAGE_FORMAT_DXT1:
					return "DXT1";
				case (int)vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
					return "DXT1_ONEBITALPHA";
				case (int)vtfFormat.IMAGE_FORMAT_DXT3:
					return "DXT3";
				case (int)vtfFormat.IMAGE_FORMAT_DXT5:
					return "DXT5";
				default:
					return "NONE";
				}
			}

/*		///////////////////////////////////////////////////////////////////
		// Récupérer les infos Offset et taille 
		// de chaque frame
		///////////////////////////////////////////////////////////////////	
		public void GetMainFramesDatas()
			{
			int m = 0, f = 0, MipW = 0, MipH = 0, MipD = 0, MipS = 0, HRSize = 0;

			BytesPerPixel = GetBytesPerPixel();
			//Start of Images Datas
			int HROffset = (int)header.headerSize;
			//Skip Thumbnail
			int SizeOfLowRes = Math.Max(header.lowResImageWidth * header.lowResImageHeight / 2, 8);

			if (header.lowResImageWidth == 0 && header.lowResImageHeight == 0)
				SizeOfLowRes = 0;
			HROffset += SizeOfLowRes;

			for (m = header.nummips - 1; m >= 0; m--)
				{
				//MipMap Dimensions
				MipW = header.width >> m;
				MipH = header.height >> m;
				MipD = header.depth >> m;
				if (MipW < 1) MipW = 1;
				if (MipH < 1) MipH = 1;
				if (MipD < 1) MipD = 1;
				//Mipmap Size
				MipS = 0;
				switch (header.highResImageFormat)
					{
					case (int)vtfFormat.IMAGE_FORMAT_DXT1:
					case (int)vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
						if (MipW < 4 && MipW > 0)
							MipW = 4;
						if (MipH < 4 && MipH > 0)
							MipH = 4;
						MipS = Math.Max((MipW * MipH * MipD) / 2, 8);
						break;
					case (int)vtfFormat.IMAGE_FORMAT_DXT3:
					case (int)vtfFormat.IMAGE_FORMAT_DXT5:
						if (MipW < 4 && MipW > 0)
							MipW = 4;
						if (MipH < 4 && MipH > 0)
							MipH = 4;
						MipS = Math.Max(MipW * MipH * MipD, 16);
						break;
					default:
						MipS = MipW * MipH * MipD * BytesPerPixel;
						break;
					}
				//Offset of Last Frame
				HROffset += (MipS * header.frames * NumFaces);
				}
			//On retourne à l'avant dernier Offset
			HROffset -= (MipS * header.frames * NumFaces);
			HRSize = MipS;
			for (f = 0; f < header.frames; f++)
				{
				arFrames.Add(new FrameEntry(f, HROffset, HRSize));
				HROffset += HRSize;
				}
			}*/


		///////////////////////////////////////////////////////////////////
		// Récupérer le contenu des Frames
		// Error=11 si erreur
		///////////////////////////////////////////////////////////////////		
		public List<Texture> LoadFrames()
			{
			int f;
			List<Texture> TexInfos = new List<Texture>();
			for (f = 0; f < header.frames; f++)
				{
				//TexInfos.Add(LoadFrame(f));
				}
			return TexInfos;
			}
/*		///////////////////////////////////////////////////////////////////
		// Récupérer le contenu des Frames
		// Error=11 si erreur
		///////////////////////////////////////////////////////////////////		
		public Texture LoadFrame(int nFrame)
			{
			Bitmap BmpTmp = new Bitmap(header.width, header.height, PixelFormat.Format32bppArgb);
			Texture TxInfo = null;
			FrameEntry FrmTmp = new FrameEntry(0, 0, 0);

			byte[] buftmp = new byte[16];

			FrmTmp = arFrames.ElementAt(nFrame);
			myStm.Seek(FrmTmp.froffset);
			Array.Resize(ref buftmp, FrmTmp.frsize);
			buftmp = myStm.ReadBytes(FrmTmp.frsize);

			switch (header.highResImageFormat)
				{
				case (int)vtfFormat.IMAGE_FORMAT_DXT1:
				case (int)vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
					BmpTmp = Graph_DecompressDXT1(buftmp, header.width, header.height);
					break;
				case (int)vtfFormat.IMAGE_FORMAT_DXT3:
					BmpTmp = Graph_DecompressDXT3(buftmp, header.width, header.height);
					break;
				case (int)vtfFormat.IMAGE_FORMAT_DXT5:
					BmpTmp = Graph_DecompressDXT5(buftmp, header.width, header.height);
					break;
				case (int)vtfFormat.IMAGE_FORMAT_BGR888:
					BmpTmp = Graph_ReadBGR888(buftmp, header.width, header.height);
					break;
				case (int)vtfFormat.IMAGE_FORMAT_BGRA8888:
					BmpTmp = Graph_ReadBGRA8888(buftmp, header.width, header.height);
					break;
				default:
					break;
				}

			Bitmap bmp = new Bitmap((int)header.width, (int)header.height, PixelFormat.Format8bppIndexed);
			bmp = BmpTmp.Clone(new Rectangle(0, 0, BmpTmp.Width, BmpTmp.Height), bmp.PixelFormat);

			TxInfo = new Texture();
			TxInfo.szName = Path.GetFileNameWithoutExtension(szFullFile);
			TxInfo.uiWidth = header.width;
			TxInfo.uiHeight = header.height;
			TxInfo.uiDiskLength = (uint)FrmTmp.frsize;
			TxInfo.Image = bmp;
			TxInfo.iLvwIdx = nFrame;
			TxInfo.iImgIdx = -1;
			TxInfo.Mipmaps = null;
			TxInfo.rawImg = null;
			TxInfo.rawPal = null;
			TxInfo.type = 0x00;
			return TxInfo;
			}*/

		/*
				///////////////////////////////////////////////////////////////////
				// Récupérer le contenu des Frames
				// Error=11 si erreur
				///////////////////////////////////////////////////////////////////		
				public List<Texture> LoadDevilFrames(BackgroundWorker worker=null)
					{
					List<Texture> TexInfos = new List<Texture>();
					Texture TxInfo = null;
					int img_name;
					Il.ilInit();
					Il.ilGenImages(1, out img_name);
					Il.ilBindImage(img_name);
					//Load Image
					if (!Il.ilLoadImage(szFullFile))
						{
						return TexInfos;
						}
					int nummipmaps = Il.ilGetInteger(Il.IL_NUM_MIPMAPS);
					int m;
					for (m = 0; m < nummipmaps; m++)
						{
						Il.ilActiveMipmap(0);
						// Set a few size variables that will simplify later code
						int ImgWidth = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
						int ImgHeight = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
						Rectangle rect = new Rectangle(0, 0, ImgWidth, ImgHeight);
						// Convert the DevIL image to a pixel byte array to copy into Bitmap
						Il.ilConvertImage(Il.IL_BGRA, Il.IL_UNSIGNED_BYTE);
						// Create a Bitmap to copy the image into, and prepare it to get data
						Bitmap bmp = new Bitmap(ImgWidth, ImgHeight);
						BitmapData bmd = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
						// Copy the pixel byte array from the DevIL image to the Bitmap
						Il.ilCopyPixels(0, 0, 0, Il.ilGetInteger(Il.IL_IMAGE_WIDTH), Il.ilGetInteger(Il.IL_IMAGE_HEIGHT), 1, Il.IL_BGRA, Il.IL_UNSIGNED_BYTE, bmd.Scan0);
						// Clean up and return Bitmap
						bmp.UnlockBits(bmd);

						TxInfo = new Texture();
						TxInfo.szName = Path.GetFileNameWithoutExtension(szFullFile) + "-" + m.ToString();
						TxInfo.uiWidth = (uint)ImgWidth;
						TxInfo.uiHeight = (uint)ImgHeight;
						TxInfo.uiDiskLength = (uint)Il.ilGetInteger(Il.IL_IMAGE_SIZE_OF_DATA);
						TxInfo.Image = bmp;
						TxInfo.bIsModified = false;
						TexInfos.Add(TxInfo);
						}
					Il.ilDeleteImages(1, ref img_name);
					return TexInfos;
					}*/


		///////////////////////////////////////////////////////////////////////////////////////
		// Fonctions de décompression
		// Utilisée dans les TexImages
		///////////////////////////////////////////////////////////////////////////////////////
		#region DXT1
		public bool UncompressDXT1(byte[] src, ref byte[] dest, uint w, uint h)
			{
			using (MemoryStream ms = new MemoryStream(src))
				{
				using (BinaryReader r = new BinaryReader(ms))
					{
					int nIdx = 0;
					uint blockCountX = (w + 3) / 4;
					uint blockCountY = (h + 3) / 4;
					uint blockWidth = (w < 4) ? w : 4;
					uint blockHeight = (h < 4) ? w : 4;

					for (uint j = 0; j < blockCountY; j++)
						{
						for (uint i = 0; i < blockCountX; i++)
							{
							byte[] blockStorage = r.ReadBytes(8);
							DecompressBlockDXT1(i * 4, j * 4, w, blockStorage, nIdx, ref dest);
							nIdx += 4;
							}
						}
					}
				}
			return true;
			}

		private void DecompressBlockDXT1(uint x, uint y, uint width, byte[] blockStorage, int nIdx, ref byte[] dest)
			{
			ushort color0 = (ushort)(blockStorage[0] | blockStorage[1] << 8);
			ushort color1 = (ushort)(blockStorage[2] | blockStorage[3] << 8);

			int temp;

			temp = (color0 >> 11) * 255 + 16;
			byte r0 = (byte)((temp / 32 + temp) / 32);
			temp = ((color0 & 0x07E0) >> 5) * 255 + 32;
			byte g0 = (byte)((temp / 64 + temp) / 64);
			temp = (color0 & 0x001F) * 255 + 16;
			byte b0 = (byte)((temp / 32 + temp) / 32);

			temp = (color1 >> 11) * 255 + 16;
			byte r1 = (byte)((temp / 32 + temp) / 32);
			temp = ((color1 & 0x07E0) >> 5) * 255 + 32;
			byte g1 = (byte)((temp / 64 + temp) / 64);
			temp = (color1 & 0x001F) * 255 + 16;
			byte b1 = (byte)((temp / 32 + temp) / 32);

			uint code = (uint)(blockStorage[4] | blockStorage[5] << 8 | blockStorage[6] << 16 | blockStorage[7] << 24);

			for (int j = 0; j < 4; j++)
				{
				for (int i = 0; i < 4; i++)
					{
					Color finalColor = Color.FromArgb(0);
					byte positionCode = (byte)((code >> 2 * (4 * j + i)) & 0x03);

					if (color0 > color1)
						{
						switch (positionCode)
							{
							case 0:
								finalColor = Color.FromArgb(255, r0, g0, b0);
								break;
							case 1:
								finalColor = Color.FromArgb(255, r1, g1, b1);
								break;
							case 2:
								finalColor = Color.FromArgb(255, (2 * r0 + r1) / 3, (2 * g0 + g1) / 3, (2 * b0 + b1) / 3);
								break;
							case 3:
								finalColor = Color.FromArgb(255, (r0 + 2 * r1) / 3, (g0 + 2 * g1) / 3, (b0 + 2 * b1) / 3);
								break;
							}
						}
					else
						{
						switch (positionCode)
							{
							case 0:
								finalColor = Color.FromArgb(255, r0, g0, b0);
								break;
							case 1:
								finalColor = Color.FromArgb(255, r1, g1, b1);
								break;
							case 2:
								finalColor = Color.FromArgb(255, (r0 + r1) / 2, (g0 + g1) / 2, (b0 + b1) / 2);
								break;
							case 3:
								finalColor = Color.FromArgb(255, 0, 0, 0);
								break;
							}
						}

					if (x + i < width)
						{
						dest[nIdx + 0] = finalColor.R;
						dest[nIdx + 1] = finalColor.G;
						dest[nIdx + 2] = finalColor.B;
						dest[nIdx + 3] = finalColor.A;
						}
					}
				}
			}
		#endregion

		#region DXT3

		public bool UncompressDXT3(byte[] src, ref byte[] dest, uint w, uint h)
			{
			using (MemoryStream ms = new MemoryStream(src))
				{
				using (BinaryReader r = new BinaryReader(ms))
					{
					int nIdx = 0;
					int x, y, j, k, i, c, bitmask;
					int color_0, color_1;

					byte[] alpha = new byte[8];

					RGBAEntry RGB_0 = new RGBAEntry();
					RGBAEntry RGB_1 = new RGBAEntry();
					RGBAEntry RGB_2 = new RGBAEntry();
					RGBAEntry RGB_3 = new RGBAEntry();
					RGBAEntry RGB_X = new RGBAEntry();

					List<RGBAEntry> rgbLst = new List<RGBAEntry>();

					for (y = 0; y < h; y += 4)
						{
						for (x = 0; x < w; x += 4)
							{
							alpha = r.ReadBytes(8);

							color_0 = r.ReadUInt16();
							color_1 = r.ReadUInt16();
							bitmask = r.ReadInt32();

							RGB_0 = Graph_Col565toRgb(color_0);
							RGB_1 = Graph_Col565toRgb(color_1);
							rgbLst.Insert(0, RGB_0);
							rgbLst.Insert(1, RGB_1);

							RGB_2.b = (2 * RGB_0.b + RGB_1.b + 1) / 3;
							RGB_2.g = (2 * RGB_0.g + RGB_1.g + 1) / 3;
							RGB_2.r = (2 * RGB_0.r + RGB_1.r + 1) / 3;
							RGB_2.a = 0xFF;

							RGB_3.b = (RGB_0.b + 2 * RGB_1.b + 1) / 3;
							RGB_3.g = (RGB_0.g + 2 * RGB_1.g + 1) / 3;
							RGB_3.r = (RGB_0.r + 2 * RGB_1.r + 1) / 3;
							RGB_3.a = 0xFF;

							rgbLst.Insert(2, RGB_2);
							rgbLst.Insert(3, RGB_3);

							for (j = 0, k = 0; j < 4; j++)
								{
								for (i = 0; i < 4; i++, k++)
									{
									c = ((bitmask & (0x03 << k * 2)) >> k * 2) & 0x03;
									RGB_X = rgbLst.ElementAt(c);
									dest[nIdx + 0] = (byte) RGB_X.r;
									dest[nIdx + 0] = (byte) RGB_X.g;
									dest[nIdx + 0] = (byte) RGB_X.b;
									dest[nIdx + 0] = (byte) RGB_X.a;
									}
								}
							}
						}
					}
				}
			return true;
			}
		#endregion

		#region DXT5
		public bool UncompressDXT5(byte[] src , ref byte[] dest,  uint w, uint h)
			{
			using (MemoryStream ms = new MemoryStream(src))
				{
				using (BinaryReader r = new BinaryReader(ms))
					{
					int nIdx=0;
					uint blockCountX = (w + 3) / 4;
					uint blockCountY = (h + 3) / 4;
					uint blockWidth = (w < 4) ? w : 4;
					uint blockHeight = (h < 4) ? w : 4;

					for (uint j = 0; j < blockCountY; j++)
						{
						for (uint i = 0; i < blockCountX; i++)
							{
							byte[] blockStorage = r.ReadBytes(16);
							DecompressBlockDXT5(i * 4, j * 4, w, blockStorage, nIdx, ref dest);
							nIdx +=4;
							}
						}
					}
				}
			return true;
			}

		private static void DecompressBlockDXT5(uint x, uint y, uint width, byte[] blockStorage, int nIdx, ref byte[] dest)
			{
			byte alpha0 = blockStorage[0];
			byte alpha1 = blockStorage[1];

			int bitOffset = 2;
			int alphaCode1 = (int)(blockStorage[bitOffset + 2] | (blockStorage[bitOffset + 3] << 8) | (blockStorage[bitOffset + 4] << 16) | (blockStorage[bitOffset + 5] << 24));
			ushort alphaCode2 = (ushort)(blockStorage[bitOffset + 0] | (blockStorage[bitOffset + 1] << 8));

			ushort color0 = (ushort)(blockStorage[8] | blockStorage[9] << 8);
			ushort color1 = (ushort)(blockStorage[10] | blockStorage[11] << 8);

			int temp;

			temp = (color0 >> 11) * 255 + 16;
			byte r0 = (byte)((temp / 32 + temp) / 32);
			temp = ((color0 & 0x07E0) >> 5) * 255 + 32;
			byte g0 = (byte)((temp / 64 + temp) / 64);
			temp = (color0 & 0x001F) * 255 + 16;
			byte b0 = (byte)((temp / 32 + temp) / 32);

			temp = (color1 >> 11) * 255 + 16;
			byte r1 = (byte)((temp / 32 + temp) / 32);
			temp = ((color1 & 0x07E0) >> 5) * 255 + 32;
			byte g1 = (byte)((temp / 64 + temp) / 64);
			temp = (color1 & 0x001F) * 255 + 16;
			byte b1 = (byte)((temp / 32 + temp) / 32);

			uint code = (uint)(blockStorage[12] | blockStorage[13] << 8 | blockStorage[14] << 16 | blockStorage[15] << 24);

			for (int j = 0; j < 4; j++)
				{
				for (int i = 0; i < 4; i++)
					{
					int alphaCodeIndex = 3 * (4 * j + i);
					int alphaCode;

					if (alphaCodeIndex <= 12)
						{
						alphaCode = (alphaCode2 >> alphaCodeIndex) & 0x07;
						}
					else if (alphaCodeIndex == 15)
						{
						alphaCode = (int)((alphaCode2 >> 15) | ((alphaCode1 << 1) & 0x06));
						}
					else
						{
						alphaCode = (int)((alphaCode1 >> (alphaCodeIndex - 16)) & 0x07);
						}

					byte finalAlpha;
					if (alphaCode == 0)
						{
						finalAlpha = alpha0;
						}
					else if (alphaCode == 1)
						{
						finalAlpha = alpha1;
						}
					else
						{
						if (alpha0 > alpha1)
							{
							finalAlpha = (byte)(((8 - alphaCode) * alpha0 + (alphaCode - 1) * alpha1) / 7);
							}
						else
							{
							if (alphaCode == 6)
								finalAlpha = 0;
							else if (alphaCode == 7)
								finalAlpha = 255;
							else
								finalAlpha = (byte)(((6 - alphaCode) * alpha0 + (alphaCode - 1) * alpha1) / 5);
							}
						}

					byte colorCode = (byte)((code >> 2 * (4 * j + i)) & 0x03);

					Color finalColor = new Color();
					switch (colorCode)
						{
						case 0:
							finalColor = Color.FromArgb(finalAlpha, r0, g0, b0);
							break;
						case 1:
							finalColor = Color.FromArgb(finalAlpha, r1, g1, b1);
							break;
						case 2:
							finalColor = Color.FromArgb(finalAlpha, (2 * r0 + r1) / 3, (2 * g0 + g1) / 3, (2 * b0 + b1) / 3);
							break;
						case 3:
							finalColor = Color.FromArgb(finalAlpha, (r0 + 2 * r1) / 3, (g0 + 2 * g1) / 3, (b0 + 2 * b1) / 3);
							break;
						}

					if (x + i < width)
						{
						dest[nIdx+0]=finalColor.R;
						dest[nIdx+1]=finalColor.G;
						dest[nIdx+2]=finalColor.B;
						dest[nIdx+3]=finalColor.A;
						}
					}
				}
			}
		#endregion

		private bool ConvertToRGBA8888(byte[] Source, ref byte[] Dest, uint uiWidth, uint uiHeight, vtfFormat SourceFormat)
			{
			return Convert(Source, ref Dest, uiWidth, uiHeight, SourceFormat, vtfFormat.IMAGE_FORMAT_RGBA8888);
			}

		private bool Convert(byte[] Source, ref byte[] Dest, uint uiWidth, uint uiHeight, vtfFormat SourceFormat, vtfFormat DestFormat)
			{
			if (Source == null || Source.Length == 0)
				return false;
			if (Dest == null || Dest.Length == 0)
				return false;
			if (SourceFormat < 0 || (int) SourceFormat > VTFImageFormatInfo.Length)
				return false;
			if (DestFormat < 0 || (int)DestFormat > VTFImageFormatInfo.Length)
				return false;

			SVTFImageFormatInfo SourceInfo = VTFImageFormatInfo[(int) SourceFormat];
			SVTFImageFormatInfo DestInfo = VTFImageFormatInfo[(int)DestFormat];

			if (!SourceInfo.bIsSupported || !DestInfo.bIsSupported)
				return false;

			if (SourceFormat == DestFormat)
				{
				Source.CopyTo(Dest,0);
				return true;
				}

			if(SourceFormat == vtfFormat.IMAGE_FORMAT_RGB888 && DestFormat == vtfFormat.IMAGE_FORMAT_RGBA8888)
				{
				uint lpLast = ComputeImageSize(uiWidth, uiHeight, 1, SourceFormat);
				for(int lpSource=0,lpDest=0; lpSource < lpLast; lpSource += 3, lpDest += 4)
					{
					Dest[lpDest+0] = Source[lpSource+0];
					Dest[lpDest+1] = Source[lpSource+1];
					Dest[lpDest+2] = Source[lpSource+2];
					Dest[lpDest+3] = 255;
					}
				return true;
				}

			if(SourceFormat == vtfFormat.IMAGE_FORMAT_RGBA8888 && DestFormat == vtfFormat.IMAGE_FORMAT_RGB888)
				{
				uint lpLast = ComputeImageSize(uiWidth, uiHeight, 1, SourceFormat);
				for (int lpSource = 0, lpDest = 0; lpSource < lpLast; lpSource += 4, lpDest += 3)
					{
					Dest[lpDest + 0] = Source[lpSource + 0];
					Dest[lpDest + 1] = Source[lpSource + 1];
					Dest[lpDest + 2] = Source[lpSource + 2];
					}
				return true;
				}

			if (SourceInfo.bIsCompressed || DestInfo.bIsCompressed)
				{
				byte[] SourceRGBA = new byte[Source.Length];
				Source.CopyTo(SourceRGBA, 0);

				bool bResult = true;

				if (SourceFormat != vtfFormat.IMAGE_FORMAT_RGBA8888)
					{
					SourceRGBA = new Byte[ComputeImageSize(uiWidth, uiHeight, 1, vtfFormat.IMAGE_FORMAT_RGBA8888)];
					}

				// decompress the source or convert it to RGBA for compressing
				switch (SourceFormat)
					{
					case vtfFormat.IMAGE_FORMAT_RGBA8888:
						break;
					case vtfFormat.IMAGE_FORMAT_DXT1:
					case vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
						bResult = UncompressDXT1(Source, ref SourceRGBA, uiWidth, uiHeight);
						break;
					case vtfFormat.IMAGE_FORMAT_DXT3:
						bResult = UncompressDXT3(Source, ref SourceRGBA, uiWidth, uiHeight);
						break;
					case vtfFormat.IMAGE_FORMAT_DXT5:
						bResult = UncompressDXT5(Source, ref SourceRGBA, uiWidth, uiHeight);
						break;
					default:
						bResult = Convert(Source, ref SourceRGBA, uiWidth, uiHeight, SourceFormat, vtfFormat.IMAGE_FORMAT_RGBA8888);
						break;
					}
				if (bResult)
					{
					// compress the source or convert it to the dest format if it is not compressed
					switch (DestFormat)
						{
						case vtfFormat.IMAGE_FORMAT_DXT1:
						case vtfFormat.IMAGE_FORMAT_DXT1_ONEBITALPHA:
						case vtfFormat.IMAGE_FORMAT_DXT3:
						case vtfFormat.IMAGE_FORMAT_DXT5:
							//bResult = CompressDXTn(SourceRGBA, ref Dest, uiWidth, uiHeight, DestFormat);
							break;
						default:
							bResult = Convert(SourceRGBA, ref Dest, uiWidth, uiHeight, vtfFormat.IMAGE_FORMAT_RGBA8888, DestFormat);
							break;
						}
					}
				return bResult;
				}
			else
				{

				}
			return false;
			}
			/*
	else
	{
		// convert from one variable order and bit format to another
		if(SourceInfo.uiBytesPerPixel <= 1)
		{
			if(DestInfo.uiBytesPerPixel <= 1)
				return ConvertTemplated<vlUInt8, vlUInt8>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 2)
				return ConvertTemplated<vlUInt8, vlUInt16>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 4)
				return ConvertTemplated<vlUInt8, vlUInt32>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 8)
				return ConvertTemplated<vlUInt8, vlUInt64>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
		}
		else if(SourceInfo.uiBytesPerPixel <= 2)
		{
			if(DestInfo.uiBytesPerPixel <= 1)
				return ConvertTemplated<vlUInt16, vlUInt8>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 2)
				return ConvertTemplated<vlUInt16, vlUInt16>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 4)
				return ConvertTemplated<vlUInt16, vlUInt32>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 8)
				return ConvertTemplated<vlUInt16, vlUInt64>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
		}
		else if(SourceInfo.uiBytesPerPixel <= 4)
		{
			if(DestInfo.uiBytesPerPixel <= 1)
				return ConvertTemplated<vlUInt32, vlUInt8>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 2)
				return ConvertTemplated<vlUInt32, vlUInt16>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 4)
				return ConvertTemplated<vlUInt32, vlUInt32>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 8)
				return ConvertTemplated<vlUInt32, vlUInt64>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
		}
		else if(SourceInfo.uiBytesPerPixel <= 8)
		{
			if(DestInfo.uiBytesPerPixel <= 1)
				return ConvertTemplated<vlUInt64, vlUInt8>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 2)
				return ConvertTemplated<vlUInt64, vlUInt16>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 4)
				return ConvertTemplated<vlUInt64, vlUInt32>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
			else if(DestInfo.uiBytesPerPixel <= 8)
				return ConvertTemplated<vlUInt64, vlUInt64>(lpSource, lpDest, uiWidth, uiHeight, SourceInfo, DestInfo);
		}
		return vlFalse;
	}

	return vlFalse;
}
			*/

		public static Bitmap Graph_ReadBGR888(byte[] buffer, int Width, int Height)
			{
			Bitmap Bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

			int x, y;

			byte a, r, g, b;

			BufferStream StmTmp = new BufferStream(buffer);

			for (y = 0; y < Height; y++)
				{
				for (x = 0; x < Width; x++)
					{
					a = 0xFF;
					b = StmTmp.ReadByte();
					g = StmTmp.ReadByte();
					r = StmTmp.ReadByte();
					Bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
					}
				}
			return Bmp;
			}
		public static Bitmap Graph_ReadBGRA8888(byte[] buffer, int Width, int Height)
			{
			Bitmap Bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

			int x, y;

			byte a, r, g, b;

			BufferStream StmTmp = new BufferStream(buffer);

			for (y = 0; y < Height; y++)
				{
				for (x = 0; x < Width; x++)
					{
					b = StmTmp.ReadByte();
					g = StmTmp.ReadByte();
					r = StmTmp.ReadByte();
					a = StmTmp.ReadByte();
					Bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
					}
				}
			return Bmp;
			}
		public static RGBAEntry Graph_Col565toRgb(int Color565)
			{
			int b, g, r;
			RGBAEntry rgbatmp = new RGBAEntry(0, 0, 0, 0xFF);
			b = Color565 & 0x1f;
			g = (Color565 & 0x7E0) >> 5;
			r = (Color565 & 0xF800) >> 11;

			rgbatmp.r = r << 3 | r >> 2;
			rgbatmp.g = g << 2 | g >> 3;
			rgbatmp.b = b << 3 | b >> 2;
			return rgbatmp;
			}		
		
		
		
		}

	}
