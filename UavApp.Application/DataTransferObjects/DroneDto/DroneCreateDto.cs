using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.Mappings;
using UavApp.Domain.Entities;

namespace UavApp.Application.DataTransferObjects.DroneDto
{
    public class DroneCreateDto : IMap
    {
        public string Model { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Drone, DroneCreateDto>();
            profile.CreateMap<DroneCreateDto, Drone>();
        }
    }
}
