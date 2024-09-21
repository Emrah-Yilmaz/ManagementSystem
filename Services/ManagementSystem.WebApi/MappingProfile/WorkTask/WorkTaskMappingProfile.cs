using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.WorkTask;

namespace ManagementSystem.WebApi.MappingProfile.WorkTask
{
    public class WorkTaskMappingProfile : AutoMapper.Profile
    {
        public WorkTaskMappingProfile()
        {
            CreateMap<WorkTasksDto, WorkTasksResponse>();
        }
    }
}
