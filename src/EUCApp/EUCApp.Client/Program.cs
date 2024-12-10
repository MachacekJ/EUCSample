// Appka je zatim zapnuta jenom v Server Renderingu

using Autofac;
using Autofac.Extensions.DependencyInjection;
using EUCApp.Client.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));

builder.Services.AddOptions();
builder.Services.AddEUCSharedConfiguration();

await builder.Build().RunAsync();
return;

static void ConfigureContainer(ContainerBuilder containerBuilder)
{

}

