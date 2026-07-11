using System.ComponentModel.DataAnnotations;
using FleetGuard.Enums;

namespace FleetGuard.Requests
{
    // These are mainly DTO's (Data Transfer Objects)
    // It only allows fields needed for registration.
    public class RegisterDeviceRequest
    {
        // Data Annotation Attribute -> Provided field.
        // -> Because our controller has [ApiController] so it validates DTO's automatically.
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