using BlazorClient;
using BlazorClient.Handlers;
using BlazorClient.Providers;
using BlazorClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var apiConfig = builder.Configuration.GetSection("ClientConfig").Get<ClientConfig>()!;

builder.Services.AddAuthorizationCore();

// Providers
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<LoadingStateProvider>();
// Services
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DataLoadService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("Match", client => client.BaseAddress = new Uri(apiConfig.MatchServer));
builder.Services.AddHttpClient("Game", client => client.BaseAddress = new Uri(apiConfig.GameServer)).AddHttpMessageHandler<AuthenticationHandler>();
;
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();
