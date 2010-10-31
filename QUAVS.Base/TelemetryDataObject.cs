using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QUAVS.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class TelemetryData
    {
        #region Privates

        private float[] _telemetry = new float[MAX_SIGNALS];

        #endregion

        #region Properties

        public const int ROLL = 0;//private float _roll;
        public const int PITCH = 1;//private float _pitch;
        public const int YAW = 2;//private float _yaw;
        public const int HEADING = 3;//private float _headingMagneticNorth;
        public const int ROLL_RATE = 4;//private float _gyro_x;
        public const int PITCH_RATE = 5;//private float _gyro_y;
        public const int YAW_RATE = 6; //private float _gyro_z;
        public const int ACCEL_X = 7;//private float _accel_x;
        public const int ACCEL_Y = 8;//private float _accel_y;
        public const int ACCEL_Z = 9;//private float _accel_z;
        public const int ALTITUDE = 10;//private float _altitude;
        public const int LATITUDE = 11; //private float _latitude;
        public const int LONGITUDE = 12;//private float _longitude;
        public const int SPEED_X = 13;//private float _speed_x;
        public const int SPEED_Y = 14;//private float _speed_y;
        public const int SPEED_Z = 15; //private float _speed_z;

        //number of Telemetry variables
        public const int MAX_SIGNALS = 16;

        /// <summary>
        /// Gets or sets the telemetry.
        /// </summary>
        /// <value>The telemetry.</value>
        public float[] Telemetry
        {
            get { return _telemetry; }
            set { _telemetry = value; }
        }
        
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public float Longitude
        {
            get { return _telemetry[LONGITUDE]; }
            set { _telemetry[LONGITUDE] = value; }
        }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public float Latitude
        {
            get { return _telemetry[LATITUDE]; }
            set { _telemetry[LATITUDE] = value; }
        }
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public float SpeedX
        {
            get { return _telemetry[SPEED_X]; }
            set { _telemetry[SPEED_X] = value; }
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public float SpeedY
        {
            get { return _telemetry[SPEED_Y]; }
            set { _telemetry[SPEED_Y] = value; }
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public float SpeedZ
        {
            get { return _telemetry[SPEED_Z]; }
            set { _telemetry[SPEED_Z] = value; }
        }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        public float Altitude
        {
            get { return _telemetry[ALTITUDE]; }
            set { _telemetry[ALTITUDE] = value; }
        }

        /// <summary>
        /// Gets or sets the roll.
        /// </summary>
        /// <value>The roll.</value>
        public float Roll
        {
            get { return _telemetry[ROLL]; }
            set { _telemetry[ROLL] = value; }
        }

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>The pitch.</value>
        public float Pitch
        {
            get { return _telemetry[PITCH]; }
            set { _telemetry[PITCH] = value; }
        }
        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>The yaw.</value>
        public float Yaw
        {
            get { return _telemetry[YAW]; }
            set { _telemetry[YAW] = value; }
        }

        /// <summary>
        /// Gets or sets the heading mag N.
        /// </summary>
        /// <value>The heading mag N.</value>
        public float HeadingMagN
        {
            get { return _telemetry[HEADING]; }
            set { _telemetry[HEADING] = value; }
        }

        /// <summary>
        /// Gets or sets the accel X.
        /// </summary>
        /// <value>The accel X.</value>
        public float AccelX
        {
            get { return _telemetry[ACCEL_X]; }
            set { _telemetry[ACCEL_X] = value; }
        }

        /// <summary>
        /// Gets or sets the accel Y.
        /// </summary>
        /// <value>The accel Y.</value>
        public float AccelY
        {
            get { return _telemetry[ACCEL_Y]; }
            set { _telemetry[ACCEL_Y] = value; }
        }

        /// <summary>
        /// Gets or sets the accel Z.
        /// </summary>
        /// <value>The accel Z.</value>
        public float AccelZ
        {
            get { return _telemetry[ACCEL_Z]; }
            set { _telemetry[ACCEL_Z] = value; }
        }

        /// <summary>
        /// Gets or sets the gyro X.
        /// </summary>
        /// <value>The gyro X.</value>
        public float RollRate
        {
            get { return _telemetry[ROLL_RATE]; }
            set { _telemetry[ROLL_RATE] = value; }
        }

        /// <summary>
        /// Gets or sets the gyro Y.
        /// </summary>
        /// <value>The gyro Y.</value>
        public float PitchRate
        {
            get { return _telemetry[PITCH_RATE]; }
            set { _telemetry[PITCH_RATE] = value; }
        }

        /// <summary>
        /// Gets or sets the gyro Z.
        /// </summary>
        /// <value>The gyro Z.</value>
        public float YawRate
        {
            get { return _telemetry[YAW_RATE]; }
            set { _telemetry[YAW_RATE] = value; }
        }

        #endregion

        #region members
        public byte[] GetBytes()
        {
            ByteAppend<byte> fcTemp = new ByteAppend<byte>();

            for (int i = 0; i < MAX_SIGNALS; i++)
            {
                fcTemp.Add(BitConverter.GetBytes(_telemetry[i]));
            }
            return fcTemp.ToArray();
        }

        public byte[] GetBytes(bool bigEndian)
        {
            //ByteAppend<byte> fcTemp = new ByteAppend<byte>();

            //for (int i = 0; i < MAX_SIGNALS; i++)
            //{
            //    fcTemp.Add(BitConverter.GetBytes(_telemetry[i]));
            //}
            //return fcTemp.ToArray();
            
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class TelemetryDataObject
    {
        #region private Members

        private TelemetryData _telemetryData = new TelemetryData();

        #endregion
       
        #region Properties

        /// <summary>
        /// Gets or sets the Telemetry data.
        /// </summary>
        /// <value>The Telemetry data.</value>
        public TelemetryData TelemetryData
        {
            get
            {
                return _telemetryData;
            }
            set
            {
                _telemetryData = value;
                TelemetryDataChanged(TelemetryData);
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

        /// <summary>
        /// 
        /// </summary>
        public delegate void TelemetryDataDelegate(TelemetryData data);
        /// <summary>
        /// Occurs when [Telemetry data changed].
        /// </summary>
        public event TelemetryDataDelegate TelemetryDataChanged;

    }
}

