using System.Text.Json.Serialization;

namespace CineVault.CLI.Models.External.TMDb;

public class TmdbSearchResponse
{
    [JsonPropertyName("results")]
    public List<TmdbMovie> Results { get; set; } = [];
}