using Microsoft.AspNetCore.Mvc;
using RemoteApp.Interfaces;

namespace RemoteApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogService _logger;
    public TestController(ILogService logger)
    {
        _logger = logger;
    }

    [HttpGet("Text")]
    public IActionResult Text()
    {
        _logger.Warning("Hello endpoint called");
        _logger.Error("Hello endpoint called");
        return Content("hello world", "text/plain");
    }

    [HttpGet("Html")]
    public IActionResult Html() =>
        Content("<h1>Hello</h1><p>Rendered in the browser.</p>", "text/html; charset=utf-8");

}