using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class FineService : IFineService
    {
        public Task<IEnumerable<FineDto>> GetAllFines()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<FineDto> GetFineById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task AddFine(FineDto fineDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateFine(int id, FineDto fineDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteFine(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
