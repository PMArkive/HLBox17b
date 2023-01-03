using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace HLBox17b
	{
	class ModName
		{
		private string _mod;
		private int _id;
		public string Mod { get { return _mod; } set { _mod = value; } }
		public int Id { get { return _id; } set { _id = value; } }
		public ModName(string name, int id)
			{
			_mod = name;
			_id = id;
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


	public class BufferStream
		{
		public MemoryStream stm;
		public BufferStream(byte[] vtfbuffer)
			{
			stm = new MemoryStream(vtfbuffer);
			}

		public virtual void Seek(int offset)
			{
			stm.Seek(offset, SeekOrigin.Begin);
			}

		public virtual long GetPos()
			{
			return stm.Position;
			}

		public virtual string ReadToEnd()
			{
			StreamReader reader = new StreamReader(stm);
			string text = reader.ReadToEnd();
			return text;
			}

		public virtual string ReadFixedString(int nLength)
			{
			byte[] byteTmp = new byte[nLength];

			stm.Read(byteTmp, 0, nLength);

			string text = Encoding.UTF8.GetString(byteTmp, 0, nLength);

			return text;
			}
		public virtual uint ReadUInt32()
			{
			int byteSize = 4;
			uint value = 0;

			byte[] byteTmp = new byte[byteSize];

			stm.Read(byteTmp, 0, byteSize);

			value = BitConverter.ToUInt32(byteTmp, 0);

			return value;
			}
		public virtual UInt16 ReadUInt16()
			{
			int byteSize = 2;
			UInt16 value = 0;

			byte[] byteTmp = new byte[byteSize];

			stm.Read(byteTmp, 0, byteSize);

			value = BitConverter.ToUInt16(byteTmp, 0);

			return value;
			}
		public virtual int ReadInt()
			{
			int byteSize = 4;
			int value = 0;

			byte[] byteTmp = new byte[byteSize];

			stm.Read(byteTmp, 0, byteSize);

			value = BitConverter.ToInt32(byteTmp, 0);

			return value;
			}
		public virtual byte[] ReadBytes(int nBytes)
			{
			byte[] byteTmp = new byte[nBytes];

			stm.Read(byteTmp, 0, nBytes);

			return byteTmp;
			}
		public virtual byte ReadByte()
			{
			byte[] byteTmp = new byte[1];
			stm.Read(byteTmp, 0, 1);
			return byteTmp[0];
			}
		public virtual float ReadFloat()
			{
			int byteSize = 4;
			float value = 0;

			byte[] byteTmp = new byte[byteSize];

			stm.Read(byteTmp, 0, byteSize);

			value = BitConverter.ToSingle(byteTmp, 0);

			return value;
			}
		}

	public class RGBAEntry
		{
		public int r;
		public int g;
		public int b;
		public int a;
		public RGBAEntry()
			{
			r = 0;
			g = 0;
			b = 0;
			a = 0xFF;
			}
		public RGBAEntry(int cr, int cg, int cb, int ca)
			{
			r = cr;
			g = cg;
			b = cb;
			a = ca;
			}
		}

	}
