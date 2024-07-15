using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IFuelLogService
    {
        Task<IEnumerable<FuelLogDto>> GetAllFuelLogs();
        Task<FuelLogDto> GetFuelLogById(int id);
        Task InsertFuelLog(FuelLogDto fuelLogDto);
        Task UpdateFuelLogById(int id, FuelLogDto fuelLogDto);
        Task DeleteFuelLogById(int id);
    }
}
