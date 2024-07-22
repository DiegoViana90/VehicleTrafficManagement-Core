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
        Task UpdateCompanByTaxNumberAsync(UpdateCompanByTaxNumberRequest updateCompanByTaxNumberRequest);
        Task DeleteCompanyById(int id);
        Task<CompanyDTOResult> GetCompanyByTaxNumberAsync(string TaxNumber);
        Task<IEnumerable<CompanyDTOResult>>  GetCompanyByName(string name);
    }
}
