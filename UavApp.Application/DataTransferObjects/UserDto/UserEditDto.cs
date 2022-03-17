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
    public class UserEditDto : IMap
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool  IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserEditDto>();
            profile.CreateMap<UserEditDto, User>();
        }
    }
}
