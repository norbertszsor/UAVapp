using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UavApp.Domain.Common.JsonData
{
    public class DroneData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AltitudeGPS { get; set; }
        public double AltitudeBMP { get; set; }
        public double PressureBMP { get; set; } 
        public double AirTemperature { get; set; }
        public double VerticalSpeed { get; set; }
        public double Speed { get; set; }
        
    }
    
}
