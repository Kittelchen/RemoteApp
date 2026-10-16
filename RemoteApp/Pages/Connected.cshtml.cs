using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RemoteApp.Pages;

public class Connected : PageModel
{
    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("MockAuthenticated") != "true")
            return RedirectToPage("/Index");

        return Page();
    }
}
