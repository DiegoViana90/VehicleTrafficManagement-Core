using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class ContractService : IContractService
    {
        public Task<IEnumerable<ContractDto>> GetAllContracts()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<ContractDto> GetContractById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task AddContract(ContractDto contractDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateContract(int id, ContractDto contractDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteContract(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
