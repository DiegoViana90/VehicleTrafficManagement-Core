using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<IEnumerable<CompanyDTOResult>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet("GetCompanyByCnpj/")]
        public async Task<ActionResult<CompanyDTOResult>> GetCompanyByCnpj(string CNPJ)
        {
            var company = await _companyService.GetCompanyByCnpjAsync(CNPJ);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost("AddCompany")]
        public async Task<ActionResult<int>> AddCompany(InsertCompanyRequestDto companyDto)
        {
            var companyId = await _companyService.AddCompany(companyDto);
            return Ok(companyId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCompany(int id, CompanyDto companyDto)
        {
            try
            {
                await _companyService.UpdateCompany(id, companyDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            try
            {
                await _companyService.DeleteCompany(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
