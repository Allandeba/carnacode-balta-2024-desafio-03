using Imc.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Imc.Services;

public static class StoragePropertiesInitializerService
{
    public static async Task<WebAssemblyHostBuilder> InitializePropertiesAsync(this WebAssemblyHostBuilder builder)
    {
        try
        {
            var storageInitializerService = builder.Services.BuildServiceProvider().GetService<IStorageInitializerService>();
            await storageInitializerService!.InitializePropertiesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return builder;
    }
}