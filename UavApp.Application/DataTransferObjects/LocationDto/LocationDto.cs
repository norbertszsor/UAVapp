using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.Mappings;
using UavApp.Domain.Common.JsonData;

namespace UavApp.Application.DataTransferObjects.LocationDto
{
    public class LocationDto : IMap
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AltitudeGPS { get; set; }
        public double AltitudeBMP { get; set; }
        public double AirTemperature { get; set; }
        public double VerticalSpeed { get; set; }
        public double Speed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DroneData, LocationDto>();
            profile.CreateMap<LocationDto, DroneData>();
        }

    }
}
