using System;
using Meadow.Peripherals.Sensors.Atmospheric;

namespace Apiarium.Hardware.MeadowApp.Models
{
    public class HiveStatus
    {
        AtmosphericConditions? InternalConditions { get; set; }
        AtmosphericConditions? ExternalConditions { get; set; }

        public HiveStatus()
        {
        }
    }
}
