using System.ComponentModel.DataAnnotations;

namespace FleetGuard.Requests
{
    public class DeviceCheckInRequest
    {
        [Range(0, 100)]
        public int BatteryLevel { get; set; }

        public bool IsEncrypted { get; set; }

        public bool IsScreenLockEnabled { get; set; }

        public bool IsRootedOrJailbroken { get; set; }

        [Required]
        public string IpAddress { get; set; } = string.Empty;
    }
}