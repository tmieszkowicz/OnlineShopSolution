using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineShopWeb;
using OnlineShopWeb.Services;
using OnlineShopWeb.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5017/") });

builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IShoppingCartService,ShoppingCartService>();

await builder.Build().RunAsync();
