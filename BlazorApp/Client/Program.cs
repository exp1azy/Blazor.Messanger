using BlazorApp.Client;
using BlazorApp.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorApp.ServerAPI"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.ClaimsIdentity.UserIdClaimType = "name";
//    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    //options.Lockout.MaxFailedAccessAttempts = 5;
//    //options.Lockout.AllowedForNewUsers = true;
//});

await builder.Build().RunAsync();
