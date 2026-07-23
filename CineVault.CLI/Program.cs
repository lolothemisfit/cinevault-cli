using CineVault.CLI.Menus;
using CineVault.CLI.Services;
using CineVault.CLI.Services.Search;
using CineVault.CLI.Models;
namespace CineVault.CLI;


public class Program
{
    public static async Task Main(string[] args)
    {
        Console.Title = "CineVault CLI";
        MovieService movieService = new();
        TvSeriesService tvSeriesService = new();
        // var movieSearchService = new MediaSearchService<Movie>(movieService);
        // var tvSeriesSearchService = new MediaSearchService<TvSeries>(tvSeriesService);

        bool isRunning = true;

        Console.WriteLine("==========================================");
        Console.WriteLine("              CINEVAULT CLI");
        Console.WriteLine("==========================================");
        Console.WriteLine();
        Console.WriteLine("Welcome to CineVault!");
        Console.WriteLine();
        Console.Write("What's your name?: ");

        string? name = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine($"Hello, {name}!");
        
        while (isRunning)
        {
            MainMenu.Show();
            // Console.Clear();

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await MovieMenu.Show(movieService);
                    break;

                case "2":
                    await TvSeriesMenu.Show(tvSeriesService);
                    break;

                case "3":
                    Console.WriteLine("Music coming soon...");
                    
                    break;

                case "4":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please select an option from 1 to 4.");
                    continue;
            }
        }

        Console.WriteLine("Thanks for using CineVault!");
    }
}
