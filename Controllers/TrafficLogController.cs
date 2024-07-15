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
    public class TrafficLogController : ControllerBase
    {
        private readonly ITrafficLogService _trafficLogService;

        public TrafficLogController(ITrafficLogService trafficLogService)
        {
            _trafficLogService = trafficLogService;
        }

        [HttpGet("GetAllTrafficLogs")]
        [SwaggerOperation(
            Summary = "Obtém todos os registros de tráfego.",
            Description = "Recupera uma lista de todos os registros de tráfego no sistema."
        )]
        [SwaggerResponse(200, "Registros de tráfego recuperados com sucesso.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IEnumerable<TrafficLogDto>> GetAllTrafficLogs()
        {
            return await _trafficLogService.GetAllTrafficLogs();
        }

        [HttpGet("GetTrafficLogById")]
        [SwaggerOperation(
            Summary = "Obtém um registro de tráfego pelo ID.",
            Description = "Recupera um registro de tráfego específico pelo ID."
        )]
        [SwaggerResponse(200, "Registro de tráfego recuperado com sucesso.")]
        [SwaggerResponse(404, "Registro de tráfego não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<TrafficLogDto> GetTrafficLogById(int id)
        {
            return await _trafficLogService.GetTrafficLogById(id);
        }

        [HttpPost("InsertTrafficLog")]
        [SwaggerOperation(
            Summary = "Insere um novo registro de tráfego.",
            Description = "Adiciona um novo registro de tráfego ao sistema."
        )]
        [SwaggerResponse(201, "Registro de tráfego criado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> InsertTrafficLog([FromBody] TrafficLogDto trafficLogDto)
        {
            await _trafficLogService.InsertTrafficLog(trafficLogDto);
            return Ok();
        }

        [HttpPut("UpdateTrafficLogById")]
        [SwaggerOperation(
            Summary = "Atualiza um registro de tráfego.",
            Description = "Atualiza um registro de tráfego existente no sistema."
        )]
        [SwaggerResponse(200, "Registro de tráfego atualizado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(404, "Registro de tráfego não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> UpdateTrafficLogById(int id, [FromBody] TrafficLogDto trafficLogDto)
        {
            await _trafficLogService.UpdateTrafficLogById(id, trafficLogDto);
            return Ok();
        }

        [HttpDelete("DeleteTrafficLogById")]
        [SwaggerOperation(
            Summary = "Exclui um registro de tráfego.",
            Description = "Remove um registro de tráfego existente do sistema."
        )]
        [SwaggerResponse(200, "Registro de tráfego excluído com sucesso.")]
        [SwaggerResponse(404, "Registro de tráfego não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> DeleteTrafficLogById(int id)
        {
            await _trafficLogService.DeleteTrafficLogById(id);
            return Ok();
        }
    }
}
