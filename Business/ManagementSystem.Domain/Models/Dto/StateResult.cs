using System.Text.Json.Serialization;

namespace ManagementSystem.Domain.Models.Dto
{
    public class StateResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("state_code")]
        public string StateCode { get; set; }
    }
}
