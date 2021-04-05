using System;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Web.Maple.Server;
using Meadow.Gateway.WiFi;

namespace Apiarium.Hardware.MeadowApp
{
    public class ApiariumApp : App<F7Micro, ApiariumApp>
    {
        //==== peripherals
        RgbPwmLed onboardLed;

        //==== controllers and such
        HiveMonitor hiveMonitor;
        MapleServer mapleServer;

        public ApiariumApp()
        {
            Initialize().Wait();
            hiveMonitor = HiveMonitor.Instance;

            //
            Console.WriteLine("Starting up HiveMonitor.");
            hiveMonitor.Start();
        }

        //==== init
        async Task Initialize()
        {
            Console.WriteLine("Initializing application...");

            //==== onboard LED; for basic status
            Console.WriteLine("Initializing onboard LED.");
            onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                3.3f, 3.3f, 3.3f,
                Meadow.Peripherals.Leds.IRgbLed.CommonType.CommonAnode);
            Console.WriteLine("Complete.");


            // initialize the wifi adpater
            Console.WriteLine("Initializing WiFi Adapter.");
            if (!Device.InitWiFiAdapter().Result) {
                throw new Exception("Could not initialize the WiFi adapter.");
            }

            // connnect to the wifi network.
            Console.WriteLine($"Connecting to WiFi Network {Secrets.WIFI_NAME}");
            var connectionResult = await Device.WiFiAdapter.Connect(Secrets.WIFI_NAME, Secrets.WIFI_PASSWORD);
            if (connectionResult.ConnectionStatus != ConnectionStatus.Success) {
                throw new Exception($"Cannot connect to network: {connectionResult.ConnectionStatus}");
            }
            Console.WriteLine($"Connected. IP: {Device.WiFiAdapter.IpAddress}");

            // create our maple web server
            Console.WriteLine("Initializing Maple Server.");
            mapleServer = new MapleServer(
                Device.WiFiAdapter.IpAddress,
                advertise: false,
                processMode: RequestProcessMode.Parallel
                );
            Console.WriteLine("Starting Maple Server.");
            mapleServer.Start();
            Console.WriteLine("Maple Server up.");

            Console.WriteLine("Application initialization complete.");
        }
    }
}