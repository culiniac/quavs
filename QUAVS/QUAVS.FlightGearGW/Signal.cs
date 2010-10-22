using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QUAVS.FlightGearGW
{
    class Signal
    {
        #region Privates
        private float _value;
        private float _min;
        private float _max;
        private float _normalized;
        #endregion


        #region Properties 
        public float Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                _normalized = value;
                if (_min < _max)
                {
                    if (_normalized < _min) _normalized = _min;
                    else if (_normalized > _max) _normalized = _max;
                }
            }
        }
        public float Min
        {
            get { return _min; }
            set { _min = value; }
        }
        public float Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public float Normalized
        {
            get { return _normalized; }
        }
        #endregion

        #region Constructurs
        public Signal(float value, float min, float max)
        {
            _min = min;
            _max = max;
            this.Value = value;
        }

        public Signal(float value)
        {
            _value = value;
            _min = 0;
            _max = 0;
            _normalized = value;
        }

        public Signal()
        {
            _value = 0;
            _min = 0;
            _max = 0;
            _normalized = 0;
        }
        #endregion
    }
}