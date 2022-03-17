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
    public class DroneViewDto : IMap
    {
        public string CustomName { get; set; }
        public string Model { get; set; }
        public int Serial { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Drone, DroneViewDto>();
            profile.CreateMap<DroneViewDto, Drone>();
        }
    }
}
