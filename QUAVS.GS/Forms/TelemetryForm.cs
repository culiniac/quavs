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
    public partial class TelemetryForm : DockContent
    {
        private TelemetryDataObject _telemetryDataObject;
        private TelemetryData _telemetryData = new TelemetryData();
        TelemetryDataObject.TelemetryDataDelegate _delegate;
        
        public void TelemetryDataChanged(TelemetryData tData)
        {
            Debug.WriteLine("TelemetryDataChanged event received", "TelemetryForm");
            _telemetryData = tData;
        }

        public TelemetryForm(TelemetryDataObject tDataObject)
        {
            if (tDataObject == null)
                throw new ArgumentNullException("TelemetryDataObject is null", "TelemetryForm");

            InitializeComponent();
            _telemetryDataObject = tDataObject;

            _delegate = new TelemetryDataObject.TelemetryDataDelegate(this.TelemetryDataChanged);
            _telemetryDataObject.TelemetryDataChanged += _delegate;

        }

        private void TelemetryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _telemetryDataObject.TelemetryDataChanged -= _delegate;
        }
    }
}
