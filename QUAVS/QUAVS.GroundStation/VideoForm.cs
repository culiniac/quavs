using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using QUAVS.Base;

namespace QUAVS.GroundStation
{
    public partial class VideoForm : DockContent 
    {
        public VideoForm()
        {
            InitializeComponent();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            VideoFeed.Run();
        }

        private void VideoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VideoFeed.Stop();
        }

        public void Stop()
        {
            VideoFeed.Stop();
        }

        public void Run()
        {
            VideoFeed.Run();
        }

        private void VideoFeed_Load(object sender, EventArgs e)
        {

        }
    }
}
