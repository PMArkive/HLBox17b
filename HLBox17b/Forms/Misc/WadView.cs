using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using HLBox17b.Classes.Tools;
using HLBox17b.Classes.Files;
using HLBox17b.Externals;
using FreeImageAPI;


namespace HLBox17b
	{

	public partial class WadView : Form
		{
		private List<Texture> ArrTexInfo;
		private Texture TexInfo;
		private int StartImg;
		private int MainScreenW;
		private int MainScreenH;
		private float[] zoomlst;
		private int maxzoom;
		private int medzoom;
		private int actzoom;
		private bool Firstime;
		private int inizoom;
		private int actmode;
		private Bitmap[] AniFrames;
		private int ActAniFrame;
		private const int MaxPaletteColors = 256;
		
		public WadView(List<Texture> TxInfo, int aImg)
			{
			ArrTexInfo = TxInfo;
			StartImg = aImg;
			TexInfo = ArrTexInfo[StartImg];
			GetScreenDimensions();
			zoomlst = new float[]{0.10f,0.20f,0.30f,0.40f,0.50f,0.60f,0.70f,0.80f,0.90f,1.00f,1.5f,2.0f,2.5f,3.0f,3.5f,4.0f,4.5f,5.0f,5.5f,6.0f,6.5f,7.0f,7.5f,8.0f,8.5f,9.0f,9.5f,10.0f,12.5f,15.0f,20.0f,25.0f,50.0f};
			maxzoom = zoomlst.Length;
			medzoom = FindMediumZoom(1.0f);
			actzoom = medzoom;
			GetInitialScale();
			inizoom = actzoom;
			InitializeComponent();
			Firstime = true;
			actmode = 0;
			AniFrames = null;
			ActAniFrame = 0;
			menuItem_Export.Enabled = ArrTexInfo.Count > 1 ? true : false;
			menuItem_ViewNormal.Checked = true;
			menuItem_ViewMipmaps.Checked = false;
			menuItem_ViewTiled.Checked = false;
			menuItem_ViewAnimated.Checked = false;
			if (ArrTexInfo.Count != 1)
				menuItem_ViewAnimated.Enabled = true;
			else
				menuItem_ViewAnimated.Enabled = false;
			}

		private void TranslateForm()
			{
			menuItem_File.Text = Program.appLng._i18n("menu_file");
			menuItem_Close.Text = Program.appLng._i18n("menu_file_close");
			menuItem_Export.Text = Program.appLng._i18n("waddlg_menu_exportanimation");
			menuItem_View.Text = Program.appLng._i18n("waddlg_menu_view");
			menuItem_ViewNormal.Text = Program.appLng._i18n("waddlg_menu_viewnormal");
			menuItem_ViewMipmaps.Text = Program.appLng._i18n("waddlg_menu_viewmipmaps");
			menuItem_ViewTiled.Text = Program.appLng._i18n("waddlg_menu_viewtiled");
			menuItem_ViewAnimated.Text = Program.appLng._i18n("waddlg_menu_viewanimated");
			}

		private int FindMediumZoom(float value)
			{
			int Idx=0;
			foreach (float zm in zoomlst)
				{
				if (zm == value)
					return Idx;
				Idx++;
				}
			return -1;
			}

		private void GetScreenDimensions()
			{
			MainScreenW = (int) Screen.PrimaryScreen.Bounds.Width;
			MainScreenH = (int) Screen.PrimaryScreen.Bounds.Height;
			}

		private void GetInitialScale()
			{
			int zoomx = medzoom;
			int zoomy = medzoom;
			float newSizeW = TexInfo.uiWidth;
			float newSizeH = TexInfo.uiHeight;
			float MaxW = ((float) MainScreenW * 0.7f);
			float MaxH = ((float) MainScreenH * 0.7f);
			//Calcul de l'échelle en X
			if (newSizeW < MaxW)
				{
				while (newSizeW < MaxW && zoomx<maxzoom-1)
					{
					zoomx++;
					newSizeW = TexInfo.uiWidth * zoomlst[zoomx];
					}
				zoomx--;
				}
			else
				{
				while (newSizeW > MaxW && zoomx > 0)
					{
					zoomx--;
					newSizeW = TexInfo.uiWidth * zoomlst[zoomx];
					}
				zoomx++;
				}
			//Calcul de l'échelle en Y
			if (newSizeH < MaxH)
				{
				while (newSizeH < MaxH && zoomy < maxzoom - 1)
					{
					zoomy++;
					newSizeH = TexInfo.uiHeight * zoomlst[zoomy];
					}
				zoomy--;
				}
			else
				{
				while (newSizeH > MaxH && zoomy > 0)
					{
					zoomy--;
					newSizeH = TexInfo.uiHeight * zoomlst[zoomy];
					}
				zoomy++;
				}
			actzoom = Math.Min(zoomx, zoomy);
			}

		private void OnLoad(object sender, EventArgs e)
			{
			this.Text = TexInfo.szName;
			toolStripColor.Text = string.Empty;
			toolStripSize.Text = TexInfo.uiWidth.ToString() + " x " + TexInfo.uiHeight.ToString();
			Update_ZoomTxt();
			TranslateForm();
			pictureBox.Image = (Image)TexInfo.Image;
			pictureBox.Width = (int)(TexInfo.uiWidth * zoomlst[actzoom]);
			pictureBox.Height = (int)(TexInfo.uiHeight * zoomlst[actzoom]);
			SetMainFormDimensions();
			SetMainFormPosition();
			}

		private Bitmap CreateMipmapedImage()
			{
			int marginX = 5;
			int marginY = 5;

			if (TexInfo.uiWidth <= 16)
				marginX = 2;
			if (TexInfo.uiHeight <= 16)
				marginY = 2;
			//Mipmap0
			int mipW = (int)TexInfo.uiWidth;
			int mipH = (int)TexInfo.uiHeight;
			Bitmap m0 = new Bitmap(mipW, mipH, PixelFormat.Format8bppIndexed);
			BitmapData bmd0 = m0.LockBits(new Rectangle(0, 0, mipW, mipH), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			System.Runtime.InteropServices.Marshal.Copy(TexInfo.rawImg, 0, bmd0.Scan0, TexInfo.rawImg.Length);
			m0.UnlockBits(bmd0);
			
			//Creation palette
			ColorPalette palBmp = m0.Palette;
			for (int i = 0, j = 0; i < MaxPaletteColors; i++)
				{
				palBmp.Entries[i] = Color.FromArgb(255,TexInfo.rawPal[j], TexInfo.rawPal[j + 1], TexInfo.rawPal[j + 2]);
				j += 3;
				}
			m0.Palette = palBmp;
			//Mipmap1
			mipW /= 2;
			mipH /= 2;
			Bitmap m1 = new Bitmap(mipW, mipH, PixelFormat.Format8bppIndexed);
			BitmapData bmd1 = m1.LockBits(new Rectangle(0, 0, mipW, mipH), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			System.Runtime.InteropServices.Marshal.Copy(TexInfo.Mipmaps.Mipmap1, 0, bmd1.Scan0, TexInfo.Mipmaps.Mipmap1.Length);
			m1.UnlockBits(bmd1);
			m1.Palette = palBmp;
			//Mipmap2
			mipW /= 2;
			mipH /= 2;
			Bitmap m2 = new Bitmap(mipW, mipH, PixelFormat.Format8bppIndexed);
			BitmapData bmd2 = m2.LockBits(new Rectangle(0, 0, mipW, mipH), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			System.Runtime.InteropServices.Marshal.Copy(TexInfo.Mipmaps.Mipmap2, 0, bmd2.Scan0, TexInfo.Mipmaps.Mipmap2.Length);
			m2.UnlockBits(bmd2);
			m2.Palette = palBmp;
			//Mipmap3
			mipW /= 2;
			mipH /= 2;
			Bitmap m3 = new Bitmap(mipW, mipH, PixelFormat.Format8bppIndexed);
			BitmapData bmd3 = m3.LockBits(new Rectangle(0, 0, mipW, mipH), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			int bytesperpixel = Image.GetPixelFormatSize(m3.PixelFormat);
			int stride = GraphTools.RowStride(bytesperpixel, m3.Width);
			int pad = m3.Width - stride;
			if (pad == 0)
				{
				System.Runtime.InteropServices.Marshal.Copy(TexInfo.Mipmaps.Mipmap3, 0, bmd3.Scan0, TexInfo.Mipmaps.Mipmap3.Length);
				}
			else
				{
				byte[] arrMM = new byte[stride * mipH];
				for (int row=0; row < mipH ; row++)
					{
					Array.Copy(TexInfo.Mipmaps.Mipmap3, (row * mipW), arrMM, row * stride, mipW);
					}
				System.Runtime.InteropServices.Marshal.Copy(arrMM, 0, bmd3.Scan0, arrMM.Length);
				}
			m3.UnlockBits(bmd3);
			m3.Palette = palBmp;
			//Creation Bitmap principal
			int width = (int)TexInfo.uiWidth + (int)(TexInfo.uiWidth / 2) + marginX;
			int height = (int)TexInfo.uiHeight;
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImage(m0, new Point(0, 0));
			g.DrawImage(m1, new Point((int)TexInfo.uiWidth + marginX, 0));
			g.DrawImage(m2, new Point((int)TexInfo.uiWidth + marginX, m1.Height + marginY));
			g.DrawImage(m3, new Point((int)width - m3.Width-1, m1.Height + marginY));
			m0.Dispose();
			m1.Dispose();
			m2.Dispose();
			m3.Dispose();
			return bmp;
			}

		private Bitmap CreateTiledImage()
			{
			int MaxX = panel1.Width;
			int MaxY = panel1.Height;
			
			int NumX = 0;
			int NumY = 0;

			int TotalX = (int) ((float) (TexInfo.uiWidth * NumX)*zoomlst[actzoom] );
			while (TotalX < MaxX)
				{
				NumX++;
				TotalX = (int)((float)(TexInfo.uiWidth * NumX) * zoomlst[actzoom]);
				}

			int TotalY = (int)((float)(TexInfo.uiHeight * NumY) * zoomlst[actzoom]);
			while (TotalY < MaxY)
				{
				NumY++;
				TotalY = (int)((float)(TexInfo.uiHeight * NumY) * zoomlst[actzoom]);
				}

			int width = (int)TexInfo.uiWidth * NumX;
			int height = (int)TexInfo.uiHeight * NumY;
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(bmp);
			for (int i = 0; i < NumX; i++)
				{
				for (int j = 0; j < NumY; j++)
					{
					g.DrawImage(TexInfo.Image, new Point((int)(TexInfo.uiWidth * i), (int)(TexInfo.uiHeight * j)));
					}
				}
			return bmp;
			}

		private void SetMainFormDimensions()
			{
			int BorderWidth = (this.Width-this.ClientSize.Width) /2;
			int TitlebarHeight = (this.Height-this.ClientSize.Height) - 2 * BorderWidth;
			this.Width = (int)(TexInfo.uiWidth * zoomlst[actzoom]) + BorderWidth * 2;
			int StatWidth = toolStripColor.Width + toolStripSize.Width + toolStripZoom.Width + BorderWidth*2 + 15;
			if (this.Width < StatWidth)
				this.Width = StatWidth;
			this.Height = (int)(TexInfo.uiHeight * zoomlst[actzoom]) + TitlebarHeight + BorderWidth * 2 + menuStrip1.Height + statusStrip1.Height + 1;
			}

		private void SetMainFormPosition()
			{
			this.Top = (int) (MainScreenH - this.Height) / 2;
			this.Left = (int) (MainScreenW - this.Width)/2;
			}

		protected override void OnMouseWheel(MouseEventArgs mea)
			{
			if (pictureBox.Image != null)
				{
				if (mea.Delta > 0)
					{
					if (actzoom < maxzoom - 1)
						{
						actzoom++;
						if (actmode == 2)
							{
							Bitmap Bmp = null;
							Bmp = CreateTiledImage();
							pictureBox.Width = Bmp.Width;
							pictureBox.Height = Bmp.Height;
							pictureBox.Image = Bmp;
							}
						else
							{
							if ((int)(pictureBox.Image.Width * zoomlst[actzoom]) > 15 * this.Width || (int)(pictureBox.Image.Height * zoomlst[actzoom]) > 15 * this.Height)
								actzoom--;
							}
						}
					Update_ZoomTxt();
					}
				else
					{
					if (Firstime && actmode == 1)
						{
						Bitmap Bmp = null;
						Bmp = CreateMipmapedImage();
						pictureBox.Width = Bmp.Width;
						pictureBox.Height = Bmp.Height;
						pictureBox.Image = Bmp;
						Firstime = false;
						}
					if (actzoom > 0)
						{
						actzoom--;
						if (actmode == 2)
							{
							Bitmap Bmp = null;
							Bmp = CreateTiledImage();
							pictureBox.Width = Bmp.Width;
							pictureBox.Height = Bmp.Height;
							pictureBox.Image = Bmp;
							}
						else
							{
							if ((int)(pictureBox.Image.Width * zoomlst[actzoom]) < 2 || (int)(pictureBox.Image.Height * zoomlst[actzoom]) < 2)
								actzoom++;
							}
						}
					Update_ZoomTxt();
					}
				if (actzoom == inizoom)
					panel1.AutoScroll = false;
				else
					panel1.AutoScroll = true;
				pictureBox.Width = (int)(pictureBox.Image.Width * zoomlst[actzoom]);
				pictureBox.Height = (int)(pictureBox.Image.Height * zoomlst[actzoom]);
				}
			}

		private void Update_ZoomTxt()
			{
			if (this.InvokeRequired)
				{
				Invoke(new MethodInvoker(delegate() { Update_ZoomTxt(); }));
				}
			else
				{
				var sformatted = String.Format("{0:##\\%}", zoomlst[actzoom] * 100);
				toolStripZoom.Text = sformatted;
				}
			}

		private void menustrip_ViewNormalClick(object sender, EventArgs e)
			{
			this.Text = TexInfo.szName;
			if (timerAni.Enabled)
				{
				timerAni.Enabled = false;
				timerAni.Stop();
				}
			menuItem_ViewNormal.Checked = true;
			menuItem_ViewMipmaps.Checked = false;
			menuItem_ViewTiled.Checked = false;
			menuItem_ViewAnimated.Checked = false;
			actmode = 0;
			actzoom = inizoom;
			pictureBox.Width = (int)(TexInfo.uiWidth * zoomlst[actzoom]);
			pictureBox.Height = (int)(TexInfo.uiHeight * zoomlst[actzoom]);
			pictureBox.Image = (Image)TexInfo.Image;
			Firstime = true;
			Update_ZoomTxt();
			}

		private void menustrip_ViewMipmapsClick(object sender, EventArgs e)
			{
			this.Text = TexInfo.szName;
			if (timerAni.Enabled)
				{
				timerAni.Enabled = false;
				timerAni.Stop();
				}
			menuItem_ViewMipmaps.Checked = true;
			menuItem_ViewNormal.Checked = false;
			menuItem_ViewTiled.Checked = false;
			menuItem_ViewAnimated.Checked = false;
			actmode = 1;
			actzoom = inizoom;
			pictureBox.Width = (int)(TexInfo.uiWidth * zoomlst[actzoom]);
			pictureBox.Height = (int)(TexInfo.uiHeight * zoomlst[actzoom]);
			pictureBox.Image = (Image)TexInfo.Image;
			Firstime = true;
			Update_ZoomTxt();
			}

		private void menustrip_ViewTiledClick(object sender, EventArgs e)
			{
			this.Text = TexInfo.szName;
			if (timerAni.Enabled)
				{
				timerAni.Enabled = false;
				timerAni.Stop();
				}
			menuItem_ViewNormal.Checked = false;
			menuItem_ViewMipmaps.Checked = false;
			menuItem_ViewTiled.Checked = true;
			menuItem_ViewAnimated.Checked = false;
			actmode = 2;
			actzoom = inizoom;
			pictureBox.Width = (int)(TexInfo.uiWidth * zoomlst[actzoom]);
			pictureBox.Height = (int)(TexInfo.uiHeight * zoomlst[actzoom]);
			pictureBox.Image = (Image)TexInfo.Image;
			Firstime = true;
			Update_ZoomTxt();
			}

		private void menuStrip_ViewAnimatedClick(object sender, EventArgs e)
			{
			menuItem_ViewAnimated.Checked = true;
			menuItem_ViewNormal.Checked = false;
			menuItem_ViewMipmaps.Checked = false;
			menuItem_ViewTiled.Checked = false;
			actmode = 3;
			actzoom = inizoom;
			if (bgWork.IsBusy != true)
				{
				bgWork.RunWorkerAsync();
				}			
			Firstime = true;
			Update_ZoomTxt();
			}

		private void bgWorkStart(object sender, DoWorkEventArgs e)
			{
			if (AniFrames == null)
				{
				AniFrames = new Bitmap[ArrTexInfo.Count];
				for (int i = 0; i < ArrTexInfo.Count; i++)
					{
					AniFrames[i] = ArrTexInfo[i].Image;
					}
				}
			}
		
		private void bgWorkEnd(object sender, RunWorkerCompletedEventArgs e)
			{
			if (AniFrames != null)
				{
				string szOnlyName = TexInfo.szName;
				string pat = @"\+\d(?<Onlytxt>.*)";
				Regex r = new Regex(pat, RegexOptions.IgnoreCase);
				Match m = r.Match(ArrTexInfo[0].szName);
				if (m.Success)
					szOnlyName = m.Groups["Onlytxt"].Value;
				this.Text = szOnlyName + " - (" + Program.appLng._i18n("waddlg_txt_animation") + ")";

				pictureBox.Width = (int)(AniFrames[0].Width * zoomlst[actzoom]);
				pictureBox.Height = (int)(AniFrames[0].Height * zoomlst[actzoom]);
				ActAniFrame = 0;
				timerAni.Enabled = true;
				timerAni.Start();
				}
			else
				{
				this.Text = TexInfo.szName;
				pictureBox.Width = (int)(TexInfo.uiWidth * zoomlst[actzoom]);
				pictureBox.Height = (int)(TexInfo.uiHeight * zoomlst[actzoom]);
				pictureBox.Image = (Image)TexInfo.Image;
				timerAni.Enabled = false;
				timerAni.Stop();
				}
			}

		private void timerAni_Tick(object sender, EventArgs e)
			{
			pictureBox.Image = AniFrames[ActAniFrame];
			ActAniFrame++;
			if (ActAniFrame == AniFrames.Length)
				ActAniFrame = 0;
			}

		private void menuItem_Close_OnClick(object sender, EventArgs e)
			{
			this.Close();
			}

		private void menuItem_Export_OnClick(object sender, EventArgs e)
			{
			//Nom de la texture
			string szOnlyName = "hlbexport.gif";
			string pat = @"\+\d(?<Onlytxt>.*)";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase);
			Match m = r.Match(ArrTexInfo[0].szName);
			if (m.Success)
				szOnlyName = m.Groups["Onlytxt"].Value+".gif";
			saveFileDlg.DefaultExt = ".gif";
			saveFileDlg.FileName = szOnlyName;
			saveFileDlg.Filter = Program.appLng._i18n("common_giffiles");
			saveFileDlg.FilterIndex = 1;
			saveFileDlg.InitialDirectory = Program.mRc.szTxs_LastExportPath == string.Empty ? System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Program.mRc.szTxs_LastExportPath;
			saveFileDlg.Title = Program.appLng._i18n("common_savefile");
			//Arrête l'animation en cours
			bool mustRestart = false;
			if (timerAni.Enabled)
				{
				timerAni.Enabled = false;
				timerAni.Stop();
				mustRestart = true;
				}
			saveFileDlg.ShowDialog();
			//Redémarre l'animation si nécessaire
			if (mustRestart)
				{
				if (bgWork.IsBusy != true)
					bgWork.RunWorkerAsync();
				}
			}

		private void wadview_ExportAnimated(object sender, CancelEventArgs e)
			{
			string name = saveFileDlg.FileName;
			Program.mRc.szTxs_LastExportPath = Path.GetDirectoryName(name);

			AnimatedGifEncoder genc = new AnimatedGifEncoder();
			genc.Start(name);
			genc.SetDelay(500);
			genc.SetRepeat(0);
			for (int i = 0, count = ArrTexInfo.Count; i < count; i++)
				{
				genc.AddFrame(ArrTexInfo[i].Image);
				}
			genc.Finish();
			}

		private void picturebox_OnMouseMove(object sender, MouseEventArgs e)
			{
			Bitmap bmp = new Bitmap(pictureBox.Image);

			int cX = (int) (e.Location.X / zoomlst[actzoom]);
			int cY = (int)(e.Location.Y / zoomlst[actzoom]);
			if (cX > pictureBox.Image.Width)
				cX = pictureBox.Image.Width;
			if (cY > pictureBox.Image.Height)
				cY = pictureBox.Image.Height;
			Color color = bmp.GetPixel(cX, cY);
			toolStripColor.Text = cX.ToString("000") + "," + cY.ToString("000") + ": " + "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
			}

		}
	}
