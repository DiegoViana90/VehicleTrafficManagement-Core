using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Enum;
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
            throw new NotImplementedException();
        }

        public async Task<ContractDto> GetContractById(int id)
        {
            // Implementar lógica
            throw new NotImplementedException();
        }

        public async Task<string> InsertContract(InsertContractRequestDto contractRequestDto)
        {
            var company = await _context.Companies.FindAsync(contractRequestDto.ClientCompanyId);
            if (company == null)
            {
                return "Cliente não encontrado";
            }

            var contract = new Contract
            {
                ServiceProviderCompanyId = contractRequestDto.ServiceProviderCompanyId,
                ClientCompanyId = contractRequestDto.ClientCompanyId,
                StartDate = contractRequestDto.StartDate,
                EndDate = contractRequestDto.EndDate,
                Status = contractRequestDto.Status,
                Vehicles = new List<Vehicle>()
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var vehicles = await _context.Vehicles
                        .Where(v => contractRequestDto.VehicleIds.Contains(v.Id) && v.ContractId != null)
                        .ToListAsync();

                    if (vehicles.Any())
                    {
                         throw new Exception("Um ou mais veículos já estão associados a um contrato.");
                    }

                    _context.Contracts.Add(contract);
                    await _context.SaveChangesAsync(); 

                    foreach (var vehicleId in contractRequestDto.VehicleIds)
                    {
                        var vehicle = await _context.Vehicles.FindAsync(vehicleId);
                        if (vehicle != null)
                        {
                            vehicle.Status = VehicleStatus.Contract;
                            vehicle.ContractId = contract.Id; 
                            _context.Entry(vehicle).State = EntityState.Modified;
                        }
                        else
                        throw new Exception("Um ou mais veículos não cadastrados.");
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    string insertContractResponse = $"Contrato da empresa {company.TradeName} criado com sucesso.";
                    return insertContractResponse;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); 
                    return ex.Message;
                }
            }
        }

        public async Task UpdateContract(int id, ContractDto contractDto)
        {
            // Implementar lógica
            throw new NotImplementedException();
        }

        public async Task DeleteContractById(int id)
        {
            // Implementar lógica
            throw new NotImplementedException();
        }
    }
}
