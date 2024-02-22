using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Imc;
using Blazored.LocalStorage;
using Imc.Data;
using Imc.Data.Interfaces;
using Imc.Services;
using Imc.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);
builder.Services.AddScoped<ICacheCleanupService, CacheCleanupService>();
builder.Services.AddScoped<IImcCalculatorService, ImcCalculatorService>();
builder.Services.AddScoped<ILocalStorageRepository, LocalStorageRepository>();

builder.Services.AddBlazoredLocalStorage();

await builder.ResetStorageAsync();

await builder.Build().RunAsync();
