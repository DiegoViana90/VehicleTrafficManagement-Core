using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IFineService
    {
        Task<IEnumerable<FineDto>> GetAllFines();
        Task<FineDto> GetFineById(int id);
        Task InsertFine(FineDto fineDto);
        Task UpdateFineById(int id, FineDto fineDto);
        Task DeleteFineById(int id);
    }
}
