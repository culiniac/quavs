using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace QUAVS.Log
{
    public class MessageQueueTraceListener : TraceListener
    {
        private Queue<string> _messages = new Queue<string>();

        public MessageQueueTraceListener()
            : base()
        {
        }

        public MessageQueueTraceListener(string name)
            : base(name)
        {
        }

        public override void WriteLine(string message)
        {
            try
            {
                lock (_messages)
                {
                    string temp = message;
                    _messages.Enqueue(message);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
        
        public override void Write(string message)
        {
            try
            {
                lock (_messages)
                {
                    string temp = message;
                    _messages.Enqueue(message);
                }
            }
            catch(Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private string GetMessages()
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                lock (_messages)
                {
                    IEnumerator<string> e = _messages.GetEnumerator();
                    while (e.MoveNext())
                    {
                        strBuilder.Append(e.Current as string);
                    }
                    _messages.Clear();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            return strBuilder.ToString();
        }

        public string Text
        {
            get { return GetMessages(); }
        }
    }
}
