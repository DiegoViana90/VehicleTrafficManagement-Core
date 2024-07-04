using Microsoft.AspNetCore.Mvc;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IEnumerable<DriverDto>> Get()
        {
            return await _driverService.GetAllDrivers();
        }

        [HttpGet("{id}")]
        public async Task<DriverDto> Get(int id)
        {
            return await _driverService.GetDriverById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DriverDto driverDto)
        {
            await _driverService.AddDriver(driverDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DriverDto driverDto)
        {
            await _driverService.UpdateDriver(id, driverDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _driverService.DeleteDriver(id);
            return Ok();
        }
    }
}
