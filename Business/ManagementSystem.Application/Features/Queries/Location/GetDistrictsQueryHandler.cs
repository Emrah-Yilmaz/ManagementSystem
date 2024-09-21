using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Location;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Location
{
    public class GetDistrictsQueryHandler : IRequestHandler<GetDistrictsQuery, List<DistrictDto>>
    {
        private readonly ILocationService _locationService;

        public GetDistrictsQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<DistrictDto>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetDistrictsByCityIdAsync(request.CityId, cancellationToken);
            return result;
        }
    }
}
