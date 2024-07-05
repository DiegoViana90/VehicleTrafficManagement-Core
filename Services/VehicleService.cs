using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehicles()
        {
            var vehicles = await _dbContext.Vehicles
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Chassis = v.Chassis,
                    Color = v.Color,
                    Brand = v.Brand,
                    Model = v.Model,
                    Mileage = v.Mileage,
                    Notes = v.Observations
                })
                .ToListAsync();

            return vehicles;
        }

        public async Task<VehicleDto> GetVehicleById(int id)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(v => v.Id == id)
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Chassis = v.Chassis,
                    Color = v.Color,
                    Brand = v.Brand,
                    Model = v.Model,
                    Mileage = v.Mileage,
                    Notes = v.Observations
                })
                .FirstOrDefaultAsync();

            return vehicle;
        }

        public async Task AddVehicle(VehicleDto vehicleDto)
        {
            var newVehicle = new Vehicle
            {
                LicensePlate = vehicleDto.LicensePlate,
                Chassis = vehicleDto.Chassis,
                Color = vehicleDto.Color,
                Brand = vehicleDto.Brand,
                Model = vehicleDto.Model,
                Mileage = vehicleDto.Mileage,
                Observations = vehicleDto.Notes
                // Preencher outros campos conforme necessário
            };

            _dbContext.Vehicles.Add(newVehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVehicle(int id, VehicleDto vehicleDto)
        {
            var existingVehicle = await _dbContext.Vehicles.FindAsync(id);

            if (existingVehicle == null)
            {
                throw new Exception($"Veículo com ID {id} não encontrado.");
            }

            existingVehicle.LicensePlate = vehicleDto.LicensePlate;
            existingVehicle.Chassis = vehicleDto.Chassis;
            existingVehicle.Color = vehicleDto.Color;
            existingVehicle.Brand = vehicleDto.Brand;
            existingVehicle.Model = vehicleDto.Model;
            existingVehicle.Mileage = vehicleDto.Mileage;
            existingVehicle.Observations = vehicleDto.Notes;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            var vehicleToDelete = await _dbContext.Vehicles.FindAsync(id);

            if (vehicleToDelete == null)
            {
                throw new Exception($"Veículo com ID {id} não encontrado.");
            }

            _dbContext.Vehicles.Remove(vehicleToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
