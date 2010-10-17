using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace QUAVS.Base
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
            string temp = message;
            _messages.Enqueue(message);
        }
        
        public override void Write(string message)
        {
            _messages.Enqueue(message);
        }

        private string GetMessages()
        {
            StringBuilder strBuilder = new StringBuilder();
            IEnumerator<string> e = _messages.GetEnumerator();

            while (e.MoveNext())
            {
                strBuilder.Append(e.Current as string);
            }

            _messages.Clear();

            return strBuilder.ToString();
        }

        public string Text
        {
            get { return GetMessages(); }
        }
    }
}
