namespace QUAVS.GS.Forms
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
            this.VideoFeed = new QUAVS.Video.VideoControl();
            this.SuspendLayout();
            // 
            // VideoFeed
            // 
            this.VideoFeed.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoCompressor", global::QUAVS.GS.Properties.Settings.Default, "VideoCompressor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoSource", global::QUAVS.GS.Properties.Settings.Default, "VideoSource", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("HUDColor", global::QUAVS.GS.Properties.Settings.Default, "HUDColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("HUDAlpha", global::QUAVS.GS.Properties.Settings.Default, "HUDAlpha", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoHeight", global::QUAVS.GS.Properties.Settings.Default, "VideoHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoWidth", global::QUAVS.GS.Properties.Settings.Default, "VideoWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoFolder", global::QUAVS.GS.Properties.Settings.Default, "VideoFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoFeed.HUDAlpha = global::QUAVS.GS.Properties.Settings.Default.HUDAlpha;
            this.VideoFeed.HUDAltitude = 0F;
            this.VideoFeed.HUDColor = global::QUAVS.GS.Properties.Settings.Default.HUDColor;
            this.VideoFeed.HUDHeading = 0F;
            this.VideoFeed.HUDPitch = 0F;
            this.VideoFeed.HUDRoll = 0F;
            this.VideoFeed.HUDSpeed = 0F;
            this.VideoFeed.HUDYaw = 0F;
            this.VideoFeed.Location = new System.Drawing.Point(0, 0);
            this.VideoFeed.Margin = new System.Windows.Forms.Padding(0);
            this.VideoFeed.MinimumSize = new System.Drawing.Size(640, 480);
            this.VideoFeed.Name = "VideoFeed";
            this.VideoFeed.Size = new System.Drawing.Size(947, 578);
            this.VideoFeed.TabIndex = 0;
            this.VideoFeed.VideoCompressor = global::QUAVS.GS.Properties.Settings.Default.VideoCompressor;
            this.VideoFeed.VideoControlVisible = false;
            this.VideoFeed.VideoFolder = null;
            this.VideoFeed.VideoFps = 0;
            this.VideoFeed.VideoHeight = global::QUAVS.GS.Properties.Settings.Default.VideoHeight;
            this.VideoFeed.VideoSource = global::QUAVS.GS.Properties.Settings.Default.VideoSource;
            this.VideoFeed.VideoWidth = global::QUAVS.GS.Properties.Settings.Default.VideoWidth;
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 578);
            this.Controls.Add(this.VideoFeed);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "VideoForm";
            this.Text = "VideoForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VideoForm_FormClosed);
            this.Load += new System.EventHandler(this.VideoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Video.VideoControl VideoFeed;

    }
}