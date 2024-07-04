using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ITrafficLogService
    {
        Task<IEnumerable<TrafficLogDto>> GetAllTrafficLogs();
        Task<TrafficLogDto> GetTrafficLogById(int id);
        Task AddTrafficLog(TrafficLogDto trafficLogDto);
        Task UpdateTrafficLog(int id, TrafficLogDto trafficLogDto);
        Task DeleteTrafficLog(int id);
    }
}
