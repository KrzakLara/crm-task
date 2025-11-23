using CRM_Property_Functions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })

    .ConfigureFunctionsWebApplication()

    .ConfigureServices(services =>
    {
        services.AddSingleton<DataverseService>();
        services.AddSingleton<GetPropertiesService>();
        services.AddMemoryCache();

    })

    .Build();

host.Run();
