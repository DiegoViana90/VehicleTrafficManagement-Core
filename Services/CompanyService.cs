using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            // Implementar lógica para obter todas as empresas do repositório
            throw new System.NotImplementedException();
        }

        public Task<CompanyDto> GetCompanyById(int id)
        {
            // Implementar lógica para obter uma empresa pelo ID do repositório
            throw new System.NotImplementedException();
        }

        public async Task<int> AddCompany(InsertCompanyRequestDto companyDto)
        {
            var company = new Company
            {
                Name = companyDto.Name,
                TradeName = companyDto.TradeName,
                CNPJ = companyDto.CNPJ,
                CompanyInformation = new CompanyInformation
                {
                    CEP = companyDto.CEP,
                    Street = companyDto.Street,
                    PropertyNumber = companyDto.PropertyNumber,
                    District = companyDto.District,
                    City = companyDto.City,
                    State = companyDto.State,
                    Country = companyDto.Country,
                    AdressComplement = companyDto.AdressComplement,
                    PhoneNumber = companyDto.PhoneNumber,
                    Email = companyDto.Email,
                    Observations = companyDto.Observations,
                    CompanyStatus = companyDto.CompanyStatus
                },
            };

            await _companyRepository.AddCompany(company);
            return company.Id;
        }

        public Task UpdateCompany(int id, CompanyDto companyDto)
        {
            // Implementar lógica para atualizar uma empresa no repositório
            throw new System.NotImplementedException();
        }

        public Task DeleteCompany(int id)
        {
            // Implementar lógica para excluir uma empresa do repositório
            throw new System.NotImplementedException();
        }
    }
}
