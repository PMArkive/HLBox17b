using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HLBox17b
	{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct RGBEntry
		{
		public byte cR;
		public byte cG;
		public byte cB;
		};

	[Serializable]
	public class TextureMipmaps
		{
		public byte[] Mipmap1;
		public byte[] Mipmap2;
		public byte[] Mipmap3;
		}

	public class Texture
		{
		public Bitmap Image;
		public uint uiWidth;
		public uint uiHeight;
		public string szName;
		public uint uiDiskLength;
		public TextureMipmaps Mipmaps;
		public byte[] rawImg;
		public byte[] rawPal;
		public int iLvwIdx;
		public int iImgIdx;
		public byte type;
		}

	public class VtfBmpInfo
		{
		public string szName;
		public string szPath;
		public int iImgIdx;
		public Bitmap Image;
		}


	[Serializable]
	public class SerialTexture
		{
		public byte[] Image;
		public uint uiWidth;
		public uint uiHeight;
		public string szName;
		public uint uiDiskLength;
		public TextureMipmaps Mipmaps;
		public byte[] rawImg;
		public byte[] rawPal;
		public int iLvwIdx;
		public int iImgIdx;
		public byte type;
		}

	public struct WADHeader
		{
		public char[] Id; //Must be 4 chars = WAD3
		public uint LumpCount;
		public uint LumpOffset;
		}

	public struct WADLump
		{
		public uint Offset;
		public uint CompressedLength;
		public uint FullLength;
		public byte Type;
		public byte Compression;
		public string Name;

		public override string ToString()
			{
			return Name;
			}
		}

	public struct CharInfo
		{
		public ushort StartOffset;
		public ushort CharWidth;

		public override string ToString()
			{
			return string.Format("Offset: {0:X} , Width: {1:X}", StartOffset, CharWidth);
			}
		}

	public class ZipDstFile
		{
		public string szName;
		public string szPath;
		}
	}
