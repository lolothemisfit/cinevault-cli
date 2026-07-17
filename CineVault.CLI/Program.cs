using CineVault.CLI.Menus;

namespace CineVault.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "CineVault CLI";

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

        MainMenu.Show();
        string? choice = Console.ReadLine();
        Console.WriteLine();

        
        switch (choice) {
            case "1":
                Console.WriteLine("Movies selected.");
                break;

            case "2":
                Console.WriteLine("TV Shows selected.");
                break;

            case "3":
                Console.WriteLine("Music selected.");
                break;

            case "4":
                Console.WriteLine("Goodbye!");
                isRunning = false;
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
        
        if (isRunning) {
            Console.WriteLine();
            Console.Write("Please select an option (1-4): ");
            Console.ReadKey();
        }    
    }
}
