using System.Net.Http;
using System.Text.Json;
using CineVault.CLI.Models;
using CineVault.CLI.Models.External.TMDb;
using System.Linq;

namespace CineVault.CLI.Services.External;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public TmdbService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");

        _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY") ?? throw new InvalidOperationException("API NOT FOUND");

        Console.WriteLine(_apiKey);
    }

    public async Task<List<TmdbMovie>> SearchMovieAsync(string query)
    {
        var response = await _httpClient.GetAsync($"search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            return [];
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        TmdbSearchResponse? searchResponse = JsonSerializer.Deserialize<TmdbSearchResponse>(jsonResponse);

        return searchResponse?.Results ?? [];
    }

    public async Task<TmdbMovieDetails?> GetMovieDetailsAsync(int movieId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"movie/{movieId}?api_key={_apiKey}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        string json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TmdbMovieDetails>(json);
    }

    public async Task<TmdbCredits?> GetMovieCreditsAsync(int movieId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(
        $"movie/{movieId}/credits?api_key={_apiKey}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        string json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TmdbCredits>(json);
    }

    public async Task<Movie?> GetMovieAsync(int movieId)
    {
      TmdbMovieDetails? details = await GetMovieDetailsAsync(movieId);

      if (details == null)
      {
          return null;
      }

        TmdbCredits? credits = await GetMovieCreditsAsync(movieId);

        if (credits == null)
        {
            return null;
        }

        TmdbDirector? director = credits.Crew.FirstOrDefault(person => person.Job.Equals("Director", StringComparison.OrdinalIgnoreCase));
        string genres = string.Join(", ", details.Genres.Select(genre => genre.Name));
        int releaseYear = DateTime.Parse(details.ReleaseDate).Year;

        return new Movie
        {
            Title = details.Title,
            Genre = genres,
            ReleaseYear = releaseYear,
            Director = director?.Name ?? "Unknown",
            Description = details.Overview,
            Rating = details.VoteAverage,
            Watched = false
        };
    }
}