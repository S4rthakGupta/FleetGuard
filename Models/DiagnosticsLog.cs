using FleetGuard.Enums;

namespace FleetGuard.Models
{
    public class DiagnosticsLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DeviceId { get; set; }

        public int BatteryLevel { get; set; }

        public DeviceStatus Status { get; set; }

        public string HealthMessage { get; set; } = string.Empty;

        public DateTime CheckedInAt { get; set; } = DateTime.UtcNow;
    }
}