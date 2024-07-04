using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class TrafficLogService : ITrafficLogService
    {
        public Task<IEnumerable<TrafficLogDto>> GetAllTrafficLogs()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<TrafficLogDto> GetTrafficLogById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task AddTrafficLog(TrafficLogDto trafficLogDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateTrafficLog(int id, TrafficLogDto trafficLogDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteTrafficLog(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
