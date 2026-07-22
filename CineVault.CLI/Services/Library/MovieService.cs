using CineVault.CLI.Models;

namespace CineVault.CLI.Services;

public class MovieService {
    private readonly List<Movie> _movies = new();

    public MovieService() {
        seedMovies();
    }

    private void seedMovies() {
        _movies.Add(new Movie {
            Title = "Inception",
            Genre = "Science Fiction",
            ReleaseYear = 2010,
            Director = "Christopher Nolan",
            Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
            Rating = 8.8,
            Watched = true
        });

        _movies.Add(new Movie {
            Title = "The Shawshank Redemption",
            Genre = "Drama",
            ReleaseYear = 1994,
            Director = "Frank Darabont",
            Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
            Rating = 9.3,
            Watched = true
        }); 

        _movies.Add(new Movie {
            Title = "The Godfather",
            Genre = "Crime",
            ReleaseYear = 1972,
            Director = "Francis Ford Coppola",
            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
            Rating = 9.2,
            Watched = true
        });

        _movies.Add(new Movie {
            Title = "The Dark Knight",
            Genre = "Action",
            ReleaseYear = 2008,
            Director = "Christopher Nolan",
            Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
            Rating = 9.0,
            Watched = true
        });

        _movies.Add(new Movie {
            Title = "Pulp Fiction",
            Genre = "Crime",
            ReleaseYear = 1994,
            Director = "Quentin Tarantino",
            Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
            Rating = 8.9,
            Watched = true
        });
    }

    public List<Movie> GetAllMovies() {
        return _movies;
    }

    public void AddMovie(Movie movie) {
        _movies.Add(movie);
    }

}