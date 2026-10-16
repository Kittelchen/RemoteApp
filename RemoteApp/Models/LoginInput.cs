namespace RemoteApp.Models;

public sealed class LoginInput
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string Token { get; set; } = string.Empty;
}
