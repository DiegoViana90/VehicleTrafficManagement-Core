using Microsoft.AspNetCore.Mvc;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContractDto>> Get()
        {
            return await _contractService.GetAllContracts();
        }

        [HttpGet("{id}")]
        public async Task<ContractDto> Get(int id)
        {
            return await _contractService.GetContractById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContractDto contractDto)
        {
            await _contractService.AddContract(contractDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContractDto contractDto)
        {
            await _contractService.UpdateContract(id, contractDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contractService.DeleteContract(id);
            return Ok();
        }
    }
}
