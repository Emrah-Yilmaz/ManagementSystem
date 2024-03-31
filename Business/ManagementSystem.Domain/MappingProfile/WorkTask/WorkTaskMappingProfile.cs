using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.WorkTask;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.WorkTask
{
    public class WorkTaskMappingProfile : AutoMapper.Profile
    {
        public WorkTaskMappingProfile()
        {
            CreateMap<CreateWorkTaskArgs, Entities.WorkTask>();

            CreateMap<UpdateWorkTaskArgs, Entities.WorkTask>();

            CreateMap<Entities.WorkTask, WorkTasksDto>()
             .ForMember(destination => destination.NameSurname, operation => operation.MapFrom(source => string.Concat(source.AssignedUser.Name + " " + source.AssignedUser.LastName)))
             .ForMember(destination => destination.Status, operation => operation.MapFrom(source => source.Status.Name))
             .ForMember(destination => destination.Department, operation => operation.MapFrom(source => source.Department.Name))
             .ForMember(destination => destination.Comments, operation => operation.MapFrom(source => source.Comments));

            CreateMap<Domain.Entities.Comment, GetCommentDto>();
        }
    }
}
