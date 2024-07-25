using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllContracts();
        Task<ContractDto> GetContractById(int id);
        Task<string> InsertContract(InsertContractRequestDto contractRequestDto);
        Task UpdateContract(int id, ContractDto contractDto);
        Task DeleteContractById(int id);
    }
}
