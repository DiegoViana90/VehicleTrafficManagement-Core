using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(int id);
        Task AddCompany(CompanyDto companyDtO);
        Task UpdateCompany(int id, CompanyDto companyDto);
        Task DeleteCompany(int id);
    }
}
