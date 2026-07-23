using CineVault.CLI.Models;
using CineVault.CLI.Services.Library.Interface;
using CineVault.CLI.Utilities;
using CineVault.CLI.Services.Search;
using CineVault.CLI.Services.External;
using CineVault.CLI.Models.External.TMDb;
using System.Threading.Tasks;

namespace CineVault.CLI.Menus;

public static class TvSeriesMenu
{
    public static async Task Show(TvSeriesService tvSeriesService)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("========== TV SERIES ==========");
            Console.WriteLine();
            Console.WriteLine("1. View TV Series");
            Console.WriteLine("2. Add TV Series");
            Console.WriteLine("3. Search My TV Series");
            Console.WriteLine("4. Search TV Series Online");
            Console.WriteLine("5. Edit TV Series");
            Console.WriteLine("6. Delete TV Series");
            Console.WriteLine("7. Back");
            Console.WriteLine();

            Console.Write("Select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewTvSeries(tvSeriesService);
                    break;

                case "2":
                    AddTvSeries(tvSeriesService);
                    break;

                case "3":
                    SearchTvSeries(new MediaSearchService<TvSeries>(tvSeriesService));
                    break;

                case "4":
                    await SearchTvSeriesOnline(tvSeriesService, new TmdbService());
                    break;

                case "5":
                    EditTvSeries(tvSeriesService);
                    break;
                
                case "6":
                    DeleteTvSeries(tvSeriesService);
                    break;

                case "7":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Pause();
                    break;
            }
        }
    }

    public static void ViewTvSeries(TvSeriesService tvSeriesService)
    {
        Console.Clear();

        Console.WriteLine("========== MY TV SERIES ==========");
        Console.WriteLine();

        List<TvSeries> tvSeriesList = tvSeriesService.GetAllTvSeries();

        if (tvSeriesList.Count == 0)
        {
            Console.WriteLine("No TV series found in your library.");
        }
        else
        {
            foreach( TvSeries tvSeries in tvSeriesList)
            {
                MediaMenuHelper.DisplayMedia(tvSeries);
                Console.WriteLine($"Seasons: {tvSeries.Seasons}");
                Console.WriteLine($"Episodes: {tvSeries.Episodes}");
                Console.WriteLine();
            }
            
        }

        Pause();
    }

    public static void AddTvSeries(TvSeriesService tvSeriesService)
    {
        Console.Clear();

        Console.WriteLine("========== ADD TV SERIES ==========");
        Console.WriteLine();

        string title = ConsoleInput.ReadString("Title: ");
        string genre = ConsoleInput.ReadString("Genre: ");
        int releaseYear = ConsoleInput.ReadInt("Release Year: ");
        string director = ConsoleInput.ReadString("Director: ");
        string description = ConsoleInput.ReadString("Description: ");
        double rating = ConsoleInput.ReadDouble("Rating: ");
        bool watched = ConsoleInput.ReadYesNo("Watched: ");
        int seasons = ConsoleInput.ReadInt("Seasons: ");
        int episodes = ConsoleInput.ReadInt("Episodes: ");

        TvSeries tvSeries = new TvSeries
        {
            Title = title,
            Genre = genre,
            ReleaseYear = releaseYear,
            Director = director,
            Description = description,
            Rating = rating,
            Watched = watched,
            Seasons = seasons,
            Episodes = episodes
        };

        tvSeriesService.AddTvSeries(tvSeries);

        Console.WriteLine();
        Console.WriteLine("TV series added successfully!");

        Pause();

    }

    public static void SearchTvSeries(MediaSearchService<TvSeries> mediaSearchService)
    {
        Console.Clear();
        Console.WriteLine("========== SEARCH TV SERIES ==========");
        Console.WriteLine();

        Console.Write("Enter Search: ");
        string query = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();

        List<TvSeries> results = mediaSearchService.Search(query);

        if (results.Count == 0)
        {
            Console.WriteLine("TV Series not found.");
        }
        else
        {
            Console.WriteLine($"Found {results.Count} tv series");
            Console.WriteLine();
            foreach(TvSeries tvSeries in results)
            {
                MediaMenuHelper.DisplayMedia(tvSeries);
                Console.WriteLine($"Seasons: {tvSeries.Seasons}");
                Console.WriteLine($"Episodes: {tvSeries.Episodes}");
                Console.WriteLine();
            }
        }

        Pause();
    }

    public static async Task SearchTvSeriesOnline(TvSeriesService tvSeriesService, TmdbService tmdbService)
    {
        Console.Clear();
        Console.WriteLine("========== SEARCH TV SERIES ONLINE ==========");
        Console.WriteLine();

        Console.Write("Enter Search: ");
        string query = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();

        List<TmdbTvSeries> results = await tmdbService.SearchTvSeriesAsync(query);

        if (results.Count == 0)
        {
            Console.WriteLine("TV Series cannot be found.");
            Pause();
            return;
        }
        
        Console.WriteLine("Search Results");
        Console.WriteLine();
        Console.WriteLine(new string('-', 40));

        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i].Title} ({results[i].FirstAirDate})");
        }
        Console.WriteLine();

        int choice = ConsoleInput.ReadInt(
        $"Select a TV series (1-{results.Count}): ",
        1,
        results.Count
        );

        TmdbTvSeries selectedTvSeries = results[choice - 1];
        TvSeries? tvSeries = await tmdbService.GetTvSeriesAsync(selectedTvSeries.Id);

        if (tvSeries == null)
        {
            Console.WriteLine("Unable to retrieve tv series details.");
            Pause();
            return;
        }

        Console.Clear();
        Console.WriteLine("========== TV SERIES DETAILS ==========");
        Console.WriteLine();
        MediaMenuHelper.DisplayMedia(tvSeries);
        Console.WriteLine($"Seasons: {tvSeries.Seasons}");
        Console.WriteLine($"Episodes: {tvSeries.Episodes}");
        Console.WriteLine();

        bool addTvSeries = ConsoleInput.ReadYesNo(
            "Add this tv series to your library? (Y/N): "
        );
        Console.WriteLine();

        if (addTvSeries)
        {
            tvSeries.Watched = ConsoleInput.ReadYesNo("Have you watched this TV series? (Y/N): ");
            tvSeriesService.AddTvSeries(tvSeries);
            Console.WriteLine("TV Series Added Successfully!");
        }
        else
        {
            Console.WriteLine("TV series not added.");
        }
        Pause();  
    }

    public static void EditTvSeries(TvSeriesService tvSeriesService)
    {
        TvSeries? tvSeries = MediaMenuHelper.SelectMedia(tvSeriesService, "EDIT");

        if (tvSeries == null)
        {
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Title: {tvSeries.Title}");
        Console.WriteLine($"Current watched status: {(tvSeries.Watched ? "Yes" : "No")}");
        Console.WriteLine();

        tvSeries.Watched = ConsoleInput.ReadYesNo(
            "Have you watched this? (Y/N): "
        );

        tvSeriesService.UpdateTvSeries(tvSeries);

        Console.WriteLine();
        Console.WriteLine("Updated successfully!");
    }

    public static void DeleteTvSeries(TvSeriesService tvSeriesService)
    {
        TvSeries? tvSeries = MediaMenuHelper.SelectMedia(tvSeriesService, "DELETE");

        if (tvSeries == null)
        {
            return;
        }

        bool confirm = ConsoleInput.ReadYesNo(
            $"Delete '{tvSeries.Title}'? (Y/N): "
        );

        if (confirm)
        {
            tvSeriesService.DeleteTvSeries(tvSeries.Id);

            Console.WriteLine("Deleted successfully!");
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}