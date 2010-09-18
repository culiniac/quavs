
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QUAVS.Base
{
    //TODO: Documentation
    public class TelemetryDataObject
    {
#region Privates

        /// <summary>
        /// 
        /// </summary>
        private double _accel_x;
        private double _accel_y;
        private double _accel_z;
        private double _gyro_x;
        private double _gyro_y;
        private double _gyro_z;
        private double _speed;
        private double _altitude;
        private double _latitude;
        private double _longitude;
        private double _headingMagneticNorth;
        private double _roll;
        private double _pitch;
        private double _yaw;
        private string _message;

#endregion

#region Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get {return _message;}
            set { _message = value; }
        }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public double Speed
        {
            get {return _speed;}
            set { _speed = value; }
        }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        public double Altitude
        {
            get { return _altitude; }
            set { _altitude = value; }
        }

        /// <summary>
        /// Gets or sets the roll.
        /// </summary>
        /// <value>The roll.</value>
        public double Roll
        {
            get { return _roll; }
            set { _roll = value; }
        }

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>The pitch.</value>
        public double Pitch
        {
            get { return _pitch; }
            set { _pitch = value; }
        }
        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>The yaw.</value>
        public double Yaw
        {
            get { return _yaw; }
            set { _yaw = value; }
        }

        /// <summary>
        /// Gets or sets the heading mag N.
        /// </summary>
        /// <value>The heading mag N.</value>
        public double HeadingMagN
        {
            get { return _headingMagneticNorth; }
            set { _headingMagneticNorth = value; }
        }

        /// <summary>
        /// Gets or sets the accel X.
        /// </summary>
        /// <value>The accel X.</value>
        public double AccelX
        {
            get { return _accel_x; }
            set { _accel_x = value; }
        }

        /// <summary>
        /// Gets or sets the accel Y.
        /// </summary>
        /// <value>The accel Y.</value>
        public double AccelY
        {
            get { return _accel_y; }
            set { _accel_y = value; }
        }

        /// <summary>
        /// Gets or sets the accel Z.
        /// </summary>
        /// <value>The accel Z.</value>
        public double AccelZ
        {
            get { return _accel_z; }
            set { _accel_z = value; }
        }

        /// <summary>
        /// Gets or sets the gyro X.
        /// </summary>
        /// <value>The gyro X.</value>
        public double GyroX
        {
            get { return _gyro_x; }
            set { _gyro_x = value; }
        }

        /// <summary>
        /// Gets or sets the gyro Y.
        /// </summary>
        /// <value>The gyro Y.</value>
        public double GyroY
        {
            get { return _gyro_y; }
            set { _gyro_y = value; }
        }

        /// <summary>
        /// Gets or sets the gyro Z.
        /// </summary>
        /// <value>The gyro Z.</value>
        public double GyroZ
        {
            get { return _gyro_z; }
            set { _gyro_z = value; }
        }

#endregion

#region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryDataObject"/> class.
        /// </summary>
        public TelemetryDataObject()
        {
            Trace.WriteLine("TelemetryDataObject Constructor: Object created");
        }

#endregion
    }
}

