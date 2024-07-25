using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.DTOs.Response;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Controllers
{
    [ApiController]
    [Route("api/vehicle/")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("InsertVehicleModel")]
        [SwaggerOperation(Summary = "Adiciona um novo Modelo de veículo ao sistema.")]
        [SwaggerResponse(201, "VehicleModel created successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<IActionResult> InsertVehicleModel([FromBody] InsertVehicleModelRequestDto insertVehicleModelRequestDto)
        {
            try
            {
                await _vehicleService.InsertVehicleModel(insertVehicleModelRequestDto);
                var message = $"Veículo {insertVehicleModelRequestDto.ModelName} foi cadastrado com sucesso.";
                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("GetAllVehicleModel")]
        [SwaggerOperation(Summary = "Busca todos os modelos de veículo cadastrado no sistema.")]
        [SwaggerResponse(201, "ok")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<ActionResult<IEnumerable<VehicleModelDtoResponse>>>GetAllVehicleModel()
        {
            try
            {var vehicleModelList = await _vehicleService.GetAllVehicleModel();
               
                return Ok(vehicleModelList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("GetAllVehicles")]
        [SwaggerOperation(Summary = "Busca todos os veículos cadastrado no sistema.")]
        [SwaggerResponse(201, "ok")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<ActionResult<IEnumerable<GetVehicleDto>>>GetAllVehicles()
        {
            try
            {var vehicleModelList = await _vehicleService.GetAllVehicles();
               
                return Ok(vehicleModelList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        

        [HttpPost("InsertVehicle")]
        [SwaggerOperation(
        Summary = "Adiciona um novo veículo.",
        Description = "Adiciona um novo veículo ao sistema."
        )]
        [SwaggerResponse(201, "Vehicle created successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<IActionResult> InsertVehicle([FromBody] InsertVehicleRequestDto insertVehicleRequestDto)
        {
            try
            {
                var newVehicleResponseDTO = await _vehicleService.InsertVehicle(insertVehicleRequestDto);
                return CreatedAtAction(nameof(GetVehicleById), new { id = newVehicleResponseDTO.LicensePlate }, newVehicleResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("GetVehicleByQRCode")]
        [SwaggerOperation(
            Summary = "Busca o veículo pelo QRCode.",
            Description = "Busca o veículo pelo QRCode."
        )]
        [SwaggerResponse(200, "Veículo encontrado com sucesso.")]
        [SwaggerResponse(404, "Veículo não encontrado.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<GetVehicleDto> GetVehicleByQRCode
        (GetVehicleByQrCodetDTORequest getVehicleByQrCodetDTORequest)
        {
            string QRCode = getVehicleByQrCodetDTORequest.QRCode;
            GetVehicleDto vehicle = await _vehicleService.GetVehicleByQRCode(QRCode);
            return vehicle;
        }

        [HttpGet("GetVehicleById")]
        [SwaggerOperation(
            Summary = "Busca o veículo pelo ID.",
            Description = "Busca o veículo pelo ID."
        )]
        [SwaggerResponse(200, "Veículo encontrado com sucesso.")]
        [SwaggerResponse(404, "Veículo não encontrado.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(id);
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("GetVehicleByChassis")]
        [SwaggerOperation(
            Summary = "Busca o veículo pelo chassi.",
            Description = "Busca o veículo pelo chassi."
        )]
        [SwaggerResponse(200, "Veículo encontrado com sucesso.")]
        [SwaggerResponse(404, "Veículo não encontrado.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> GetVehicleByChassis
        (GetVehicleByChassisRequestDTO getVehicleByChassisRequestDTO)
        {
            try
            {
                string chassis = getVehicleByChassisRequestDTO.Chassis;
                var vehicle = await _vehicleService.GetVehicleByChassis(chassis);
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("GetVehicleByLicensePlate")]
        [SwaggerOperation(
            Summary = "Busca o veículo pela placa.",
            Description = "Busca o veículo pela placa."
        )]
        [SwaggerResponse(200, "Veículo encontrado com sucesso.")]
        [SwaggerResponse(404, "Veículo não encontrado.")]
        [SwaggerResponse(400, "Requisição inválida.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public async Task<IActionResult> GetVehicleByLicensePlate
        (GetVehicleByLicensePlateRequestDTO getVehicleByLicensePlateRequestDTO)
        {
            try
            {
                string licensePlate = getVehicleByLicensePlateRequestDTO.LicensePlate;
                var vehicle = await _vehicleService.GetVehicleByLicensePlate(licensePlate);
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
