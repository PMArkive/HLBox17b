using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
using SevenZip;
using FreeImageAPI;
using HLBox17b;
using HLBox17b.Classes.Tools;
using HLBox17b.Classes.Files;
using HLBox17b.Forms.Misc;
using HLBox17b.Externals;
using Tao.DevIl;


namespace HLBox17b.Forms.MdiForms
	{
	public partial class VtfMdi : MdiChild
		{
		public string szClipBrdStringId;
		public string szDragNDropGuid;
		public string szDefImgExportFormat;

		public VtfFileInfo vtfInfo;
		private Bitmap pictureBmp;
		private int ActMode;
		private float[] zoomlst;
		private int maxzoom;
		private int medzoom;
		private int actzoom;
		private int OldAnim_Mipmap;
		private int OldAnim_Face;
		private int OldAnim_Slice;
		private uint ActSpeed;
		private List<Bitmap> AniFrames;
		private int ActAniFrame;
		private bool FirstBrowse;
		private int defFolderIconIdx;
		private List<VtfBmpInfo> VirtualVtf;

		public VtfMdi(string szFileName)
			{
			szDefImgExportFormat = "*." + Program.mRc.szTxs_DefImgType;
			szClipBrdStringId = "HLBoxTextureInfoItem";
			szDragNDropGuid = System.Guid.NewGuid().ToString();
			bIsModified = false;
			bIsNew = false;
			szFullFile = szFileName;
			if (szFullFile==null || szFullFile==string.Empty)
				this.Close();
			zoomlst = new float[] { 0.10f, 0.20f, 0.30f, 0.40f, 0.50f, 0.60f, 0.70f, 0.80f, 0.90f, 1.00f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f};//, 8.5f, 9.0f, 9.5f, 10.0f, 12.5f, 15.0f, 20.0f, 25.0f, 50.0f };
			maxzoom = zoomlst.Length;
			medzoom = vtfmdi_FindMediumZoom(1.0f);
			actzoom = medzoom;

			vtfInfo = new VtfFileInfo();
			
			VirtualVtf = new List<VtfBmpInfo>();
			
			vtfmdi_CheckForVmtFile();

			pictureBmp = null;
			InitializeComponent();
			TranslateForm();

			AniFrames = null;
			ActAniFrame = -1;

			OldAnim_Mipmap = -1;
			OldAnim_Face = -1;
			OldAnim_Slice = -1;

			ActMode = 0;
			ActSpeed = 100;

			FirstBrowse = true;

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

		private void vtfmdi_CheckForVmtFile()
			{
			if (szFullFile == "")
				return;
			
			vtfInfo.onlyname = FilesTools.GetOnlyName(szFullFile);
			
			string szExt = FilesTools.GetExtension(szFullFile);
			if (szExt == "vmt")
				{
				vtfInfo.vmtfile = szFullFile;
				szFullFile = Path.Combine(Path.GetDirectoryName(szFullFile), vtfInfo.onlyname + ".vtf");
				}
			else
				{
				vtfInfo.vmtfile = Path.Combine(Path.GetDirectoryName(szFullFile), vtfInfo.onlyname + ".vmt");
				}

			FileInfo fi = new FileInfo(vtfInfo.vmtfile);
			if (fi.Exists)
				{
				vtfInfo.bvmexists = true;
				vtfInfo.vmtcontent = File.ReadAllText(vtfInfo.vmtfile);
				}
			else
				{
				vtfInfo.bvmexists = false;
				vtfInfo.vmtcontent = string.Empty;
				}
			}

		private void vtfmdi_OnShown(object sender, EventArgs e)
			{
			vtfmdi_ShowVtfFile();
			}

		private void vtfmdi_ShowVtfFile()
			{
			if (szFullFile == "")
				{
				this.Close();
				return;
				}

			FileInfo fi = new FileInfo(szFullFile);
			if (fi.Exists)
				{
				vtfInfo.filelength = fi.Length;
				}
			else
				{
				this.Close();
				return;
				}

			VTFFile vf = new VTFFile(szFullFile);

			if (vf.Open())
				{
				if (vf.ReadHeader())
					{
					vtfInfo.signature = vf.GetSignature();
					vtfInfo.version = vf.GetVersion();
					vtfInfo.width = vf.GetWidth();
					vtfInfo.height = vf.GetHeight();
					vtfInfo.flags = vf.GetFlags();
					vtfInfo.frames = vf.GetFrames();
					vtfInfo.faces = vf.GetFaces();
					vtfInfo.slices = vf.GetSlices();
					vtfInfo.firstFrame = vf.GetFirstFrame();
					vtfInfo.reflectivity1 = vf.GetReflectivity1();
					vtfInfo.reflectivity2 = vf.GetReflectivity2();
					vtfInfo.reflectivity3 = vf.GetReflectivity3();
					vtfInfo.bumpmapScale = vf.GetBumpScale();
					vtfInfo.highResImageFormat = vf.GetHighResImageFormat();
					vtfInfo.nummips = vf.GetNumMipmaps();
					vtfInfo.lowResImageFormat = vf.GetLowResImageFormat();
					vtfInfo.lowResImageWidth = vf.GetLowResImageWidth();
					vtfInfo.lowResImageHeight = vf.GetLowResImageHeigt();
					vtfInfo.depth = vf.GetImageDepth();
					vtfInfo.ResourceCount = vf.GetResourceCount();
					vtfInfo.BytesPerPixel = vf.GetBytesperPixel();
					vtfInfo.HighResFormatStr = vf.GetHihResFormatString();
					vtfInfo.LowResFormatStr = vf.GetLowResFormatString();
					}
				}
			vf.Close();


			vtfmdi_FillImgSel();
			vtfmdi_FillFlags();
			vtfmdi_FillInfos();

			ActAniFrame = vtfInfo.firstFrame;

			trackbar_Zoom.Minimum=0;
			trackbar_Zoom.Maximum = maxzoom-1;
			trackbar_Zoom.Value = medzoom;

			trackBar_Frame.Minimum = 0;
			trackBar_Frame.Maximum = vtfInfo.frames - 1;
			trackBar_Frame.Value = ActAniFrame;

			dt_ActFrame.Value = ActAniFrame;

			dt_ActMipmap.Value = 0;
			dt_ActFace.Value = 0;
			dt_ActSlice.Value = 0;

			tabControl.SelectedIndex = 0;
			tabContent.SelectedIndex = 0;
			timerAni_Stop();
			trackbar_Zoom.Enabled = true;
			vtfmdi_ChangeImage();


			/// Nouveau test à supprimer

//			vf.GetMainFramesDatas();
//			Texture TxInfo = vf.LoadFrame(0);
//			pictureBmp = TxInfo.Image;
//			pictureBox.Image = pictureBmp;
//			vtfmdi_CreateImage();
			CheckEditMenusStates();
			pictureBox.Focus();
			
			}

		private void vtfmdi_actframe_ValueChanged(object sender, EventArgs e)
			{
			vtfmdi_ChangeImage();
			}

		private void vtfmdi_actmipmap_ValueChanged(object sender, EventArgs e)
			{
			vtfmdi_ChangeImage();
			}

		private void vtfmdi_actface_ValueChanged(object sender, EventArgs e)
			{
			vtfmdi_ChangeImage();
			}

		private void vtfmdi_actslice_ValueChanged(object sender, EventArgs e)
			{
			vtfmdi_ChangeImage();
			}

		private void vtfmdi_ChangeImage()
			{
			if (timerAni.Enabled)
				{
				vtfmdi_GetAnimation();
				return;
				}
			if (bgLoader.IsBusy)
				{
				if (bgLoader.WorkerSupportsCancellation)
					{
					bgLoader.CancelAsync();
					}
				}
			if (bgLoader.IsBusy != true)
				{
				this.UseWaitCursor = true;
				Show_Loader(true, Program.appLng._i18n("common_loading"));
				List<object> args = new List<object>();
				args.Add((int)dt_ActMipmap.Value);
				args.Add((int)dt_ActFrame.Value);
				args.Add((int)dt_ActFace.Value);
				args.Add((int)dt_ActSlice.Value);
				bgLoader.RunWorkerAsync(args);
				}

			}
		private void BgLoader_DoWork(object sender, DoWorkEventArgs e)
			{
			BackgroundWorker worker = sender as BackgroundWorker;

			List<object> argsList = e.Argument as List<object>;
			int ActMipmap = (int) argsList[0];
			int ActFrame = (int) argsList[1];
			int ActFace = (int)argsList[2]; //unused
			int ActSlice = (int)argsList[3]; //unused
			//Init Tao
			Il.ilInit();

			// Create a DevIL image "name" (which is actually a number)
			int img_name;
			Il.ilGenImages(1, out img_name);
			Il.ilBindImage(img_name);

			//Load Image
			if (!Il.ilLoadImage(szFullFile))
				{
				if (worker.WorkerSupportsCancellation)
					worker.CancelAsync();
				return;
				}
			Il.ilActiveLayer(ActFace);
			Il.ilActiveImage(ActFrame);
			Il.ilActiveMipmap(ActMipmap);
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
			}

		private void BgLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (Percent < 100)
				Update_Loader(Percent, String.Format(Program.appLng._i18n("common_progression"),e.ProgressPercentage.ToString()));
			else
				Update_Loader(100, Program.appLng._i18n("common_parsing"));
			}

		private void BgLoader_Completed(object sender, RunWorkerCompletedEventArgs e)
			{
			this.UseWaitCursor = false;
			if (e.Error != null)
				{
				MessageBox.Show("Error: " + e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Show_Loader(false, null);
				this.Close();
				return;
				}
			Show_Loader(false, null);
			vtfmdi_CreateImage();
			CheckEditMenusStates();
			pictureBox.Focus();
			}


		private void menuitem_ClickViewNormal(object sender, EventArgs e)
			{
			ActMode = 0;
			if (timerAni.Enabled)
				timerAni_Stop();
			vtfmdi_CreateImage();
			CheckEditMenusStates();
			}

		private void menuitem_ClickViewTiled(object sender, EventArgs e)
			{
			ActMode = 1;
			if (timerAni.Enabled)
				timerAni_Stop();
			vtfmdi_CreateImage();
			CheckEditMenusStates();
			}

		private void menuitem_ClickViewAnimated(object sender, EventArgs e)
			{
			ActMode = 2;
			vtfmdi_CreateImage();
			CheckEditMenusStates();
			}

		private void vtfmdi_CreateImage()
			{
			switch (ActMode)
				{
				case 2:
					vtfmdi_GetAnimation();
					break;
				case 1:
					pictureBox.Image = vtfmdi_CreateTiledImage();
					vtfmdi_SetPictureSize();
					break;
				default:
					pictureBox.Image = new Bitmap(pictureBmp);
					vtfmdi_SetPictureSize();
					break;
				}
			}

		private void vtfmdi_GetAnimation()
			{
			int NewAnim_Mipmap = (int) dt_ActMipmap.Value;
			int NewAnim_Face = (int)dt_ActFace.Value;
			int NewAnim_Slice = (int)dt_ActSlice.Value;

			if (NewAnim_Mipmap == OldAnim_Mipmap && NewAnim_Face == OldAnim_Face && NewAnim_Slice == OldAnim_Slice)
				{
				if (AniFrames != null && AniFrames.Count != 0)
					{
					pictureBox.Width = (int)(AniFrames[ActAniFrame].Width * zoomlst[actzoom]);
					pictureBox.Height = (int)(AniFrames[ActAniFrame].Height * zoomlst[actzoom]);
					timerAni_Start();
					return;
					}
				}

			if (bgAnimator.IsBusy != true)
				{
				OldAnim_Mipmap = NewAnim_Mipmap;
				OldAnim_Face = NewAnim_Face;
				OldAnim_Slice = NewAnim_Slice;
				AniFrames = new List<Bitmap>();
				List<object> args = new List<object>();
				args.Add((int)NewAnim_Mipmap);
				args.Add((int)NewAnim_Face);
				args.Add((int)NewAnim_Slice);
				bgAnimator.RunWorkerAsync(args);
				}		
			}

		private void bganim_DoWork(object sender, DoWorkEventArgs e)
			{
			BackgroundWorker worker = sender as BackgroundWorker;

			List<object> argsList = e.Argument as List<object>;
			int ActMipmap = (int)argsList[0];
			int ActFace = (int)argsList[1];
			int ActSlice = (int)argsList[2];

			//Init Tao
			Il.ilInit();

			// Create a DevIL image "name" (which is actually a number)
			int img_name;
			Il.ilGenImages(1, out img_name);
			Il.ilBindImage(img_name);

			//Load Image
			if (!Il.ilLoadImage(szFullFile))
				{
				if (worker.WorkerSupportsCancellation)
					worker.CancelAsync();
				return;
				}

			AniFrames.Clear();

			// Set a few size variables that will simplify later code
			int ImgWidth = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
			int ImgHeight = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

			for (int i = 0; i < vtfInfo.frames; i++)
				{
				Il.ilBindImage(img_name);
				Il.ilActiveImage(i);
				Il.ilActiveMipmap(ActMipmap);
				Il.ilActiveLayer(ActFace);
				Rectangle rect = new Rectangle(0, 0, ImgWidth, ImgHeight);
				Il.ilConvertImage(Il.IL_BGRA, Il.IL_UNSIGNED_BYTE);
				Bitmap Bmp = new Bitmap(ImgWidth, ImgHeight);
				BitmapData bmd = Bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				Il.ilCopyPixels(0, 0, 0, ImgWidth, ImgHeight, 1, Il.IL_BGRA, Il.IL_UNSIGNED_BYTE, bmd.Scan0);
				Bmp.UnlockBits(bmd);
				AniFrames.Add(Bmp);
				}
			Il.ilDeleteImages(1, ref img_name);
			}

		private void bganim_Completed(object sender, RunWorkerCompletedEventArgs e)
			{
			if (AniFrames.Count != 0)
				{
				pictureBox.Width = (int)(AniFrames[ActAniFrame].Width * zoomlst[actzoom]);
				pictureBox.Height = (int)(AniFrames[ActAniFrame].Height * zoomlst[actzoom]);
				timerAni_Start();
				}
			else
				{
				pictureBox.Image = new Bitmap(pictureBmp);
				vtfmdi_SetPictureSize();
				timerAni_Stop();
				}
			}


		private void timerAni_Start()
			{
			timerAni.Stop();
			btn_PlayStopAnim.Image = global::HLBox17b.Properties.Resources.control_stop_square;
			dt_ActFrame.Enabled = false;
			dt_ActMipmap.Enabled = false;
			dt_ActFace.Enabled = false;
			dt_ActSlice.Enabled = false;
			timerAni.Enabled = true;
			timerAni.Interval = (int) dt_Speed.Value;
			timerAni.Start();
			}

		private void timerAni_Stop()
			{
			if (timerAni.Enabled)
				{
				timerAni.Enabled = false;
				timerAni.Stop();
				}
			btn_PlayStopAnim.Image = global::HLBox17b.Properties.Resources.control;
			CheckEditMenusStates();
			}

		private void timerAni_Tick(object sender, EventArgs e)
			{
			trackBar_Frame.Value = ActAniFrame;
			if (ActAniFrame < AniFrames.Count)
				pictureBox.Image = AniFrames[ActAniFrame];
			ActAniFrame++;
			if (ActAniFrame == vtfInfo.frames)
				ActAniFrame = 0;
			}

		private void trackbarframe_ValueChanged(object sender, EventArgs e)
			{
			if (timerAni.Enabled)
				return;
			}
		
		private void dtspeed_ChangeAnimationSpeed(object sender, EventArgs e)
			{
			if (!timerAni.Enabled)
				return;
			ActMode = 2;
			vtfmdi_CreateImage();
			CheckEditMenusStates();
			timerAni_Start();
			}

		private void btnplaystop_Click(object sender, EventArgs e)
			{
			if (timerAni.Enabled)
				{
				ActMode = 1;
				timerAni_Stop();
				}
			else
				{
				ActMode = 2;
				vtfmdi_CreateImage();
				CheckEditMenusStates();
				timerAni_Start();
				}
			}

		private Bitmap vtfmdi_CreateTiledImage()
			{
			int NumX = 3;
			int NumY = 3;

			int width = (int)pictureBmp.Width * NumX;
			int height = (int)pictureBmp.Height * NumY;
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(bmp);
			for (int i = 0; i < NumX; i++)
				{
				for (int j = 0; j < NumY; j++)
					{
					g.DrawImage(pictureBmp, new Point((int)(pictureBmp.Width * i), (int)(pictureBmp.Height * j)));
					}
				}
			return bmp;
			}


		#region check menu state

		private void CheckEditMenusStates()
			{
			if (pictureBmp != null && pictureBox.Image != null)
				{
				menuItem_Copy.Enabled = true;
				menuItem_Export.Enabled = true;
				}
			else
				{
				menuItem_Copy.Enabled = false;
				menuItem_Export.Enabled = false;
				}

			menuitem_ViewAnimated.Enabled = vtfInfo.frames > 1 ? true : false;
			ctxitem_ViewAnimated.Enabled = vtfInfo.frames > 1 ? true : false;
			dt_Speed.Enabled = vtfInfo.frames > 1 ? true : false;
			btn_PlayStopAnim.Enabled = vtfInfo.frames > 1 ? true : false;
			menuitem_ViewNormal.Checked = ActMode == 0 ? true : false;
			ctxitem_ViewNormal.Checked = ActMode == 0 ? true : false;
			menuitem_ViewTiled.Checked = ActMode == 1 ? true : false;
			ctxitem_ViewTiled.Checked = ActMode == 1 ? true : false;
			menuitem_ViewAnimated.Checked = ActMode == 2 ? true : false;
			ctxitem_ViewAnimated.Checked = ActMode == 2 ? true : false;
			dt_ActFrame.Enabled = vtfInfo.frames > 1 ? true : false;
			dt_ActMipmap.Enabled = vtfInfo.nummips > 1 ? true : false;
			dt_ActSlice.Enabled = vtfInfo.slices > 1 ? true : false;
			dt_ActFace.Enabled = vtfInfo.faces > 1 ? true : false;
			}

		#endregion

		#region fill tabs

		private void vtfmdi_FillImgSel()
			{
			//Frames
			if (vtfInfo.frames <= 1)
				{
				vtfInfo.frames = 1;
				dt_ActFrame.Enabled = false;
				lbl_FrameLimits.Text = string.Empty;
				}
			else
				{
				lbl_FrameLimits.Text = "0 - " + (vtfInfo.frames - 1).ToString();
				}
			dt_ActFrame.Minimum = 0;
			dt_ActFrame.Maximum = vtfInfo.frames - 1;
			//Mipmaps
			if (vtfInfo.nummips <= 1)
				{
				vtfInfo.nummips = 1;
				dt_ActMipmap.Enabled = false;
				lbl_MipmapsLimits.Text = string.Empty;
				}
			else
				{
				lbl_MipmapsLimits.Text = "0 - " + (vtfInfo.nummips - 1).ToString();
				}
			dt_ActMipmap.Minimum = 0;
			dt_ActMipmap.Maximum = vtfInfo.nummips-1;
			//Faces
			if (vtfInfo.faces <= 1)
				{
				vtfInfo.faces = 1;
				dt_ActFace.Enabled = false;
				lbl_FaceLimits.Text = string.Empty;
				}
			else
				{
				lbl_FaceLimits.Text = "0 - " + (vtfInfo.faces - 1).ToString();
				}
			dt_ActFace.Minimum = 0;
			dt_ActFace.Maximum = vtfInfo.faces - 1;
			//Slices
			if (vtfInfo.slices <= 1)
				{
				vtfInfo.slices = 1;
				dt_ActSlice.Enabled = false;
				lbl_SliceLimits.Text = string.Empty;
				}
			else
				{
				lbl_SliceLimits.Text = "0 - " + (vtfInfo.slices - 1).ToString();
				}
			dt_ActSlice.Minimum = 0;
			dt_ActSlice.Maximum = vtfInfo.slices - 1;


			dt_Speed.Minimum = 20;
			dt_Speed.Maximum = 1000;
			dt_Speed.Increment = 10;
			dt_Speed.Value = ActSpeed;
			}

		private void vtfmdi_FillInfos()
			{
			dt_Version.Text = vtfInfo.version;
			dt_Filesize.Text = FilesTools.formatSize(vtfInfo.filelength);
			dt_Frames.Text = vtfInfo.frames.ToString();
			dt_Startframe.Text = vtfInfo.firstFrame.ToString();
			dt_Faces.Text = vtfInfo.faces.ToString();
			dt_Slices.Text = vtfInfo.slices.ToString();
			dt_Mipmaps.Text = vtfInfo.nummips.ToString();
			dt_Bumpmap.Text = String.Format("{0:0.000}", vtfInfo.bumpmapScale);
			dt_Reflectivity.Text = String.Format("{0:0.000}/{0:0.000}/{0:0.000}", vtfInfo.reflectivity1,vtfInfo.reflectivity2,vtfInfo.reflectivity3);
			dt_DimsHigh.Text = String.Format("{0} x {0}", vtfInfo.width,vtfInfo.height);
			dt_FormatHigh.Text = vtfInfo.highResImageFormat.ToString() + " (" + vtfInfo.HighResFormatStr + ")";
			dt_DimsLow.Text = String.Format("{0} x {0}", vtfInfo.lowResImageWidth, vtfInfo.lowResImageHeight);
			dt_FormatLow.Text = vtfInfo.lowResImageFormat.ToString() + " (" + vtfInfo.LowResFormatStr + ")";
			dt_Depth.Text = vtfInfo.depth.ToString();
			dt_Bpp.Text = vtfInfo.BytesPerPixel.ToString();
			}
		private void vtfmdi_FillFlags()
			{
			string[] FlagsStr = new string[] {
				"Point Sample",
				"Trilinear",
				"Clamp S",
				"Clamp T",
				"Anisotropic",
				"Hint DXT5",
				"Pwl Corrected/SRGB/No Compress",
				"Normal Map",
				"No MipMaps",
				"No Level Of Detail",
				"No Minimum Mipmap",
				"Procedural",
				"One Bit Alpha",
				"Eight Bit Alpha",
				"Environment Map",
				"Render Target",
				"Depth Render Target",
				"No Debug Override",
				"Single Copy",
				"Pre SRGB/One Over Mipmap Level In Alpha",
				"Premultiply Color By One Over Mipmap Level",
				"Normal To DuDv",
				"Alpha Test Mipmap Generation",
				"No Depth Buffer",
				"Nice Filtered",
				"Clamp U",
				"Vertex Texture",
				"SSBump",
				"Border"
			};
			uint[] FlagsValues = new uint[] {
				0x00000001,
				0x00000002,
				0x00000004,
				0x00000008,
				0x00000010,
				0x00000020,
				0x00000040,
				0x00000080,
				0x00000100,
				0x00000200,
				0x00000400,
				0x00000800,
				0x00001000,
				0x00002000,
				0x00004000,
				0x00008000,
				0x00010000,
				0x00020000,
				0x00040000,
				0x00080000,
				0x00100000,
				0x00200000,
				0x00400000,
				0x00800000,
				0x01000000,
				0x02000000,
				0x04000000,
				0x08000000,
				0x20000000
			};
			listbox_Flags.Items.Clear();
			for (int i = 0; i < FlagsStr.LongLength; i++)
				{
				listbox_Flags.Items.Add(FlagsStr[i]);
				if ((vtfInfo.flags & FlagsValues[i]) !=0 )
					listbox_Flags.SetItemChecked(i, true);
				}
			}

		#endregion

		#region Loader

		private void Show_Loader(bool bState, string szInfo)
			{
			if (bState)
				{
				sb_Progress.Visible = true;
				sb_Infos.Visible = true;
				if (szInfo != null)
					sb_Infos.Text = szInfo;
				else
					sb_Infos.Text = string.Empty;
				sb_Zoom.Visible = false;
				}
			else
				{
				sb_Progress.Visible = false;
				sb_Infos.Visible = true;
				if (szInfo != null)
					sb_Infos.Text = szInfo;
				else
					sb_Infos.Text = string.Empty;
				sb_Zoom.Visible = true;
				}
			}

		private void Update_Loader(int Percent, string szInfo)
			{
			if (Percent > sb_Progress.Maximum)
				Percent = sb_Progress.Maximum;
			sb_Progress.Value = Percent;
			if (szInfo != null)
				sb_Infos.Text = szInfo;
			}

		#endregion

		#region Zoom

		private int vtfmdi_FindMediumZoom(float value)
			{
			int Idx = 0;
			foreach (float zm in zoomlst)
				{
				if (zm == value)
					return Idx;
				Idx++;
				}
			return -1;
			}

		private void vtfmdi_SetPictureSize()
			{
			actzoom = trackbar_Zoom.Value;
			if (pictureBox.Image != null)
				{
				pictureBox.Width = (int)(pictureBox.Image.Width * zoomlst[actzoom]);
				pictureBox.Height = (int)(pictureBox.Image.Height * zoomlst[actzoom]);
				}
			Update_ZoomTxt();
			pictureBox.Focus();
			}

		private void trackbarzoom_ValueChanged(object sender, EventArgs e)
			{
			vtfmdi_SetPictureSize();
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
				sb_Zoom.Text = sformatted;
				}
			}

		#endregion

		#region Menus_Clicks

		private void menuItem_Copy_Click(object sender, EventArgs e)
			{
			DoCopy();
			}


		private void menuItem_Export_Click(object sender, EventArgs e)
			{
			DoExport();
			}

		#endregion

		#region import_export

		private void vtfmdi_OnKeyDown(object sender, KeyEventArgs e)
			{
			switch (e.KeyCode)
				{
				case Keys.C:
					if (e.Control)
						DoCopy();
					break;
				default:
					return;
				}
			}


		private void DoCopy()
			{
			if (pictureBmp == null || pictureBox.Image == null)
				return;

			ClipBoardItemsList ClpList = new ClipBoardItemsList();

			Bitmap bmptmp = new Bitmap(pictureBox.Image);
			bool reserveLastClr = GraphTools.HasTranparency(bmptmp);
			Texture TxTmp = GraphTools.GetTxInfoFromBitmap(bmptmp, reserveLastClr);
			TxTmp.szName = vtfInfo.onlyname;
			bmptmp.Dispose();
			SerialTexture SrTxTmp = new SerialTexture();
			SrTxTmp.szName = TxTmp.szName;
			SrTxTmp.uiDiskLength = TxTmp.uiDiskLength;
			SrTxTmp.uiHeight = TxTmp.uiHeight;
			SrTxTmp.uiWidth = TxTmp.uiWidth;
			SrTxTmp.Image = GraphTools.ImageToByte(TxTmp.Image);
			SrTxTmp.rawImg = TxTmp.rawImg;
			SrTxTmp.Mipmaps = TxTmp.Mipmaps;
			SrTxTmp.rawPal = TxTmp.rawPal;
			SrTxTmp.iLvwIdx = TxTmp.iLvwIdx;
			SrTxTmp.iImgIdx = TxTmp.iImgIdx;
			ClpList.Add(SrTxTmp);
			Clipboard.SetData(szClipBrdStringId, ClpList);
			CheckEditMenusStates();			
			}
			

		private void DoExport()
			{
			SaveFileDialog saveImgFileDlg = new SaveFileDialog();
			saveImgFileDlg.Filter = Program.appLng._i18n("common_sav_imgfiles");
			saveImgFileDlg.Title = Program.appLng._i18n("common_savefile");
			saveImgFileDlg.AddExtension = true;
			saveImgFileDlg.CheckFileExists = false;
			saveImgFileDlg.CheckPathExists = true;
			saveImgFileDlg.ValidateNames = true;
			saveImgFileDlg.SupportMultiDottedExtensions = false;
			saveImgFileDlg.DereferenceLinks = true;
			saveImgFileDlg.Title = Program.appLng._i18n("common_savefile");
			saveImgFileDlg.FilterIndex = 1;
			saveImgFileDlg.DefaultExt = szDefImgExportFormat;

			saveImgFileDlg.InitialDirectory = Program.mRc.szTxs_LastExportPath;
			saveImgFileDlg.FileName = vtfInfo.onlyname;
			saveImgFileDlg.RestoreDirectory = true;

			if (saveImgFileDlg.ShowDialog() == DialogResult.OK)
				{
				Bitmap bmTexture = new Bitmap(pictureBox.Image);
				string szExt = Path.GetExtension(saveImgFileDlg.FileName).ToLower();
				Program.mRc.szTxs_LastExportPath = Path.GetDirectoryName(saveImgFileDlg.FileName);
				switch (szExt)
					{
					case ".tga":
						using (FreeImageBitmap frBmp = new FreeImageBitmap(bmTexture))
							{
							frBmp.Save(saveImgFileDlg.FileName, FREE_IMAGE_FORMAT.FIF_TARGA);
							}
						break;
					case ".bmp":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
						break;
					case ".png":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
						break;
					case ".gif":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Gif);
						break;
					case ".jpg":
					case ".jpeg":
						bmTexture.Save(saveImgFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
						break;
					default:
						MessageBox.Show(Program.appLng._i18n("waddlg_msg_bad_image_format"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					}
				bmTexture.Dispose();
				}
			}

		#endregion

		private void maintabs_ChangeTab(object sender, EventArgs e)
			{
			if (tabControl.SelectedIndex == 3)
				{
				tabContent.SelectedIndex = 2;
				timerAni_Stop();
				trackbar_Zoom.Enabled = false;
				if (FirstBrowse)
					vtfmdi_BrowserInit();
				dirsTreeView.Focus();
				if (dirsTreeView.SelectedNode != null)
					{
					 TreeNode selNode=dirsTreeView.SelectedNode;
					 selNode.EnsureVisible();
					}
				return;
				}
			tabContent.SelectedIndex = 0;
			trackbar_Zoom.Enabled = true;
			}


		public virtual int dirsTreeView_GetFolderImageIndex(string szFolder)
			{
			return (int) ShellAPI.GetFolderIconIndex(szFolder);
			}
		
		
		private void vtfmdi_BrowserInit()
			{
			int driveImage=0;
			TreeNode node = null;

			defFolderIconIdx = dirsTreeView_GetFolderImageIndex(Path.GetDirectoryName(Application.ExecutablePath));

			SystemImageList.SetTVImageList(dirsTreeView.Handle);

			//get a list of the drives
			string[] drives = Environment.GetLogicalDrives();
                
			foreach (string drive in drives)
				{
				DriveInfo di = new DriveInfo(drive);

				driveImage = dirsTreeView_GetFolderImageIndex(drive);

				node = new TreeNode(drive.Substring(0,2),driveImage,driveImage);
				node.Tag = drive;
				if (di.IsReady == true)
					node.Nodes.Add("*");
				dirsTreeView.Nodes.Add(node);
				}
			dirsTreeView.BeginUpdate();
			dirsTreeView_BrowseToDefault(Program.mRc.szVtf_LastBrowserPath);
			dirsTreeView.EndUpdate();
			dirsTreeView.Focus();
			FirstBrowse = false;
			}

		private void dirsTreeView_BrowseToDefault(string szdefault)
			{
			if (szdefault == string.Empty)
				return;
			if (!FilesTools.CheckDestinationPath(szdefault))
				return;
			List<string> parts = szdefault.Split(Path.DirectorySeparatorChar).ToList();
			string szfullpath = parts[0] + Path.DirectorySeparatorChar;
			TreeNode NodeTmp = null;
			foreach (TreeNode node in dirsTreeView.Nodes)
				{
				if ((string)node.Tag == szfullpath)
					NodeTmp = dirsTreeView_ExpandNode(node, parts, szfullpath);
				}

			if (NodeTmp == null)
				return;
			dirsTreeView.SelectedNode = NodeTmp;
			}

		private TreeNode dirsTreeView_ExpandNode(TreeNode node, List<string> path, string szfullpath)
			{
			path.RemoveAt(0);
			node.Expand();
			if (path.Count == 0)
				return node;
			szfullpath += path[0] + Path.DirectorySeparatorChar;
			foreach (TreeNode mynode in node.Nodes)
				{
				if ((string)mynode.Tag == (string)szfullpath)
					return dirsTreeView_ExpandNode(mynode, path, szfullpath);
				}
			return null;
			}


		private void dirsTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
			{
			if (e.Node.Nodes.Count > 0)
				{
				if (e.Node.Nodes[0].Text == "*" && e.Node.Nodes[0].Tag == null)
					{
					e.Node.Nodes.Clear();
					string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());
					foreach (string dir in dirs)
						{
						DirectoryInfo di = new DirectoryInfo(dir);
						TreeNode node = new TreeNode(di.Name, defFolderIconIdx, defFolderIconIdx);
						try
							{
							node.Tag = dir+ Path.DirectorySeparatorChar;
							if (di.GetDirectories().Count() > 0)
								node.Nodes.Add(null, "*", defFolderIconIdx, defFolderIconIdx);
							e.Node.Nodes.Add(node);
							}
						catch (UnauthorizedAccessException)
							{
							}
						catch (Exception ex)
							{
							MessageBox.Show(ex.Message, "FolderBrowser",MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						finally
							{
							}
						}
					}
				}
			}

		private void dirsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
			{
			string szActPath = ((string)(e.Node.Tag)).TrimEnd(Path.DirectorySeparatorChar);
			Program.mRc.szVtf_LastBrowserPath = szActPath;
			if (!bgBrowser.IsBusy)
				{
				this.UseWaitCursor = true;
				Show_Loader(true, Program.appLng._i18n("common_loading"));
				lvw_Browse.BeginUpdate();
				string[] vtffiles = Directory.GetFiles((string)(e.Node.Tag), "*.vtf");
				List<object> args = new List<object>();
				args.Add((string[])vtffiles);
				bgBrowser.RunWorkerAsync(args);
				}
			else
				{
				bgBrowser.CancelAsync();
				//return;
				}
			}

		private void bgbrowser_DoWork(object sender, DoWorkEventArgs e)
			{
			BackgroundWorker worker = sender as BackgroundWorker;

			List<object> argsList = e.Argument as List<object>;
			string[] vtffiles = (string[])argsList[0];

			int ProgMaxOps = (int)vtffiles.Length;

			double dTotal = (double)(ProgMaxOps);

			//Init Tao
			Il.ilInit();
			// Create a DevIL image "name" (which is actually a number)
			int img_name;
			Il.ilGenImages(1, out img_name);

			lvw_Browse.Clear();
			VirtualVtf.Clear();
			vtfImagesList.Images.Clear();
			lvw_Browse.VirtualListSize = 0;

			for (int nIdx = 0; nIdx < ProgMaxOps; nIdx++)
				{
				double dIndex = (double)(nIdx);
				double dProgressPercentage = (dIndex / dTotal);
				int iProgressPercentage = (int)(dProgressPercentage * 100);
				worker.ReportProgress(iProgressPercentage);
				Il.ilBindImage(img_name);
				if (!Il.ilLoadImage(vtffiles[nIdx]))
					continue;
				VtfBmpInfo vtfInfo = new VtfBmpInfo();
				vtfInfo.szName = FilesTools.GetOnlyName(vtffiles[nIdx]);
				vtfInfo.szPath = vtffiles[nIdx];
				vtfInfo.iImgIdx = -1;
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
				vtfInfo.Image = pictureBmp;
				VirtualVtf.Add(vtfInfo);
				}
			worker.ReportProgress(100, null);
			}

		private void bgbrowser_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
			{
			this.UseWaitCursor = false;
			Show_Loader(false, null);
			lvw_Browse.VirtualListSize = VirtualVtf.Count;
			lvw_Browse.EndUpdate();
			}

		private void bgbrowser_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
			int Percent = e.ProgressPercentage;
			if (Percent < 100)
				Update_Loader(Percent, String.Format(Program.appLng._i18n("common_progression"), e.ProgressPercentage.ToString()));
			else
				Update_Loader(100, null);
			}

		private void lvwbrowse_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
			{
			if (e.Item == null)
				{
				VtfBmpInfo VtfInfo = VirtualVtf[e.ItemIndex];
				ListViewItem Texitem = new ListViewItem();
				Texitem.Tag = VtfInfo.szPath;
				Texitem.Text = VtfInfo.szName;
				Texitem.ImageIndex = VtfInfo.iImgIdx;
				if (VtfInfo.iImgIdx == -1)
					{
					VirtualVtf[e.ItemIndex].iImgIdx = vtfImagesList.Images.Count;
					Texitem.ImageIndex = VirtualVtf[e.ItemIndex].iImgIdx;
					Bitmap Bmp = GraphTools.RescaleImage(VtfInfo.Image, vtfImagesList.ImageSize, lvw_Browse.BackColor);
					vtfImagesList.Images.Add(Bmp);
					}
				e.Item = Texitem;
				}
			}

		private void lvwbrowse_OnDoubleClick(object sender, MouseEventArgs e)
			{
			List<ListViewItem> SelectedItems = new List<ListViewItem>();
			for (int i = 0; i < this.lvw_Browse.SelectedIndices.Count; i++)
				{
				SelectedItems.Add((ListViewItem)lvw_Browse.Items[lvw_Browse.SelectedIndices[i]]);
				}

			if (SelectedItems.Count != 1)
				return;
			ListViewItem SelItem = SelectedItems[0];
			szFullFile = (string) SelItem.Tag;

			timerAni_Stop();
			AniFrames.Clear();
			ActMode = 0;
			vtfmdi_ShowVtfFile();
			}
		}

	public struct VtfFileInfo
		{
		public long filelength;
		public string signature;		
		public string version;
		public UInt16 width;			
		public UInt16 height;			
		public uint flags;			
		public UInt16 frames;			
		public UInt16 firstFrame;
		public UInt16 faces;
		public UInt16 slices;			
		public float reflectivity1;	
		public float reflectivity2;	
		public float reflectivity3;	
		public float bumpmapScale;		
		public uint highResImageFormat;
		public UInt16 nummips;		
		public int lowResImageFormat;
		public UInt16 lowResImageWidth;
		public UInt16 lowResImageHeight;
		public UInt16 depth;
		public int ResourceCount;
		public UInt16 BytesPerPixel;
		public string HighResFormatStr;
		public string LowResFormatStr;
		public string vmtfile;
		public bool bvmexists;
		public String vmtcontent;
		public string onlyname;
		}

	}

