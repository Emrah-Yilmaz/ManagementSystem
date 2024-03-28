using ManagementSystem.Domain.Entities;
using ManagementSystem.WebApi.Models.WorkTask.Response;

namespace ManagementSystem.Domain.MappingProfile
{
    public class WorkTaskMappingProfile : AutoMapper.Profile
    {
        public WorkTaskMappingProfile()
        {
            CreateMap<WorkTask, WorkTasksDto>()
             .ForMember(destination => destination.NameSurname, operation => operation.MapFrom(source => string.Concat(source.User.Name + " " + source.User.LastName)))
             .ForMember(destination => destination.Status, operation => operation.MapFrom(source => source.Status.Name));
        }
    }
}
