using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Dto.Request;
using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/contract")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("GetAllContracts")]
        [SwaggerOperation(
            Summary = "Obtém todos os contratos.",
            Description = "Recupera uma lista de todos os contratos no sistema."
        )]
        [SwaggerResponse(200, "Contratos recuperados com sucesso.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IEnumerable<ContractDto>> GetAllContracts()
        {
            return await _contractService.GetAllContracts();
        }

        [HttpPost("GetContractById")]
        [SwaggerOperation(
            Summary = "Obtém um contrato pelo ID.",
            Description = "Recupera um contrato específico pelo ID."
        )]
        [SwaggerResponse(200, "Contrato recuperado com sucesso.")]
        [SwaggerResponse(404, "Contrato não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<ContractDto> GetContractById(GetContractByIdRequest getContractByIdRequest)
        {   int contractId = getContractByIdRequest.Id;
            return await _contractService.GetContractById(contractId);
        }

        [HttpPost("GetContractByCompanyName")]
        [SwaggerOperation(
            Summary = "Obtém um contrato pelo Nome da Contratante.",
            Description = "Recupera um contrato específico pelo Contratante."
        )]
        [SwaggerResponse(200, "Contrato recuperado com sucesso.")]
        [SwaggerResponse(404, "Contrato não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<ContractDto> GetContractByCompanyName
        (
            GetContractByCompanyNameRequest getContractByCompanyNameRequest
        )

        {   string companyName = getContractByCompanyNameRequest.Name;
            return await _contractService.GetContractByCompanyName(companyName);
        }

        [HttpPost("InsertContract")]
        [SwaggerOperation(
            Summary = "Insere um novo contrato.",
            Description = "Adiciona um novo contrato ao sistema."
        )]
        [SwaggerResponse(201, "Contrato criado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> InsertContract([FromBody] InsertContractRequestDto contractRequestDto)
        { 
            try
            {
            string insertContractResponse = await _contractService.InsertContract(contractRequestDto);
            return Ok(insertContractResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateContract")]
        [SwaggerOperation(
            Summary = "Atualiza um contrato.",
            Description = "Atualiza um contrato existente no sistema."
        )]
        [SwaggerResponse(200, "Contrato atualizado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(404, "Contrato não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> UpdateContract([FromBody] ContractDto contractDto)
        {
            await _contractService.UpdateContract(contractDto);
            return Ok();
        }

        [Authorize(Policy = "RequireMasterRole")]
        [HttpDelete("DeleteContractById")]
        [SwaggerOperation(
            Summary = "Exclui um contrato.",
            Description = "Remove um contrato existente do sistema."
        )]
        [SwaggerResponse(200, "Contrato excluído com sucesso.")]
        [SwaggerResponse(404, "Contrato não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> DeleteContractById(int id)
        {
            await _contractService.DeleteContractById(id);
            return Ok();
        }
    }
}
