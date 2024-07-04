using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllContracts();
        Task<ContractDto> GetContractById(int id);
        Task AddContract(ContractDto contractDto);
        Task UpdateContract(int id, ContractDto contractDto);
        Task DeleteContract(int id);
    }
}
