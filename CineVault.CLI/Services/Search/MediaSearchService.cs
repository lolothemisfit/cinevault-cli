using CineVault.CLI.Models;
using CineVault.CLI.Services.Library.Interface;

namespace CineVault.CLI.Services.Search;

public class MediaSearchService<T> where T : Media
{
    private readonly IMediaService<T> _mediaService;

    public MediaSearchService(IMediaService<T> mediaService)
    {
        _mediaService = mediaService;
    }

    public List<T> SearchByTitle(string title)
    {
        return _mediaService
            .GetAll()
            .Where(media => media.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<T> SearchByGenre(string genre)
    {
        return _mediaService
            .GetAll()
            .Where(media => media.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<T> SearchByDirector(string director)
    {
        return _mediaService
            .GetAll()
            .Where(media => media.Director.Contains(director, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<T> SearchByReleaseYear(int releaseYear)
    {
        return _mediaService
            .GetAll()
            .Where(media => media.ReleaseYear == releaseYear)
            .ToList();
    }

    public List<T> SearchByRating(double rating)
    {
        return _mediaService
            .GetAll()
            .Where(media => media.Rating >= rating)
            .ToList();
    }

    public List<T> Search(string query)
    {
        List<T> results = [];

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