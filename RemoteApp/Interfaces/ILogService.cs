namespace RemoteApp.Interfaces;

public interface ILogService
{
    void Debug(string message);
    void Info(string message);
    void Warning(string message);
    void Error(string message, Exception? ex = null);
}