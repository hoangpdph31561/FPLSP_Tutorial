using MajorService;
using MajorService.Repo.Inplements;
using MajorService.Repo.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped<IMajorRequestRepo, MajorRequestRepo>();
builder.Services.AddScoped<IMajorUserRepo, MajorUserRepo>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7225") });

await builder.Build().RunAsync();
