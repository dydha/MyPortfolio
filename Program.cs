using Blazored.LocalStorage;
using BlazorThemeSwitcher.Contracts;
using BlazorThemeSwitcher.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyPortfolio;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IThemePreferenceStorage, LocalStorageThemePreferenceStorage>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
await builder.Build().RunAsync();
