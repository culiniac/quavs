using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using QUAVS.Types;

namespace QUAVS.Video
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
        /// Gets or sets the video capture.
        /// </summary>
        /// <value>The video capture.</value>
        [BrowsableAttribute(false)]
        internal VideoCapture VideoCapture
        {
            get { return _cam; }
            set { _cam = value; }
        }
        
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
            set
            {
                _cam.VideoWidth = value;
                ResizeVideo();
            }
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
            set
            {
                _cam.VideoHeight = value;
                ResizeVideo();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [control visible].
        /// </summary>
        /// <value><c>true</c> if [control visible]; otherwise, <c>false</c>.</value>
        [Category("QUAVS")]
        [Description("Video Control Visibile")]
        public bool VideoControlVisible
        {
            get { return toolStripVideoControl.Visible; }
            set {
                toolStripVideoControl.Visible = value;
                ResizeVideo();
            }

        }

        /// <summary>
        /// Gets or sets the color of the HUD.
        /// </summary>
        /// <value>The color of the HUD.</value>
        [Category("QUAVS")]
        [Description("HUD color")]
        public Color HUDColor
        {
            get { return _cam.HUD.HUDColor; }
            set { _cam.HUD.HUDColor = value; }
        }

        /// <summary>
        /// Gets or sets the HUD alpha.
        /// </summary>
        /// <value>The HUD alpha.</value>
        [Category("QUAVS")]
        [Description("HUD color")]
        public byte HUDAlpha
        {
            get { return _cam.HUD.HUDAlpha; }
            set { _cam.HUD.HUDAlpha = value; }
        }

        /// <summary>
        /// Gets or sets the HUD speed.
        /// </summary>
        /// <value>The HUD speed.</value>
        [Category("QUAVS")]
        [Description("HUD speed")]
        public float HUDSpeed
        {
            get { return _cam.HUD.Speed; }
            set { _cam.HUD.Speed = value; }
        }


        [Category("QUAVS")]
        [Description("HUD pitch")]
        public float HUDPitch
        {
            get { return _cam.HUD.Pitch; }
            set { _cam.HUD.Pitch = value; }
        }

        [Category("QUAVS")]
        [Description("HUD roll")]
        public float HUDRoll
        {
            get { return _cam.HUD.Roll; }
            set { _cam.HUD.Roll = value; }
        }

        [Category("QUAVS")]
        [Description("HUD yaw")]
        public float HUDYaw
        {
            get { return _cam.HUD.Yaw; }
            set { _cam.HUD.Yaw = value; }
        }

        [Category("QUAVS")]
        [Description("HUD altitude")]
        public float HUDAltitude
        {
            get { return _cam.HUD.Altitude; }
            set { _cam.HUD.Altitude = value; }
        }

        [Category("QUAVS")]
        [Description("HUD altitude")]
        public float HUDHeading
        {
            get { return _cam.HUD.HeadingMagN; }
            set { _cam.HUD.HeadingMagN = value; }
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
        public void Play()
        {
            _cam.Play();
            UpdateVideoControl();
        }

        /// <summary>
        /// Records the video capture.
        /// </summary>
        public void Record()
        {
            _cam.Record();
            UpdateVideoControl();
        }

        /// <summary>
        /// Stops the video capture.
        /// </summary>
        public void Stop()
        {
            _cam.Stop();
            UpdateVideoControl();
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            _cam.Pause();
            UpdateVideoControl();
        }

        #endregion

        /// <summary>
        /// Updates the video control.
        /// </summary>
        private void UpdateVideoControl()
        {
            toolStripPauseButton.Visible = _cam.CanPause;
            toolStripPlayButton.Visible = _cam.CanPlay;
            toolStripRecordButton.Visible = _cam.CanRecord;
            toolStripStopButton.Visible = _cam.CanStop;
            toolStripStatusLabel.Text = _cam.State.ToString();
        }

        /// <summary>
        /// Handles the Resize event of the VideoControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VideoControl_Resize(object sender, EventArgs e)
        {
            ResizeVideo();
        }

        /// <summary>
        /// Resizes the video.
        /// </summary>
        private void ResizeVideo()
        {
            // Resize and reposition video panel
            panelVideo.Height = _cam.VideoHeight;
            panelVideo.Width = _cam.VideoWidth;
            
            if(VideoControlVisible)
                panelVideo.Top = (this.Height - _cam.VideoHeight + toolStripVideoControl.Height) / 2;
            else
                panelVideo.Top = (this.Height - _cam.VideoHeight) / 2;

            panelVideo.Left = (this.Width - _cam.VideoWidth) / 2;

            if (panelVideo.Top < 0) panelVideo.Top = 0;
            if (panelVideo.Left < 0) panelVideo.Left = 0;
        }

        private void toolStripPlayButton_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void toolStripPauseButton_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void toolStripRecordButton_Click(object sender, EventArgs e)
        {
            Record();
        }

        private void toolStripStopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

    }
}
