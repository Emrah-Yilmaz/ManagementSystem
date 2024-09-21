using System.Text.Json.Serialization;

public class DistrictsApiResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("data")]
    public List<LocationDataResult> Data { get; set; }
}
