using RemoteApp;
using RemoteApp.Services;
using RemoteApp.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        
        builder.Services.AddBootstrap(builder.Environment);
        
        var app = builder.Build();
        
        app.MapControllers();
        
        app.Run();
    }
}