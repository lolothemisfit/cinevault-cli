namespace CineVault.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "CineVault CLI";

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

    }
}
