using CineVault.CLI.Menus;
using CineVault.CLI.Services;

namespace CineVault.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "CineVault CLI";
        MovieService movieService = new();

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
                    MovieMenu.Show(movieService);
                    break;

                case "2":
                    Console.WriteLine("TV Shows coming soon...");
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
