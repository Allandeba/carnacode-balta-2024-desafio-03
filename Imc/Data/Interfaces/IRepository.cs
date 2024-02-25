using Imc.Models;

namespace Imc.Data.Interfaces;

public interface IRepository
{
    Task<List<ImcCalculator>?> GetItemListAsync();
    Task SetItemAsync(IList<ImcCalculator> value);
    Task SetItemAsync<T>(string itemKey, T value);
    Task<T?> GetItemAsync<T>(string itemKey);
}

public interface ILocalStorageRepository : IRepository;