namespace CineVault.CLI.Services.Library.Interface;

public interface IMediaService<T> 
{
    void Add(T item);
    List<T> GetAll();
    T? GetById(int id);
    void Update(T item);
    void Delete(int id);
}