using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ITrafficLogService
    {
        Task<IEnumerable<TrafficLogDto>> GetAllTrafficLogs();
        Task<TrafficLogDto> GetTrafficLogById(int id);
        Task InsertTrafficLog(TrafficLogDto trafficLogDto);
        Task UpdateTrafficLogById(int id, TrafficLogDto trafficLogDto);
        Task DeleteTrafficLogById(int id);
    }
}
