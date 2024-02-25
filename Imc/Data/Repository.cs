using Blazored.LocalStorage;
using Imc.Data.Interfaces;
using Imc.Models;
using Imc.Shared.Constants;

namespace Imc.Data;

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
        if (modelList is null) return modelList;
        
        var ordenedList = modelList.OrderByDescending(x => x.CreatedDateTime).ToList();
        return ordenedList;
    }

    public async Task SetItemAsync<T>(string itemKey, T value)
    {
        await _localStorage.SetItemAsync(itemKey, value);
    }

    public async Task<T?> GetItemAsync<T>(string itemKey)
    {
        return await _localStorage.GetItemAsync<T>(itemKey);
    }

    public async Task SetItemAsync(IList<ImcCalculator> value) => await _localStorage.SetItemAsync(StorageConstants.Key, value);
}