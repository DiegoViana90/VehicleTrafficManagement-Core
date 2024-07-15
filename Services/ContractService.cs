using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Services
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _context;

        public ContractService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContractDto>> GetAllContracts()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public async Task<ContractDto> GetContractById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public async Task InsertContract(InsertContractRequestDto contractRequestDto)
        {
            var contract = new Contract
            {
                ServiceProviderCompanyId = contractRequestDto.ServiceProviderCompanyId,
                ClientCompanyId = contractRequestDto.ClientCompanyId,
                StartDate = contractRequestDto.StartDate,
                EndDate = contractRequestDto.EndDate,
                Status = contractRequestDto.Status,
                Vehicles = new List<Vehicle>()
            };

            foreach (var vehicleId in contractRequestDto.VehicleIds)
            {
                var vehicle = await _context.Vehicles.FindAsync(vehicleId);
                if (vehicle != null)
                {
                    contract.Vehicles.Add(vehicle);
                }
            }

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContract(int id, ContractDto contractDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public async Task DeleteContractById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}
