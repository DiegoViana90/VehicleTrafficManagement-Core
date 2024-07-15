using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.DTOs.Request;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTOResult>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(int id);
        Task<string> InsertCompany(InsertCompanyRequestDto insertCompanyRequestDto);
        Task UpdateCompanById(int id, CompanyDto companyDto);
        Task DeleteCompanyById(int id);
        Task<CompanyDTOResult> GetCompanyByCnpjAsync(string CNPJ);
        Task<IEnumerable<CompanyDTOResult>>  GetCompanyByName(string name);
    }
}
