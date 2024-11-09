using AutoMapper;
using CommonLibrary.Options;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.Persistence.City;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Domain.Ports;
using ManagementSystem.Domain.Services.Abstract.Location;
using Microsoft.Extensions.Options;
using Packages.Exceptions.Types;
using System.Text.Json;

namespace ManagementSystem.Domain.Services.Concrete.Location
{
    public class LocationService : ILocationService
    {
        private readonly ILocationManager _locationManager;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IDistrictRepository _districtRepository;
        private readonly IQuarterRepository _quarterRepository;
        private readonly IOptions<LocationOptions> _options;
        private static readonly string PublishedStatus = StatusType.Published.ToString();

        public LocationService(ILocationManager cityManager, ICityRepository cityRepository, IMapper mapper, IDistrictRepository districtRepository, IQuarterRepository quarterRepository, IOptions<LocationOptions> options)
        {
            _locationManager = cityManager;
            _cityRepository = cityRepository;
            _mapper = mapper;
            _districtRepository = districtRepository;
            _quarterRepository = quarterRepository;
            _options = options;
        }

        public async Task<bool> CreateCityAsync(CancellationToken cancellationToken = default)
        {
            var jsonResponse = await _locationManager.GetCitiesAsync(cancellationToken);

            // JSON verisini ApiResponse nesnesine dönüştürüyoruz
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonResponse);

            if (apiResponse == null && apiResponse.Error)
            {
                throw new ArgumentNullException();

            }
            else
            {
                var externalCities = apiResponse.Data.States;
                var mappedCities = _mapper.Map<List<City>>(externalCities);
                foreach (var city in mappedCities)
                {
                    city.Status = StatusType.Published.ToString();
                }

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

        public async Task<bool> CreateDistrictsAsync(CancellationToken cancellationToken = default)
        {
            var cities = await _cityRepository.GetAllAsync(false, cancellationToken);
            if (cities is null || cities.Count == 0)
                return false;
            var districtList = new List<District>();

            foreach (var city in cities)
            {
                if (city.Name == "İstanbul" || city.Name == "Istanbul")
                {
                    string test = string.Empty;
                }
                var apiResponse = await _locationManager.GetDistrictsAsync(city.Name.ToLower(), cancellationToken);
                if (apiResponse.Status != "OK")
                    continue;

                // Convert API data to entities
                var externalDistrictList = apiResponse?.Data.SelectMany(p => p.Districts.Select(d => new District
                {
                    Name = d.Name,
                    CityId = city.Id,
                    Status = PublishedStatus
                })).ToList();

                if (externalDistrictList is not null || externalDistrictList.Count > 0)
                    districtList.AddRange(externalDistrictList);
            }

            var savedList = await _districtRepository.AddRangeAsync(districtList);
            if (savedList > 0)
                return true;
            else
                throw new BusinessException();
            
        }

        public async Task<bool> CreateQuartersAsync(CancellationToken cancellationToken = default)
        {
            var districts = await _districtRepository.GetAllAsync(false, default);
            if (districts is null || districts.Count == 0)
                return false;

            int limit = _options.Value.Quarter.Limit;
            int offset = _options.Value.Quarter.Offset;

            var quarters = new List<Quarter>();

            while (true)
            {
                var result = await _locationManager.GetQuartersAsync(limit, offset, cancellationToken);
                var exQuarters = result?.Data;
                if (exQuarters is null || exQuarters.Count == 0)
                    break;
                foreach (var district in districts)
                {
                    var filteredList = exQuarters.Where(p => p.District == district.Name).ToList();
                    if (filteredList is null || filteredList.Count == 0)
                        continue;

                    // Quarter listesi oluştur
                    var quarterList = filteredList.Select(p => new Quarter
                    {
                        Name = p.Name,
                        DistrictId = district.Id,
                        Status = PublishedStatus // Eğer Status özelliği varsa
                    }).ToList();
                    if (quarterList is not null || quarterList.Count > 0)
                        quarters.AddRange(quarterList);
                }
                offset += limit;
            }
            var savedList = await _quarterRepository.AddRangeAsync(quarters);
            if (savedList > 0)
                return true;
            else
                throw new BusinessException();
        }

        public async Task<List<CityDto>?> GetCitiesAsync(CancellationToken cancellationToken = default)
        {
            var cities = await _cityRepository.GetAllAsync(false, default);

            if (cities is null || cities.Count == 0)
                return null;

            var mappedCities = _mapper.Map<List<CityDto>>(cities);
            return mappedCities;
        }

        public async Task<List<DistrictDto>?> GetDistrictsByCityIdAsync(int cityId, CancellationToken cancellationToken = default)
        {
            var districts = await _districtRepository.GetListAsync(
                predicate: d => d.CityId == cityId,
                noTracking: false,
                cancellationToken: default);

            if (districts is null || districts.Count == 0)
                return null;

            var mappedDistricts = _mapper.Map<List<DistrictDto>>(districts);
            return mappedDistricts;
        }

        public async Task<List<QuarterDto>?> GetQuartersByDistrictIdAsync(int districtId, CancellationToken cancellationToken = default)
        {
            var quarters = await _quarterRepository.GetListAsync(predicate: p => p.DistrictId == districtId,
                                                            noTracking: false,
                                                            cancellationToken: default);

            if (quarters is null || quarters.Count == 0)
                return null;

            var mappedQuarters = _mapper.Map<List<QuarterDto>>(quarters);
            return mappedQuarters;
        }
    }
}
