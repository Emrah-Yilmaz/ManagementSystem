using System.Text.Json.Serialization;

public class Province
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("area")]
    public int Area { get; set; }

    [JsonPropertyName("altitude")]
    public int Altitude { get; set; }

    [JsonPropertyName("areaCode")]
    public List<int> AreaCode { get; set; }

    [JsonPropertyName("isMetropolitan")]
    public bool IsMetropolitan { get; set; }

    [JsonPropertyName("districts")]
    public List<DistrictDto> Districts { get; set; }
}
