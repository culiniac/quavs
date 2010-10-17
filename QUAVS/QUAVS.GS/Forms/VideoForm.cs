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
    public partial class VideoForm : DockContent, IObserver
    {
        public delegate void UpdateObserverDelegate(TelemetryDataObject dataObject);

        private TelemetryDataObject _dataObject;

        #region IObserver - Methods
        
        public void UpdateObserver(TelemetryDataObject dataObject)
        {
            if (this.InvokeRequired == false)
            {
                VideoFeed.HUDSpeed = dataObject.SpeedZ;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoForm"/> class.
        /// </summary>
        public VideoForm(TelemetryDataObject dataObject)
        {
            InitializeComponent();
            _dataObject = dataObject;
            _dataObject.RegisterObserver(this);
        }

        /// <summary>
        /// Handles the Load event of the VideoForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VideoForm_Load(object sender, EventArgs e)
        {
            VideoFeed.Play();
        }

        private void VideoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _dataObject.RemoveObserver(this);
        }

    }
}
