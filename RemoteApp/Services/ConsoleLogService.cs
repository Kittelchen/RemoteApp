using RemoteApp.Interfaces;

namespace RemoteApp.Services;

public class ConsoleLogService : ILogService
{
    public void Debug (string message) => Write("DEBUG", message, ConsoleColor.Gray);
    public void Info(string message) => Write("INFO", message, ConsoleColor.Gray);
    public void Warning(string message) => Write("WARN", message, ConsoleColor.Yellow);
    public void Error(string message, Exception? ex = null)
    {
        Write("ERROR", ex is null ? message : $"{message}{Environment.NewLine}{ex}", ConsoleColor.Red);
    }
    
    private static readonly object _lock = new();

    private static void Write(string level, string message, ConsoleColor color)
    {
        lock (_lock)   // avoids interleaved output from parallel requests
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}");
            Console.ForegroundColor = previous;
        }
    }

}