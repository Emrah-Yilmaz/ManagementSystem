using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.User;

namespace ManagementSystem.WebApi.MappingProfile.User
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<LoginDto, LoginResponse>();
        }
    }
}
