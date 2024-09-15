using ManagementSystem.Domain.Services.Abstract.City;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.City
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, bool>
    {
        private readonly ICityService _cityService;

        public CreateCityCommandHandler(ICityService cityService)
        {
            _cityService = cityService;
        }

        public Task<bool> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var result = _cityService.CreateAsync(cancellationToken);
            return result;
        }
    }
}
