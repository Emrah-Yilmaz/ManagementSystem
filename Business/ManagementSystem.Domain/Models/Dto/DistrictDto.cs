using System.Text.Json.Serialization;

public class DistrictDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("area")]
    public int Area { get; set; }
}
