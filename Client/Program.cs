using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using LolaOfficial.Client.Services;
using LolaOfficial.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//################################################################################  enable auth on the client side
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddHttpClient<ILoginClient, LoginClient>
("BlazingChatClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


//################################################################################

await builder.Build().RunAsync();
  