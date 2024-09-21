using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Location
{
    public class GetQuartersQuery : IRequest<List<QuarterDto>>
    {
        public int DistrictId { get; set; }
    }
}
