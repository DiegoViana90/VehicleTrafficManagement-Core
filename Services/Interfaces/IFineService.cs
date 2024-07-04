using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IFineService
    {
        Task<IEnumerable<FineDto>> GetAllFines();
        Task<FineDto> GetFineById(int id);
        Task AddFine(FineDto fineDto);
        Task UpdateFine(int id, FineDto fineDto);
        Task DeleteFine(int id);
    }
}
