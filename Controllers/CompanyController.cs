using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [Route("api/company/")]
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

        [HttpPost("GetCompanyByTaxNumber")]
        [SwaggerOperation(Summary = "Busca empresa por TaxNumber.",
         Description = "Recupera uma empresa específica pelo TaxNumber.")]
        [SwaggerResponse(200, "Success", typeof(CompanyDTOResult))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<ActionResult<CompanyDTOResult>> GetCompanyByTaxNumber([FromBody]
         GetCompanyByTaxNumberRequest getCompanyByTaxNumberRequest)
        {
            string taxNumber = getCompanyByTaxNumberRequest.TaxNumber;

            var company = await _companyService.GetCompanyByTaxNumberAsync(taxNumber);
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
        public async Task<ActionResult<string>> InsertCompany(InsertCompanyRequestDto insertCompanyRequestDto)
        {
            var companyName = await _companyService.InsertCompany(insertCompanyRequestDto);
            var message = $"A empresa {companyName} foi cadastrada com sucesso.";
            return Ok(new { Message = message });
        }

        [HttpPut("UpdateCompanById")]
        [SwaggerOperation(Summary = "Atualiza uma empresa por Id.",
         Description = "Atualiza uma empresa existente no sistema.")]
        [SwaggerResponse(200, "Company updated successfully")]
        [SwaggerResponse(404, "Company not found")]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<ActionResult> UpdateCompanById(int id, CompanyDto companyDto)
        {
            try
            {
                await _companyService.UpdateCompanById(id, companyDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("UpdateCompanByTaxNumber")]
        [SwaggerOperation(Summary = "Atualiza uma empresa pelo TaxNumber.",
         Description = "Atualiza uma empresa existente no sistema.")]
        [SwaggerResponse(200, "Company updated successfully")]
        [SwaggerResponse(404, "Company not found")]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<ActionResult> UpdateCompanByTaxNumber(UpdateCompanByTaxNumberRequest updateCompanByTaxNumberRequest)
        {
            try
            {
                await _companyService.UpdateCompanByTaxNumberAsync(updateCompanByTaxNumberRequest);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("DeleteCompanyById")]
        [SwaggerOperation(Summary = "Deleta uma empresa.",
        Description = "Deleta uma empresa pelo Id.")]
        [SwaggerResponse(200, "Company deleted successfully")]
        [SwaggerResponse(404, "Company not found")]
        public async Task<ActionResult> DeleteCompanyById(int id)
        {
            try
            {
                await _companyService.DeleteCompanyById(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
