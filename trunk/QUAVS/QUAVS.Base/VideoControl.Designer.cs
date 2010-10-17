namespace QUAVS.Base
{
    partial class VideoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelVideo = new System.Windows.Forms.Panel();
            this.toolStripVideoControl = new System.Windows.Forms.ToolStrip();
            this.toolStripPlayButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripPauseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRecordButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripStopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripVideoControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelVideo
            // 
            this.panelVideo.BackColor = System.Drawing.Color.Black;
            this.panelVideo.Location = new System.Drawing.Point(166, 125);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(446, 321);
            this.panelVideo.TabIndex = 0;
            // 
            // toolStripVideoControl
            // 
            this.toolStripVideoControl.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripVideoControl.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripVideoControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPlayButton,
            this.toolStripPauseButton,
            this.toolStripRecordButton,
            this.toolStripStopButton,
            this.toolStripSeparator1,
            this.toolStripStatusLabel,
            this.toolStripLabel});
            this.toolStripVideoControl.Location = new System.Drawing.Point(0, 0);
            this.toolStripVideoControl.Name = "toolStripVideoControl";
            this.toolStripVideoControl.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripVideoControl.Size = new System.Drawing.Size(783, 25);
            this.toolStripVideoControl.TabIndex = 2;
            this.toolStripVideoControl.Text = "toolStrip1";
            // 
            // toolStripPlayButton
            // 
            this.toolStripPlayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPlayButton.Image = global::QUAVS.Base.Properties.Resources.PlayHS;
            this.toolStripPlayButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripPlayButton.Name = "toolStripPlayButton";
            this.toolStripPlayButton.Size = new System.Drawing.Size(23, 20);
            this.toolStripPlayButton.Text = "Play";
            this.toolStripPlayButton.Click += new System.EventHandler(this.toolStripPlayButton_Click);
            // 
            // toolStripPauseButton
            // 
            this.toolStripPauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPauseButton.Image = global::QUAVS.Base.Properties.Resources.PauseHS;
            this.toolStripPauseButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripPauseButton.Name = "toolStripPauseButton";
            this.toolStripPauseButton.Size = new System.Drawing.Size(23, 20);
            this.toolStripPauseButton.Text = "Pause";
            this.toolStripPauseButton.Click += new System.EventHandler(this.toolStripPauseButton_Click);
            // 
            // toolStripRecordButton
            // 
            this.toolStripRecordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRecordButton.Image = global::QUAVS.Base.Properties.Resources.RecordHS;
            this.toolStripRecordButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripRecordButton.Name = "toolStripRecordButton";
            this.toolStripRecordButton.Size = new System.Drawing.Size(23, 20);
            this.toolStripRecordButton.Text = "Record";
            this.toolStripRecordButton.Click += new System.EventHandler(this.toolStripRecordButton_Click);
            // 
            // toolStripStopButton
            // 
            this.toolStripStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStopButton.Image = global::QUAVS.Base.Properties.Resources.StopHS;
            this.toolStripStopButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripStopButton.Name = "toolStripStopButton";
            this.toolStripStopButton.Size = new System.Drawing.Size(23, 20);
            this.toolStripStopButton.Text = "Stop";
            this.toolStripStopButton.Click += new System.EventHandler(this.toolStripStopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(16, 20);
            this.toolStripStatusLabel.Text = "...";
            this.toolStripStatusLabel.ToolTipText = "Status";
            // 
            // toolStripLabel
            // 
            this.toolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel.Name = "toolStripLabel";
            this.toolStripLabel.Size = new System.Drawing.Size(78, 20);
            this.toolStripLabel.Text = "QUAVS Video";
            // 
            // VideoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.toolStripVideoControl);
            this.Controls.Add(this.panelVideo);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "VideoControl";
            this.Size = new System.Drawing.Size(783, 594);
            this.Resize += new System.EventHandler(this.VideoControl_Resize);
            this.toolStripVideoControl.ResumeLayout(false);
            this.toolStripVideoControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.ToolStrip toolStripVideoControl;
        private System.Windows.Forms.ToolStripButton toolStripPlayButton;
        private System.Windows.Forms.ToolStripButton toolStripPauseButton;
        private System.Windows.Forms.ToolStripButton toolStripRecordButton;
        private System.Windows.Forms.ToolStripButton toolStripStopButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel;
    }
}
