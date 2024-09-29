using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Location
{
    public class GetDistrictsQuery : IRequest<List<DistrictDto>>
    {
        public int CityId { get; set; }
    }
}
