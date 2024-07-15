using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class FuelLogService : IFuelLogService
    {
        public Task<IEnumerable<FuelLogDto>> GetAllFuelLogs()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<FuelLogDto> GetFuelLogById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task InsertFuelLog(FuelLogDto fuelLogDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateFuelLogById(int id, FuelLogDto fuelLogDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteFuelLogById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
