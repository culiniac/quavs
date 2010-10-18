using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QUAVS.Base;
using WeifenLuo.WinFormsUI.Docking;
using System.Diagnostics;

namespace QUAVS.GS
{
    public partial class VideoForm : DockContent
    {
        private TelemetryDataObject _telemetryDataObject;
        private TelemetryData _telemetryData = new TelemetryData();
        TelemetryDataObject.TelemetryDataDelegate _delegate;

        public void TelemetryDataChanged(TelemetryData tData)
        {
            Debug.WriteLine("TelemetryDataChanged event received", "VideoForm");
            _telemetryData = tData;
            VideoFeed.HUDSpeed = _telemetryData.SpeedX;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoForm"/> class.
        /// </summary>
        public VideoForm(TelemetryDataObject tDataObject)
        {
            if (tDataObject == null)
                throw new ArgumentNullException("TelemetryDataObject is null", "VideoForm");

            InitializeComponent();
            _telemetryDataObject = tDataObject;

            _delegate = new TelemetryDataObject.TelemetryDataDelegate(this.TelemetryDataChanged);
            _telemetryDataObject.TelemetryDataChanged += _delegate;
        }

        /// <summary>
        /// Handles the Load event of the VideoForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VideoForm_Load(object sender, EventArgs e)
        {
            
        }

        private void VideoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _telemetryDataObject.TelemetryDataChanged -= _delegate;
        }

    }
}
