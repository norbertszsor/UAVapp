using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UavApp.Application.Mappings;
using UavApp.Domain.Entities;

namespace UavApp.Application.DataTransferObjects.UserDto
{
    public class UserViewDto : IMap
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string IsActive { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserViewDto>();
            profile.CreateMap<UserViewDto, User>();
        }
    }
}
