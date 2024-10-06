using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Openware.Security.ActiveDirectory;

namespace ActiveDirectoryManagerTest;

public class ActiveDirectoryConnectionManagerTest
{
    private readonly IHost _host;
    public ActiveDirectoryConnectionManagerTest()
    {
        _host = BuildHost();
    }

    [Fact]
    public void Can_Create_Instance()
    {
        var service = _host.Services.GetService<IActiveDirectoryConnectionManager>();
        Assert.NotNull(service);
    }


    private IHost BuildHost()
    {
        var hostBuilder = CreateHostBuilder();
        IHost host = null;
        try
        {
            host = hostBuilder.Build();

        }
        catch (Exception ex)
        {
            throw;

        }
        return host;

    }
    private IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, configuration) =>
            {
                configuration.AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddActiveDirectoryManager();
            });
    }
}