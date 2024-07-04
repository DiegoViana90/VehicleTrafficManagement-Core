using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrafficLogController : ControllerBase
    {
        private readonly ITrafficLogService _trafficLogService;

        public TrafficLogController(ITrafficLogService trafficLogService)
        {
            _trafficLogService = trafficLogService;
        }

        [HttpGet]
        public async Task<IEnumerable<TrafficLogDto>> Get()
        {
            return await _trafficLogService.GetAllTrafficLogs();
        }

        [HttpGet("{id}")]
        public async Task<TrafficLogDto> Get(int id)
        {
            return await _trafficLogService.GetTrafficLogById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrafficLogDto trafficLogDto)
        {
            await _trafficLogService.AddTrafficLog(trafficLogDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TrafficLogDto trafficLogDto)
        {
            await _trafficLogService.UpdateTrafficLog(id, trafficLogDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _trafficLogService.DeleteTrafficLog(id);
            return Ok();
        }
    }
}
