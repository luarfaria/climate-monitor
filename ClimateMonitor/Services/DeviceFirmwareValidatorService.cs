using System.Text.RegularExpressions;

namespace ClimateMonitor.Services;

public class DeviceFirmwareValidatorService
{
    private static readonly ISet<string> ValidSecrets = new HashSet<string>
    {
        "secret-ABC-123-XYZ-001",
        "secret-ABC-123-XYZ-002",
        "secret-ABC-123-XYZ-003"
    };

    public bool ValidateDeviceFirmware(string firmware) 
        => Regex.Match(firmware, "^(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)(?:-((?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-\r\n]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\\+([0-9a-zA-Z-]+(?:\\.[0-9azA-Z-]+)*))?$").Success;
}
