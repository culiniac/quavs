using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace QUAVS.Log
{
    public class TextWriterTraceListenerWithTime : TextWriterTraceListener
    {
        public TextWriterTraceListenerWithTime()
            : base()
        {
        }

        public TextWriterTraceListenerWithTime(Stream stream)
            : base(stream)
        {
        }

        public TextWriterTraceListenerWithTime(string path)
            : base(path)
        {
        }

        public TextWriterTraceListenerWithTime(TextWriter writer)
            : base(writer)
        {
        }

        public TextWriterTraceListenerWithTime(Stream stream, string name)
            : base(stream, name)
        {
        }

        public TextWriterTraceListenerWithTime(string path, string name)
            : base(path, name)
        {
        }

        public TextWriterTraceListenerWithTime(TextWriter writer, string name)
            : base(writer, name)
        {
        }

        public override void WriteLine(string message)
        {
            base.Write(DateTime.Now.ToString());
            base.Write(" ");
            base.WriteLine(message);
        }

        public override void Write(string message)
        {
            base.Write(DateTime.Now.ToString());
            base.Write(" ");
            base.Write(message);
        }

    }
}
