using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Design;
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
        private VideoCapture _cam;
        
        #endregion

        #region Parameters

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        /// <value>The video.</value>
        [BrowsableAttribute(false)]
        internal VideoCapture Video
        {
            get { return _cam; }
            set { _cam = value; }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        [BrowsableAttribute(false)]
        public VideoCaptureState State
        {
            get { return _cam.State; }
        }

        /// <summary>
        /// Gets or sets the STR video source.
        /// </summary>
        /// <value>The video source.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        [EditorAttribute(typeof(VideoSourceUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public VideoSourceType VideoSource
        {
            get { return _cam.CaptureDevice; }
            set { _cam.CaptureDevice = value; }
        }

        /// <summary>
        /// Gets or sets the video compressor.
        /// </summary>
        /// <value>The video compressor.</value>
        [Category("QUAVS")]
        [Description("select video compressor")]
        [EditorAttribute(typeof(VideoCodecUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public VideoCodecType VideoCompressor
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
        //[EditorAttribute(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string VideoFolder
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
        public int VideoFps
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
        public double HUDSpeed
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
            _cam = new VideoCapture(panelVideo.Handle);
        }

        /// /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cam != null)
                {
                    _cam.Dispose();
                    _cam = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        
        #region Methods
        /// <summary>
        /// Runs the video capture.
        /// </summary>
        public void Run()
        {
            _cam.Start();
        }

        /// <summary>
        /// Records the video capture.
        /// </summary>
        public void Record()
        {
            _cam.Recording();
        }

        /// <summary>
        /// Stops the video capture.
        /// </summary>
        public void Stop()
        {
            _cam.Stop();
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            _cam.Pause();
        }

        #endregion

        private void VideoControl_Resize(object sender, EventArgs e)
        {
            // Resize and reposition video panel
            panelVideo.Height = _cam.VideoHeight;
            panelVideo.Width = _cam.VideoWidth;
            panelVideo.Top = (this.Height - _cam.VideoHeight) / 2;
            panelVideo.Left = (this.Width - _cam.VideoWidth) / 2;

            if (panelVideo.Top < 0) panelVideo.Top = 0;
            if (panelVideo.Left < 0) panelVideo.Left = 0;
        }

    }
}
