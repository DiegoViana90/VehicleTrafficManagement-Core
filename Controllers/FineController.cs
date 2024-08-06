using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet("GetAllFines")]
        [SwaggerOperation(
            Summary = "Obtém todas as multas.",
            Description = "Recupera uma lista de todas as multas no sistema."
        )]
        [SwaggerResponse(200, "Multas recuperadas com sucesso.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IEnumerable<FineDto>> GetAllFines()
        {
            return await _fineService.GetAllFines();
        }

        [HttpGet("GetFineById")]
        [SwaggerOperation(
            Summary = "Obtém uma multa pelo ID.",
            Description = "Recupera uma multa específica pelo ID."
        )]
        [SwaggerResponse(200, "Multa recuperada com sucesso.")]
        [SwaggerResponse(404, "Multa não encontrada.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<FineDto> GetFineById(int id)
        {
            return await _fineService.GetFineById(id);
        }

        [HttpPost("InsertFine")]
        [SwaggerOperation(
            Summary = "Insere uma nova multa.",
            Description = "Adiciona uma nova multa ao sistema."
        )]
        [SwaggerResponse(201, "Multa criada com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> InsertFine([FromBody] FineDto fineDto)
        {
            try
            {
                await _fineService.InsertFine(fineDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("UpdateFineById")]
        [SwaggerOperation(
            Summary = "Atualiza uma multa.",
            Description = "Atualiza uma multa existente no sistema."
        )]
        [SwaggerResponse(200, "Multa atualizada com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(404, "Multa não encontrada.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> UpdateFineByIdById(int id, [FromBody] FineDto fineDto)
        {
            await _fineService.UpdateFineById(id, fineDto);
            return Ok();
        }

        [HttpDelete("DeleteFineById")]
        [SwaggerOperation(
            Summary = "Exclui uma multa.",
            Description = "Remove uma multa existente do sistema."
        )]
        [SwaggerResponse(200, "Multa excluída com sucesso.")]
        [SwaggerResponse(404, "Multa não encontrada.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> DeleteFineById(int id)
        {
            await _fineService.DeleteFineById(id);
            return Ok();
        }
    }
}
