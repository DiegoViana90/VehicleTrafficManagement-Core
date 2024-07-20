using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Data;

namespace VehicleTrafficManagement.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _dapperContext;

        public CompanyRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<CompanyDTOResult> GetCompanyByTaxNumberAsync(string TaxNumber)
        {
            var parameters = new Dictionary<string, dynamic> { { "paramTaxNumber", TaxNumber } };
            var company = await _dapperContext.ExecuteWithSingleResultAsync<CompanyDTOResult>("public.getcompanybyTaxNumber", parameters);
            return company;
        }

        public async Task<IEnumerable<CompanyDTOResult>> GetAllCompanies()
        {
            var companies = await _dapperContext.ExecuteWithListResultAsync<CompanyDTOResult>("public.GetAllCompanies", new Dictionary<string, dynamic>());
            return companies;
        }

        public async Task<IEnumerable<CompanyDTOResult>> GetCompanyByName(string name)
        {
            var parameters = new Dictionary<string, dynamic> { { "paramName", name } };
            var companies = await _dapperContext.ExecuteWithListResultAsync<CompanyDTOResult>("public.getcompanybyname", parameters);
            return companies;
        }
    }
}