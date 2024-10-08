﻿using AutoMapper;
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
            var cities = await _cityRepository.GetAll();
            if (cities is null || cities.Count == 0)
                return false;


            foreach (var city in cities)
            {
                var apiResponse = await _locationManager.GetDistrictsAsync(city.Name.ToLower(), cancellationToken);
                if (apiResponse.Status != "OK")
                    continue;

                // Convert API data to entities
                var districtList = apiResponse?.Data.SelectMany(p => p.Districts.Select(d => new District
                {
                    Name = d.Name,
                    CityId = city.Id,
                    Status = StatusType.Published.ToString()
                })).ToList();

                var savedList = await _districtRepository.AddRangeAsync(districtList);
                if (savedList > 0)
                {
                    continue;
                }
                else
                {
                    throw new BusinessException();
                }
            }

            return true;


        }

        public async Task<bool> CreateQuartersAsync(CancellationToken cancellationToken = default)
        {
            var districts = await _districtRepository.GetAll();
            if (districts is null || districts.Count == 0)
                return false;

            int limit = _options.Value.Quarter.Limit;
            int offset = _options.Value.Quarter.Offset;
            while (true)
            {
                var result = await _locationManager.GetQuartersAsync(limit, offset, cancellationToken);
                var quarters = result?.Data;
                if (quarters is null || quarters.Count == 0)
                    break;
                foreach (var district in districts)
                {
                    var filteredList = quarters.Where(p => p.District == district.Name).ToList();
                    if (filteredList is null || filteredList.Count == 0)
                        continue;

                    // Quarter listesi oluştur
                    var quarterList = filteredList.Select(p => new Quarter
                    {
                        Name = p.Name,
                        DistrictId = district.Id,
                        Status = StatusType.Published.ToString() // Eğer Status özelliği varsa
                    }).ToList();

                    var savedList = await _quarterRepository.AddRangeAsync(quarterList);
                    if (savedList > 0)
                    {
                        continue;
                    }
                    else
                        throw new BusinessException();
                }
                offset += limit;
            }
            return true;
        }

        public async Task<List<CityDto>?> GetCitiesAsync(CancellationToken cancellationToken = default)
        {
            var cities = await _cityRepository.GetAll();

            if (cities is null || cities.Count == 0)
                return null;

            var mappedCities = _mapper.Map<List<CityDto>>(cities);
            return mappedCities;
        }

        public async Task<List<DistrictDto>?> GetDistrictsByCityIdAsync(int cityId, CancellationToken cancellationToken = default)
        {
            var districts = await _districtRepository.GetList(d => d.CityId == cityId);

            if (districts is null || districts.Count == 0)
                return null;

            var mappedDistricts = _mapper.Map<List<DistrictDto>>(districts);
            return mappedDistricts;
        }

        public async Task<List<QuarterDto>?> GetQuartersByDistrictIdAsync(int districtId, CancellationToken cancellationToken = default)
        {
            var quarters = await _quarterRepository.GetList(predicate: p => p.DistrictId == districtId,
                                                            noTracking: false,
                                                            orderBy: null,
                                                            includes: null);

            if (quarters is null || quarters.Count == 0)
                return null;

            var mappedQuarters = _mapper.Map<List<QuarterDto>>(quarters);
            return mappedQuarters;
        }
    }
}
