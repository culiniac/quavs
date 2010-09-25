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
    public class VideoSettings
    {
        private string _videoSource;
        private string _videoCompressor;

        /// <summary>
        /// Gets or sets the STR video source.
        /// </summary>
        /// <value>The video source.</value>
        [Category("QUAVS")]
        [Description("select video source")]
        [EditorAttribute(typeof(VideoSourceUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string VideoSource
        {
            get { return _videoSource; }
            set { _videoSource = value; }
        }

        /// <summary>
        /// Gets or sets the video compressor.
        /// </summary>
        /// <value>The video compressor.</value>
        [Category("QUAVS")]
        [Description("select video compressor")]
        [EditorAttribute(typeof(VideoCodecUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string VideoCompressor
        {
            get { return _videoCompressor; }
            set { _videoCompressor = value; }
        }

    }
}
