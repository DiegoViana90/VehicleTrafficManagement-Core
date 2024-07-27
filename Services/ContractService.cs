using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Dto.Request;
using VehicleTrafficManagement.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ContractDto> GetContractById(int contractId)
        {
            var contract = await _context.Contracts
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == contractId);

            if (contract == null)
            {
                throw new Exception("Contrato não encontrado");
            }

            ContractDto contractDto = new ContractDto
            {
                Id = contract.Id,
                ServiceProviderCompanyId = contract.ServiceProviderCompanyId,
                ClientCompanyId = contract.ClientCompanyId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Status = contract.Status,
                VehicleIds = contract.Vehicles.Select(v => v.Id).ToList() // Liste os IDs dos veículos
            };

            return contractDto;
        }

        public async Task<ContractDto> GetContractByCompanyName(string companyName)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Name == companyName);

            if (company == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            var contract = await _context.Contracts
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.ClientCompanyId == company.CompaniesId && c.Status == ContractStatus.Active);

            if (contract == null)
            {
                throw new Exception("Contrato não encontrado");
            }

            ContractDto contractDto = new ContractDto
            {
                Id = contract.Id,
                ServiceProviderCompanyId = contract.ServiceProviderCompanyId,
                ClientCompanyId = contract.ClientCompanyId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Status = contract.Status,
                VehicleIds = contract.Vehicles.Select(v => v.Id).ToList()
            };

            return contractDto;
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
                StartDate = DateTime.SpecifyKind(contractRequestDto.StartDate, DateTimeKind.Utc),
                EndDate = contractRequestDto.EndDate.HasValue ? DateTime.SpecifyKind(contractRequestDto.EndDate.Value, DateTimeKind.Utc) : (DateTime?)null,
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
                        {
                            throw new Exception("Um ou mais veículos não cadastrados.");
                        }
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

        public async Task<string> UpdateContract(ContractDto contractDto)
        {
            var contract = await _context.Contracts.FindAsync(contractDto.Id);
            if (contract == null)
            {
                return "Contrato não encontrado";
            }

            var company = await _context.Companies.FindAsync(contractDto.ClientCompanyId);
            if (company == null)
            {
                return "Cliente não encontrado";
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    contract.ServiceProviderCompanyId = contractDto.ServiceProviderCompanyId;
                    contract.ClientCompanyId = contractDto.ClientCompanyId;
                    contract.StartDate = DateTime.SpecifyKind(contractDto.StartDate, DateTimeKind.Utc);
                    contract.EndDate = contractDto.EndDate.HasValue ? DateTime.SpecifyKind(contractDto.EndDate.Value, DateTimeKind.Utc) : (DateTime?)null;

                    var vehiclesInContract = await _context.Vehicles
                        .Where(v => v.ContractId == contractDto.Id)
                        .ToListAsync();

                    var vehicleIdsToUpdate = contractDto.VehicleIds.ToHashSet();
                    var vehiclesToRemove = vehiclesInContract.Where(v => !vehicleIdsToUpdate.Contains(v.Id)).ToList();
                    var vehicleIdsToAdd = vehicleIdsToUpdate.Except(vehiclesInContract.Select(v => v.Id)).ToList();

                    foreach (var vehicle in vehiclesToRemove)
                    {
                        vehicle.Status = VehicleStatus.Stopped;
                        vehicle.ContractId = null;
                        _context.Entry(vehicle).State = EntityState.Modified;
                    }

                    foreach (var vehicleId in vehicleIdsToAdd)
                    {
                        var vehicle = await _context.Vehicles.FindAsync(vehicleId);
                        if (vehicle != null)
                        {
                            if (vehicle.ContractId != null && vehicle.ContractId != contract.Id)
                            {
                                throw new Exception("Um ou mais veículos já estão associados a um contrato diferente.");
                            }

                            vehicle.Status = VehicleStatus.Contract;
                            vehicle.ContractId = contract.Id;
                            _context.Entry(vehicle).State = EntityState.Modified;
                        }
                        else
                        {
                            throw new Exception("Um ou mais veículos não cadastrados.");
                        }
                    }

                    if (vehicleIdsToUpdate.Count == 0 || vehicleIdsToUpdate.All(id => vehiclesInContract.Any(v => v.Id == id && v.ContractId == null)))
                    {
                        contract.Status = ContractStatus.Inactive;
                    }
                    else
                    {
                        contract.Status = contractDto.Status;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    string updateContractResponse = $"Contrato da empresa {company.TradeName} atualizado com sucesso.";
                    return updateContractResponse;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ex.Message;
                }
            }
        }

        public async Task DeleteContractById(int id)
        {
            // Implementar lógica
            throw new NotImplementedException();
        }
    }
}
