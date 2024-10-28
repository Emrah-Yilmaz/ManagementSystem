using AutoMapper;
using CommonLibrary.Messages;
using ManagementSystem.Application.Events.DepartmentEvents;

namespace ManagementSystem.WebApi.MappingProfile.Others
{
    public class EventAndMessageMappingProfile : Profile
    {
        public EventAndMessageMappingProfile()
        {
            CreateMap<SendEmailEvent, CreatedDepartmentMessage>().ReverseMap();
        }
    }
}
