using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QUAVS.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace QUAVS.GS
{
    public partial class VideoForm : DockContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoForm"/> class.
        /// </summary>
        public VideoForm()
        {
            InitializeComponent();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            VideoFeed.Run();
            toolStripPauseButton.Enabled = true;
            toolStripRecordButton.Enabled = true;
            toolStripStartButton.Enabled = false;
            toolStripStopButton.Enabled = true;
            toolStripStatusLabel.Text = VideoFeed.State.ToString();
        }

        private void VideoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VideoFeed.Stop();
        }

        private void toolStripStartButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Run();
            toolStripPauseButton.Enabled = true;
            toolStripRecordButton.Enabled = true;
            toolStripStartButton.Enabled = false;
            toolStripStopButton.Enabled = true;
            toolStripStatusLabel.Text = VideoFeed.State.ToString();
        }

        private void toolStripPauseButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Pause();
            toolStripPauseButton.Enabled = false;
            toolStripRecordButton.Enabled = true;
            toolStripStartButton.Enabled = true;
            toolStripStopButton.Enabled = true;
            toolStripStatusLabel.Text = VideoFeed.State.ToString();
        }

        private void toolStripRecordButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Record();
            toolStripPauseButton.Enabled = true;
            toolStripRecordButton.Enabled = false;
            toolStripStartButton.Enabled = true;
            toolStripStopButton.Enabled = true;
            toolStripStatusLabel.Text = VideoFeed.State.ToString();
        }

        private void toolStripStopButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Stop();
            toolStripPauseButton.Enabled = false;
            toolStripRecordButton.Enabled = true;
            toolStripStartButton.Enabled = true;
            toolStripStopButton.Enabled = false;
            toolStripStatusLabel.Text = VideoFeed.State.ToString();
        }
    }
}
