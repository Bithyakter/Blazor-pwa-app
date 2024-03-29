using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using BlazorPwaApp.Client;
using BlazorPwaApp.Client.Services;
using BlazorPwaApp.Client.Services.CountryService;
using BlazorPwaApp.Client.Services.DistrictService;
using BlazorPwaApp.Client.Services.FacilityService;
using BlazorPwaApp.Client.Services.ProvinceService;
using BlazorPwaApp.Client.Services.UserAccountService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddBlazorise(options =>
    {
       options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<ISessionService, LocalStorageSessionService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://staging-sc.api.arcapps.org/sc-api/") });
await builder.Build().RunAsync();
