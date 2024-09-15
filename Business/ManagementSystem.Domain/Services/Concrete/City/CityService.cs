using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.City;
using ManagementSystem.Domain.Ports;
using ManagementSystem.Domain.Services.Abstract.City;
using Packages.Exceptions.Types;
using System.Text.Json;

namespace ManagementSystem.Domain.Services.Concrete.City
{
    public class CityService : ICityService
    {
        private readonly ICityManager _cityManager;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityManager cityManager, ICityRepository cityRepository, IMapper mapper)
        {
            _cityManager = cityManager;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CancellationToken cancellationToken = default)
        {
            var jsonResponse = await _cityManager.GetCitiesAsync(cancellationToken);
            // JSON verisini ApiResponse nesnesine dönüştürüyoruz
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonResponse);

            if (apiResponse == null && apiResponse.Error)
            {
                throw new ArgumentNullException();

            }
            else
            {
                var externalCities = apiResponse.Data.States;
                var mappedCities = _mapper.Map<List<Domain.Entities.City>>(externalCities);

                var result = await _cityRepository.AddRangeAsync(mappedCities);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    throw new BusinessException();
                }
            }
        }
    }
}
