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
    public class FuelLogController : ControllerBase
    {
        private readonly IFuelLogService _fuelLogService;

        public FuelLogController(IFuelLogService fuelLogService)
        {
            _fuelLogService = fuelLogService;
        }

        [HttpGet("GetAllFuelLogs")]
        [SwaggerOperation(
            Summary = "Obtém todos os registros de combustível.",
            Description = "Recupera uma lista de todos os registros de combustível no sistema."
        )]
        [SwaggerResponse(200, "Registros de combustível recuperados com sucesso.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IEnumerable<FuelLogDto>> GetAllFuelLogs()
        {
            return await _fuelLogService.GetAllFuelLogs();
        }

        [HttpGet("GetFuelLogById")]
        [SwaggerOperation(
            Summary = "Obtém um registro de combustível pelo ID.",
            Description = "Recupera um registro de combustível específico pelo ID."
        )]
        [SwaggerResponse(200, "Registro de combustível recuperado com sucesso.")]
        [SwaggerResponse(404, "Registro de combustível não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<FuelLogDto> GetFuelLogById(int id)
        {
            return await _fuelLogService.GetFuelLogById(id);
        }

        [HttpPost("InsertFuelLog")]
        [SwaggerOperation(
            Summary = "Insere um novo registro de combustível.",
            Description = "Adiciona um novo registro de combustível ao sistema."
        )]
        [SwaggerResponse(201, "Registro de combustível criado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> InsertFuelLog([FromBody] FuelLogDto fuelLogDto)
        {
            await _fuelLogService.InsertFuelLog(fuelLogDto);
            return Ok();
        }

        [HttpPut("UpdateFuelLogByIdById")]
        [SwaggerOperation(
            Summary = "Atualiza um registro de combustível.",
            Description = "Atualiza um registro de combustível existente no sistema."
        )]
        [SwaggerResponse(200, "Registro de combustível atualizado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(404, "Registro de combustível não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> UpdateFuelLogByIdById(int id, [FromBody] FuelLogDto fuelLogDto)
        {
            await _fuelLogService.UpdateFuelLogById(id, fuelLogDto);
            return Ok();
        }

        [HttpDelete("DeleteFuelLogByIdById")]
        [SwaggerOperation(
            Summary = "Exclui um registro de combustível.",
            Description = "Remove um registro de combustível existente do sistema."
        )]
        [SwaggerResponse(200, "Registro de combustível excluído com sucesso.")]
        [SwaggerResponse(404, "Registro de combustível não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> DeleteFuelLogByIdById(int id)
        {
            await _fuelLogService.DeleteFuelLogById(id);
            return Ok();
        }
    }
}
