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
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("GetAllVehicles")]
        [SwaggerOperation(Summary = "Busca todos os veículos.", 
        Description = "Recupera uma lista de todos os veículos.")]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<VehicleDto>))]
        public async Task<IEnumerable<VehicleDto>> GetAllVehicles()
        {
            return await _vehicleService.GetAllVehicles();
        }

        [HttpGet("GetVehicleById/{id}")]
        [SwaggerOperation(Summary = "Busca veículo por ID.", 
        Description = "Recupera um veículo específico pelo ID.")]
        [SwaggerResponse(200, "Success", typeof(VehicleDto))]
        [SwaggerResponse(404, "Vehicle not found")]
        public async Task<VehicleDto> GetVehicleById(int id)
        {
            return await _vehicleService.GetVehicleById(id);
        }

        [HttpPost("AddVehicle")]
        [SwaggerOperation(Summary = "Adiciona um novo veículo.",
         Description = "Adiciona um novo veículo ao sistema.")]
        [SwaggerResponse(201, "Vehicle created successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<IActionResult> AddVehicle([FromBody] InsertVehicleRequestDto requestDto)
        {
            var vehicleDto = new VehicleDto
            {
                LicensePlate = requestDto.LicensePlate,
                Chassis = requestDto.Chassis,
                Color = requestDto.Color,
                Brand = requestDto.Brand,
                Model = requestDto.Model,
                Mileage = requestDto.Mileage,
                Notes = requestDto.Notes
            };

            await _vehicleService.AddVehicle(vehicleDto);
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicleDto.Id }, vehicleDto);
        }

        [HttpPut("UpdateVehicle/{id}")]
        [SwaggerOperation(Summary = "Atualiza um veículo por Id.",
         Description = "Atualiza um veículo existente no sistema.")]
        [SwaggerResponse(200, "Vehicle updated successfully")]
        [SwaggerResponse(404, "Vehicle not found")]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleDto vehicleDto)
        {
            await _vehicleService.UpdateVehicle(id, vehicleDto);
            return Ok();
        }

        [HttpDelete("DeleteVehicle/{id}")]
        [SwaggerOperation(Summary = "Deleta um veículo.", 
        Description = "Delete um veículo pelo Id.")]
        [SwaggerResponse(200, "Vehicle deleted successfully")]
        [SwaggerResponse(404, "Vehicle not found")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            await _vehicleService.DeleteVehicle(id);
            return Ok();
        }
    }
}
