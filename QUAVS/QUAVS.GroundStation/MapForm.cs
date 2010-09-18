using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using WeifenLuo.WinFormsUI.Docking;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace QUAVS.GroundStation
{
    public partial class MapForm : DockContent
    {
        public MapForm()
        {
            InitializeComponent();

            // set cache mode only if no internet avaible
            if (!DesignMode)
            {
                // set cache mode only if no internet avaible
                try
                {
                    System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("www.google.com");
                }
                catch
                {
                    MainMap.Manager.Mode = AccessMode.CacheOnly;
                    MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET - Demo.WindowsForms", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // config map             
                MainMap.MapType = MapType.GoogleHybrid;
                MainMap.MaxZoom = 17;
                MainMap.MinZoom = 1;
                MainMap.Zoom = MainMap.MinZoom + 4;
            }
        }
    }
}
