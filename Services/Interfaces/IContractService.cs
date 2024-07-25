using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Dto.Request;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllContracts();
        Task<ContractDto> GetContractById(int contractId);
        Task<ContractDto> GetContractByCompanyName(string companyName);
        Task<string> InsertContract(InsertContractRequestDto contractRequestDto);
        Task UpdateContract(int id, ContractDto contractDto);
        Task DeleteContractById(int id);
    }
}
