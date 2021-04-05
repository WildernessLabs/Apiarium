using System;
using Apiarium.Hardware.MeadowApp.Models;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Atmospheric;
using Meadow.Hardware;

namespace Apiarium.Hardware.MeadowApp
{
    public class HiveMonitor
    {
        //==== peripherals
        II2cBus i2cBus;
        Si70xx si7021;
        bool hasSi7021 = true;
        Bme280 bme280;
        bool hasBme280 = true;

        //==== singleton stuff
        private static readonly Lazy<HiveMonitor> instance =
            new Lazy<HiveMonitor>(() => new HiveMonitor());
        public static HiveMonitor Instance {
            get { return instance.Value; }
        }

        private HiveMonitor()
        {
            Initialize();
        }

        //==== properties
        public HiveStatus LastKnownHiveStatus { get; set; } = new HiveStatus();


        //==== init
        void Initialize()
        {
            Console.WriteLine("Initialize hive monitoring hardware...");

            //==== I2C Bus
            Console.WriteLine("Creating I2C Bus.");
            i2cBus = ApiariumApp.Device.CreateI2cBus(I2cBusSpeed.Fast);
            Console.WriteLine("I2C Bus created.");

            //==== onboard Si7021
            if (hasSi7021) {
                Console.WriteLine("Creating SI7021.");
                si7021 = new Si70xx(i2cBus);
                Console.WriteLine($"SI7021, successfully created.");
                var siReading = si7021.Read().Result;
                Console.WriteLine($"Initial reading, Temperature: {siReading.Temperature}°C, Humidity: {siReading.Humidity}%.");
            }

            //==== external Bme280
            if (hasBme280) {
                Console.WriteLine("Creating BME280.");
                bme280 = new Bme280(i2cBus, Bme280.I2cAddress.Adddress0x76);
                Console.WriteLine("BME280 successfullly created.");
                var bmeReading = bme280.Read().Result;
                Console.WriteLine($"Initial reading, Temperature: {bme280.Temperature}°C, Humidity: {bme280.Humidity}%, Pressure: {bme280.Pressure}.");
            }

            Console.WriteLine("Hardware initialization complete.");
        }
    }
}
