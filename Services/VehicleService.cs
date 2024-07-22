using System;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.DTOs.Response;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Util;

namespace VehicleTrafficManagement.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertVehicleModel(InsertVehicleModelRequestDto insertVehicleModelRequestDto)
        {
            var existingModel = await _dbContext.VehicleModel
                .FirstOrDefaultAsync(vm => vm.ModelName == insertVehicleModelRequestDto.ModelName);

            if (existingModel != null)
            {
                throw new Exception($"Já existe um modelo de veículo com o nome '{insertVehicleModelRequestDto.ModelName}'.");
            }

            VehicleModel newVehicleModel = new VehicleModel
            {
                ModelName = insertVehicleModelRequestDto.ModelName,
                Manufacturer = insertVehicleModelRequestDto.Manufacturer,
                Observations = insertVehicleModelRequestDto.Observations
            };

            _dbContext.VehicleModel.Add(newVehicleModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<NewVehicleResponseDTO> InsertVehicle(InsertVehicleRequestDto insertVehicleRequestDto)
        {
            bool isChassisValid = Validator.IsChassisValid(insertVehicleRequestDto.Chassis);
            if (!isChassisValid)
            {
                throw new Exception($"Chassi {insertVehicleRequestDto.Chassis} Inválido.");
            }
            var existingVehicle = await _dbContext.Vehicles
                .FirstOrDefaultAsync(v => v.Chassis == insertVehicleRequestDto.Chassis);

            if (existingVehicle != null)
            {
                throw new Exception($"Veículo com chassi {insertVehicleRequestDto.Chassis} já existe.");
            }

            string hashedChassi = QrGenerator.ApplySaltAndHash(insertVehicleRequestDto.Chassis);
            string qrCodeBase64 = QrGenerator.GenerateQRCode(hashedChassi);

            Vehicle newVehicle = new Vehicle
            {
                VehicleModelId = insertVehicleRequestDto.VehicleModelId,
                LicensePlate = insertVehicleRequestDto.LicensePlate,
                Chassis = insertVehicleRequestDto.Chassis,
                Color = insertVehicleRequestDto.Color,
                FuelType = insertVehicleRequestDto.FuelType,
                Mileage = insertVehicleRequestDto.Mileage,
                Status = insertVehicleRequestDto.Status,
                ContractId = insertVehicleRequestDto.ContractId,
                StringQRCODE = qrCodeBase64,
                HashedChassi = hashedChassi,
            };

            _dbContext.Vehicles.Add(newVehicle);
            await _dbContext.SaveChangesAsync();

            NewVehicleResponseDTO newVehicleResponseDto = new NewVehicleResponseDTO
            {
                Message = "Veículo cadastrado com sucesso",
                LicensePlate = newVehicle.LicensePlate,
                StringQRCODE = newVehicle.StringQRCODE
            };

            return newVehicleResponseDto;
        }

        public async Task<GetVehicleDto> GetVehicleByQRCode(string QRCode)
        {
            string hashedChassi = QrGenerator.DecodeQRCode(QRCode);
            GetVehicleDto vehicleDto = await GetVehicleByHashedChassi(hashedChassi);
            return vehicleDto;
        }

         private async Task<GetVehicleDto> GetVehicleByHashedChassi(string hashedChassi)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.HashedChassi == hashedChassi)
                .Select(v => new GetVehicleDto
                {
                    Id = v.Id,
                    VehicleModelId = v.VehicleModelId,
                    LicensePlate = v.LicensePlate,
                    Chassis = v.Chassis,
                    Color = v.Color,
                    FuelType = v.FuelType,
                    Mileage = v.Mileage,
                    Status = v.Status,
                    ContractId = v.ContractId
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com hashed chassi {hashedChassi} não encontrado.");
            }

            return vehicle;
        }

       public async Task<GetVehicleDto> GetVehicleByLicensePlate(string licensePlate)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.LicensePlate == licensePlate)
                .Select(v => new GetVehicleDto
                {
                    Id = v.Id,
                    VehicleModelId = v.VehicleModelId,
                    LicensePlate = v.LicensePlate,
                    Chassis = v.Chassis,
                    Color = v.Color,
                    FuelType = v.FuelType,
                    Mileage = v.Mileage,
                    Status = v.Status,
                    ContractId = v.ContractId
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com placa {licensePlate} não encontrado.");
            }

            return vehicle;
        }

        public async Task<GetVehicleDto> GetVehicleById(int id)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.Id == id)
                .Select(v => new GetVehicleDto
                {
                    Id = v.Id,
                    VehicleModelId = v.VehicleModelId,
                    LicensePlate = v.LicensePlate,
                    Chassis = v.Chassis,
                    Color = v.Color,
                    FuelType = v.FuelType,
                    Mileage = v.Mileage,
                    Status = v.Status,
                    ContractId = v.ContractId
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com ID {id} não encontrado.");
            }

            return vehicle;
        }

        public async Task<GetVehicleDto> GetVehicleByChassis(string chassis)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.Chassis == chassis)
                .Select(v => new GetVehicleDto
                {
                    Id = v.Id,
                    VehicleModelId = v.VehicleModelId,
                    LicensePlate = v.LicensePlate,
                    Chassis = v.Chassis,
                    Color = v.Color,
                    FuelType = v.FuelType,
                    Mileage = v.Mileage,
                    Status = v.Status,
                    ContractId = v.ContractId
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com chassi {chassis} não encontrado.");
            }

            return vehicle;
        }

        public async Task<IEnumerable<VehicleModelDtoResponse>> GetAllVehicleModel()
        {
                 IEnumerable<VehicleModelDtoResponse> vehicleModelList = await _dbContext.VehicleModel
                .Select(vm => new VehicleModelDtoResponse
                {VehicleModelId = vm.VehicleModelId,
                 Manufacturer = vm.Manufacturer,
                 ModelName = vm.ModelName,
                 Observations = vm.Observations
                })
                 .ToListAsync();

                 return vehicleModelList;
        }
    }
}
