using CineVault.CLI.Models;
using CineVault.CLI.Services.Library.Interface;

namespace CineVault.CLI.Utilities;

public static class MediaMenuHelper
{
    public static void DisplayMedia(Media media)
    {
        Console.WriteLine($"Title: {media.Title}");
        Console.WriteLine($"Genre: {media.Genre}");
        Console.WriteLine($"Release Year: {media.ReleaseYear}");
        Console.WriteLine($"Director: {media.Director}");
        Console.WriteLine($"Description: {media.Description}");
        Console.WriteLine($"Rating: {media.Rating}");
        Console.WriteLine($"Watched: {(media.Watched ? "Yes" : "No")}");
    }

    public static T? SelectMedia<T>(IMediaService<T> mediaService, string action)
    where T : Media
    {
        Console.Clear();

        Console.WriteLine($"========== {action} ==========");
        Console.WriteLine();

        List<T> mediaItems = mediaService.GetAll();

        if (mediaItems.Count == 0)
        {
            Console.WriteLine("No items found.");
            return null;
        }

        for (int i = 0; i < mediaItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {mediaItems[i].Title}");
        }

        Console.WriteLine();

        int choice = ConsoleInput.ReadInt(
            $"Select an item (1-{mediaItems.Count}): ",
            1,
            mediaItems.Count
        );

        return mediaItems[choice - 1];
    }
}