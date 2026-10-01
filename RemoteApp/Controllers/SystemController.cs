using Microsoft.AspNetCore.Mvc;
using RemoteApp.Interfaces;

namespace RemoteApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    private readonly ISystemService _system;

    public SystemController(ISystemService system) => _system = system;

    // GET /System/Process/firefox   or   /System/Process/firefox.exe
    [HttpGet("Process/{name}")]
    public IActionResult Process(string name)
    {
        if (!_system.TryGetProcessStatus(name, out var status))
            return BadRequest(new { error = "Invalid process name" });

        return Ok(status);
    }
}