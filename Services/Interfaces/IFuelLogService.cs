using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IFuelLogService
    {
        Task<IEnumerable<FuelLogDto>> GetAllFuelLogs();
        Task<FuelLogDto> GetFuelLogById(int id);
        Task AddFuelLog(FuelLogDto fuelLogDto);
        Task UpdateFuelLog(int id, FuelLogDto fuelLogDto);
        Task DeleteFuelLog(int id);
    }
}
