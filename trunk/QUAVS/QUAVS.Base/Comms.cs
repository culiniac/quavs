using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections;

namespace QUAVS.Base
{
    public abstract class Comms<T> : IDisposable
    {
        //create an Serial Port object
        protected SerialPort _sp; 
        //Error handling
        protected SerialError _spErr;
        protected Queue<byte> _recievedData = new Queue<byte>();

        private T _object;
        
        #region Constructor

        public Comms(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, T o)
        {
            _sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            _sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            _sp.ErrorReceived += new SerialErrorReceivedEventHandler(sp_ErrorReceived);

            _object = o;
            Trace.WriteLine("Constructor: SerialCom", "TelemetryComms");
        }

        public Comms()
        {
            _sp = new SerialPort();
            Trace.WriteLine("Constructor: SerialCom", "TelemetryComms");
        }

        public Comms(string portName)
        {
            _sp = new SerialPort(portName);
            Trace.WriteLine("Constructor: SerialCom", "TelemetryComms");
        }
        #endregion

        public void InitializeComPort()
        {
            try
            {
                //open the COM port
                _sp.Open();
                Trace.WriteLine("SerialCom: " + _sp.PortName + " opened)", "TelemetryComms");

            }
            catch (IOException ioex)
            {
                TraceException.WriteLine(ioex);
            }
            catch (Exception ex)
            {
                TraceException.WriteLine(ex);
            }
        }

        //Serial Port Error Event Handler
        private void sp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _spErr = e.EventType;
        }

        //Serial Port DataReceived Handler
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] data = new byte[_sp.BytesToRead];
                int ret = _sp.Read(data, 0, data.Length);

                for (int i = 0; i < ret; i++)
                {
                    _recievedData.Enqueue(data[i]);
                }

                ProcessData();
            }
            catch (Exception ex)
            {
                TraceException.WriteLine(ex);
            }
        }

        protected abstract void ProcessData();
        
        public void Dispose()
        {
            if (_sp != null)
                _sp.Dispose();
        }
    }
}
