using System.Text.Json.Serialization;

namespace CineVault.CLI.Models.External.TMDb;

public class TmdbTvSeriesSearchResponse
{
    [JsonPropertyName("results")]
    public List<TmdbTvSeries> Results { get; set; } = [];
}