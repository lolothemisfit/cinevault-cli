using System.Text.Json;

namespace CineVault.CLI.Services.Storage;

public class JsonStorageService
{
    public void Save<T>(string filePath, List<T> items)
    {
        string? directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(items, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, json);
    }

    public List<T> Load<T>(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
            return new List<T>();
        }

        string json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}