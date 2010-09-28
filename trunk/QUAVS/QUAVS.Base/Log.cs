using System;
using System.Collections.Generic;
using System.Text;

namespace QUAVS.Base
{
    static class Log
    {
        public static void Info(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void Warn(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void Error(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

    }
}
