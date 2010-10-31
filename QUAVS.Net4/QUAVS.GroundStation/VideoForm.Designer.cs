namespace QUAVS.GroundStation
{
    partial class VideoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VideoFeed = new QUAVS.Base.VideoControl();
            this.SuspendLayout();
            // 
            // VideoFeed
            // 
            this.VideoFeed.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VideoFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoFeed.Fps = 30;
            this.VideoFeed.Location = new System.Drawing.Point(0, 0);
            this.VideoFeed.Margin = new System.Windows.Forms.Padding(0);
            this.VideoFeed.MinimumSize = new System.Drawing.Size(640, 480);
            this.VideoFeed.Name = "VideoFeed";
            this.VideoFeed.Size = new System.Drawing.Size(722, 533);
            this.VideoFeed.Speed = 0D;
            this.VideoFeed.StrVideoFolder = "test.avi";
            this.VideoFeed.StrVideoCompressor = "MJPEG Compressor";
            this.VideoFeed.StrVideoSource = "Microsoft LifeCam VX-5000";
            this.VideoFeed.TabIndex = 0;
            this.VideoFeed.VideoHeight = 480;
            this.VideoFeed.VideoWidth = 640;
            this.VideoFeed.Load += new System.EventHandler(this.VideoFeed_Load);
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 533);
            this.Controls.Add(this.VideoFeed);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "VideoForm";
            this.Text = "Video Feed";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VideoForm_FormClosed);
            this.Load += new System.EventHandler(this.VideoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Base.VideoControl VideoFeed;
    }
}