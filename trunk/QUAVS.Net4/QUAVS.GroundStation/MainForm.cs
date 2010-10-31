using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

using WeifenLuo.WinFormsUI.Docking;

namespace QUAVS.GroundStation
{
    public partial class MainForm : Form
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;

        private VideoForm m_videoForm = new VideoForm();
        private MapForm m_mapForm = new MapForm();


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_videoForm.Show(this.DockingPanel);
            m_mapForm.Show(this.DockingPanel);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_videoForm.Stop();
        }
    }
}
