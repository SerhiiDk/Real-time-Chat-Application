using Blazored.Toast;
using ChatApplication.UI;
using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazorBootstrap();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("HttpClient").GetValue<string>("BaseAddress")!) });
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddSingleton<AuthenticationStateService>();

await builder.Build().RunAsync();
