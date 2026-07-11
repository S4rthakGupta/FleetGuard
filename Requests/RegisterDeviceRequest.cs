using System.ComponentModel.DataAnnotations;
using FleetGuard.Enums;

namespace FleetGuard.Requests
{
    public class RegisterDeviceRequest
    {
        [Required]
        public string DeviceName { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public DevicePlatform Platform { get; set; }

        [Required]
        public string OperatingSystemVersion { get; set; } = string.Empty;
    }
}