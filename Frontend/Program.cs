using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Componentes;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using UseCases.ControleAcessos;
using Frontend.Helpers;
using UseCases.CarrinhoCompras;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<ICarrinhoService, CarrinhoService>();


await builder.Build().RunAsync();
