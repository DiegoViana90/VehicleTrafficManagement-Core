using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Repositories;

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

        public async Task<CompanyDto> GetCompanyByName(string name)
        {
            var company = await _companyRepository.GetCompanyByName(name);
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

        public async Task<CompanyDTOResult> GetCompanyByCnpjAsync(string CNPJ)
        {
            return await _companyRepository.GetCompanyByCnpjAsync(CNPJ);
        }

        public async Task<int> AddCompany(InsertCompanyRequestDto companyDto)
        {
            var company = new Company
            {
                TradeName = companyDto.TradeName,
                CNPJ = companyDto.CNPJ,
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company.CompaniesId;
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
