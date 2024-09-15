using System.Text.Json.Serialization;

public class DistrictsApiResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("data")]
    public List<Province> Data { get; set; }
}
