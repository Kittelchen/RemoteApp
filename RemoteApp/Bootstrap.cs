using RemoteApp.Interfaces;
using RemoteApp.Services;

namespace RemoteApp;

public static class Bootstrap
{
    public static IServiceCollection AddBootstrap(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddSingleton<ILogService, ConsoleLogService>();
        services.AddSingleton<ISystemService, SystemService>();
        
        return services;
    }
}