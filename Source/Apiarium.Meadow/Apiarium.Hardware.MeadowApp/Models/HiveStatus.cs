using System;
using Meadow.Peripherals.Sensors.Atmospheric;

namespace Apiarium.Hardware.MeadowApp.Models
{
    public class HiveStatus
    {
        public AtmosphericConditions? InternalConditions { get; set; }
        public AtmosphericConditions? ExternalConditions { get; set; }

        public HiveStatus()
        {
        }
    }
}
