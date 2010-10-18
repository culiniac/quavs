using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace QUAVS.Base
{
    static public class TraceException
    {
        static public void WriteLine(Exception e)
        {
            string currenttime = DateTime.Now.ToString("[d/M/yyyy HH:mm:ss.fff]");
            string message = currenttime + "  " + e.ToString();
            StackTrace st = new StackTrace(1, true);
            message += st.ToString();

            Trace.TraceError(message);
        }
    }
}
