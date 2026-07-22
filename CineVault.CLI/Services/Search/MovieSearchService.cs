using CineVault.CLI.Models;
using CineVault.CLI.Services;

namespace CineVault.CLI.Services.Search;

public class MovieSearchService
{
    private readonly MovieService _movieService;

    public MovieSearchService(MovieService movieService)
    {
        _movieService = movieService;
    }

    public List<Movie> SearchByTitle(string title)
    {
        return _movieService
            .GetAllMovies()
            .Where(movie => movie.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Movie> SearchByGenre(string genre)
    {
        return _movieService
            .GetAllMovies()
            .Where(movie => movie.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Movie> SearchByDirector(string director)
    {
        return _movieService
            .GetAllMovies()
            .Where(movie => movie.Director.Contains(director, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Movie> SearchByReleaseYear(int releaseYear)
    {
        return _movieService
            .GetAllMovies()
            .Where(movie => movie.ReleaseYear == releaseYear)
            .ToList();
    }

    public List<Movie> SearchByRating(double rating)
    {
        return _movieService
            .GetAllMovies()
            .Where(movie => movie.Rating >= rating)
            .ToList();
    }

    public List<Movie> Search(string query)
    {
        var results = new List<Movie>();

        results.AddRange(SearchByTitle(query));
        results.AddRange(SearchByGenre(query));
        results.AddRange(SearchByDirector(query));
        
        if (int.TryParse(query, out int year))
        {
            results.AddRange(SearchByReleaseYear(year));
        }
        
        if (double.TryParse(query, out double rating))
        {
            results.AddRange(SearchByRating(rating));
        }

        return results.Distinct().ToList();
    }
}


