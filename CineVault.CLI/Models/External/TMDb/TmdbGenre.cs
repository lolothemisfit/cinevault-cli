using System.Text.Json.Serialization;

namespace CineVault.CLI.Models.External.TMDb;

public class TmdbGenre
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}