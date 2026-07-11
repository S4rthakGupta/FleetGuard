using System.ComponentModel.DataAnnotations;
using FleetGuard.Enums;

namespace FleetGuard.Requests
{
   // This one has all the fields - the client can update including the status as well.
    public class UpdateDeviceRequest
    {
        [Required]
        public string DeviceName { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public DevicePlatform Platform { get; set; }

        [Required]
        public string OperatingSystemVersion { get; set; } = string.Empty;

        [Required]
        public DeviceStatus Status { get; set; }
    }
}