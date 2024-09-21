using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Location;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Location
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<CityDto>>
    {
        private readonly ILocationService _locationService;

        public GetCitiesQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetCitiesAsync(cancellationToken);
            return result;
        }
    }
}
