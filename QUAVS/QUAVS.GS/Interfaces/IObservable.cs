using System;
using System.Collections.Generic;
using System.Text;

namespace QUAVS.GS
{
    public interface IObservable
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }
}
