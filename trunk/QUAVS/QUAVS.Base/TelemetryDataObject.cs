using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QUAVS.Base
{
    public struct TelemetryData
    {
        #region Privates

        public double _accel_x;
        public double _accel_y;
        public double _accel_z;
        public double _gyro_x;
        public double _gyro_y;
        public double _gyro_z;
        public double _speed_x;
        public double _speed_y;
        public double _speed_z;
        public double _altitude;
        public double _latitude;
        public double _longitude;
        public double _headingMagneticNorth;
        public double _roll;
        public double _pitch;
        public double _yaw;

        #endregion

        #region Properties
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
        public double SpeedX
        {
            get { return _speed_x; }
            set { _speed_x = value; }
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public double SpeedY
        {
            get { return _speed_y; }
            set { _speed_y = value; }
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public double SpeedZ
        {
            get { return _speed_z; }
            set { _speed_z = value; }
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
    }
    /// <summary>
    /// 
    /// </summary>
    public class TelemetryDataObject
    {
        #region Public Members

        private TelemetryData _telemetry = new TelemetryData();
       
        #endregion
        
        #region Properties
        
        public TelemetryData Telemetry
        {
            get { return _telemetry; }
            set 
            { 
                _telemetry = value;
                TelemetryDataChanged(_telemetry);
                Debug.WriteLine("Telemetry data changed", "TelemetryDataObject");
            }
        }

        #endregion
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryDataObject"/> class.
        /// </summary>
        public TelemetryDataObject()
        {
            Trace.TraceInformation("{0} : TelemetryDataObject Constructor : Object created", DateTime.Now.ToString("[d/M/yyyy HH:mm:ss.fff]"));
        }
        #endregion

        public delegate void TelemetryDataDelegate(TelemetryData data);
        public event TelemetryDataDelegate TelemetryDataChanged;

    }
}

