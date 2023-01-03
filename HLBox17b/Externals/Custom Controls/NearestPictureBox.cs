using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;


namespace HLBox17b.Externals
	{
	public class NearestPictureBox : PictureBox
		{
		private InterpolationMode _interpolationMode = InterpolationMode.NearestNeighbor;

		public NearestPictureBox()
			{
			this.DoubleBuffered = true;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			}

		/// <summary>
		/// Get or Set the interpolation mode for zooming operation
		/// </summary>
		[Bindable(false)]
		[Category("Design")]
		[DefaultValue(InterpolationMode.NearestNeighbor)]
		public InterpolationMode InterpolationMode
			{
			get
				{
				return _interpolationMode;
				}
			set
				{
				_interpolationMode = value;
				}
			}

		/// <summary>
		/// Paint the image
		/// </summary>
		/// <param name="pe">The paint event</param>
		protected override void OnPaint(PaintEventArgs pe)
			{
			if (pe.Graphics.PixelOffsetMode != System.Drawing.Drawing2D.PixelOffsetMode.Half)
				pe.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
			if (pe.Graphics.InterpolationMode != _interpolationMode)
					pe.Graphics.InterpolationMode = _interpolationMode;
			base.OnPaint(pe);
			}
		}
	}
