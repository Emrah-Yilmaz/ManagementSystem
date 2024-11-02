using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.Services.Abstract.Location;
using MediatR;
using Packages.Repositories.Enums;

namespace ManagementSystem.Application.Features.Commands.City
{
    public class SyncLocationCommandHandler : IRequestHandler<SyncLocationCommand, bool>
    {
        private readonly ILocationService _locationService;

        public SyncLocationCommandHandler(ILocationService cityService)
        {
            _locationService = cityService;
        }

        public Task<bool> Handle(SyncLocationCommand request, CancellationToken cancellationToken)
        {
            if (request.LocationType == Domain.Models.Enums.LocationType.City)
            {
                var result = _locationService.CreateCityAsync(cancellationToken);
                return result;
            }
            else if (request.LocationType == Domain.Models.Enums.LocationType.District)
            {
                var result = _locationService.CreateDistrictsAsync();
                return result;
            }
            else 
            {
                var result = _locationService.CreateQuartersAsync();
                return result;
            }

        }
    }
}
