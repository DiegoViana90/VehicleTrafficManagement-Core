using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.DTOs.Request;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTOResult>> GetAllCompanies();
        Task<IEnumerable<CompanyDTOResult>> GetAllCompaniesByCompany(int companyRelated);
        Task<CompanyDto> GetCompanyById(int id);
        Task<string> InsertCompany(InsertCompanyRequestDto insertCompanyRequestDto);
        Task UpdateCompanyById(int id, CompanyDto companyDto);
        Task DeleteCompanyById(int id);
        Task<CompanyDTOResult> GetCompanyByTaxNumberAsync(string TaxNumber);
        Task<CompanyDTOResult> GetCompanyByTaxNumberAndCompanyRelated(string taxNumber, int companyRelated);
        Task<IEnumerable<CompanyDTOResult>>  GetCompanyByName(string name);
        Task UpdateCompanyByTaxNumberAndCompanyRelatedAsync(UpdateCompanyByTaxNumberRequest updateCompanByTaxNumberRequest);
    }
}
