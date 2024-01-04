using Blazored.SessionStorage;
using FPLSP_Tutorial.WASM;
using FPLSP_Tutorial.WASM.Pages;
using FPLSP_Tutorial.WASM.Repositories.Implements;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("API",
    options => { options.BaseAddress = new Uri(builder.Configuration["API:Https"]); });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7225") });

builder.Services.AddScoped<IClientPostService, ClientPostService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IMajorRequestRepository, MajorRequestRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostTagRepository, PostTagRepository>();
builder.Services.AddScoped<IUserMajorRepository, UserMajorRepository>();

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();