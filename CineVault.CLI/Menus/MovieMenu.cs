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
            Console.WriteLine("6. Delete Movie");
            Console.WriteLine("7. Back");
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
                    SearchMovies(new MediaSearchService<Movie>(movieService));
                    break;

                case "4":
                    await SearchMoviesOnline(movieService, new TmdbService());
                    break;

                case "5":
                    EditMovie(movieService);
                    break;
                
                case "6":
                    DeleteMovie(movieService);
                    break;

                case "7":
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
                MediaMenuHelper.DisplayMedia(movie);
                Console.WriteLine();
            }
        }

        Pause();
    }

    
    private static void AddMovie(MovieService movieService)
    {
        Console.Clear();

        Console.WriteLine("========== ADD MOVIE ==========");
        Console.WriteLine();

        string title = ConsoleInput.ReadString("Title: ");
        string genre = ConsoleInput.ReadString("Genre: ");
        int releaseYear = ConsoleInput.ReadInt("Release Year: ");
        string director = ConsoleInput.ReadString("Director: ");
        string description = ConsoleInput.ReadString("Description: ");
        double rating = ConsoleInput.ReadDouble("Rating: ");
        bool watched = ConsoleInput.ReadYesNo("Watched: ");

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

    private static void SearchMovies(MediaSearchService<Movie> movieSearchService)
    {
        Console.Clear();

        Console.WriteLine("========== SEARCH MOVIES ==========");
        Console.WriteLine();

        Console.Write("Enter search: ");
        string query = Console.ReadLine() ?? string.Empty;
        
        Console.WriteLine();

        List<Movie> results = movieSearchService.Search(query);

        if (results.Count == 0)
        {
            Console.WriteLine("No movies found matching the search query.");
        }
        else
        {
            Console.WriteLine($"Found {results.Count} movie(s):");
            Console.WriteLine();
            foreach (Movie movie in results)
            {
                MediaMenuHelper.DisplayMedia(movie);
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
        Console.WriteLine();
        int choice = ConsoleInput.ReadInt(
            $"Select a movie (1-{results.Count}): ",
            1,
            results.Count
        );
        Console.WriteLine();

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
        MediaMenuHelper.DisplayMedia(movie);
        Console.WriteLine();

        bool addMovie = ConsoleInput.ReadYesNo(
            "Add this movie to your library? (Y/N): "
        );

        if (addMovie)
        {
            Console.WriteLine();
            movie.Watched = ConsoleInput.ReadYesNo("Have you watched this movie? (Y/N): ");
            movieService.AddMovie(movie);
            Console.WriteLine();
            Console.WriteLine("Movie Added Successfully!");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Movie not added.");
        }
        Pause();
    }

    public static void EditMovie(MovieService movieService)
    {
        Console.Clear();

        Console.WriteLine("========== EDIT MOVIE ==========");
        Console.WriteLine();

        MediaMenuHelper.EditMedia(movieService);
        Pause();

    }

    public static void DeleteMovie(MovieService movieService)
    {
        Console.Clear();

        Console.WriteLine("========== DELETE MOVIE ==========");
        Console.WriteLine();

        MediaMenuHelper.DeleteMedia(movieService);

        Pause();
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

    }
}