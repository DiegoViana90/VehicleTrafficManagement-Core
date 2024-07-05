using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleDto>> Get()
        {
            return await _vehicleService.GetAllVehicles();
        }

        [HttpGet("{id}")]
        public async Task<VehicleDto> Get(int id)
        {
            return await _vehicleService.GetVehicleById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VehicleDto vehicleDto)
        {
            await _vehicleService.AddVehicle(vehicleDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VehicleDto vehicleDto)
        {
            await _vehicleService.UpdateVehicle(id, vehicleDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteVehicle(id);
            return Ok();
        }
    }
}
