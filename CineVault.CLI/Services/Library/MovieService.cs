using CineVault.CLI.Models;
using CineVault.CLI.Services.Library;
using CineVault.CLI.Services.Storage;

namespace CineVault.CLI.Services;

public class MovieService : MediaService<Movie> {

private readonly JsonStorageService _storage = new JsonStorageService();
    private const string FilePath = "CineVault.CLI/Data/movies.json";
    

    public MovieService()
    {
        List<Movie> movies = _storage.Load<Movie>(FilePath);

        if (movies.Count == 0)
        {
            seedMovies();
            SaveChanges();
        }
        else
        {
            foreach (Movie movie in movies)
            {
                Add(movie);
            }
        }
    }

    private void seedMovies() {
        Add(new Movie {
            Id = 1,
            Title = "Inception",
            Genre = "Science Fiction",
            ReleaseYear = 2010,
            Director = "Christopher Nolan",
            Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
            Rating = 8.8,
            Watched = true
        });

        Add(new Movie {
            Id = 2,
            Title = "The Shawshank Redemption",
            Genre = "Drama",
            ReleaseYear = 1994,
            Director = "Frank Darabont",
            Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
            Rating = 9.3,
            Watched = true
        }); 

        Add(new Movie {
            Id = 3,
            Title = "The Godfather",
            Genre = "Crime",
            ReleaseYear = 1972,
            Director = "Francis Ford Coppola",
            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
            Rating = 9.2,
            Watched = true
        });

        Add(new Movie {
            Id = 4,
            Title = "The Dark Knight",
            Genre = "Action",
            ReleaseYear = 2008,
            Director = "Christopher Nolan",
            Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
            Rating = 9.0,
            Watched = true
        });

        Add(new Movie {
            Id = 5,
            Title = "Pulp Fiction",
            Genre = "Crime",
            ReleaseYear = 1994,
            Director = "Quentin Tarantino",
            Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
            Rating = 8.9,
            Watched = true
        });
    }

    public Movie? GetMovieById(int id) {
        return GetById(id);
    }

    public List<Movie> GetAllMovies() {
        return GetAll();
    }

    public void AddMovie(Movie movie) 
    {
        movie.Id = GetNextId();
        Add(movie);
        SaveChanges();
    }

    public void UpdateMovie(Movie movie)
    {
        movie.Id = GetNextId();
        Update(movie);
        SaveChanges();
    }

    public void DeleteMovie(int id) {
            Delete(id);
            SaveChanges();
    }
    private void SaveChanges()
    {
        _storage.Save(FilePath, GetAll());
    }

}