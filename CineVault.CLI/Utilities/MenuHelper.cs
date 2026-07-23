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

    public static void EditMedia<T>(IMediaService<T> mediaService)
    where T : Media
    {
        Console.Clear();

        Console.WriteLine("========== EDIT ==========");
        Console.WriteLine();

        List<T> mediaItems = mediaService.GetAll();

        if (mediaItems.Count == 0)
        {
            Console.WriteLine("No items found.");
            return;
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
        T selectedItem = mediaItems[choice - 1];

        Console.WriteLine();
        Console.WriteLine($"Title: {selectedItem.Title}");
        Console.WriteLine($"Current watched status: {(selectedItem.Watched ? "Yes" : "No")}");
        Console.WriteLine();

        selectedItem.Watched = ConsoleInput.ReadYesNo(
            "Have you watched this? (Y/N): "
        );

        mediaService.Update(selectedItem);

        Console.WriteLine();
        Console.WriteLine("Updated successfully!");
    }

    public static void DeleteMedia<T>(IMediaService<T> mediaService)
    where T : Media
    {
        Console.Clear();

        Console.WriteLine("========== DELETE ==========");
        Console.WriteLine();

        List<T> mediaItems = mediaService.GetAll();

        if (mediaItems.Count == 0)
        {
            Console.WriteLine("No items found.");
            return;
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

        T selectedItem = mediaItems[choice - 1];

        bool confirm = ConsoleInput.ReadYesNo(
            $"Delete '{selectedItem.Title}'? (Y/N): "
        );

        if (confirm)
        {
            mediaService.Delete(selectedItem.Id);
            Console.WriteLine("Deleted successfully!");
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }
    }

}