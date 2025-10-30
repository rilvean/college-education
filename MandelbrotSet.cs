using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FractalVisualization
{
	public static class MandelbrotSet
	{
		static Bitmap? bmp;
		static Complex center = new Complex { x = 0, y = 0 };
		static double scale = 150;

		struct Complex
		{
			public double x;
			public double y;
		}

		static Complex ToLocal(int x, int y, int width, int height) => new Complex
		{
			x = (double)(x - (double)width / 2) / scale + center.x,
			y = -(double)(y - (double)height / 2) / scale + center.y
		};

		static Complex ToLocal(int x, int y)
		{
			if (bmp == null)
				throw new NullReferenceException(nameof(bmp));
			return ToLocal(x, y, bmp.Width, bmp.Height);
		}

		static Complex ToLocal(Point point) => ToLocal(point.X, point.Y);

		public static void InitBitmap(PictureBox pictureBox)
		{
			bmp = new(pictureBox.Width, pictureBox.Height, PixelFormat.Format32bppArgb);
			pictureBox.Image = bmp;
		}

		public static void MouseZoom(MouseEventArgs e) => scale *= e.Delta > 0 ? 1.3 : 1 / 1.3;

		public static void ZoomIn() => scale *= 1.1;
		public static void ZoomOut() => scale /= 1.1;

		public static void FillArgbPixel(int x, int y, int accuracy, byte[] argb, int width, int height)
		{
			int pixel = (y * width + x) * 4;
			Complex x0y0 = ToLocal(x, y, width, height);
			double xn = x0y0.x;
			double yn = x0y0.y;
			double xnPlus1, ynPlus1;
			bool valid = true;

			int i;
			for (i = 0; i < accuracy; i++)
			{
				xnPlus1 = xn * xn - yn * yn + x0y0.x;
				ynPlus1 = 2 * xn * yn + x0y0.y;

				if (xnPlus1 * xnPlus1 + ynPlus1 * ynPlus1 > 4)
				{
					valid = false;
					break;
				}

				xn = xnPlus1;
				yn = ynPlus1;
			}

			if (valid)
			{
				argb[pixel] = 48; // blue
				argb[pixel + 1] = 4; // green
				argb[pixel + 2] = 32; // red
				argb[pixel + 3] = 255; // alpha
			}
			else
			{
				double coefficent = (double)i / accuracy;

				argb[pixel] = (byte)(coefficent * 255 + (1 - coefficent) * 96); // blue
				argb[pixel + 1] = (byte)(coefficent * 255 + (1 - coefficent) * 4); // green
				argb[pixel + 2] = (byte)(coefficent * 0 + (1 - coefficent) * 64); // red
				argb[pixel + 3] = 255; // alpha
			}
		}

		public static void Draw(PictureBox pictureBox, int accuracy)
		{
			if (bmp == null)
				throw new NullReferenceException(nameof(bmp));

			Bitmap bmpCopy = new(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);

			BitmapData bmpCopyData = bmpCopy.LockBits(
				new Rectangle(0, 0, bmpCopy.Width, bmpCopy.Height),
				ImageLockMode.WriteOnly,
				PixelFormat.Format32bppArgb);

			IntPtr ptr = bmpCopyData.Scan0;
			int count = bmpCopyData.Stride * bmpCopy.Height;
			byte[] argb = new byte[count];

			Marshal.Copy(ptr, argb, 0, count);

			int width = bmpCopy.Width;
			int height = bmpCopy.Height;
			Parallel.For(0, height, y =>
			{
				for (int x = 0; x < width; x++)
					FillArgbPixel(x, y, accuracy, argb, width, height);
			});

			Marshal.Copy(argb, 0, ptr, count);
			bmpCopy.UnlockBits(bmpCopyData);

			bmp.Dispose();
			bmp = bmpCopy;
			pictureBox.Image = bmp;
			pictureBox.Invalidate();
		}

		public static void Move(Point p) => center = ToLocal(p);
	}
}
