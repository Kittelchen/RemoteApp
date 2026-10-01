using RemoteApp.Interfaces;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RemoteApp.Services;

public partial class SystemService : ISystemService
{
    private readonly ILogService _log;

    public SystemService(ILogService log) => _log = log;

    // letters, digits, dot, dash, underscore, space; max 64 chars
    [GeneratedRegex(@"^[\w\.\- ]{1,64}$")]
    private static  partial Regex ValidName();

    public bool TryGetProcessStatus(string name, out ProcessStatus status)
    {
        status = default!;

        if (!ValidName().IsMatch(name))
            return false;

        // GetProcessesByName expects the name without ".exe"
        var clean = name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
            ? name[..^4]
            : name;

        var processes = Process.GetProcessesByName(clean);
        var count = processes.Length;
        foreach (var p in processes) p.Dispose();

        _log.Info($"Process check '{clean}': {count} instance(s)");
        status = new ProcessStatus(clean, count > 0, count);
        return true;
    }
}