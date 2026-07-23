using CineVault.CLI.Models;
using CineVault.CLI.Services.Library;
using CineVault.CLI.Services.Storage;


public class TvSeriesService : MediaService<TvSeries> {
   
    private readonly JsonStorageService _storage = new JsonStorageService();
    private const string FilePath ="CineVault.CLI/Data/tvseries.json";
    public TvSeriesService() {
        List<TvSeries> tvSeries = _storage.Load<TvSeries>(FilePath);

        if (tvSeries.Count == 0) {
            seedTvSeries();
            SaveChanges();
        } 
        else 
        {
            foreach (TvSeries series in tvSeries)
            {
                Add(series);
            }
        }
    }

    private void seedTvSeries() {
        Add(new TvSeries {
            Id = 1,
            Title = "Breaking Bad",
            Genre = "Crime, Drama, Thriller",
            ReleaseYear = 2008,
            Seasons = 5,
            Episodes = 62,
            Description = "A high school chemistry teacher turned methamphetamine producer navigates the dangers of the criminal underworld.",
            Rating = 9.5,
            Watched = true
        });

        Add(new TvSeries {
            Id = 2,
            Title = "Game of Thrones",
            Genre = "Action, Adventure, Drama",
            ReleaseYear = 2011,
            Seasons = 8,
            Episodes = 73,
            Description = "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia.",
            Rating = 9.3,
            Watched = true
        });
        Add(new TvSeries {
            Id = 3,
            Title = "Stranger Things",
            Genre = "Drama, Fantasy, Horror",
            ReleaseYear = 2016,
            Seasons = 4,
            Episodes = 34,
            Description = "A group of kids in a small town uncover a secret government experiment and a portal to a terrifying alternate dimension.",
            Rating = 8.8,
            Watched = true
        });

        Add(new TvSeries {
            Id = 4,
            Title = "The Crown",
            Genre = "Drama, History",
            ReleaseYear = 2016,
            Seasons = 5,
            Episodes = 50,
            Description = "A dramatized history of the reign of Queen Elizabeth II and the personal and political events that shaped the second half of the 20th century.",
            Rating = 8.7,
            Watched = false
        });

        Add(new TvSeries {
            Id = 5,
            Title = "The Mandalorian",
            Genre = "Action, Adventure, Sci-Fi",
            ReleaseYear = 2019,
            Seasons = 3,
            Episodes = 24,
            Description = "A lone bounty hunter in the outer reaches of the galaxy protects a mysterious child while navigating danger and shifting alliances.",
            Rating = 8.8,
            Watched = false
        });
    }

    public TvSeries? GetTvSeriesById(int id) {
        return GetById(id);
    }

    public List<TvSeries> GetAllTvSeries() {
        return GetAll();
    }

    public void AddTvSeries(TvSeries tvSeries) {
        tvSeries.Id = GetNextId();
        Add(tvSeries);
        SaveChanges();
    }

    public void UpdateTvSeries(TvSeries tvSeries) {
        tvSeries.Id = GetNextId();
        Update(tvSeries);
        SaveChanges();
    }

    public void DeleteTvSeries(int id) {
        Delete(id);
        SaveChanges();
    }
    private void SaveChanges()
    {
        _storage.Save(FilePath, GetAll());
    }
}