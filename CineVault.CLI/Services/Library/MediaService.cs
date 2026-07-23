using CineVault.CLI.Models;
using CineVault.CLI.Services.Library.Interface;

namespace CineVault.CLI.Services.Library;

public class MediaService<T> : IMediaService<T>
    where T : IEntity
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public List<T> GetAll()
    {
        return _items;
    }

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public void Update(T item)
    {
        T? existingItem = GetById(item.Id);

        if (existingItem != null)
        {
            int index = _items.IndexOf(existingItem);
            _items[index] = item;
        }
    }

    public void Delete(int id)
    {
        T? item = GetById(id);

        if (item != null)
        {
            _items.Remove(item);
        }
    }

    protected int GetNextId()
    {
        List<T> items = GetAll();

        if (items.Count == 0)
        {
            return 1;
        }

        return items.Max(item => item.Id) + 1;
    }
}