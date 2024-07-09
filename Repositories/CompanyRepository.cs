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

        public async Task<CompanyDTOResult> GetCompanyByCnpjAsync(string CNPJ)
        {
            var parameters = new Dictionary<string, dynamic> { { "paramCNPJ", CNPJ } };
            var company = await _dapperContext.ExecuteWithSingleResultAsync<CompanyDTOResult>("public.getcompanybycnpj", parameters);
            return company;
        }

        public async Task<IEnumerable<CompanyDTOResult>> GetAllCompanies()
        {
            var companies = await _dapperContext.ExecuteWithMultipleResultsAsync<CompanyDTOResult>("public.GetAllCompanies", new Dictionary<string, dynamic>());
            return companies;
        }

        public async Task<CompanyDTOResult> GetCompanyByName(string name)
        {
            var parameters = new Dictionary<string, dynamic> { { "paramName", name } };
            var company = await _dapperContext.ExecuteWithSingleResultAsync<CompanyDTOResult>("public.getcompanybyname", parameters);
            return company;
        }
    }
}
