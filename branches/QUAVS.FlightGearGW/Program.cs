using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using QUAVS.Base;

namespace QUAVS.FlightGearGW
{
    class Program
    {
        //telemetry index
        public const int ROLL = 0;
        public const int PITCH = 1;
        public const int ALPHA = 2;
        public const int HEADING = 3;
        public const int ROLL_RATE = 4;
        public const int PITCH_RATE = 5;
        public const int YAW_RATE = 6;
        public const int X_ACCEL = 7;
        public const int Y_ACCEL = 8;
        public const int Z_ACCEL = 9;
        public const int ALT = 10;
        public const int LAT = 11;
        public const int LONG = 12;
        public const int AIRSPEED = 13;

        //number of telemetry variables
        public const int MAX_SIGNALS = 14;

        //The main socket on which the server listens to the clients
        static Socket serverSocket;

        //create an Serial Port object
        static SerialPort _sp;
        //Error handling
        static SerialError _spErr;

        static byte[] byteData = new byte[56];
        static List<byte> bBuffer = new List<byte>();
        //static QUAVS_Telemetry teledata = new QUAVS_Telemetry();
        static Queue<byte[]> messages = new Queue<byte[]>();

        static bool running;

        static Signal[] telemetry = new Signal[MAX_SIGNALS];


        static void Main(string[] args)
        {

            try
            {
                _sp = new SerialPort("COM5");
                InitializeComPort();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            for (int i = 0; i < MAX_SIGNALS; i++)
                telemetry[i] = new Signal();

            try
            {
                Thread procMsg = new Thread(new ThreadStart(process));
                running = true;

                //We are using UDP sockets
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //Assign the any IP of the machine and listen on port number 5000
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 5000);
                //Bind this address to the server
                serverSocket.Bind(ipEndPoint);

                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                //The epSender identifies the incoming clients
                EndPoint epSender = (EndPoint)ipeSender;
                //Start receiving data
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender, new AsyncCallback(OnReceive), epSender);
                Console.WriteLine("Awaiting data...");
                procMsg.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
            running = false;

        }

        static void OnReceive(IAsyncResult ar)
        {
            try
            {
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;
                int len = serverSocket.EndReceiveFrom(ar, ref epSender);
                
                Console.WriteLine(len.ToString());
                Console.WriteLine(byteData.Length.ToString());
                
                messages.Enqueue(byteData);

                //Start receiving again
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender, new AsyncCallback(OnReceive), epSender);
                Console.WriteLine("Awaiting data...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void OnSend(IAsyncResult ar)
        {
            try
            {
                serverSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void process()
        {
            byte[] data;
            byte[] comm = new byte[64];
            byte _checksum_a = 0;
            byte _checksum_b = 0;
            byte[] tmp = new byte[4];
            
            comm[0] = 0x51;
            comm[1] = 0x2A;
            comm[2] = 0x01;
            comm[3] = 0x01;
            comm[4] = 56;
            comm[5] = 0;

            while (running)
            {
                if (messages.Count > 0)
                {
                    data = messages.Dequeue();
                    for (int i = 0; i < MAX_SIGNALS; i++)
                    {
                        
                        int x = i * 4;

                        //FG2.0 network order - float 4 bytes in network order
                        tmp[3] = data[x];
                        tmp[2] = data[x+1];
                        tmp[1] = data[x+2];
                        tmp[0] = data[x+3];
                        
                        telemetry[i].Value = BitConverter.ToSingle(tmp,0);
                    }

                    data.CopyTo(comm, 6);
                    
                    for (int i = 2; i < 62; i++)
                    {
                        _checksum_a += comm[i];
                        _checksum_b += _checksum_a;
                    }

                    comm[62] = _checksum_a;
                    comm[63] = _checksum_b;

                    _sp.Write(comm, 0, 64);

                    _checksum_a = 0;
                    _checksum_b = 0;
                    Console.WriteLine("Data sent...");
                    
                }
            }
        }

        static public void InitializeComPort()
        {
            try
            {
                //open the COM port
                _sp.BaudRate = 57600;
                _sp.Open();
                Console.WriteLine("SerialCom: " + _sp.PortName + " opened.");
            }
            catch (IOException ioex)
            {
                Console.WriteLine("Error opening device:" + ioex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing device:" + ex.Message);
            }
            finally
            {
                //Bind the events on the following event handler
                _sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                _sp.ErrorReceived += new SerialErrorReceivedEventHandler(sp_ErrorReceived);
            }
        }

        //Serial Port Error Event Handler
        static private void sp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _spErr = e.EventType;
        }

        //Serial Port DataReceived Handler
        static private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read all the data waiting in the buffer
            try
            {
                Console.WriteLine("Message received: " + _sp.BytesToRead + " bytes");
                // Buffer and process binary data
                while (_sp.BytesToRead > 0)
                    bBuffer.Add((byte)_sp.ReadByte());
                ProcessBuffer(bBuffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static private void ProcessBuffer(List<byte> bBuffer)
        {
            if (bBuffer.Count > (MAX_SIGNALS + 1) * 8)
            {
                // do processing on receive
            }

        }
    }

    
}
