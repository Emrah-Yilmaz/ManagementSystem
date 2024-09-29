using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Location;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Location
{
    public class GetQuartersQueryHandler : IRequestHandler<GetQuartersQuery, List<QuarterDto>>
    {
        private readonly ILocationService _locationService;

        public GetQuartersQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public Task<List<QuarterDto>> Handle(GetQuartersQuery request, CancellationToken cancellationToken)
        {
            var result = _locationService.GetQuartersByDistrictIdAsync(request.DistrictId, cancellationToken);
            return result;
        }
    }
}
