using System;
using System.Collections.Generic;
using System.Text;

namespace QUAVS.Base
{
    public class UAV
    {
        private int _uavID;

        public int UavID
        {
            get { return _uavID; }
            set { _uavID = value; }
        }
    
        public UAV()
        {
            throw new System.NotImplementedException();
        }
    }
}
