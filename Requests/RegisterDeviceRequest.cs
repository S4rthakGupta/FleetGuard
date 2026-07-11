using FleetGuard.Enums;

namespace FleetGuard.Requests
{
    public class RegisterDeviceRequest
    {
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        public DevicePlatform Platform { get; set; } = DevicePlatform.Other;

        public string OperatingSystemVersion {  get; set; } = string.Empty;
    }
}
