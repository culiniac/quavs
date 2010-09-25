using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using QUAVS.Base;

namespace QUAVS.GS
{
    public partial class SettingsForm : DockContent
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.propertyGrid.SelectedObject = QUAVS.GS.Properties.Settings.Default;
        }
    }
}
