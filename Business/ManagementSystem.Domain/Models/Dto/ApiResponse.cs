using System.Text.Json.Serialization;

namespace ManagementSystem.Domain.Models.Dto
{
    public class ApiResponse
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public CountryResult Data { get; set; }
    }
}
