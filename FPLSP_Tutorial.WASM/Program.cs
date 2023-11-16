using FPLSP_Tutorial.WASM;
using FPLSP_Tutorial.WASM.Service.Interfaces;
using FPLSP_Tutorial.WASM.Service.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("API", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["API:Https"]);
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7225") });
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
