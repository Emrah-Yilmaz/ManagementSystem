using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Domain.MappingProfile
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserArgs, User>();
        }
    }
}
