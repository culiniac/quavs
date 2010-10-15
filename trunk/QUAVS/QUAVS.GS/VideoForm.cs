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
            // VideoFeed.Run(false);
        }

        private void VideoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VideoFeed.Stop();
        }

        private void toolStripStartButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Run();
        }

        private void toolStripPauseButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Pause();
        }

        private void toolStripRecordButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Record();
        }

        private void toolStripStopButton_Click(object sender, EventArgs e)
        {
            VideoFeed.Stop();
        }
    }
}
