using RemoteApp.Interfaces;
using RemoteApp.Services;
using Microsoft.AspNetCore.Identity;

namespace RemoteApp;

public static class Bootstrap
{
    public static IServiceCollection AddBootstrap(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddSingleton<ILogService, ConsoleLogService>();
        services.AddSingleton<ISystemService, SystemService>();

        var databaseDirectory = Path.Combine(env.ContentRootPath, "App_Data");
        Directory.CreateDirectory(databaseDirectory);
        var databasePath = Path.Combine(databaseDirectory, "remoteapp.db");
        services.AddScoped<IPasswordHasher<MockUser>, PasswordHasher<MockUser>>();
        services.AddScoped<MockAuthenticationService>(serviceProvider =>
            new MockAuthenticationService(
                databasePath,
                serviceProvider.GetRequiredService<IPasswordHasher<MockUser>>()));

        services.AddDistributedMemoryCache();
        services.AddSession();
        services.AddRazorPages();
        return services;
    }
}