using CurrieTechnologies.Razor.SweetAlert2;
using EventsManagement.Presentation.Front;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7245") });
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
