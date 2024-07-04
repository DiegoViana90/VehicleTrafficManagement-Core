using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class VehicleService : IVehicleService
    {
        public Task<IEnumerable<VehicleDto>> GetAllVehicles()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<VehicleDto> GetVehicleById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task AddVehicle(VehicleDto vehicleDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateVehicle(int id, VehicleDto vehicleDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteVehicle(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
