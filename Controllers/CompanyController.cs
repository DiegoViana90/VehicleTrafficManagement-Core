using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Busca todas as empresas.",
        Description = "Recupera uma lista de todas as empresas. FLUXO OK")]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<CompanyDTOResult>))]
        public async Task<ActionResult<IEnumerable<CompanyDTOResult>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [HttpGet("GetCompanyById")]
        [SwaggerOperation(Summary = "Busca empresa por ID.",
        Description = "Recupera uma empresa específica pelo ID. FLUXO OK")]
        [SwaggerResponse(200, "Success", typeof(CompanyDto))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet("GetCompanyByName")]
        [SwaggerOperation(Summary = "Busca empresa por nome.",
        Description = "Recupera uma empresa específica pelo nome. FLUXO OK")]
        [SwaggerResponse(200, "Success", typeof(CompanyDto))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<ActionResult<CompanyDto>> GetCompanyByName(string name)
        {
            var company = await _companyService.GetCompanyByName(name);
            if (company == null)
            {
                return NotFound("Empresa não encontrada.");
            }

            return Ok(company);
        }

        [HttpGet("GetCompanyByCnpj")]
        [SwaggerOperation(Summary = "Busca empresa por CNPJ.",
         Description = "Recupera uma empresa específica pelo CNPJ. FLUXO OK")]
        [SwaggerResponse(200, "Success", typeof(CompanyDTOResult))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<ActionResult<CompanyDTOResult>> GetCompanyByCnpj(string CNPJ)
        {
            var company = await _companyService.GetCompanyByCnpjAsync(CNPJ);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost("InsertCompany")]
        [SwaggerOperation(Summary = "Adiciona uma nova empresa.",
        Description = "Adiciona uma nova empresa ao sistema.  FLUXO OK")]
        [SwaggerResponse(200, "Company created successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<ActionResult<string>> InsertCompany(InsertCompanyRequestDto companyDto)
        {
            var companyName = await _companyService.InsertCompany(companyDto);
            var message = $"A empresa {companyName} foi cadastrada com sucesso.";
            return Ok(new { Message = message });
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma empresa por Id.",
         Description = "Atualiza uma empresa existente no sistema.")]
        [SwaggerResponse(200, "Company updated successfully")]
        [SwaggerResponse(404, "Company not found")]
        [SwaggerResponse(400, "Invalid request")]
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
        [SwaggerOperation(Summary = "Deleta uma empresa.",
        Description = "Deleta uma empresa pelo Id.")]
        [SwaggerResponse(200, "Company deleted successfully")]
        [SwaggerResponse(404, "Company not found")]
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
