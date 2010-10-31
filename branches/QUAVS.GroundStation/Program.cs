using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QUAVS.Base;

namespace QUAVS.GroundStation
{
    static class Program
    {
        static public TelemetryDataObject _Telemetry;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
