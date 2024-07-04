using Microsoft.AspNetCore.Mvc;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuelLogController : ControllerBase
    {
        private readonly IFuelLogService _fuelLogService;

        public FuelLogController(IFuelLogService fuelLogService)
        {
            _fuelLogService = fuelLogService;
        }

        [HttpGet]
        public async Task<IEnumerable<FuelLogDto>> Get()
        {
            return await _fuelLogService.GetAllFuelLogs();
        }

        [HttpGet("{id}")]
        public async Task<FuelLogDto> Get(int id)
        {
            return await _fuelLogService.GetFuelLogById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuelLogDto fuelLogDto)
        {
            await _fuelLogService.AddFuelLog(fuelLogDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FuelLogDto fuelLogDto)
        {
            await _fuelLogService.UpdateFuelLog(id, fuelLogDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fuelLogService.DeleteFuelLog(id);
            return Ok();
        }
    }
}
