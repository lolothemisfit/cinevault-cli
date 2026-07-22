using CineVault.CLI.Models;
using CineVault.CLI.Services;
using CineVault.CLI.Utilities;
using CineVault.CLI.Services.Search;
using CineVault.CLI.Services.External;
using CineVault.CLI.Models.External.TMDb;


namespace CineVault.CLI.Menus;

public static class MovieMenu
{
    public static async Task Show(MovieService movieService)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("========== MOVIES ==========");
            Console.WriteLine();
            Console.WriteLine("1. View Movies");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. Search My Movies");
            Console.WriteLine("4. Search Movies Online");
            Console.WriteLine("5. Edit Movie");
            Console.WriteLine("6. Back");
            Console.WriteLine();

            Console.Write("Select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewMovies(movieService);
                    break;

                case "2":
                    AddMovie(movieService);
                    break;

                case "3":
                    SearchMovies(new MovieSearchService(movieService));
                    break;

                case "4":
                    await SearchMoviesOnline(movieService, new TmdbService());
                    break;

                // case "5":
                //     EditMovie(movieService);
                //     break;

                case "6":
                    isRunning = false;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }

    private static void ViewMovies(MovieService movieService)
    {
        Console.Clear();

        Console.WriteLine("========== MOVIES ==========");
        Console.WriteLine();

        List<Movie> movies = movieService.GetAllMovies();

        if (movies.Count == 0)
        {
            Console.WriteLine("No movies found.");
        }
        else
        {
            foreach (Movie movie in movies)
            {
                DisplayMovie(movie);
            }
        }

        Pause();
    }

    private static void DisplayMovie(Movie movie)
    {
        Console.WriteLine($"Title: {movie.Title}");
        Console.WriteLine($"Genre: {movie.Genre}");
        Console.WriteLine($"Release Year: {movie.ReleaseYear}");
        Console.WriteLine($"Director: {movie.Director}");
        Console.WriteLine($"Description: {movie.Description}");
        Console.WriteLine($"Rating: {movie.Rating}");
        Console.WriteLine($"Watched: {(movie.Watched ? "Yes" : "No")}");
        Console.WriteLine();
    }
    private static void AddMovie(MovieService movieService)
    {
        Console.Clear();

        Console.WriteLine("========== ADD MOVIE ==========");
        Console.WriteLine();

        Console.Write("Title: ");
        string title = ConsoleInput.ReadString("Title: ");

        Console.Write("Genre: ");
        string genre = ConsoleInput.ReadString("Genre: ");

        Console.Write("Release Year: ");
        int releaseYear = ConsoleInput.ReadInt("Release Year: ");

        Console.Write("Director: ");
        string director = ConsoleInput.ReadString("Director: ");

        Console.Write("Description: ");
        string description = ConsoleInput.ReadString("Description: ");

        Console.Write("Rating: ");
        double rating = ConsoleInput.ReadDouble("Rating: ");

        Console.Write("Watched? (Y/N): ");
        bool watched = ConsoleInput.ReadYesNo("Watched? (Y/N): ");

        Movie movie = new Movie
        {
            Title = title,
            Genre = genre,
            ReleaseYear = releaseYear,
            Director = director,
            Description = description,
            Rating = rating,
            Watched = watched
        };

        movieService.AddMovie(movie);

        Console.WriteLine();
        Console.WriteLine("Movie added successfully!");

        Pause();
    }

    private static void SearchMovies(MovieSearchService movieSearchService)
    {
        Console.Clear();

        Console.WriteLine("========== SEARCH MOVIES ==========");
        Console.WriteLine();

        Console.Write("Enter search: ");
        string query = Console.ReadLine() ?? string.Empty;

        List<Movie> results = movieSearchService.Search(query);

        if (results.Count == 0)
        {
            Console.WriteLine("No movies found matching the search query.");
        }
        else
        {
            Console.WriteLine($"Found {results.Count} movie(s):");
            foreach (Movie movie in results)
            {
                DisplayMovie(movie);
            }
        }

        Pause();
    }

    private static async Task SearchMoviesOnline(MovieService movieService, TmdbService tmdbService)
    {
        Console.Clear();

        Console.WriteLine("========== SEARCH MOVIES ONLINE ==========");
        Console.WriteLine();

        Console.Write("Enter search: ");
        string query = Console.ReadLine() ?? string.Empty;

        Console.WriteLine();

        List<TmdbMovie> results = await tmdbService.SearchMovieAsync(query);

        if (results.Count == 0)
        {
            Console.WriteLine("No movies found.");
            Pause();
            return;
        }

        Console.WriteLine("Search Results");
        Console.WriteLine(new string('-', 40));

        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i].Title} ({results[i].ReleaseDate})");
        }
        int choice = ConsoleInput.ReadInt(
            $"Select a movie (1-{results.Count}): ",
            1,
            results.Count
        );

        TmdbMovie selectedMovie = results[choice - 1];
        Movie? movie = await tmdbService.GetMovieAsync(selectedMovie.Id);

        if (movie == null)
        {
            Console.WriteLine("Unable to retrieve movie details.");
            Pause();
            return;
        }

        Console.Clear();

        Console.WriteLine("========== MOVIE DETAILS ==========");
        Console.WriteLine();
        DisplayMovie(movie);

        bool addMovie = ConsoleInput.ReadYesNo(
            "Add this movie to your library? (Y/N): "
        );

        if (addMovie)
        {
            movie.Watched = ConsoleInput.ReadYesNo("Have you watched this movie? (Y/N): ");
            movieService.AddMovie(movie);
            Console.WriteLine("Movie Added Successfully!");
        }
        else
        {
            Console.WriteLine("Movie not added.");
        }
        Pause();
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

    }
}