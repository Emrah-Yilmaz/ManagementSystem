using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.User;

namespace ManagementSystem.WebApi.MappingProfile
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<LoginDto, LoginResponse>();
        }
    }
}
