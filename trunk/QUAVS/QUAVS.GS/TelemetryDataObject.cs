
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QUAVS.GS
{
    /// <summary>
    /// 
    /// </summary>
    public class TelemetryDataObject : IObservable
    {
        #region Privates

        private List<IObserver> _observer;

        private double _accel_x = 0;
        private double _accel_y = 0;
        private double _accel_z = 0;
        private double _gyro_x = 0;
        private double _gyro_y = 0;
        private double _gyro_z = 0;
        private double _speed_x = 0;
        private double _speed_y = 0;
        private double _speed_z = 0;
        private double _altitude = 0;
        private double _latitude = 0;
        private double _longitude = 0;
        private double _headingMagneticNorth = 0;
        private double _roll = 0;
        private double _pitch = 0;
        private double _yaw = 0;
        
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
            get {return _speed_x;}
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
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryDataObject"/> class.
        /// </summary>
        public TelemetryDataObject()
        {
            _observer = new List<IObserver>();
            Trace.WriteLine("TelemetryDataObject Constructor: Object created");
        }
        #endregion

        #region Interface Methods

        public void RemoveObserver(IObserver observer)
        {
            int i = _observer.IndexOf(observer);
            if (i >= 0)
            {
                _observer.Remove(observer);
                Debug.WriteLine("TelemetryDataObject Observer removed: " + observer.ToString());
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            this._observer.Add(observer);
            Debug.WriteLine("TelemetryDataObject Observer added: " + observer.ToString());
        }

        public void NotifyObservers()
        {
            if (_observer.Count > 0)
            {
                // update every observer here!
                foreach (IObserver observer in this._observer)
                {
                    observer.UpdateObserver(this);
                }

                Debug.WriteLine("TelemetryDataObject NotifyingObservers: " + _observer.Count.ToString());
            }
            else
                Trace.WriteLine("TelemetryDataObject WARNING: No Observers to NOTIFY!");
        }
        #endregion
    }
}

