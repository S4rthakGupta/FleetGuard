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
    }
}