using CineVault.CLI.Models;
using CineVault.CLI.Services;
using CineVault.CLI.Utilities;

namespace CineVault.CLI.Menus;

public static class MovieMenu
{
    public static void Show(MovieService movieService)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("========== MOVIES ==========");
            Console.WriteLine();
            Console.WriteLine("1. View Movies");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. Search Movies");
            Console.WriteLine("4. Back");
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
                    Console.WriteLine("Search Movies coming soon...");
                    Pause();
                    break;

                case "4":
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
                Console.WriteLine($"{movie.Title} ({movie.ReleaseYear})");
                Console.WriteLine($"Genre   : {movie.Genre}");
                Console.WriteLine($"Director: {movie.Director}");
                Console.WriteLine($"Description: {movie.Description}");
                Console.WriteLine($"Rating  : {movie.Rating}/10");
                Console.WriteLine($"Watched : {(movie.Watched ? "Yes" : "No")}");
                Console.WriteLine(new string('-', 40));
            }
        }

        Pause();
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

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

    }
}