using Openware.Security.ActiveDirectory;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension class to add default implementations
/// </summary>
public static class DependecyInjection
{
    /// <summary>
    /// Adds the default implementation for Acitve Directory Operations
    /// </summary>
    /// <param name="services">the <see cref="IServiceCollection"/> services</param>
    /// <returns>An instanc eof<see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddActiveDirectoryManager(this IServiceCollection services)
    => services.AddActiveDirectoryManager(_ => { });

    /// <summary>
    /// Adds the default implementation for Acitve Directory Operations and configure the options
    /// </summary>
    /// <param name="services">the <see cref="IServiceCollection"/> services</param>
    /// <param name="configure">A delegate which is run to configure the services.</param>
    /// <returns></returns>
    public static IServiceCollection AddActiveDirectoryManager(this IServiceCollection services,
        Action<ActiveDirectoryConnectionOptions> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        services.Configure(configure);

        services.AddScoped<IActiveDirectoryConnectionManager, ActiveDirectoryConnectionManager>();
        services.AddScoped<IActiveDirectoryUserManager, ActiveDirectoryUserManager>();

        return services;
    }
}
