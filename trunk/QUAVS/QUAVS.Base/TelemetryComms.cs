using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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


    class TelemetryComms : Comms<TelemetryDataObject>
    {
        private TelemetryData _telemetryData = new TelemetryData();
        private int _stateMachine = 0;

        static private int buffer_len = 1024;
        
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
                    if(data == 0x51)
                        _stateMachine++;
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
                    if ((_checksum_a == QBP_checksum_a) && (_checksum_b == QBP_checksum_a))
                    { 
                        //parseTelemetryData();							 
                    }

                    // Done one message
                    _stateMachine = 0;
                    QBP_payload_counter = 0;
                    _checksum_a = 0;
                    _checksum_a = 0;
                    break;
            }
        }

        private void parseTelemetryData()
        {
            switch (QBP_class)
            {
                case 0x01:
                    switch (QBP_message_id)
                    {
                        case 0x01:
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
