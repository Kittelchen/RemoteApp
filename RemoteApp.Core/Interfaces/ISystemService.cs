using System.Diagnostics;

namespace RemoteApp.Interfaces;

public record ProcessStatus(string Process, bool Running, int Instances);

public interface ISystemService
{
    bool TryGetProcessStatus(string name, out ProcessStatus status);
}