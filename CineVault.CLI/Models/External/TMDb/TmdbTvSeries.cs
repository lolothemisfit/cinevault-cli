using System.Text.Json.Serialization;

namespace CineVault.CLI.Models.External.TMDb;

public class TmdbTvSeries
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("first_air_date")]
    public string FirstAirDate { get; set; } = string.Empty;
}