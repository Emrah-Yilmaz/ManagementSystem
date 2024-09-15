using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManagementSystem.Infrastructure.Adapters
{
    public class CityAdapter : ICityManager
    {
        private readonly HttpClient _httpClient;

        public CityAdapter()
        {
            _httpClient = new HttpClient();
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
                // Hata durumunda bilgi veriyoruz
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
