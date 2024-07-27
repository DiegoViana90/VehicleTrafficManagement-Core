using System;
using System.Collections.Generic;
using System.Linq;
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
                .FirstOrDefaultAsync(v => v.Chassis == insertVehicleRequestDto.Chassis
                                          && v.CompaniesId == insertVehicleRequestDto.CompaniesId);

            if (existingVehicle != null)
            {
                throw new Exception($"Veículo com chassi {insertVehicleRequestDto.Chassis} já existe para a empresa {insertVehicleRequestDto.CompaniesId}.");
            }

            var company = await _dbContext.Companies.FindAsync(insertVehicleRequestDto.CompaniesId);
            if (company == null)
            {
                throw new Exception("Empresa não encontrada");
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
                ModelYear = insertVehicleRequestDto.ModelYear,
                ManufactureYear = insertVehicleRequestDto.ManufactureYear,
                CompaniesId = insertVehicleRequestDto.CompaniesId
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
            string hashedChassi = QRCode;
            GetVehicleDto vehicleDto = await GetVehicleByHashedChassi(hashedChassi);
            return vehicleDto;
        }

        private async Task<GetVehicleDto> GetVehicleByHashedChassi(string hashedChassi)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.HashedChassi == hashedChassi)
                .Select(v => new
                {
                    v.Id,
                    v.VehicleModelId,
                    v.LicensePlate,
                    v.Chassis,
                    v.Color,
                    v.FuelType,
                    v.Mileage,
                    v.Status,
                    v.ContractId,
                    v.ModelYear,
                    v.ManufactureYear
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com hashed chassi {hashedChassi} não encontrado.");
            }

            var vehicleModel = await _dbContext.VehicleModel
                .Where(vm => vm.VehicleModelId == vehicle.VehicleModelId)
                .Select(vm => new
                {
                    vm.ModelName,
                    vm.Manufacturer,
                    vm.Observations
                })
                .FirstOrDefaultAsync();

            if (vehicleModel == null)
            {
                throw new Exception($"Modelo do veículo com ID {vehicle.VehicleModelId} não encontrado.");
            }

            GetVehicleDto getVehicleDtoResult = new GetVehicleDto
            {
                Id = vehicle.Id,
                VehicleModelId = vehicle.VehicleModelId,
                LicensePlate = vehicle.LicensePlate,
                Chassis = vehicle.Chassis,
                Color = vehicle.Color,
                FuelType = vehicle.FuelType,
                Mileage = vehicle.Mileage,
                Status = vehicle.Status,
                ContractId = vehicle.ContractId,
                ModelYear = vehicle.ModelYear,
                ManufactureYear = vehicle.ManufactureYear,
                Manufacturer = vehicleModel.Manufacturer,
                ModelName = vehicleModel.ModelName,
                Observations = vehicleModel.Observations
            };

            return getVehicleDtoResult;
        }


        public async Task<GetVehicleDto> GetVehicleByLicensePlate(string licensePlate, int companyId)
        {
            var company = await _dbContext.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            var vehicle = await _dbContext.Vehicles
                .Where(v => v.LicensePlate == licensePlate && v.CompaniesId == companyId)
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
                    ContractId = v.ContractId,
                    ModelYear = v.ModelYear,
                    ManufactureYear = v.ManufactureYear
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com placa {licensePlate} não encontrado na empresa com ID {companyId}.");
            }

            var vehicleModel = await _dbContext.VehicleModel
                .Where(vm => vm.VehicleModelId == vehicle.VehicleModelId)
                .Select(vm => new
                {
                    vm.ModelName,
                    vm.Manufacturer,
                    vm.Observations
                })
                .FirstOrDefaultAsync();

            if (vehicleModel == null)
            {
                throw new Exception($"Modelo do veículo com ID {vehicle.VehicleModelId} não encontrado.");
            }

            GetVehicleDto getVehicleDtoresult = new GetVehicleDto
            {
                Id = vehicle.Id,
                VehicleModelId = vehicle.VehicleModelId,
                LicensePlate = vehicle.LicensePlate,
                Chassis = vehicle.Chassis,
                Color = vehicle.Color,
                FuelType = vehicle.FuelType,
                Mileage = vehicle.Mileage,
                Status = vehicle.Status,
                ContractId = vehicle.ContractId,
                ModelYear = vehicle.ModelYear,
                ManufactureYear = vehicle.ManufactureYear,
                Manufacturer = vehicleModel.Manufacturer,
                ModelName = vehicleModel.ModelName,
                Observations = vehicleModel.Observations
            };

            return getVehicleDtoresult;
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
                    ContractId = v.ContractId,
                    ModelYear = v.ModelYear,
                    ManufactureYear = v.ManufactureYear
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com ID {id} não encontrado.");
            }

            return vehicle;
        }

        public async Task<GetVehicleDto> GetVehicleModelById(int id)
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
                    ContractId = v.ContractId,
                    ModelYear = v.ModelYear,
                    ManufactureYear = v.ManufactureYear
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com ID {id} não encontrado.");
            }

            return vehicle;
        }

        public async Task<GetVehicleDto> GetVehicleByChassis(string chassis, int companyId)
        {
            var company = await _dbContext.Companies.FindAsync(companyId);
            if (company == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            var vehicle = await _dbContext.Vehicles
                .Where(v => v.Chassis == chassis && v.CompaniesId == companyId)
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
                    ContractId = v.ContractId,
                    ModelYear = v.ModelYear,
                    ManufactureYear = v.ManufactureYear
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                throw new Exception($"Veículo com chassi {chassis} não encontrado na empresa com ID {companyId}.");
            }

            var vehicleModel = await _dbContext.VehicleModel
                .Where(vm => vm.VehicleModelId == vehicle.VehicleModelId)
                .Select(vm => new
                {
                    vm.ModelName,
                    vm.Manufacturer,
                    vm.Observations
                })
                .FirstOrDefaultAsync();

            if (vehicleModel == null)
            {
                throw new Exception($"Modelo do veículo com ID {vehicle.VehicleModelId} não encontrado.");
            }

            GetVehicleDto getVehicleDtoresult = new GetVehicleDto
            {
                Id = vehicle.Id,
                VehicleModelId = vehicle.VehicleModelId,
                LicensePlate = vehicle.LicensePlate,
                Chassis = vehicle.Chassis,
                Color = vehicle.Color,
                FuelType = vehicle.FuelType,
                Mileage = vehicle.Mileage,
                Status = vehicle.Status,
                ContractId = vehicle.ContractId,
                ModelYear = vehicle.ModelYear,
                ManufactureYear = vehicle.ManufactureYear,
                Manufacturer = vehicleModel.Manufacturer,
                ModelName = vehicleModel.ModelName,
                Observations = vehicleModel.Observations
            };

            return getVehicleDtoresult;
        }


        public async Task<IEnumerable<VehicleModelDtoResponse>> GetAllVehicleModel()
        {
            IEnumerable<VehicleModelDtoResponse> vehicleModelList = await _dbContext.VehicleModel
           .Select(vm => new VehicleModelDtoResponse
           {
               VehicleModelId = vm.VehicleModelId,
               Manufacturer = vm.Manufacturer,
               ModelName = vm.ModelName,
               Observations = vm.Observations
           })
            .ToListAsync();

            return vehicleModelList;
        }

        public async Task<IEnumerable<GetVehicleDto>> GetAllVehicles()
        {
            IEnumerable<GetVehicleDto> vehicleList = await _dbContext.Vehicles
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
               ContractId = v.ContractId,
               ModelYear = v.ModelYear,
               ManufactureYear = v.ManufactureYear,
               Manufacturer = v.VehicleModel.Manufacturer,
               ModelName = v.VehicleModel.ModelName,
               Observations = v.VehicleModel.Observations
           })
            .ToListAsync();

            return vehicleList;
        }
        public async Task<IEnumerable<GetVehicleDto>> GetAllVehiclesFromCompany(int companyId)
        {
            IEnumerable<GetVehicleDto> vehicleList = await _dbContext.Vehicles
                .Where(v => v.CompaniesId == companyId)
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
                    ContractId = v.ContractId,
                    ModelYear = v.ModelYear,
                    ManufactureYear = v.ManufactureYear,
                    Manufacturer = v.VehicleModel.Manufacturer,
                    ModelName = v.VehicleModel.ModelName,
                    Observations = v.VehicleModel.Observations
                })
                .ToListAsync();

            return vehicleList;
        }
    }
}
