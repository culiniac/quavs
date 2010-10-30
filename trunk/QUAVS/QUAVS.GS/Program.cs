using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QUAVS.Base;
using System.IO;
using System.Diagnostics;
using System.Threading;
using QUAVS.Log;

namespace QUAVS.GS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread thrd = Thread.CurrentThread;

            thrd.Priority = ThreadPriority.AboveNormal;

            Trace.WriteLine("Priority is now: " + thrd.Priority); 

            FileStreamWithBackup fs = new FileStreamWithBackup("quavs.gs.log", 2000000000, 10, FileMode.Append);
            fs.CanSplitData = false;
            TextWriterTraceListener listener = new TextWriterTraceListener(fs);
            Trace.AutoFlush = true;
            Trace.Listeners.Add(listener);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
