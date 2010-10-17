using System;
using System.Collections.Generic;
using System.Text;

namespace QUAVS.GS
{
    public interface IObserver
    {
        void UpdateObserver(TelemetryDataObject dataObject);
        //void DisplayData();
    }
}
