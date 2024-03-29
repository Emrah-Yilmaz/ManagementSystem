using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile
{
    public class WorkTaskMappingProfile : AutoMapper.Profile
    {
        public WorkTaskMappingProfile()
        {
            CreateMap<WorkTask, WorkTasksDto>()
             .ForMember(destination => destination.NameSurname, operation => operation.MapFrom(source => string.Concat(source.AssignedUser.Name + " " + source.AssignedUser.LastName)))
             .ForMember(destination => destination.Status, operation => operation.MapFrom(source => source.Status.Name));
        }
    }
}
