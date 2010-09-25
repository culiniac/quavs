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
        public VideoForm()
        {
            InitializeComponent();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            Run();
        }

        public void Stop()
        {
            VideoFeed.Stop();
        }

        public void Run()
        {
            VideoFeed.Run();
        }

        private void VideoForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
