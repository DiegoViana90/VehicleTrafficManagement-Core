using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company> GetCompanyById(int id);
        Task AddCompany(Company company);
        Task UpdateCompany(Company company);
        Task DeleteCompany(int id);
    }
}
