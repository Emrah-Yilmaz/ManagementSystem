﻿using CommonLibrary.Options;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Ports;
using Microsoft.Extensions.Options;
using Packages.Exceptions.Types;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManagementSystem.Infrastructure.Adapters
{
    public class LocationAdapter : ILocationManager
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<LocationOptions> _options;

        public LocationAdapter(IOptions<LocationOptions> options)
        {
            _httpClient = new HttpClient();
            _options = options;
        }
        public async Task<string> GetCitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var url = "https://countriesnow.space/api/v0.1/countries/states";

                // Gövde verisini hazırlıyoruz
                var requestBody = new
                {
                    country = "turkey"
                };

                // JSON'a dönüştürüyoruz
                var jsonContent = JsonSerializer.Serialize(requestBody);

                // StringContent ile içerik hazırlıyoruz
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // POST isteği gönderiyoruz
                var response = await _httpClient.PostAsync(url, content);

                // Yanıtın başarılı olup olmadığını kontrol ediyoruz
                response.EnsureSuccessStatusCode();

                // Yanıtı okuyoruz
                var responseBody = await response.Content.ReadAsStringAsync();

                // Yanıtı döndürüyoruz
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                throw new BusinessException(e.Message, e);
            }
        }

        public async Task<DistrictsApiResponse> GetDistrictsAsync(string cityName, CancellationToken cancellationToken = default)
        {
            try
            {
                // API URL'sini oluştur
                var url = $"https://turkiyeapi.dev/api/v1/provinces?name={Uri.EscapeDataString(cityName)}";

                // GET isteği gönder
                var response = await _httpClient.GetAsync(url);

                // Yanıtı JSON formatında oku
                var responseBody = await response.Content.ReadAsStringAsync();

                // JSON yanıtını ApiResponse nesnesine dönüştür
                var apiResponse = JsonSerializer.Deserialize<DistrictsApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true
                });

                return apiResponse;
            }
            catch (HttpRequestException e)
            {
                throw new BusinessException(e.Message, e);
            }
        }

        public async Task<QuarterApiResponse> GetQuartersAsync(int limit, int offset, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_options.Value.Quarter.Url}?limit={_options.Value.Quarter.Limit}&offset={offset}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                // Yanıtı JSON formatında oku
                var responseBody = await response.Content.ReadAsStringAsync();

                // JSON yanıtını ApiResponse nesnesine dönüştür
                var apiResponse = JsonSerializer.Deserialize<QuarterApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true
                });

                return apiResponse;


                //// API URL'sini oluştur
                //var url = "https://turkiyeapi.dev/api/v1/neighborhoods";

                //// GET isteği gönder
                //var response = await _httpClient.GetAsync(url);

                //// Yanıtı JSON formatında oku
                //var responseBody = await response.Content.ReadAsStringAsync();

                //// JSON yanıtını ApiResponse nesnesine dönüştür
                //var apiResponse = JsonSerializer.Deserialize<QuarterApiResponse>(responseBody, new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                //    PropertyNameCaseInsensitive = true
                //});

                //return apiResponse;
            }
            catch (HttpRequestException e)
            {
                throw new BusinessException(e.Message, e);
            }
        }
    }
}
