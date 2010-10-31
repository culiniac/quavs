using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using QUAVS.Base;
using QUAVS.Log;

namespace QUAVS.Video
{
    /// <summary>
    /// 
    /// </summary>
    class HUD
    {
        /// <summary>
        /// Private variables to hold Telemetry data
        /// </summary>
        #region privates
        private float _speed;
        private float _altitude;
        private float _latitude;
        private float _longitude;
        private float _headingMagneticNorth;
        private float _roll;
        private float _pitch;
        private float _yaw;
        private string _message;


        private Pen _pen1;
        private Pen _pen2;
        private SolidBrush _brush;

        /// <summary>
        /// format1 for HUD numbers
        /// </summary>
        private StringFormat _format1;
        /// <summary>
        /// format2 for HUD messages
        /// </summary>
        private StringFormat _format2;
        private Rectangle[] _rectangleTextBkg = new Rectangle[2];
        private Rectangle[] _rectangleHUD = new Rectangle[3];


        //actual HUD graphics coordinates
        private Point[] _pointsHUD1 = new Point[2];
        private Point[] _pointsHUD2 = new Point[3];
        private Point[] _pointsHUD3 = new Point[7];
        private Rectangle[] _rectangleHUD1 = new Rectangle[4];
        private Rectangle[] _rectanglePitch = new Rectangle[4];

        private Color _color = Color.FromArgb(255, 0, 255, 0);
        private byte _alpha = 255;

        private int _pitch_resolution = 10;
        private int _yaw_resolution = 10;
        private int _reticlesize = 6;
        private int _reticlesize1 = 2;

        private Graphics _g;
        private GraphicsState _oldState;

        private String _s;

        private int _videoWidth;
        private int _videoHeight;

        private Font _fontOverlay;
        private Font _transparentFont;
        private SolidBrush _transparentBrush; 
        #endregion
        
        #region Accessors & Mutators
        
        public Color HUDColor
        {
            get { return _color; }
            set 
            {
                _color = Color.FromArgb(_alpha, value.R, value.G, value.B);
            }
        }

        public byte HUDAlpha
        {
            get { return _alpha; }
            set 
            {
                _alpha = value;
                _color = Color.FromArgb(_alpha, _color.R, _color.G, _color.B);
            }
        }

        public int VideoWidth
        {
            get { return _videoWidth; }
            set { _videoWidth = value; }
        }
        public int VideoHeight
        {
            get { return _videoHeight; }
            set { _videoHeight = value; }
        }

        public float Longitude
        {
            get { return _longitude; }
            set { if(value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _longitude = value; }
        }
        public float Latitude
        {
            get { return _latitude; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _latitude = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _speed = value; }
        }

        public float Altitude
        {
            get { return _altitude; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _altitude = value; }
        }

        public float Roll
        {
            get { return _roll; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _roll = value; }
        }

        public float Pitch
        {
            get { return _pitch; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _pitch = value; }
        }
        public float Yaw
        {
            get { return _yaw; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _yaw = value; }
        }

        public float HeadingMagN
        {
            get { return _headingMagneticNorth; }
            set { if (value != float.NaN & value != float.NegativeInfinity & value != float.PositiveInfinity) _headingMagneticNorth = value; }
        }
        
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="HUD"/> class.
        /// </summary>
        public HUD()
        {
            try
            {
                _speed = 0;
                _altitude = 0;
                _latitude = 0;
                _longitude = 0;
                _headingMagneticNorth = 0;
                _roll = 0;
                _pitch = 0;
                _yaw = 0;
                _message = "Initialized...";

                _videoWidth = 640;
                _videoHeight = 480;

                // TO DO: fix the font sizes
                // ADD: more parameters for color, font size, etc

                _fontOverlay = new Font("Tahoma", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                _transparentFont = new Font("Tahoma", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

                _transparentBrush = new SolidBrush(_color);

                _pen1 = new Pen(_color, 1);
                _pen2 = new Pen(_color, 1);
                _pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                _brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));


                // Construct 2 new StringFormat objects
                _format1 = new StringFormat(StringFormatFlags.NoClip);
                _format1.LineAlignment = StringAlignment.Near;
                _format1.Alignment = StringAlignment.Center;

                _format2 = new StringFormat(StringFormatFlags.NoClip);
                _format2.LineAlignment = StringAlignment.Near;
                _format2.Alignment = StringAlignment.Near;
            }
            catch (Exception e)
            {
                TraceException.WriteLine(e);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            try
            {
                _fontOverlay.Dispose();
                _transparentBrush.Dispose();
                _transparentFont.Dispose();
            }
            catch (Exception e)
            {
                TraceException.WriteLine(e);
            }
        }

        /// <summary>
        /// Draws the HUD.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="dst">The DST.</param>
        public void DrawHUD(Bitmap src, Bitmap dst)
        {
            try
            {
                _transparentBrush.Color = _color;
                _pen1.Color = _color;
                _pen2.Color = _color;

                //_brush.Color = _color_brush;

                //Initialize Graphics
                _g = Graphics.FromImage(src);
                _g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                _g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit; //.ClearTypeGridFit;

                // Draw top and bottom black bands
                _rectangleTextBkg[0] = new Rectangle(0, 0, _videoWidth, 20);
                _rectangleTextBkg[1] = new Rectangle(0, _videoHeight - 20, _videoWidth, _videoHeight);
                _g.FillRectangles(_brush, _rectangleTextBkg);
                _s = DateTime.Now.ToString();
                _s += " " + _message;
                _g.DrawString(_s, _fontOverlay, _transparentBrush, (RectangleF)_rectangleTextBkg[1], _format2);

                // Draw numeric boxes for:
                // 1. Heading
                _rectangleHUD[0] = new Rectangle((_videoWidth / 2) - 25, 30, 50, 15);
                // 2. Altitude
                _rectangleHUD[1] = new Rectangle(_videoWidth - 95, (_videoHeight / 2) - 8, 50, 15);
                // 3. Speed
                _rectangleHUD[2] = new Rectangle(45, (_videoHeight / 2) - 8, 50, 15);
                //_g.DrawRectangles(_pen1, _rectangleHUD);
                _g.FillRectangles(_brush, _rectangleHUD);

                //Draw numeric values inside each box:
                // 1. Heading
                _g.DrawString(_headingMagneticNorth.ToString(), _transparentFont, _transparentBrush, (RectangleF)_rectangleHUD[0], _format1);
                // 2. Altitude
                _g.DrawString(_altitude.ToString(), _transparentFont, _transparentBrush, (RectangleF)_rectangleHUD[1], _format1);
                //3. Speed
                _g.DrawString(_speed.ToString(), _transparentFont, _transparentBrush, (RectangleF)_rectangleHUD[2], _format1);

                //Draw aircraft indicator
                _pointsHUD3[0].X = _videoWidth / 2 - 20;
                _pointsHUD3[0].Y = _videoHeight / 2;

                _pointsHUD3[1].X = _videoWidth / 2 - 10;
                _pointsHUD3[1].Y = _videoHeight / 2;

                _pointsHUD3[2].X = _videoWidth / 2 - 5;
                _pointsHUD3[2].Y = _videoHeight / 2 + 10;

                _pointsHUD3[3].X = _videoWidth / 2;
                _pointsHUD3[3].Y = _videoHeight / 2 + 3;

                _pointsHUD3[4].X = _videoWidth / 2 + 5;
                _pointsHUD3[4].Y = _videoHeight / 2 + 10;

                _pointsHUD3[5].X = _videoWidth / 2 + 10;
                _pointsHUD3[5].Y = _videoHeight / 2;

                _pointsHUD3[6].X = _videoWidth / 2 + 20;
                _pointsHUD3[6].Y = _videoHeight / 2;
                _g.DrawLines(_pen1, _pointsHUD3);

                // Draw horizontal speed indicator
                Rectangle rectangleObj = new Rectangle(_videoWidth / 2 - _reticlesize / 2, _videoHeight / 2 - _reticlesize / 2, _reticlesize, _reticlesize);
                _g.DrawEllipse(_pen1, rectangleObj);
                // this line should represent the horizontal direction and speed up to X m/s
                _pointsHUD1[0].X = _videoWidth / 2 + 20;
                _pointsHUD1[0].Y = _videoHeight / 2 - 30;
                _pointsHUD1[1].X = _videoWidth / 2;
                _pointsHUD1[1].Y = _videoHeight / 2;
                _g.DrawLines(_pen1, _pointsHUD1);

                double pitch = _pitch;
                if (_pitch >= 90)
                    pitch = 180 - _pitch;
                if (_pitch <= -90)
                    pitch = -180 - _pitch;

                // save existing matrices
                _oldState = _g.Save();

                //yaw
                _g.TranslateTransform((float)_yaw * _yaw_resolution, 0, MatrixOrder.Append);

                //pitch
                _g.TranslateTransform(0, (float)pitch * _pitch_resolution, MatrixOrder.Append);
                if (_pitch >= 90 || _pitch <= -90)
                    _g.RotateTransform((float)180, MatrixOrder.Append);

                // make rotation point the origin 
                _g.TranslateTransform(-_videoWidth / 2, -_videoHeight / 2);
                // roll
                _g.RotateTransform((float)-_roll, MatrixOrder.Append);
                // translate back
                _g.TranslateTransform(_videoWidth / 2, _videoHeight / 2, MatrixOrder.Append);

                //DrawHUD horizont line
                _pointsHUD1[0].X = _videoWidth / 2 - 150;
                _pointsHUD1[0].Y = _videoHeight / 2;
                _pointsHUD1[1].X = _videoWidth / 2 - 25;
                _pointsHUD1[1].Y = _videoHeight / 2;
                _g.DrawLines(_pen1, _pointsHUD1);
                _pointsHUD1[0].X = _videoWidth / 2 + 150;
                _pointsHUD1[0].Y = _videoHeight / 2;
                _pointsHUD1[1].X = _videoWidth / 2 + 25;
                _pointsHUD1[1].Y = _videoHeight / 2;
                _g.DrawLines(_pen1, _pointsHUD1);

                rectangleObj = new Rectangle(_videoWidth / 2 - _reticlesize1 / 2, _videoHeight / 2 - _reticlesize1 / 2, _reticlesize1, _reticlesize1);
                _g.DrawEllipse(_pen1, rectangleObj);

                for (int i = 1; i <= 36; i++)
                {
                    _pointsHUD2[0].X = _videoWidth / 2 - 75;
                    _pointsHUD2[0].Y = _videoHeight / 2 + _pitch_resolution * 10 * i - 10;
                    _pointsHUD2[1].X = _videoWidth / 2 - 75;
                    _pointsHUD2[1].Y = _videoHeight / 2 + _pitch_resolution * 10 * i;
                    _pointsHUD2[2].X = _videoWidth / 2 - 25;
                    _pointsHUD2[2].Y = _videoHeight / 2 + _pitch_resolution * 10 * i;
                    _g.DrawLines(_pen2, _pointsHUD2);
                    _pointsHUD2[0].X = _videoWidth / 2 + 75;
                    _pointsHUD2[0].Y = _videoHeight / 2 + _pitch_resolution * 10 * i - 10;
                    _pointsHUD2[1].X = _videoWidth / 2 + 75;
                    _pointsHUD2[1].Y = _videoHeight / 2 + _pitch_resolution * 10 * i;
                    _pointsHUD2[2].X = _videoWidth / 2 + 25;
                    _pointsHUD2[2].Y = _videoHeight / 2 + _pitch_resolution * 10 * i;
                    _g.DrawLines(_pen2, _pointsHUD2);

                    _pointsHUD2[0].X = _videoWidth / 2 - 75;
                    _pointsHUD2[0].Y = _videoHeight / 2 - _pitch_resolution * 10 * i + 10;
                    _pointsHUD2[1].X = _videoWidth / 2 - 75;
                    _pointsHUD2[1].Y = _videoHeight / 2 - _pitch_resolution * 10 * i;
                    _pointsHUD2[2].X = _videoWidth / 2 - 25;
                    _pointsHUD2[2].Y = _videoHeight / 2 - _pitch_resolution * 10 * i;
                    _g.DrawLines(_pen1, _pointsHUD2);
                    _pointsHUD2[0].X = _videoWidth / 2 + 75;
                    _pointsHUD2[0].Y = _videoHeight / 2 - _pitch_resolution * 10 * i + 10;
                    _pointsHUD2[1].X = _videoWidth / 2 + 75;
                    _pointsHUD2[1].Y = _videoHeight / 2 - _pitch_resolution * 10 * i;
                    _pointsHUD2[2].X = _videoWidth / 2 + 25;
                    _pointsHUD2[2].Y = _videoHeight / 2 - _pitch_resolution * 10 * i;
                    _g.DrawLines(_pen1, _pointsHUD2);

                    // Draw numeric boxes

                    _rectanglePitch[0] = new Rectangle(_videoWidth / 2 - 125, _videoHeight / 2 + _pitch_resolution * 10 * i - 15, 50, 15);
                    _rectanglePitch[1] = new Rectangle(_videoWidth / 2 + 75, _videoHeight / 2 + _pitch_resolution * 10 * i - 15, 50, 15);

                    _g.DrawString((-i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)_rectanglePitch[0], _format1);
                    _g.DrawString((-i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)_rectanglePitch[1], _format1);

                    _rectanglePitch[2] = new Rectangle(_videoWidth / 2 - 125, _videoHeight / 2 - _pitch_resolution * 10 * i, 50, 15);
                    _rectanglePitch[3] = new Rectangle(_videoWidth / 2 + 75, _videoHeight / 2 - _pitch_resolution * 10 * i, 50, 15);

                    _g.DrawString((i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)_rectanglePitch[2], _format1);
                    _g.DrawString((i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)_rectanglePitch[3], _format1);
                }
                _g.Restore(_oldState); // restore old

                _oldState = _g.Save(); // save existing matrices
                // make rotation point the origin
                _g.TranslateTransform(-_videoWidth / 2, -_videoHeight + 100);
                // roll 
                _g.RotateTransform((float)_roll, MatrixOrder.Append);
                // translate back
                _g.TranslateTransform(_videoWidth / 2, _videoHeight - 100, MatrixOrder.Append);

                src.RotateFlip(RotateFlipType.RotateNoneFlipY);

                _g.Dispose();

                _g = Graphics.FromImage(dst);
                _g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // draw the overlay bitmap over the video's bitmap
                _g.DrawImage(src, 0, 0, src.Width, src.Height);
                _g.Dispose();
            }
            catch (Exception e)
            {
                TraceException.WriteLine(e);
            }
        }
    }
}
