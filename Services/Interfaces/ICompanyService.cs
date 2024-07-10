using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTOResult>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(int id);
        Task<string> InsertCompany(InsertCompanyRequestDto companyDto);
        Task UpdateCompany(int id, CompanyDto companyDto);
        Task DeleteCompany(int id);
        Task<CompanyDTOResult> GetCompanyByCnpjAsync(string CNPJ);
        Task<IEnumerable<CompanyDTOResult>>  GetCompanyByName(string name);
    }
}
