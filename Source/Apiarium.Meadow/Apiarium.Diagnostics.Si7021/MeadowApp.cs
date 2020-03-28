using System;
using System.Threading;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Atmospheric;
using Meadow.Peripherals.Sensors.Atmospheric;

namespace MeadowApp
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        Si70xx si7021;
        RgbPwmLed onboardLed;

        public MeadowApp()
        {
            Initialize();

            // start updating continuously
            try {
                si7021.StartUpdating();
            }
            catch (Exception e) {
                Console.WriteLine($"err: {e.Message}");
            }
            Console.WriteLine("Adios Muchachos.");

        }

        void Initialize()
        {
            Console.WriteLine("Initialize hardware...");

            // configure our SI7021 on the I2C Bus
            var i2cBus = Device.CreateI2cBus();

            si7021 = new Si70xx(i2cBus);

            // get an initial reading
            ReadConditions().Wait();


            //// Note that the filter is an optional parameter. If you're
            //// interested in all notifications, don't pass a filter/predicate.
            //si7021.Subscribe(new FilterableObserver<AtmosphericConditionChangeResult, AtmosphericConditions>(
            //    e => {
            //        Console.WriteLine($"Temp: {e.New.Temperature.ToString("###.#º")}");
            //    }));

            onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                3.3f, 3.3f, 3.3f,
                Meadow.Peripherals.Leds.IRgbLed.CommonType.CommonAnode);
        }

        protected async Task ReadConditions()
        {
            var conditions = await si7021.Read();
            Console.WriteLine("Initial Readings:");
            Console.WriteLine($"  Temperature: {conditions.Temperature}ºC");
            Console.WriteLine($"  Relative Humidity: {conditions.Humidity}%");
        }

    }
}