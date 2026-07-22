using System.Text.Json.Serialization;

namespace CineVault.CLI.Models.External.TMDb;

public class TmdbCredits
{
    [JsonPropertyName("crew")]
    public List<TmdbDirector> Crew { get; set; } = [];

}