using System.Text.Json.Serialization;

public class QuarterResult
{
    [JsonPropertyName("provinceId")]
    public int ProvinceId { get; set; }

    [JsonPropertyName("districtId")]
    public int DistrictId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("province")]
    public string Province { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }
}
