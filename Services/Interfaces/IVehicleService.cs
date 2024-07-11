using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.DTOs.Request;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IVehicleService
    {
        // Task<IEnumerable<VehicleDto>> GetAllVehicles();
        // Task<VehicleDto> GetVehicleById(int id);
        // Task AddVehicle(VehicleDto vehicleDto);
        // Task UpdateVehicle(int id, VehicleDto vehicleDto);
        // Task DeleteVehicle(int id);
        Task InsertVehicleModel (InsertVehicleModelRequestDto insertVehicleModelRequestDto);
    }
}
