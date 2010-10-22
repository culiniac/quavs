﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.IO;
using System.IO.Ports;

namespace QUAVS.Base
{
    enum TelemetryClass : byte
    {
        ACK = 0x01,
        CMD = 0x02,
        INS = 0x03,
        GPS = 0x04
    }

    enum TelemetryACK : byte
    {
        ACK_NAK = 0x00,
        ACK_ACK = 0x01
    }

    enum TelemetryCMD : byte
    {
        CMD_ = 0x00
    }

    enum TelemetryINS : byte
    {
        INS_ = 0x00
    }


    public class TelemetryComms : Comms
    {
        private TelemetryDataObject _data = new TelemetryDataObject();
        private TelemetryData _dataTelemetry = new TelemetryData();

        public TelemetryDataObject Data
        {
            get { return _data; }
            set { _data = value; }
        }


        private int _stateMachine = 0;

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

        static double[] telemetry = new double[MAX_SIGNALS];

        static private int buffer_len = 56;
        
        byte[] QBP_buffer = new byte[buffer_len];

        private byte _checksum_a = 0;
        private byte _checksum_b = 0;

        byte QBP_class = 0;
        byte QBP_message_id = 0;
        byte QBP_payload_length_hi = 0;
        byte QBP_payload_length_lo = 0;
        byte QBP_payload_counter = 0;
        byte QBP_checksum_a = 0;
        byte QBP_checksum_b = 0;

        public TelemetryComms(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
            : base(portName, baudRate, parity, dataBits, stopBits)
        {
        }

        public TelemetryComms(string portName)
            :base(portName)
        {
        }

        protected override void ProcessData()
        {
            try
            {
                lock (_recievedData)
                {
                    IEnumerator<byte> e = _recievedData.GetEnumerator();
                    while (e.MoveNext())
                    {
                        TelemetryCommsStateMachine(e.Current);
                    }
                    _recievedData.Clear();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private void TelemetryCommsStateMachine(byte data)
        {
            switch(_stateMachine)
            {
                case 0:
                    if (data == 0x51)
                    {
                        _stateMachine++;
                    }
                    break;
                case 1:
                    if(data == 0x2A)
                        _stateMachine++;
                    else
                        _stateMachine = 0;
                    break;
                case 2:
                    QBP_class = data;
                    checksum(data);
                    _stateMachine++;
                    break;
                case 3:
                    QBP_message_id = data;
                    checksum(data);
                    _stateMachine++;
                    break;
                case 4:
                    QBP_payload_length_hi = data;
                    if (QBP_payload_length_hi > buffer_len) //bad data
                    {
                        _stateMachine = 0;
                        QBP_payload_counter = 0;
                        _checksum_a = 0;
                        _checksum_a = 0;
                    }
                    checksum(data);
                    _stateMachine++;
                    break;
                case 5:
                    QBP_payload_length_lo = data;
                    checksum(data);
                    _stateMachine++;
                    break;
                case 6:
                    QBP_buffer[QBP_payload_counter] = data;
                    checksum(data);
                    QBP_payload_counter++;
                    if (QBP_payload_counter < QBP_payload_length_hi)
                    {
                        //do nothing until all data is received
                    }
                    else
                    {
                        _stateMachine++; 
                    }
                    break;
                case 7:
                    QBP_checksum_a = data;
                    _stateMachine++;
                    break;
                case 8:
                    QBP_checksum_b = data;
                    if ((_checksum_a == QBP_checksum_a) && (_checksum_b == QBP_checksum_b))
                    {
                        parseMessage();
                    }
                    else
                    {
                        Trace.WriteLine("Checksum error");
                    }

                    // Done one message
                    _stateMachine = 0;
                    QBP_payload_counter = 0;
                    _checksum_a = 0;
                    _checksum_b = 0;
                    break;
            }
        }

        private void parseMessage()
        {
            switch (QBP_class)
            {
                case 0x01:
                    switch (QBP_message_id)
                    {
                        case 0x01:
                            byte[] tmp = new byte[4];
                            for (int i = 0; i < MAX_SIGNALS; i++)
                            {

                                int x = i * 4;

                                //FG2.0 network order - float 4 bytes in network order
                                tmp[3] = QBP_buffer[x];
                                tmp[2] = QBP_buffer[x + 1];
                                tmp[1] = QBP_buffer[x + 2];
                                tmp[0] = QBP_buffer[x + 3];

                                telemetry[i] = BitConverter.ToSingle(tmp, 0);
                            }
                            _dataTelemetry.AccelX = telemetry[X_ACCEL];
                            _dataTelemetry.AccelY = telemetry[Y_ACCEL];
                            _dataTelemetry.AccelZ = telemetry[Z_ACCEL];
                            //public const int ROLL_RATE = 4;
                            //public const int PITCH_RATE = 5;
                            //public const int YAW_RATE = 6;
                            _dataTelemetry.Roll = telemetry[ROLL];
                            _dataTelemetry.Pitch = telemetry[PITCH];
                            _dataTelemetry.SpeedX = telemetry[AIRSPEED]; //I need to map it like this for now.
                            _dataTelemetry.Latitude = telemetry[LAT];
                            _dataTelemetry.Longitude = telemetry[LONG];
                            _dataTelemetry.HeadingMagN = telemetry[HEADING];
                            _dataTelemetry.Altitude = telemetry[ALT];
                            _dataTelemetry.Yaw = 0; // telemetry[ALPHA];
                            
                            _data.TelemetryData = _dataTelemetry;

                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            
        }

        void checksum(byte data)
        {
            _checksum_a += data;
            _checksum_b += _checksum_a;
        }
    }
}
