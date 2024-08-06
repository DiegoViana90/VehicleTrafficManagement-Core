using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.DTOs.Request;
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

        [HttpPost("GetFineByFineNumberAndVehicleId")]
        [SwaggerOperation(
     Summary = "Obtém uma multa pelo número e pelo ID do veículo.",
     Description = "Retorna uma multa com base no número fornecido e no ID do veículo."
 )]
        [SwaggerResponse(200, "Multa encontrada.")]
        [SwaggerResponse(404, "Multa não encontrada.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> GetFineByFineNumberAndVehicleId([FromBody]
         FineNumberAndVehicleIdRequest fineNumberAndVehicleIdRequest)
        {
            try
            {
                var fine = await _fineService.GetFineByFineNumberAndVehicleId(fineNumberAndVehicleIdRequest);
                if (fine == null)
                {
                    return NotFound(new { message = "Multa não encontrada." });
                }
                return Ok(fine);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno do servidor." });
            }
        }

        [HttpPost("InsertFine")]
        [SwaggerOperation(
        Summary = "Insere uma nova multa.",
        Description = "Adiciona uma nova multa ao sistema."
    )]
        [SwaggerResponse(201, "Multa criada com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(409, "Multa com este número já existe.")]
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
                if (ex.Message == "Multa com este número já existe para este veículo.")
                {
                    return Conflict(new { message = ex.Message });
                }

                return StatusCode(500, new { message = "Erro interno do servidor." });
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


        [HttpPut("UpdateFine")]
        [SwaggerOperation(
    Summary = "Atualiza uma multa existente.",
    Description = "Atualiza os dados de uma multa existente no sistema."
    )]
        [SwaggerResponse(200, "Multa atualizada com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(404, "Multa não encontrada.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> UpdateFine([FromBody] FineDto fineDto)
        {
            try
            {
                FineNumberAndVehicleIdRequest getRequest = new FineNumberAndVehicleIdRequest
                {
                    FineNumber = fineDto.FineNumber,
                    VehicleId = fineDto.VehicleId
                };
                
                var existingFine = await _fineService.GetFineByFineNumberAndVehicleId(getRequest);
                if (existingFine == null)
                {
                    return NotFound(new { message = "Multa não encontrada." });
                }

                await _fineService.UpdateFine(fineDto);
                return Ok(new { message = "Multa atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor." });
            }
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
