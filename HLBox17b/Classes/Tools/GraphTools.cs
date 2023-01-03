using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using FreeImageAPI;
using System.Windows.Forms;
using System.ComponentModel;
using Tao.DevIl;

namespace HLBox17b.Classes.Tools
	{
	class GraphTools
		{
		private const int MaxPaletteColors = 256;
		/////////////////////////////////////////////////////////////////////
		/// Retourner un Bitmap sous forme de byte et retourne sous un format special
		/////////////////////////////////////////////////////////////////////
		public static byte[] ImageToByte(Image img)
			{
			return ImageToByte(img, ImageFormat.Png);
			}
		/////////////////////////////////////////////////////////////////////
		/// Retourner un Bitmap sous forme de byte
		/////////////////////////////////////////////////////////////////////
		public static byte[] ImageToByte(Image img, ImageFormat newFormat)
			{
			byte[] byteArray = new byte[0];
			using (MemoryStream stream = new MemoryStream())
				{
				img.Save(stream, newFormat);
				stream.Close();
				byteArray = stream.ToArray();
				}
			return byteArray;
			}
		/////////////////////////////////////////////////////////////////////
		/// Retourner un byte Array en Bitmap
		/////////////////////////////////////////////////////////////////////
		public static Bitmap ByteToImage(byte[] byteArray)
			{
			MemoryStream ms = new MemoryStream(byteArray);
			Image returnImage = Image.FromStream(ms);
			Bitmap bmp = new Bitmap(returnImage);
			return bmp;
			}
		/////////////////////////////////////////////////////////////////////
		/// Retourner si une image possède de la transparence
		/////////////////////////////////////////////////////////////////////
		public static bool HasTranparency(Bitmap bmp)
			{
			for (int row = 0; row < bmp.Height; row++)
				{
				for (int col = 0; col < bmp.Width; col++)
					{
					Color clr = bmp.GetPixel(col, row);
					if (clr.A == 0)
						return true;
					}
				}
			return false;
			}
		/////////////////////////////////////////////////////////////////////
		/// Retourner Texture Info from Bitmap
		/////////////////////////////////////////////////////////////////////
		public static Texture GetTxInfoFromBitmap(Bitmap Itembmp, bool reserveLastClr)
			{
			int MaxPaletteColors = 256;
			bool bBadSize = false;
			Texture TxInfo = new Texture();


			uint imgWidth = (uint)Itembmp.Width;
			uint imgHeight = (uint)Itembmp.Height;

			if (imgWidth < 16 || imgHeight < 16)
				bBadSize = true;
			if (imgWidth > 512 || imgHeight > 512)
				bBadSize = true;
			if ((imgWidth % 16) != 0 || (imgHeight % 16) != 0)
				bBadSize = true;
			if (bBadSize)
				{
				TxInfo.szName = "##!!##";
				TxInfo.uiWidth = imgWidth;
				TxInfo.uiHeight = imgHeight;
				return TxInfo;
				}

			Bitmap myNewPng = GetConvertedImage(Itembmp, ImageFormat.Png);
			FreeImageBitmap mainImage = new FreeImageBitmap(myNewPng);
			if (mainImage.IsTransparent)
				{
				reserveLastClr = true;
				for (int row = 0; row < mainImage.Height; row++)
					{
					for (int col = 0; col < mainImage.Width; col++)
						{
						Color clr = mainImage.GetPixel(col, row);
						if (clr.A == 0)
							mainImage.SetPixel(col, row, Color.FromArgb(255, 0, 0, 255));
						}
					}
				}
			if (!mainImage.HasPalette)
				{
				if (mainImage.ImageType != FREE_IMAGE_TYPE.FIT_BITMAP)
					mainImage.ConvertType(FREE_IMAGE_TYPE.FIT_BITMAP, true);
				mainImage.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
				}
			//Sauvegarder Image Principale
			FreeImageBitmap savImage = new FreeImageBitmap(mainImage);
			//Reserver une couleur si nécessaire
			int r = reserveLastClr ? 1 : 0;
			mainImage.Quantize(FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, MaxPaletteColors - r);
			if (reserveLastClr)
				mainImage.Palette[MaxPaletteColors - 1] = new RGBQUAD(Color.Blue);
			//Color palette
			byte[] palBytes = new byte[MaxPaletteColors * 3];
			for (int p = 0, palIdx = 0; p < mainImage.Palette.Length; p++)
				{
				palBytes[palIdx++] = mainImage.Palette[p].rgbRed;
				palBytes[palIdx++] = mainImage.Palette[p].rgbGreen;
				palBytes[palIdx++] = mainImage.Palette[p].rgbBlue;
				}
			//Rotate and Flip Image
			mainImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

			//Main Image
			byte[] mainimg = new byte[imgWidth * imgHeight];
			System.Runtime.InteropServices.Marshal.Copy(mainImage.GetScanlinePointer(0), mainimg, 0, mainimg.Length);
			Array.Reverse(mainimg);

			//Mip map data
			TextureMipmaps mipmaps = new TextureMipmaps();
			int factor = 2;
			for (int a = 0; a < 3; a++)
				{
				int widthMM = (mainImage.Width / factor);
				int heightMM = (mainImage.Height / factor);
				using (FreeImageBitmap clBmp = mainImage.GetScaledInstance(widthMM, heightMM, FREE_IMAGE_FILTER.FILTER_LANCZOS3))
					{
					clBmp.Quantize(FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, MaxPaletteColors, mainImage.Palette);

					int bytesperpixel = FreeImageBitmap.GetPixelFormatSize(clBmp.PixelFormat);
					int stride = RowStride(bytesperpixel, clBmp.Width);
					int pad = clBmp.Width - stride;

					byte[] arrMM = new byte[widthMM * heightMM];
					if (pad == 0)
						{
						System.Runtime.InteropServices.Marshal.Copy(clBmp.GetScanlinePointer(0), arrMM, 0, arrMM.Length);
						}
					else
						{
						byte[] tmparr = new byte[stride * heightMM];
						System.Runtime.InteropServices.Marshal.Copy(clBmp.GetScanlinePointer(0), tmparr, 0, tmparr.Length);
						for (int row = 0; row < heightMM; row++)
							{
							Array.Copy(tmparr, (row * stride), arrMM, row * widthMM, widthMM);
							}
						}
					Array.Reverse(arrMM);
					switch (a)
						{
						case 0:
							mipmaps.Mipmap1 = arrMM;
							break;
						case 1:
							mipmaps.Mipmap2 = arrMM;
							break;
						case 2:
							mipmaps.Mipmap3 = arrMM;
							break;
						}
					}
				factor *= 2;
				}

			//Save TxInfo
			TxInfo.szName = string.Empty;
			TxInfo.uiWidth = imgWidth;
			TxInfo.uiHeight = imgHeight;
			TxInfo.uiDiskLength = 812 + ((85 * imgWidth * imgHeight) / 64);
			TxInfo.rawImg = mainimg;
			TxInfo.Mipmaps = mipmaps;
			TxInfo.rawPal = palBytes;
			TxInfo.Image = savImage.ToBitmap();
			TxInfo.iLvwIdx = -1;
			TxInfo.iImgIdx = -1;
			TxInfo.type = 0x43;
			myNewPng.Dispose();
			mainImage.Dispose();
			savImage.Dispose();
			return TxInfo;
			}
		/////////////////////////////////////////////////////////////////////
		/// Donne la largeur réelle d'une ligne (stride)
		/////////////////////////////////////////////////////////////////////
		private static int PaddedRowWidth(int bitsPerPixel, int w, int padToNBytes)
			{
			int padBits = 8 * padToNBytes;
			return ((w * bitsPerPixel + (padBits - 1)) / padBits) * padToNBytes;
			}
		public static int RowStride(int bitsPerPixel, int width)
			{
			return PaddedRowWidth(bitsPerPixel, width, 4);
			}
		/////////////////////////////////////////////////////////////////////
		/// Converti une Image en autre Format
		/////////////////////////////////////////////////////////////////////
		public static Image GetConvertedImage(Image image, ImageFormat newFormat)
			{
			Image result;
			// saves the image to the stream, and then reloads it as a new image format; thus conversion.. kind of
			using (MemoryStream stream = new MemoryStream())
				{
				image.Save(stream, newFormat);
				stream.Seek(0, SeekOrigin.Begin);
				result = Image.FromStream(stream);
				}
			return result;
			}
		/////////////////////////////////////////////////////////////////////
		/// Converti un Bitmap en un autre Format
		/////////////////////////////////////////////////////////////////////
		public static Bitmap GetConvertedImage(Bitmap image, ImageFormat newFormat)
			{
			Image result;
			// saves the image to the stream, and then reloads it as a new image format; thus conversion.. kind of
			using (MemoryStream stream = new MemoryStream())
				{
				image.Save(stream, newFormat);
				stream.Seek(0, SeekOrigin.Begin);
				result = Image.FromStream(stream);
				}
			Bitmap bmpTmp = new Bitmap(result);
			result.Dispose();
			return bmpTmp;
			}
		/////////////////////////////////////////////////////////////////////
		/// Redimensionne une image en HD
		/////////////////////////////////////////////////////////////////////
		public static Bitmap RescaleImageHD(Bitmap Original, int NewW, int NewH)
			{
			//a holder for the result
			Bitmap result = new Bitmap(NewW, NewH, PixelFormat.Format32bppArgb);
			//set the resolutions the same to avoid cropping due to resolution differences
			result.SetResolution(Original.HorizontalResolution, Original.VerticalResolution);

			//use a graphics object to draw the resized image into the bitmap
			using (Graphics graphics = Graphics.FromImage(result))
				{
				//set the resize quality modes to high quality
				graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				//draw the image into the target bitmap
				graphics.DrawImage(Original,
					new Rectangle(0, 0, result.Width, result.Height),
					new Rectangle(0, 0, Original.Width, Original.Height),
					GraphicsUnit.Pixel);
				}

			//return the resulting bitmap
			return result;
			}

		/////////////////////////////////////////////////////////////////////
		/// Redimensionne une image
		/////////////////////////////////////////////////////////////////////
		public static Bitmap RescaleImage(Bitmap Original, int NewW, int NewH)
			{
			int exW = Original.Width;
			int exH = Original.Height;
			Bitmap Resized = new Bitmap(NewW, NewH, PixelFormat.Format32bppRgb);
			Graphics g = Graphics.FromImage(Resized);
			g.InterpolationMode = InterpolationMode.NearestNeighbor;
			g.PixelOffsetMode = PixelOffsetMode.Half;
			g.DrawImage(Original, new Rectangle(0, 0, NewW, NewH), new Rectangle(0, 0, exW, exH), GraphicsUnit.Pixel);
			g.Dispose();
			return Resized;
			}
		/////////////////////////////////////////////////////////////////////
		/// Rescale Image
		/////////////////////////////////////////////////////////////////////
		public static Bitmap RescaleImage(Bitmap Original, Size Matrice, Color bgColor)
			{
			double exW, exH, nwW, nwH, Lf, Tp;
			double ScaleXY, ScaleX, ScaleY;

			exW = Original.Width;
			exH = Original.Height;
			if (exW == 0 || exH == 0)
				return Original;
			if (exW > Matrice.Width || exH > Matrice.Height)
				{
				ScaleX = (double)(Matrice.Width / exW);
				ScaleY = (double)(Matrice.Height / exH);
				ScaleXY = ScaleX < ScaleY ? ScaleX : ScaleY;
				nwW = (int)(exW * ScaleXY);
				nwH = (int)(exH * ScaleXY);
				}
			else
				{
				nwW = exW;
				nwH = exH;
				}
			Lf = (Matrice.Width - nwW) / 2;
			Tp = (Matrice.Height - nwH) / 2;

			FreeImageBitmap mainImage = new FreeImageBitmap(Original);
			mainImage.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_32_BPP);
			FreeImageBitmap clBmp = mainImage.GetScaledInstance((int)nwW, (int)nwH, FREE_IMAGE_FILTER.FILTER_LANCZOS3);
			FreeImageBitmap thumb = new FreeImageBitmap(Matrice.Width, Matrice.Height, PixelFormat.Format32bppRgb);
			thumb.FillBackground<RGBQUAD>(new RGBQUAD(bgColor));
			thumb.Paste(clBmp, new Point((int)Lf, (int)Tp), 256);
			thumb.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
			mainImage.Dispose();
			clBmp.Dispose();
			return thumb.ToBitmap();
			}
		/////////////////////////////////////////////////////////////////////
		/// Create null Bitmap
		/////////////////////////////////////////////////////////////////////
		public static Bitmap CreateNullBitmap(int nWidth, int nHeight, Color bgColor1, Color bgColor2)
			{
			Brush _texture = null;
			int GridCellSize = 4;

			if (bgColor1 == bgColor2)
				_texture = new SolidBrush(bgColor1);
			else
				{
				Bitmap _gridTile = CreateCheckerBoxTile(GridCellSize, bgColor1, bgColor2);
				_texture = new TextureBrush(_gridTile);
				}

			Bitmap BmpTmp = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppRgb);

			using (Graphics g = Graphics.FromImage(BmpTmp))
				{
				g.FillRectangle(_texture, new Rectangle(0, 0, nWidth, nHeight));
				}
			return BmpTmp;
			}
		/////////////////////////////////////////////////////////////////////
		/// Creates a bitmap image containing a 2x2 grid using the specified cell size and colors.
		/////////////////////////////////////////////////////////////////////
		public static Bitmap CreateCheckerBoxTile(int cellSize, Color cellColor, Color alternateCellColor)
			{
			Bitmap result;
			int width;
			int height;

			width = cellSize * 2;
			height = cellSize * 2;
			result = new Bitmap(width, height);

			using (Graphics g = Graphics.FromImage(result))
				{
				using (Brush brush = new SolidBrush(cellColor))
					{
					g.FillRectangle(brush, new Rectangle(cellSize, 0, cellSize, cellSize));
					g.FillRectangle(brush, new Rectangle(0, cellSize, cellSize, cellSize));
					}

				using (Brush brush = new SolidBrush(alternateCellColor))
					{
					g.FillRectangle(brush, new Rectangle(0, 0, cellSize, cellSize));
					g.FillRectangle(brush, new Rectangle(cellSize, cellSize, cellSize, cellSize));
					}
				}

			return result;
			}

		}
	
	}