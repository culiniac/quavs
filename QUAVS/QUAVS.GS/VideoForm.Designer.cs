namespace QUAVS.GS
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
            this.toolStripVideo = new System.Windows.Forms.ToolStrip();
            this.toolStripStartButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripPauseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRecordButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripStopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripLabel();
            this.VideoFeed = new QUAVS.Base.VideoControl();
            this.toolStripVideo.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripVideo
            // 
            this.toolStripVideo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStartButton,
            this.toolStripPauseButton,
            this.toolStripRecordButton,
            this.toolStripStopButton,
            this.toolStripSeparator1,
            this.toolStripStatusLabel});
            this.toolStripVideo.Location = new System.Drawing.Point(0, 0);
            this.toolStripVideo.Name = "toolStripVideo";
            this.toolStripVideo.Size = new System.Drawing.Size(947, 25);
            this.toolStripVideo.TabIndex = 1;
            this.toolStripVideo.Text = "toolStrip1";
            // 
            // toolStripStartButton
            // 
            this.toolStripStartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStartButton.Image = global::QUAVS.GS.Properties.Resources.PlayHS;
            this.toolStripStartButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripStartButton.Name = "toolStripStartButton";
            this.toolStripStartButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripStartButton.Text = "Start";
            this.toolStripStartButton.Click += new System.EventHandler(this.toolStripStartButton_Click);
            // 
            // toolStripPauseButton
            // 
            this.toolStripPauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPauseButton.Image = global::QUAVS.GS.Properties.Resources.PauseHS;
            this.toolStripPauseButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripPauseButton.Name = "toolStripPauseButton";
            this.toolStripPauseButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripPauseButton.Text = "Pause";
            this.toolStripPauseButton.Click += new System.EventHandler(this.toolStripPauseButton_Click);
            // 
            // toolStripRecordButton
            // 
            this.toolStripRecordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRecordButton.Image = global::QUAVS.GS.Properties.Resources.RecordHS;
            this.toolStripRecordButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripRecordButton.Name = "toolStripRecordButton";
            this.toolStripRecordButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripRecordButton.Text = "Record";
            this.toolStripRecordButton.Click += new System.EventHandler(this.toolStripRecordButton_Click);
            // 
            // toolStripStopButton
            // 
            this.toolStripStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStopButton.Image = global::QUAVS.GS.Properties.Resources.StopHS;
            this.toolStripStopButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripStopButton.Name = "toolStripStopButton";
            this.toolStripStopButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripStopButton.Text = "Stop";
            this.toolStripStopButton.Click += new System.EventHandler(this.toolStripStopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(16, 22);
            this.toolStripStatusLabel.Text = "...";
            // 
            // VideoFeed
            // 
            this.VideoFeed.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoCompressor", global::QUAVS.GS.Properties.Settings.Default, "VideoCompressor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.DataBindings.Add(new System.Windows.Forms.Binding("VideoSource", global::QUAVS.GS.Properties.Settings.Default, "VideoSource", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoFeed.HUDSpeed = 0D;
            this.VideoFeed.Location = new System.Drawing.Point(0, 0);
            this.VideoFeed.Margin = new System.Windows.Forms.Padding(0);
            this.VideoFeed.MinimumSize = new System.Drawing.Size(640, 480);
            this.VideoFeed.Name = "VideoFeed";
            this.VideoFeed.Size = new System.Drawing.Size(947, 578);
            this.VideoFeed.TabIndex = 0;
            this.VideoFeed.VideoCompressor = global::QUAVS.GS.Properties.Settings.Default.VideoCompressor;
            this.VideoFeed.VideoFolder = "test.avi";
            this.VideoFeed.VideoFps = 0;
            this.VideoFeed.VideoHeight = 480;
            this.VideoFeed.VideoSource = global::QUAVS.GS.Properties.Settings.Default.VideoSource;
            this.VideoFeed.VideoWidth = 640;
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 578);
            this.Controls.Add(this.toolStripVideo);
            this.Controls.Add(this.VideoFeed);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "VideoForm";
            this.Text = "VideoForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VideoForm_FormClosed);
            this.Load += new System.EventHandler(this.VideoForm_Load);
            this.toolStripVideo.ResumeLayout(false);
            this.toolStripVideo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Base.VideoControl VideoFeed;
        private System.Windows.Forms.ToolStrip toolStripVideo;
        private System.Windows.Forms.ToolStripButton toolStripStartButton;
        private System.Windows.Forms.ToolStripButton toolStripPauseButton;
        private System.Windows.Forms.ToolStripButton toolStripRecordButton;
        private System.Windows.Forms.ToolStripButton toolStripStopButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripStatusLabel;
    }
}