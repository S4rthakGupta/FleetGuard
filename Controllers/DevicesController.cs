using FleetGuard.Data;
using FleetGuard.Enums;
using FleetGuard.Models;
using FleetGuard.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetGuard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly FleetGuardDbContext _context;

        public DevicesController(FleetGuardDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Device>> RegisterDevice(
            RegisterDeviceRequest request)
        {
            string normalizedSerialNumber =
                request.SerialNumber.Trim().ToUpperInvariant();

            bool serialNumberExists =
                await _context.Devices.AnyAsync(device =>
                    device.SerialNumber.ToUpper() == normalizedSerialNumber);

            if (serialNumberExists)
            {
                return Conflict(new
                {
                    message = "A device with this serial number already exists."
                });
            }

            Device device = new Device
            {
                DeviceName = request.DeviceName.Trim(),
                SerialNumber = normalizedSerialNumber,
                Platform = request.Platform,
                OperatingSystemVersion =
                    request.OperatingSystemVersion.Trim()
            };

            _context.Devices.Add(device);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDeviceById),
                new { id = device.Id },
                device);
        }

        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetAllDevices()
        {
            List<Device> devices =
                await _context.Devices.ToListAsync();

            return Ok(devices);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Device>> GetDeviceById(Guid id)
        {
            Device? device =
                await _context.Devices.FindAsync(id);

            if (device is null)
            {
                return NotFound(new
                {
                    message = "Device not found."
                });
            }

            return Ok(device);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Device>> UpdateDevice(
            Guid id,
            UpdateDeviceRequest request)
        {
            Device? device =
                await _context.Devices.FindAsync(id);

            if (device is null)
            {
                return NotFound(new
                {
                    message = "Device not found."
                });
            }

            string normalizedSerialNumber =
            request.SerialNumber.Trim().ToUpperInvariant();

            bool serialNumberExists =
                await _context.Devices.AnyAsync(existingDevice =>
                    existingDevice.Id != id &&
                    existingDevice.SerialNumber.ToUpper() ==
                        normalizedSerialNumber);

            if (serialNumberExists)
            {
                return Conflict(new
                {
                    message = "Another device already uses this serial number."
                });
            }

            device.DeviceName = request.DeviceName.Trim();
            device.SerialNumber = normalizedSerialNumber;
            device.Platform = request.Platform;
            device.OperatingSystemVersion =
                request.OperatingSystemVersion.Trim();
            device.Status = request.Status;

            await _context.SaveChangesAsync();

            return Ok(device);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            Device? device =
                await _context.Devices.FindAsync(id);

            if (device is null)
            {
                return NotFound(new
                {
                    message = "Device not found."
                });
            }

            _context.Devices.Remove(device);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id:guid}/check-in")]
        public async Task<ActionResult<Device>> CheckInDevice(
            Guid id,
            DeviceCheckInRequest request)
        {
            Device? device =
                await _context.Devices.FindAsync(id);

            if (device is null)
            {
                return NotFound(new
                {
                    message = "Device not found."
                });
            }

            device.BatteryLevel = request.BatteryLevel;
            device.IsEncrypted = request.IsEncrypted;
            device.IsScreenLockEnabled =
                request.IsScreenLockEnabled;
            device.IsRootedOrJailbroken =
                request.IsRootedOrJailbroken;
            device.IpAddress = request.IpAddress;
            device.LastCheckInAt = DateTime.UtcNow;

            if (request.IsRootedOrJailbroken)
            {
                device.Status = DeviceStatus.Critical;
                device.HealthMessage =
                    "Critical: Device is rooted or jailbroken.";
            }
            else if (!request.IsEncrypted)
            {
                device.Status = DeviceStatus.Critical;
                device.HealthMessage =
                    "Critical: Device storage is not encrypted.";
            }
            else if (!request.IsScreenLockEnabled)
            {
                device.Status = DeviceStatus.Warning;
                device.HealthMessage =
                    "Warning: Screen lock is not enabled.";
            }
            else if (request.BatteryLevel < 20)
            {
                device.Status = DeviceStatus.Warning;
                device.HealthMessage =
                    "Warning: Battery level is below 20%.";
            }
            else
            {
                device.Status = DeviceStatus.Healthy;
                device.HealthMessage =
                    "Healthy: Device passed all current checks.";
            }

            DiagnosticsLog log = new DiagnosticsLog
            {
                DeviceId = device.Id,
                BatteryLevel = request.BatteryLevel,
                Status = device.Status,
                HealthMessage = device.HealthMessage,
                CheckedInAt = DateTime.UtcNow
            };

            _context.DiagnosticsLog.Add(log);

            await _context.SaveChangesAsync();

            return Ok(device);
        }

        [HttpGet("{id:guid}/diagnostics")]
        public async Task<ActionResult<List<DiagnosticsLog>>> GetDiagnosticLogs(Guid id)
        {
            bool deviceExists =
                await _context.Devices.AnyAsync(d => d.Id == id);

            if (!deviceExists)
            {
                return NotFound(new
                {
                    message = "Device not found."
                });
            }

            List<DiagnosticsLog> logs =
                await _context.DiagnosticsLog
                    .Where(log => log.DeviceId == id)
                    .OrderByDescending(log => log.CheckedInAt)
                    .ToListAsync();

            return Ok(logs);
        }
    }
}