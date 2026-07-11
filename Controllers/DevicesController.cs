using FleetGuard.Data;
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
            bool serialNumberExists =
                await _context.Devices.AnyAsync(device =>
                    device.SerialNumber == request.SerialNumber);

            if (serialNumberExists)
            {
                return Conflict(new
                {
                    message = "A device with this serial number already exists."
                });
            }

            Device device = new Device
            {
                DeviceName = request.DeviceName,
                SerialNumber = request.SerialNumber,
                Platform = request.Platform,
                OperatingSystemVersion = request.OperatingSystemVersion
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

            bool serialNumberExists =
                await _context.Devices.AnyAsync(existingDevice =>
                    existingDevice.SerialNumber == request.SerialNumber &&
                    existingDevice.Id != id);

            if (serialNumberExists)
            {
                return Conflict(new
                {
                    message = "Another device already uses this serial number."
                });
            }

            device.DeviceName = request.DeviceName;
            device.SerialNumber = request.SerialNumber;
            device.Platform = request.Platform;
            device.OperatingSystemVersion =
                request.OperatingSystemVersion;
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
    }
}