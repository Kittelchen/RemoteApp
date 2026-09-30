using Microsoft.AspNetCore.Mvc;

namespace RemoteApp.Controllers;

[ApiController]
[Route("[controller]")]
public class Testcontroller : ControllerBase
{
    [HttpGet("Text")]
    public IActionResult Text() => Content("hello world", "text/plain");
    
    [HttpGet("Html")]
    public IActionResult Html() =>
        Content("<h1>Hello</h1><p>Rendered in the browser.</p>", "text/html; charset=utf-8");

}