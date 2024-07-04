using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Services
{
    public class CompanyService : ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task<CompanyDto> GetCompanyById(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task AddCompany(CompanyDto companyDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task UpdateCompany(int id, CompanyDto companyDto)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }

        public Task DeleteCompany(int id)
        {
            // Implementar lógica
            throw new System.NotImplementedException();
        }
    }
}

