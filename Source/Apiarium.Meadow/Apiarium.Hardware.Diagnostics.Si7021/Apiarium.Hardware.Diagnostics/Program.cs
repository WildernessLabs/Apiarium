﻿using System.Threading;
using Meadow;

namespace Apiarium.Hardware.Diagnostics
{
    class Program
    {
        static IApp app;
        public static void Main(string[] args)
        {
            // instantiate and run new meadow app
            app = new MeadowApp();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}