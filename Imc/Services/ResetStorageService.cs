using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Imc.Services;

public static class ResetStorageService
{
    public static async Task<WebAssemblyHostBuilder> ResetStorageAsync(this WebAssemblyHostBuilder builder)
    {
        try
        {
            var cacheCleanupService = builder.Services.BuildServiceProvider().GetService<ICacheCleanupService>();
            await cacheCleanupService!.CleanupCacheAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return builder;
    }
}