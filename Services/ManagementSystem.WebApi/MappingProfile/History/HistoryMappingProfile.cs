using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.History;
using System.Text.Json;

namespace ManagementSystem.WebApi.MappingProfile.History
{
    public class HistoryMappingProfile : Profile
    {
        public HistoryMappingProfile()
        {
            CreateMap<HistoryDto, LogResponse>()
                       .ForMember(dest => dest.ChangedBy, opt =>
                           opt.MapFrom(src => Newtonsoft.Json.JsonConvert.DeserializeObject<ChangedByInfo>(src.ChangedBy)));

            CreateMap<List<HistoryDto>, HistoryResponse>()
                .ConvertUsing<HistoryDtoListConverter>();
        }
    }
    public class HistoryDtoListConverter : ITypeConverter<List<HistoryDto>, HistoryResponse>
    {

        public HistoryResponse Convert(List<HistoryDto> source, HistoryResponse destination, ResolutionContext context)
        {
            return new HistoryResponse
            {
                Entity = source.FirstOrDefault()?.TableName,
                Logs = context.Mapper.Map<List<LogResponse>>(source)
            };
        }
    }
}
