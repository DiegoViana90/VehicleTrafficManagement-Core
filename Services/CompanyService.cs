using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Repositories;
using VehicleTrafficManagement.Util;
using VehicleTrafficManagement.DTOs.Request;
using Microsoft.EntityFrameworkCore;

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
                TaxNumber = company.TaxNumber,
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


        public async Task<CompanyDTOResult> GetCompanyByTaxNumberAsync(string TaxNumber)
        {
            TaxNumber = Formatter.RemoveMaskTaxNumber(TaxNumber);
            bool isTaxNumberValid = Validator.IsValidTaxNumber(TaxNumber);

            if (!isTaxNumberValid)
            {
                throw new ArgumentException("TaxNumber Inválido!");
            }

            return await _companyRepository.GetCompanyByTaxNumberAsync(TaxNumber);
        }

        public async Task<string> InsertCompany(InsertCompanyRequestDto insertCompanyRequestDto)
        {

            insertCompanyRequestDto.TaxNumber = Formatter.RemoveMaskTaxNumber(insertCompanyRequestDto.TaxNumber);
            bool taxNumberExists = await TaxNumberExists(insertCompanyRequestDto.TaxNumber);

            if (taxNumberExists)
            {
                throw new ArgumentException("TaxNumber já cadastrado na base!");
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
                TaxNumber = insertCompanyRequestDto.TaxNumber,
                CompanyInformationId = companyInformation.CompanyInformationId
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return company.Name;
        }

        public async Task UpdateCompanByTaxNumberAsync(UpdateCompanByTaxNumberRequest updateCompanByTaxNumberRequest)
        {
            CompanyDTOResult companyResult = await GetCompanyByTaxNumberAsync(updateCompanByTaxNumberRequest.TaxNumber);
            if (companyResult == null)
            {
                throw new KeyNotFoundException("Empresa não encontrada.");
            }

            Company company = new Company
            {
                CompaniesId = companyResult.CompaniesId,
                Name = companyResult.Name,
                TaxNumber = companyResult.TaxNumber,
                CompanyInformationId = companyResult.CompanyInformationId,
                TradeName = updateCompanByTaxNumberRequest.TradeName,
            };

            CompanyInformation companyInformation = new CompanyInformation
            {
                CompanyInformationId = companyResult.CompanyInformationId,
                CEP = updateCompanByTaxNumberRequest.CEP,
                Street = updateCompanByTaxNumberRequest.Street,
                PropertyNumber = updateCompanByTaxNumberRequest.PropertyNumber,
                District = updateCompanByTaxNumberRequest.District,
                City = updateCompanByTaxNumberRequest.City,
                State = updateCompanByTaxNumberRequest.State,
                Country = updateCompanByTaxNumberRequest.Country,
                AdressComplement = updateCompanByTaxNumberRequest.AddressComplement,
                PhoneNumber = updateCompanByTaxNumberRequest.PhoneNumber,
                Email = updateCompanByTaxNumberRequest.Email,
                Observations = updateCompanByTaxNumberRequest.Observations
            };

            var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    _context.Companies.Update(company);
                    _context.CompanyInformation.Update(companyInformation);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        private async Task<bool> TaxNumberExists(string taxNumber)
        {
            var company = await GetCompanyByTaxNumberAsync(taxNumber);
            return company != null;
        }

        public async Task UpdateCompanById(int id, CompanyDto companyDto)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                throw new KeyNotFoundException("Empresa não encontrada.");
            }

            company.TradeName = companyDto.TradeName;
            company.TaxNumber = companyDto.TaxNumber;

            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompanyById(int id)
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
