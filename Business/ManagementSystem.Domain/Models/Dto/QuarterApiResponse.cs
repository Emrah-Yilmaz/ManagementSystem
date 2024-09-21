using System.Text.Json.Serialization;

namespace ManagementSystem.Domain.Models.Dto
{
    public class QuarterApiResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public List<QuarterResult> Data { get; set; }
    }
}