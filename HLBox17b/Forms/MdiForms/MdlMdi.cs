using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
using HLBox17b;
using HLBox17b.Classes.Tools;
using HLBox17b.Classes.Files;
using HLBox17b.Forms.Misc;
using HLBox17b.Externals;
using Tao.DevIl;

namespace HLBox17b.Forms.MdiForms
	{
	public partial class MdlMdi : HLBox17b.Forms.MdiForms.MdiChild
		{
		private Bitmap pictureBmp;

		public MdlMdi(string szFileName)
			{
			szFullFile = szFileName;
			if (szFullFile == null || szFullFile == string.Empty)
				this.Close();
			pictureBmp = null;
			InitializeComponent();
			TranslateForm();
			mdichild_UpdateTitle();
			CheckEditMenusStates();
			statBar.Visible = true;
			Show_Loader(false, null);
			}

		private void TranslateForm()
			{

			}

		private void vtfmdi_FormClosing(object sender, FormClosingEventArgs e)
			{
			if (pictureBmp != null)
				pictureBmp.Dispose();
			}
		private void CheckEditMenusStates()
			{
			}
		private void Show_Loader(bool bState, string szInfo)
			{
			if (bState)
				{
				}
			else
				{
	
				}
			}
		private void mdlmdi_OnShown(object sender, EventArgs e)
			{
			mdlmdi_ShowMdlFile();
			}

		private void mdlmdi_ShowMdlFile()
			{
			if (szFullFile == "")
				{
				this.Close();
				return;
				}

			FileInfo fi = new FileInfo(szFullFile);
			if (!fi.Exists)
				{
				this.Close();
				return;
				}

			//Init Tao
			Il.ilInit();

			// Create a DevIL image "name" (which is actually a number)
			int img_name;
			Il.ilGenImages(1, out img_name);
			Il.ilBindImage(img_name);

			//Load Image
			if (!Il.ilLoadImage(szFullFile))
				{
				this.Close();
				return;
				}
			Il.ilActiveLayer(0);
			Il.ilActiveImage(0);
			Il.ilActiveMipmap(0);
			// Set a few size variables that will simplify later code
			int ImgWidth = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
			int ImgHeight = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
			Rectangle rect = new Rectangle(0, 0, ImgWidth, ImgHeight);
			// Convert the DevIL image to a pixel byte array to copy into Bitmap
			Il.ilConvertImage(Il.IL_BGRA, Il.IL_UNSIGNED_BYTE);
			// Create a Bitmap to copy the image into, and prepare it to get data
			pictureBmp = new Bitmap(ImgWidth, ImgHeight);
			BitmapData bmd = pictureBmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			// Copy the pixel byte array from the DevIL image to the Bitmap
			Il.ilCopyPixels(0, 0, 0, Il.ilGetInteger(Il.IL_IMAGE_WIDTH), Il.ilGetInteger(Il.IL_IMAGE_HEIGHT), 1, Il.IL_BGRA, Il.IL_UNSIGNED_BYTE, bmd.Scan0);
			// Clean up and return Bitmap
			Il.ilDeleteImages(1, ref img_name);
			pictureBmp.UnlockBits(bmd);
			pictureBox.Image = pictureBmp;
			}
		}
	}
