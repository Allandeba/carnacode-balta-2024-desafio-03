using Blazored.LocalStorage;
using Imc.Models;
using Imc.Shared.Constants;

namespace Imc.Data;

public interface IRepository
{
    Task<List<ImcCalculator>?> GetItemListAsync();
    Task SetItemAsync(IList<ImcCalculator> value);
}

public interface ILocalStorageRepository : IRepository;

public class LocalStorageRepository : ILocalStorageRepository
{
    private readonly ILocalStorageService _localStorage;
    public LocalStorageRepository(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async Task<List<ImcCalculator>?> GetItemListAsync()
    {
        var modelList = await _localStorage.GetItemAsync<List<ImcCalculator>>(StorageConstants.Key);
        if (modelList is not null)
            modelList.Reverse();

        return modelList;
    }

    public async Task SetItemAsync(IList<ImcCalculator> value) => await _localStorage.SetItemAsync(StorageConstants.Key, value);
}