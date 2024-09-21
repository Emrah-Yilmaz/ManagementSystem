using System.Text.Json.Serialization;

namespace ManagementSystem.Domain.Models.Dto
{
    public class CountryResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iso3")]
        public string Iso3 { get; set; }

        [JsonPropertyName("iso2")]
        public string Iso2 { get; set; }

        [JsonPropertyName("states")]
        public List<StateResult> States { get; set; }
    }
}
