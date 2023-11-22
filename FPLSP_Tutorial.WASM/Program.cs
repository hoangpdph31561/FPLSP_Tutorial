using FPLSP_Tutorial.WASM;
using FPLSP_Tutorial.WASM.Repositories.Implements;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using FPLSP_Tutorial.WASM.Pages.Recreation;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("API", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["API:Https"]);
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7225") });

builder.Services.AddScoped<IMajorRequestRepo, MajorRequestRepo>();
builder.Services.AddScoped<IMajorUserRepo, MajorUserRepo>();
builder.Services.AddScoped<IMajorRepo, MajorRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IClientPostService, ClientPostService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
