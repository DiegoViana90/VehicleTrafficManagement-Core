using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using Dapper;
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
            var company = _dapperContext.ExecuteWithSingleResult<CompanyDTOResult>("public.getcompanybycnpj", parameters);

            return company;
        }

        public async Task<IEnumerable<CompanyDTOResult>> GetAllCompanies()
        {
            var companies = _dapperContext.ExecuteWithMultipleResults<CompanyDTOResult>("public.GetAllCompanies",
             new Dictionary<string, dynamic>());
            return companies;
        }


    }
}
