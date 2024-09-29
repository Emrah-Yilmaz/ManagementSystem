using ManagementSystem.Domain.Models.Args.User;

namespace ManagementSystem.Domain.MappingProfile
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserArgs, Domain.Entities.User>();
        }
    }
}