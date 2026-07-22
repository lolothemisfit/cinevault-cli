namespace CineVault.CLI.Utilities;

public static class ConsoleInput
{
    public static string? ReadString(string prompt)
    {
         while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.");
                continue;
            }

            if (double.TryParse(input, out _))
            {
                Console.WriteLine("Please enter text, not a number.");
                continue;
            }

            if (bool.TryParse(input, out _))
            {
                Console.WriteLine("Please enter text.");
                continue;
            }

            return input.Trim();
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid whole number.");
        }
    }

    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            int value = ReadInt(prompt);

            if (value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }
    
    public static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (double.TryParse(Console.ReadLine(), out double value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid number.");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Please enter Y or N.");
                continue;
            }

            input = input.Trim().ToUpper();

            if (input == "Y")
            {
                return true;
            }

            if (input == "N")
            {
                return false;
            }

            Console.WriteLine("Please enter Y or N.");
        }
    }
}