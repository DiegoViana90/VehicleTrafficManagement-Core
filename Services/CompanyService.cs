using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Repositories;
using VehicleTrafficManagement.Util;

namespace VehicleTrafficManagement.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ApplicationDbContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyDTOResult>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAllCompanies();
            return companies;
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return null;
            }

            return new CompanyDto
            {
                Id = company.CompaniesId,
                TradeName = company.TradeName,
                CNPJ = company.CNPJ,
            };
        }

        public async Task<IEnumerable<CompanyDTOResult>> GetCompanyByName(string name)
        {
            var companies = await _companyRepository.GetCompanyByName(name);
            if (companies == null || !companies.Any())
            {
                return Enumerable.Empty<CompanyDTOResult>();
            }

            return companies;
        }


        public async Task<CompanyDTOResult> GetCompanyByCnpjAsync(string CNPJ)
        {
            CNPJ = Formatter.RemoveMaskCnpj(CNPJ);
            bool isCNPJValid = CnpjValidator.IsCNPJ(CNPJ);

            if (!isCNPJValid)
            {
                throw new ArgumentException("CNPJ Inválido!");
            }

            return await _companyRepository.GetCompanyByCnpjAsync(CNPJ);
        }

        public async Task<string> InsertCompany(InsertCompanyRequestDto insertCompanyRequestDto)
        {

            bool cnpjExists = await CNPJExists(insertCompanyRequestDto.CNPJ);

            if (cnpjExists)
            {
                throw new ArgumentException("CNPJ já cadastrado na base!");
            }

            var companyInformation = new CompanyInformation
            {
                CEP = insertCompanyRequestDto.CEP,
                Street = insertCompanyRequestDto.Street,
                PropertyNumber = insertCompanyRequestDto.PropertyNumber,
                District = insertCompanyRequestDto.District,
                City = insertCompanyRequestDto.City,
                State = insertCompanyRequestDto.State,
                Country = insertCompanyRequestDto.Country,
                AdressComplement = insertCompanyRequestDto.AdressComplement,
                PhoneNumber = insertCompanyRequestDto.PhoneNumber,
                Email = insertCompanyRequestDto.Email,
                Observations = insertCompanyRequestDto.Observations,
                CompanyStatus = insertCompanyRequestDto.CompanyStatus
            };

            _context.CompanyInformation.Add(companyInformation);
            await _context.SaveChangesAsync();

            var company = new Company
            {
                Name = insertCompanyRequestDto.Name,
                TradeName = insertCompanyRequestDto.TradeName,
                CNPJ = insertCompanyRequestDto.CNPJ,
                CompanyInformationId = companyInformation.CompanyInformationId
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return company.Name;
        }

        private async Task<bool> CNPJExists(string CNPJ)
        {
            var company = await GetCompanyByCnpjAsync(CNPJ);
            return company != null;
        }

        public async Task UpdateCompany(int id, CompanyDto companyDto)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                throw new KeyNotFoundException("Empresa não encontrada.");
            }

            company.TradeName = companyDto.TradeName;
            company.CNPJ = companyDto.CNPJ;

            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                throw new KeyNotFoundException("Empresa não encontrada.");
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}
