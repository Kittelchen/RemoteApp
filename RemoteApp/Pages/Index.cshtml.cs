using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteApp.Models;
using RemoteApp.Services;

namespace RemoteApp.Pages;

public class Index(MockAuthenticationService authentication) : PageModel
{
    [BindProperty]
    public LoginInput Login { get; set; } = new();

    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (await authentication.AuthenticateAsync(Login.Username, Login.Password, cancellationToken))
        {
            HttpContext.Session.SetString("MockAuthenticated", "true");
            return RedirectToPage("/Connected");
        }

        ErrorMessage = "Benutzername oder Passwort ist falsch.";
        return Page();
    }
}