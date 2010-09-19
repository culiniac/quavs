using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace QUAVS.Base
{
    public partial class VideoControl : UserControl
    {
        #region Private Member Variables

        /// <summary>
        /// main VideoCapture object
        /// </summary>
        private VideoCapture _cam = null;
        
        #endregion

        #region Parameters

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        /// <value>The video.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        internal VideoCapture Video
        {
            get { return _cam; }
            set { _cam = value; }
        }

        /// <summary>
        /// Gets or sets the STR video source.
        /// </summary>
        /// <value>The STR video source.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        public string StrVideoSource
        {
            get { return _cam.CaptureDevice; }
            set { _cam.CaptureDevice = value; }
        }

        /// <summary>
        /// Gets or sets the STR video compressor.
        /// </summary>
        /// <value>The STR video compressor.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        public string StrVideoCompressor
        {
            get { return _cam.CompressorCodec; }
            set { _cam.CompressorCodec = value; }
        }

        /// <summary>
        /// Gets or sets the name of the STR file.
        /// </summary>
        /// <value>The name of the STR file.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        [EditorAttribute(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string StrVideoFolder
        {
            get { return _cam.VideoFile; }
            set { _cam.VideoFile = value; }
        }

        /// <summary>
        /// Gets or sets the FPS.
        /// </summary>
        /// <value>The FPS.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        public int Fps
        {
            get { return _cam.Fps; }
            set { _cam.Fps = value; }
        }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        /// <value>The video width.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        public int VideoWidth
        {
            get { return _cam.VideoWidth; }
            set { _cam.VideoWidth = value; }
        }

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        /// <value>The video height.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        public int VideoHeight
        {
            get { return _cam.VideoHeight; }
            set { _cam.VideoHeight = value; }
        }

        [Category("QUAVS")]
        [Description("HUD speed")]
        public double Speed
        {
            get { return _cam.HUD.Speed; }
            set { _cam.HUD.Speed = value; }
        }

        #endregion

        
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoControl"/> class.
        /// </summary>
        public VideoControl()
        {
            InitializeComponent();
            _cam = new VideoCapture(this.Handle);
        }
        
        #region Methods
        /// <summary>
        /// Runs the video capture.
        /// </summary>
        public void Run()
        {
            //add to constructure if you can
            _cam.InitializeCapture();
            _cam.Start();
        }

        /// <summary>
        /// Stops the video capture.
        /// </summary>
        public void Stop()
        {
            if (_cam != null)
            {
                _cam.Dispose();
                _cam = null;
            }
        }
        #endregion

    }
}
