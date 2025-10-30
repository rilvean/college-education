namespace FractalVisualization
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			picMain = new PictureBox();
			timerTime = new System.Windows.Forms.Timer(components);
			timerAnimation = new System.Windows.Forms.Timer(components);
			((System.ComponentModel.ISupportInitialize)picMain).BeginInit();
			SuspendLayout();
			// 
			// picMain
			// 
			picMain.Dock = DockStyle.Fill;
			picMain.Location = new Point(0, 0);
			picMain.Name = "picMain";
			picMain.Size = new Size(782, 553);
			picMain.TabIndex = 0;
			picMain.TabStop = false;
			picMain.MouseClick += picMain_MouseClick;
			// 
			// timerTime
			// 
			timerTime.Tick += timerTime_Tick;
			// 
			// timerAnimation
			// 
			timerAnimation.Tick += timerAnimation_Tick;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(782, 553);
			Controls.Add(picMain);
			DoubleBuffered = true;
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			MaximizeBox = false;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			KeyDown += MainForm_KeyDown;
			((System.ComponentModel.ISupportInitialize)picMain).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox picMain;
		private System.Windows.Forms.Timer timerTime;
		private System.Windows.Forms.Timer timerAnimation;
	}
}
