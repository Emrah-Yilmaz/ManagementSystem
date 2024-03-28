using ManagementSystem.WebApi.Models.WorkTask.Response;

namespace ManagementSystem.WebApi.MappingProfile
{
    public class WorkTaskMappingProfile : AutoMapper.Profile
    {
        public WorkTaskMappingProfile()
        {
            CreateMap<WorkTasksDto, WorkTasksResponse>();
        }
    }
}
