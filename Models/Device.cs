using FleetGuard.Enums;

namespace FleetGuard.Models
{
    // This class answers about what info does FleetGuard store for every managed device.

    // This below class defines the structure of every device object.
        public class Device
        {
            public Guid Id { get; set; } = Guid.NewGuid();

            public string DeviceName { get; set; } = string.Empty;

            public string SerialNumber { get; set; } = string.Empty;

            public DevicePlatform Platform { get; set; } =
                DevicePlatform.Other;

            public DeviceStatus Status { get; set; } =
                DeviceStatus.Pending;

            public string OperatingSystemVersion { get; set; } =
                string.Empty;

            public int? BatteryLevel { get; set; }

            public bool? IsEncrypted { get; set; }

            public bool? IsScreenLockEnabled { get; set; }

            public bool? IsRootedOrJailbroken { get; set; }

            public string? IpAddress { get; set; }

            public DateTime? LastCheckInAt { get; set; }

            public string HealthMessage { get; set; } =
                "Device has not checked in yet.";
        }
    }

// string.Empty: It represents an empty string with no characters.
// It is often preferred over using
// "" because it improves code readability and avoids the creation of multiple instances in memory.

// why getters and setters?
// -> it allows to read and assigb/modify value.