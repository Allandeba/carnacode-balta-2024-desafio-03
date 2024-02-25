using Blazored.LocalStorage;
using Imc.Services.Interfaces;
using Imc.Shared.Constants;
using Microsoft.JSInterop;

namespace Imc.Services;

public class StorageInitializerService(IJSRuntime jsRuntime, ILocalStorageService localStorage) : IStorageInitializerService
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task InitializePropertiesAsync()
    {
        try
        {
            var showMessage = await _localStorage.GetItemAsStringAsync(StorageConstants.ShowInstallMessage);
            if (string.IsNullOrEmpty(showMessage))
                await _localStorage.SetItemAsync<bool>(StorageConstants.ShowInstallMessage, true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}