using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Repositories
{
    public interface ICompanyRepository
    {
        Task<CompanyDTOResult> GetCompanyByCnpjAsync(string cnpj);
        Task<IEnumerable<CompanyDTOResult>> GetAllCompanies();
        Task<IEnumerable<CompanyDTOResult>> GetCompanyByName(string name); 
    }
}