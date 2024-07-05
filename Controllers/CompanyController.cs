using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet("GetAllCompanies")]
        [SwaggerOperation(Summary = "Busca todas as empresas",
                           Description = "Recupera uma lista de todas as empresas.")]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<CompanyDto>))]
        public async Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            return await _companyService.GetAllCompanies();
        }

        [HttpGet("GetCompanyById/{id}")]
        [SwaggerOperation(Summary = "Busca empresa por ID",
                           Description = "Recupera uma empresa específica pelo ID.")]
        [SwaggerResponse(200, "Success", typeof(CompanyDto))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<IActionResult> GetCompanyByID(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost("AddCompany")]
        [SwaggerOperation(Summary = "Adiciona uma nova empresa",
        Description = "Adiciona uma nova empresa ao sistema.")]
        [SwaggerResponse(201, "Empresa criada com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        public async Task<IActionResult> AddCompany([FromBody] InsertCompanyRequestDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyId = await _companyService.AddCompany(companyDto);

            return CreatedAtAction(nameof(GetAllCompanies), new { id = companyId }, companyDto);
        }

        [HttpPut("UpdateCompany/{id}")]
        [SwaggerOperation(Summary = "Atualiza empresa por ID",
        Description = "Atualiza uma empresa existente no sistema.")]
        [SwaggerResponse(200, "Empresa atualizada com sucesso")]
        [SwaggerResponse(404, "Empresa não encontrada")]
        [SwaggerResponse(400, "Requisição inválida")]
        public async Task<IActionResult> UpdateCompanyByID(int id, [FromBody] CompanyDto companyDto)
        {
            await _companyService.UpdateCompany(id, companyDto);
            return Ok();
        }

        [HttpDelete("DeleteCompany/{id}")]
        [SwaggerOperation(Summary = "Deleta empresa por ID",
                           Description = "Deleta uma empresa pelo ID.")]
        [SwaggerResponse(200, "Empresa deletada com sucesso")]
        [SwaggerResponse(404, "Empresa não encontrada")]
        public async Task<IActionResult> DeleteCompanyByID(int id)
        {
            await _companyService.DeleteCompany(id);
            return Ok();
        }
    }
}
