using VehicleTrafficManagement.Dto;

namespace VehicleTrafficManagement.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(int id);
        Task<int> AddCompany(InsertCompanyRequestDto companyDto); // Ajustado para retornar Task<int>
        Task UpdateCompany(int id, CompanyDto companyDto);
        Task DeleteCompany(int id);
    }
}
