using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HLBox17b.Classes.Tools
	{
	class StreamTools
		{
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Lire une structure depuis un flux
		/// </summary>
		public static T ReadStruct<T>(FileStream fs, uint offsetpos)
			{
			int sizeofT = Marshal.SizeOf(typeof(T));
			byte[] buffer = new byte[sizeofT];
			if (fs != null)
				{
				fs.Seek(offsetpos, SeekOrigin.Begin);
				fs.Read(buffer, 0, sizeofT);
				}

			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			T temp = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();
			return temp;
			}

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Lire une structure depuis un flux à un emplacement spécifique
		/// </summary>
		public static T GetAt<T>(FileStream fs, int Idx, uint ZeroBasedIdx)
			{
			int sizeofT = Marshal.SizeOf(typeof(T));
			byte[] buffer = new byte[sizeofT];
			if (fs != null)
				{
				ZeroBasedIdx += (uint)Idx * (uint)sizeofT;
				fs.Seek(ZeroBasedIdx, SeekOrigin.Begin);
				fs.Read(buffer, 0, sizeofT);
				}
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			T temp = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();
			return temp;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///Lit un entier depuis le flux à l'offset actuel
		/// </summary>
		public static int ReadInt(FileStream fs)
			{
			byte[] bytes = { 0, 0 };
			fs.Read(bytes, 0, 2);

			int i = BitConverter.ToInt16(bytes, 0);
			return i;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///Lit un entier non signé depuis le flux à l'offset actuel
		/// </summary>
		public static uint ReadUInt(FileStream fs)
			{
			byte[] bytes = { 0, 0, 0, 0 };
			fs.Read(bytes, 0, 4);
			uint i = BitConverter.ToUInt32(bytes, 0);
			return i;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///Lit un char depuis le flux à l'offset actuel
		/// </summary>
		public static int ReadChar(FileStream fs)
			{
			int i = fs.ReadByte();
			return i;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///Lit des caractères nulls tant qu'ils apparaissent et rembobine d'un cran
		/// </summary>
		public static int ReadNullChars(FileStream fs)
			{
			int numNulls = 0;
			int b = 0;
			while (b == 0)
				{
				b = fs.ReadByte();
				if (b == 0)
					numNulls++;
				}
			fs.Seek(-1, SeekOrigin.Current);
			return numNulls;
			}
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///Lit une null terminated string depuis le flux à l'offset actuel
		/// </summary>
		public static string ReadNullTerminatedString(FileStream fs)
			{
			StringBuilder builder = new StringBuilder();
			char b;
			while (true)
				{
				b = (char)fs.ReadByte();
				if (b == 0)
					break;
				builder.Append(b);
				}
			return builder.ToString();
			}
		}
	}
