using Microsoft.AspNetCore.Mvc;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet]
        public async Task<IEnumerable<FineDto>> Get()
        {
            return await _fineService.GetAllFines();
        }

        [HttpGet("{id}")]
        public async Task<FineDto> Get(int id)
        {
            return await _fineService.GetFineById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FineDto fineDto)
        {
            await _fineService.AddFine(fineDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FineDto fineDto)
        {
            await _fineService.UpdateFine(id, fineDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fineService.DeleteFine(id);
            return Ok();
        }
    }
}
