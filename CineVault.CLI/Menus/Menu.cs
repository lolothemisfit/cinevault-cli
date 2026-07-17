namespace CineVault.CLI.Menus;

public static class MainMenu {

    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("1. Movies");
        Console.WriteLine("2. TV Shows");
        Console.WriteLine("3. Music");
        Console.WriteLine("4. Exit");
        Console.WriteLine();

        Console.Write("Please select an option (1-4): ");
    }
}