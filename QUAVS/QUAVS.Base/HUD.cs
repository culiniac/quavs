using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace QUAVS.Base
{
    class HUD
    {
        // telemetry TO DO: add telemetry data members variables
        // private vars moved to Telemetry Data object
        //private double _speed;
        //private double _altitude;
        //private double _latitude;
        //private double _longitude;
        //private double _headingMagneticNorth;
        //private double _roll;
        //private double _pitch;
        //private double _yaw;

        private TelemetryDataObject _dataObject;

        private int _videoWidth;
        private int _videoHeight;

        private Font _fontOverlay;
        private Font _transparentFont;
        private SolidBrush _transparentBrush;
        
        #region Accessors & Mutators
        /// <summary>
        /// Longitude attribute for HUD
        /// </summary>
        //public double Longitude
        //{
        //    get { return _longitude; }
        //    set { _longitude = value; }
        //}
        //public double Latitude
        //{
        //    get { return _latitude; }
        //    set { _latitude = value; }
        //}
        //public double Speed
        //{
        //    get { return _speed; }
        //    set { _speed = value; }
        //}

        //public double Altitude
        //{
        //    get { return _altitude; }
        //    set { _altitude = value; }
        //}

        //public double Roll
        //{
        //    get { return _roll; }
        //    set { _roll = value; }
        //}

        //public double Pitch
        //{
        //    get { return _pitch; }
        //    set { _pitch = value; }
        //}
        //public double Yaw
        //{
        //    get { return _yaw; }
        //    set { _yaw = value; }
        //}

        //public double HeadingMagN
        //{
        //    get { return _headingMagneticNorth; }
        //    set { _headingMagneticNorth = value; }
        //}
        #endregion

        public HUD(int vW, int vH, TelemetryDataObject dataObject)
        {
            int fSize;
            
            _videoWidth = vW;
            _videoHeight = vH;

            _dataObject = dataObject;

            // scale the font size in some portion to the video image
            fSize = (vW / 32);
            _fontOverlay = new Font("Tahoma", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // scale the font size in some portion to the video image
            _transparentFont = new Font("Tahoma", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            _transparentBrush = new SolidBrush(Color.FromArgb(255, 0, 255, 0));
            
            Trace.WriteLine("HUD Contructor: object created");
        }


        public void Dispose()
        {
            _fontOverlay.Dispose();
            _transparentBrush.Dispose();
            _transparentFont.Dispose();
        }

        public void DrawHUD(Bitmap src, Bitmap dst)
        {
            Graphics g;
            String s;

            //Initialize Graphics
            g = Graphics.FromImage(src);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; //.AntiAliasGridFit;

            //Select Pen, Brush, Fonts, Font Types
            Pen myPen = new Pen(Color.FromArgb(255, 0, 255, 0), 1);
            Pen myPen1 = new Pen(Color.FromArgb(255, 0, 255, 0), 1);
            myPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            SolidBrush myBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0)); //FromArgb(0, 0, 0, 0));
            // Construct 2 new StringFormat objects
            StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
            StringFormat format2 = new StringFormat(StringFormatFlags.NoClip);
            // Set the LineAlignment and Alignment properties for
            // both StringFormat objects to different values.
            format1.LineAlignment = StringAlignment.Near;
            format1.Alignment = StringAlignment.Center;
            format2.LineAlignment = StringAlignment.Near;
            format2.Alignment = StringAlignment.Near;

            // Draw top and bottom black bands
            Rectangle[] rectangleTextBkg = new Rectangle[2];
            rectangleTextBkg[0] = new Rectangle(0, 0, _videoWidth, 15);
            rectangleTextBkg[1] = new Rectangle(0, _videoHeight - 15, _videoWidth, _videoHeight);
            g.FillRectangles(myBrush, rectangleTextBkg);
            s = DateTime.Now.ToString();
            s += " Session ID: " + _dataObject.Altitude.ToString();
            g.DrawString(s, _fontOverlay, _transparentBrush, (RectangleF)rectangleTextBkg[1], format2);

            // Draw numeric boxes for:
            Rectangle[] rectangleHUD = new Rectangle[3];
            // 1. Heading
            rectangleHUD[0] = new Rectangle((_videoWidth / 2) - 25, 30, 50, 15);
            // 2. Altitude
            rectangleHUD[1] = new Rectangle(_videoWidth - 95, (_videoHeight / 2) - 8, 50, 15);
            // 3. Speed
            rectangleHUD[2] = new Rectangle(45, (_videoHeight / 2) - 8, 50, 15);
            g.DrawRectangles(myPen, rectangleHUD);
            
            //Draw numeric values inside each box:
            // 1. Heading
            g.DrawString(_dataObject.HeadingMagN.ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectangleHUD[0], format1);
            // 2. Altitude
            g.DrawString(_dataObject.Altitude.ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectangleHUD[1], format1);
            //3. Speed
            g.DrawString(_dataObject.Speed.ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectangleHUD[2], format1);
            

            //int radius = 150;
            //Rectangle rectangleRoll = new Rectangle(VideoWidth / 2 - radius, VideoHeight / 2 - radius, 2 * radius, 2 * radius);
            //g.DrawArc(myPen, rectangleRoll, 230.0f, 80.0f);

            //Draw real hud
            Point[] pointsHUD = new Point[2];
            Point[] pointsHUD1 = new Point[3];
            Point[] pointsHUD2 = new Point[7];
            Rectangle[] rectangleHUD1 = new Rectangle[4];

            //TO DO: add as private variable and allow configuration with parameters
            int pitch_resolution = 10;
            int yaw_resolution = 10;
            int reticlesize = 20;

            pointsHUD2[0].X = _videoWidth / 2 - 20;
            pointsHUD2[0].Y = _videoHeight / 2;
            pointsHUD2[1].X = _videoWidth / 2 - 10;
            pointsHUD2[1].Y = _videoHeight / 2;
            pointsHUD2[2].X = _videoWidth / 2 - 5;
            pointsHUD2[2].Y = _videoHeight / 2 + 10;
            pointsHUD2[3].X = _videoWidth / 2;
            pointsHUD2[3].Y = _videoHeight / 2;
            pointsHUD2[4].X = _videoWidth / 2 + 5;
            pointsHUD2[4].Y = _videoHeight / 2 + 10;
            pointsHUD2[5].X = _videoWidth / 2 + 10;
            pointsHUD2[5].Y = _videoHeight / 2;
            pointsHUD2[6].X = _videoWidth / 2 + 20;
            pointsHUD2[6].Y = _videoHeight / 2;
            g.DrawLines(myPen, pointsHUD2);


            double pitch = 0;
            pitch = _dataObject.Pitch;
            if (pitch >= 90)
                pitch = 180 - pitch;
            if (pitch <= -90)
                pitch = - 180 - pitch;

            GraphicsState oldState = g.Save(); // save existing matrices
            //yaw
            g.TranslateTransform((float)_dataObject.Yaw * yaw_resolution, 0, MatrixOrder.Append);
            // make rotation point the origin
            //pitch
            //g.TranslateTransform(0, (float)DataObject.Pitch * pitch_resolution, MatrixOrder.Append);
            g.TranslateTransform(0, (float)pitch * pitch_resolution, MatrixOrder.Append);
            if (_dataObject.Pitch >= 90 || _dataObject.Pitch <= -90)
                g.RotateTransform((float)180, MatrixOrder.Append);

            // roll 
            g.TranslateTransform(-_videoWidth / 2, -_videoHeight / 2);
            g.RotateTransform((float)-_dataObject.Roll, MatrixOrder.Append);
            // translate back
            g.TranslateTransform(_videoWidth / 2, _videoHeight / 2, MatrixOrder.Append);

            
            //Rectangle rectangleObj = new Rectangle(VideoWidth / 2 - reticlesize / 2, VideoHeight / 2 - reticlesize / 2, reticlesize, reticlesize);
            //g.DrawEllipse(myPen, rectangleObj);  
            
            //DrawHUD horizont line
            pointsHUD[0].X = _videoWidth / 2 - 150;
            pointsHUD[0].Y = _videoHeight / 2;
            pointsHUD[1].X = _videoWidth / 2 - 25;
            pointsHUD[1].Y = _videoHeight / 2;
            g.DrawLines(myPen, pointsHUD);
            pointsHUD[0].X = _videoWidth / 2 + 150;
            pointsHUD[0].Y = _videoHeight / 2;
            pointsHUD[1].X = _videoWidth / 2 + 25;
            pointsHUD[1].Y = _videoHeight / 2;
            g.DrawLines(myPen, pointsHUD);
            
            for (int i = 1; i <= 36; i++)
            {
                pointsHUD1[0].X = _videoWidth / 2 - 75;
                pointsHUD1[0].Y = _videoHeight / 2 + pitch_resolution * 10 * i - 10;
                pointsHUD1[1].X = _videoWidth / 2 - 75;
                pointsHUD1[1].Y = _videoHeight / 2 + pitch_resolution * 10 * i;
                pointsHUD1[2].X = _videoWidth / 2 - 25;
                pointsHUD1[2].Y = _videoHeight / 2 + pitch_resolution * 10 * i;
                g.DrawLines(myPen1, pointsHUD1);
                pointsHUD1[0].X = _videoWidth / 2 + 75;
                pointsHUD1[0].Y = _videoHeight / 2 + pitch_resolution * 10 * i - 10;
                pointsHUD1[1].X = _videoWidth / 2 + 75;
                pointsHUD1[1].Y = _videoHeight / 2 + pitch_resolution * 10 * i;
                pointsHUD1[2].X = _videoWidth / 2 + 25;
                pointsHUD1[2].Y = _videoHeight / 2 + pitch_resolution * 10 * i;
                g.DrawLines(myPen1, pointsHUD1);

                pointsHUD1[0].X = _videoWidth / 2 - 75;
                pointsHUD1[0].Y = _videoHeight / 2 - pitch_resolution * 10 * i + 10;
                pointsHUD1[1].X = _videoWidth / 2 - 75;
                pointsHUD1[1].Y = _videoHeight / 2 - pitch_resolution * 10 * i;
                pointsHUD1[2].X = _videoWidth / 2 - 25;
                pointsHUD1[2].Y = _videoHeight / 2 - pitch_resolution * 10 * i;
                g.DrawLines(myPen, pointsHUD1);
                pointsHUD1[0].X = _videoWidth / 2 + 75;
                pointsHUD1[0].Y = _videoHeight / 2 - pitch_resolution * 10 * i + 10;
                pointsHUD1[1].X = _videoWidth / 2 + 75;
                pointsHUD1[1].Y = _videoHeight / 2 - pitch_resolution * 10 * i;
                pointsHUD1[2].X = _videoWidth / 2 + 25;
                pointsHUD1[2].Y = _videoHeight / 2 - pitch_resolution * 10 * i;
                g.DrawLines(myPen, pointsHUD1);

                // Draw numeric boxes
                Rectangle[] rectanglePitch = new Rectangle[4];
                rectanglePitch[0] = new Rectangle(_videoWidth / 2 - 125, _videoHeight / 2 + pitch_resolution * 10 * i - 15, 50, 15);
                rectanglePitch[1] = new Rectangle(_videoWidth / 2 + 75, _videoHeight / 2 + pitch_resolution * 10 * i - 15, 50, 15);

                g.DrawString((-i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectanglePitch[0], format1);
                g.DrawString((-i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectanglePitch[1], format1);

                rectanglePitch[2] = new Rectangle(_videoWidth / 2 - 125, _videoHeight / 2 - pitch_resolution * 10 * i, 50, 15);
                rectanglePitch[3] = new Rectangle(_videoWidth / 2 + 75, _videoHeight / 2 - pitch_resolution * 10 * i, 50, 15);

                g.DrawString((i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectanglePitch[2], format1);
                g.DrawString((i * 10).ToString(), _fontOverlay, _transparentBrush, (RectangleF)rectanglePitch[3], format1);
            }
            g.Restore(oldState); // restore old

            oldState = g.Save(); // save existing matrices
            // make rotation point the origin
            g.TranslateTransform(-_videoWidth / 2, -_videoHeight + 100);
            // roll 
            g.RotateTransform((float)_dataObject.Roll, MatrixOrder.Append);
            // translate back
            g.TranslateTransform(_videoWidth / 2, _videoHeight - 100, MatrixOrder.Append);

            Rectangle rectangleObj = new Rectangle(_videoWidth / 2 - reticlesize / 2, _videoHeight - 100 - reticlesize / 2, reticlesize, reticlesize);
            g.DrawEllipse(myPen, rectangleObj);

            pointsHUD[0].X = _videoWidth / 2 - 50;
            pointsHUD[0].Y = _videoHeight - 100;
            pointsHUD[1].X = _videoWidth / 2 - 20;
            pointsHUD[1].Y = _videoHeight - 100;
            g.DrawLines(myPen, pointsHUD);
            pointsHUD[0].X = _videoWidth / 2 + 50;
            pointsHUD[0].Y = _videoHeight - 100;
            pointsHUD[1].X = _videoWidth / 2 + 20;
            pointsHUD[1].Y = _videoHeight - 100;
            g.DrawLines(myPen, pointsHUD);
            pointsHUD[0].X = _videoWidth / 2 ;
            pointsHUD[0].Y = _videoHeight - 100 - reticlesize;
            pointsHUD[1].X = _videoWidth / 2 ;
            pointsHUD[1].Y = _videoHeight - 100;
            g.DrawLines(myPen, pointsHUD);
            g.Dispose();
            
            src.RotateFlip(RotateFlipType.RotateNoneFlipY);
            
            g = Graphics.FromImage(dst);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // draw the overlay bitmap over the video's bitmap
            g.DrawImage(src, 0, 0, src.Width, src.Height);
            g.Dispose();
        }
    }
}
