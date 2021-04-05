using System.Threading;
using Meadow;

namespace Apiarium.Hardware.MeadowApp
{
    class Program
    {
        static IApp app;
        public static void Main(string[] args)
        {
            // instantiate and run new meadow app
            app = new ApiariumApp();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}