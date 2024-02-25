using Blazored.LocalStorage;
using Imc.Models;
using Imc.Services.Interfaces;
using Imc.Shared.Constants;
using Microsoft.JSInterop;

namespace Imc.Services;

public class CacheCleanupService(IJSRuntime jsRuntime, ILocalStorageService localStorage) : ICacheCleanupService
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public async Task CleanupCacheAsync()
    {
        try
        {
            var itemList = await localStorage.GetItemAsync<List<ImcCalculator>>(StorageConstants.Key);
            if (itemList == null)
                return;
            
            for (int i = itemList.Count() - 1; i >= 0; i--)
            {
                var item = itemList[i]; 
                if (!item.IsValid())
                    itemList.Remove(item);
            }

            await localStorage.SetItemAsync(StorageConstants.Key, itemList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
