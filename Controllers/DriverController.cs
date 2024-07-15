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
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("GetAllDrivers")]
        [SwaggerOperation(
            Summary = "Obtém todos os motoristas.",
            Description = "Recupera uma lista de todos os motoristas no sistema."
        )]
        [SwaggerResponse(200, "Motoristas recuperados com sucesso.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IEnumerable<DriverDto>> GetAllDrivers()
        {
            return await _driverService.GetAllDrivers();
        }

        [HttpGet("GetDriverById")]
        [SwaggerOperation(
            Summary = "Obtém um motorista pelo ID.",
            Description = "Recupera um motorista específico pelo ID."
        )]
        [SwaggerResponse(200, "Motorista recuperado com sucesso.")]
        [SwaggerResponse(404, "Motorista não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<DriverDto> GetDriverById(int id)
        {
            return await _driverService.GetDriverById(id);
        }

        [HttpPost("InsertDriver")]
        [SwaggerOperation(
            Summary = "Insere um novo motorista.",
            Description = "Adiciona um novo motorista ao sistema."
        )]
        [SwaggerResponse(201, "Motorista criado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> InsertDriver([FromBody] DriverDto driverDto)
        {
            await _driverService.InsertDriver(driverDto);
            return Ok();
        }

        [HttpPut("UpdateDriverById")]
        [SwaggerOperation(
            Summary = "Atualiza um motorista.",
            Description = "Atualiza um motorista existente no sistema."
        )]
        [SwaggerResponse(200, "Motorista atualizado com sucesso.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(404, "Motorista não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> UpdateDriverById(int id, [FromBody] DriverDto driverDto)
        {
            await _driverService.UpdateDriverById(id, driverDto);
            return Ok();
        }

        [HttpDelete("DeleteDriverById")]
        [SwaggerOperation(
            Summary = "Exclui um motorista.",
            Description = "Remove um motorista existente do sistema."
        )]
        [SwaggerResponse(200, "Motorista excluído com sucesso.")]
        [SwaggerResponse(404, "Motorista não encontrado.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> DeleteDriverById(int id)
        {
            await _driverService.DeleteDriverById(id);
            return Ok();
        }
    }
}
