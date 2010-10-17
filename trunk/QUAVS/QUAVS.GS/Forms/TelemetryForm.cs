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
    public partial class TelemetryForm : DockContent, IObserver
    {
        public delegate void UpdateObserverDelegate(TelemetryDataObject dataObject);

        private TelemetryDataObject _dataObject;

        #region IObserver - Methods
        
        public void UpdateObserver(TelemetryDataObject dataObject)
        {
            if (this.InvokeRequired == false)
            {
                //DisplayData();
            }
            else
            {
                UpdateObserverDelegate updateDelegate = new UpdateObserverDelegate(UpdateObserver);
                this.BeginInvoke(updateDelegate, new object[] { dataObject });
            }
        }

        //public void DisplayData()
        //{
        //    try
        //    {
                                
        //    }
        //    catch (Exception e)
        //    {
        //        Trace.WriteLine("VideoForm DisplayData: Exception: " + e.Message);
        //    }
        //}

        #endregion

        public TelemetryForm(TelemetryDataObject dataObject)
        {
            InitializeComponent();
            _dataObject = dataObject;
            _dataObject.RegisterObserver(this);
        }

        private void TelemetryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _dataObject.RemoveObserver(this);
        }
    }
}
