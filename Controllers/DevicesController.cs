using FleetGuard.Models;
using FleetGuard.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FleetGuard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private static readonly List<Device> Devices = new();
        [HttpPost]
        public ActionResult<Device> RegisterDevice(RegisterDeviceRequest request)
        {
            Device device = new Device
            {
                DeviceName = request.DeviceName,
                SerialNumber = request.SerialNumber,
                Platform = request.Platform,
                OperatingSystemVersion = request.OperatingSystemVersion
            };

            Devices.Add(device);

            return CreatedAtAction(
                nameof(GetDeviceById),
                new { id = device.Id },
                device);
        }
        [HttpGet]
        public ActionResult<List<Device>> GetAllDevices()
        {
            return Ok(Devices);
        }


        [HttpGet("{id:guid}")]
        public ActionResult<Device> GetDeviceById(Guid id)
        {
            Device? device = Devices.FirstOrDefault(
                device => device.Id == id);

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
