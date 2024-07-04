using Microsoft.AspNetCore.Mvc;

using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> Get()
        {
            return await _companyService.GetAllCompanies();
        }

        [HttpGet("{id}")]
        public async Task<CompanyDto> Get(int id)
        {
            return await _companyService.GetCompanyById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDto companyDto)
        {
            await _companyService.AddCompany(companyDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompanyDto companyDto)
        {
            await _companyService.UpdateCompany(id, companyDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteCompany(id);
            return Ok();
        }
    }
}
