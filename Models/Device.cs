using FleetGuard.Enums;

namespace FleetGuard.Models
{
    public class Device
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DeviceName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DevicePlatform Platform { get; set; } = DevicePlatform.Other;
        public DeviceStatus Status { get; set; } = DeviceStatus.Pending;
        public string OperatingSystemVersion {  get; set; } = string.Empty;
    }
}
