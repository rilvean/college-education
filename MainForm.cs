using System.Diagnostics;

namespace FractalVisualization
{
	public partial class MainForm : Form
	{
		readonly int accuracy = 512;
		readonly Stopwatch stopwatch = new();

		bool isZooming = true;
		int animationStep = 0;
		const int animationStepsTotal = 100;

		public MainForm()
		{
			InitializeComponent();

			MandelbrotSet.InitBitmap(picMain);
			MandelbrotSet.Draw(picMain, accuracy);

			picMain.MouseWheel += PicMain_MouseWheel;

			stopwatch.Start();
			timerTime.Start();
		}

		private void PicMain_MouseWheel(object? sender, MouseEventArgs e)
		{
			MandelbrotSet.MouseZoom(e);
			MandelbrotSet.Draw(picMain, accuracy);
		}

		private void picMain_MouseClick(object sender, MouseEventArgs e)
		{
			MandelbrotSet.Move(e.Location);
			MandelbrotSet.Draw(picMain, accuracy);
		}

		private void timerTime_Tick(object sender, EventArgs e)
		{
			TimeSpan time = stopwatch.Elapsed;
			Text = $@"{time:mm\:ss\.ff}";
		}

		private void timerAnimation_Tick(object sender, EventArgs e)
		{
			if (isZooming)
				MandelbrotSet.ZoomIn();
			else
				MandelbrotSet.ZoomOut();

			MandelbrotSet.Draw(picMain, accuracy);

			animationStep++;

			if (animationStep == animationStepsTotal)
			{
				isZooming = !isZooming;
				animationStep = 0;
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				if (timerAnimation.Enabled)
					timerAnimation.Stop();
				else
					timerAnimation.Start();
			}

			if (e.KeyCode == Keys.Escape)
				Dispose();
		}
	}
}
