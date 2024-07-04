using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllVehicles();
        Task<VehicleDto> GetVehicleById(int id);
        Task AddVehicle(VehicleDto vehicleDto);
        Task UpdateVehicle(int id, VehicleDto vehicleDto);
        Task DeleteVehicle(int id);
    }
}
