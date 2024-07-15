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

        public Task InsertFine(FineDto fineDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateFineById(int id, FineDto fineDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteFineById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
