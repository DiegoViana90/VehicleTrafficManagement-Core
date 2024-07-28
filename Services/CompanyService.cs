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

        public async Task<IEnumerable<CompanyDTOResult>> GetAllCompaniesByCompany(int companyRelated)
        {
            var companies = await _context.Companies
                .Where(c => c.CompanyRelated == companyRelated)
                .ToListAsync();

            if (!companies.Any())
            {
                throw new Exception($"Nenhuma empresa encontrada relacionada com o ID {companyRelated}.");
            }

            var companyInformationIds = companies.Select(c => c.CompanyInformationId).Distinct().ToList();
            var companiesInformation = await _context.CompanyInformation
                .Where(ci => companyInformationIds.Contains(ci.CompanyInformationId))
                .ToListAsync();

            var companyDTOResults = new List<CompanyDTOResult>();

            foreach (var company in companies)
            {
                var companyInfo = companiesInformation.FirstOrDefault(ci => ci.CompanyInformationId == company.CompanyInformationId);

                var companyDTO = new CompanyDTOResult
                {
                    CompaniesId = company.CompaniesId,
                    Name = company.Name,
                    TradeName = company.TradeName,
                    TaxNumber = company.TaxNumber,
                    CEP = companyInfo?.CEP,
                    Street = companyInfo?.Street,
                    PropertyNumber = companyInfo?.PropertyNumber,
                    District = companyInfo?.District,
                    City = companyInfo?.City,
                    State = companyInfo?.State,
                    Country = companyInfo?.Country,
                    AdressComplement = companyInfo?.AdressComplement,
                    PhoneNumber = companyInfo?.PhoneNumber,
                    Email = companyInfo?.Email,
                    Observations = companyInfo?.Observations,
                    CompanyStatus = companyInfo.CompanyStatus,
                    CompanyInformationId = company.CompanyInformationId,
                    CompanyRelated = company.CompanyRelated,
                };

                companyDTOResults.Add(companyDTO);
            }

            return companyDTOResults;
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

        public async Task<CompanyDTOResult> GetCompanyByName(string name, int companyRelated)
        {           
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Name == name && c.CompanyRelated == companyRelated);

            if (company == null)
            {
                return null;
            }

            var companyInformation = await _context.CompanyInformation
                .FirstOrDefaultAsync(ci => ci.CompanyInformationId == company.CompanyInformationId);

            if (companyInformation == null)
            {
                return null;
            }

            return new CompanyDTOResult
            {
                CompaniesId = company.CompaniesId,
                Name = company.Name,
                TradeName = company.TradeName,
                TaxNumber = company.TaxNumber,
                CEP = companyInformation.CEP,
                Street = companyInformation.Street,
                PropertyNumber = companyInformation.PropertyNumber,
                District = companyInformation.District,
                City = companyInformation.City,
                State = companyInformation.State,
                Country = companyInformation.Country,
                AdressComplement = companyInformation.AdressComplement,
                PhoneNumber = companyInformation.PhoneNumber,
                Email = companyInformation.Email,
                Observations = companyInformation.Observations,
                CompanyStatus = companyInformation.CompanyStatus,
                CompanyInformationId = company.CompanyInformationId,
                CompanyRelated = company.CompanyRelated
            };
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

        public async Task<CompanyDTOResult> GetCompanyByTaxNumberAndCompanyRelated(string taxNumber, int companyRelated)
        {
            taxNumber = Formatter.RemoveMaskTaxNumber(taxNumber);
            bool isTaxNumberValid = Validator.IsValidTaxNumber(taxNumber);

            if (!isTaxNumberValid)
            {
                throw new ArgumentException("TaxNumber Inválido!");
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.TaxNumber == taxNumber && c.CompanyRelated == companyRelated);

            if (company == null)
            {
                throw new Exception($"Nenhuma empresa encontrada com o TaxNumber {taxNumber} e CompanyRelated {companyRelated}.");
            }

            var companyInfo = await _context.CompanyInformation
                .FirstOrDefaultAsync(ci => ci.CompanyInformationId == company.CompanyInformationId);

            if (companyInfo == null)
            {
                throw new Exception($"Nenhuma informação adicional encontrada para a empresa com CompanyInformationId {company.CompanyInformationId}.");
            }

            var companyDTO = new CompanyDTOResult
            {
                CompaniesId = company.CompaniesId,
                Name = company.Name,
                TradeName = company.TradeName,
                TaxNumber = company.TaxNumber,
                CEP = companyInfo.CEP,
                Street = companyInfo.Street,
                PropertyNumber = companyInfo.PropertyNumber,
                District = companyInfo.District,
                City = companyInfo.City,
                State = companyInfo.State,
                Country = companyInfo.Country,
                AdressComplement = companyInfo.AdressComplement,
                PhoneNumber = companyInfo.PhoneNumber,
                Email = companyInfo.Email,
                Observations = companyInfo.Observations,
                CompanyStatus = companyInfo.CompanyStatus,
                CompanyInformationId = company.CompanyInformationId,
                CompanyRelated = company.CompanyRelated,
            };

            return companyDTO;
        }


        public async Task<string> InsertCompany(InsertCompanyRequestDto insertCompanyRequestDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                insertCompanyRequestDto.TaxNumber = Formatter.RemoveMaskTaxNumber(insertCompanyRequestDto.TaxNumber);
                bool taxNumberExists = await TaxNumberExists(insertCompanyRequestDto.TaxNumber, insertCompanyRequestDto.CompanyRelated);

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
                    CompanyInformationId = companyInformation.CompanyInformationId,
                    CompanyRelated = insertCompanyRequestDto.CompanyRelated
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return company.Name;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Erro ao inserir a empresa: " + ex.Message);
            }
        }

        private async Task<bool> TaxNumberExists(string taxNumber, int companyRelated)
        {
            return await _context.Companies
                .AnyAsync(c => c.TaxNumber == taxNumber && c.CompanyRelated == companyRelated);
        }

        public async Task UpdateCompanyByTaxNumberAndCompanyRelatedAsync(UpdateCompanyByTaxNumberRequest updateCompanyByTaxNumberRequest)
        {
            CompanyDTOResult companyResult = await GetCompanyByTaxNumberAndCompanyRelated(updateCompanyByTaxNumberRequest.TaxNumber, updateCompanyByTaxNumberRequest.CompanyRelated);
            if (companyResult == null)
            {
                throw new KeyNotFoundException("Empresa não encontrada.");
            }

            var existingCompany = await _context.Companies.FindAsync(companyResult.CompaniesId);
            if (existingCompany == null)
            {
                throw new KeyNotFoundException("Empresa não encontrada no contexto.");
            }

            existingCompany.TradeName = updateCompanyByTaxNumberRequest.TradeName;

            var existingCompanyInformation = await _context.CompanyInformation.FindAsync(companyResult.CompanyInformationId);
            if (existingCompanyInformation == null)
            {
                throw new KeyNotFoundException("Informações da empresa não encontradas no contexto.");
            }

            existingCompanyInformation.CEP = updateCompanyByTaxNumberRequest.CEP;
            existingCompanyInformation.Street = updateCompanyByTaxNumberRequest.Street;
            existingCompanyInformation.PropertyNumber = updateCompanyByTaxNumberRequest.PropertyNumber;
            existingCompanyInformation.District = updateCompanyByTaxNumberRequest.District;
            existingCompanyInformation.City = updateCompanyByTaxNumberRequest.City;
            existingCompanyInformation.State = updateCompanyByTaxNumberRequest.State;
            existingCompanyInformation.Country = updateCompanyByTaxNumberRequest.Country;
            existingCompanyInformation.AdressComplement = updateCompanyByTaxNumberRequest.AddressComplement;
            existingCompanyInformation.PhoneNumber = updateCompanyByTaxNumberRequest.PhoneNumber;
            existingCompanyInformation.Email = updateCompanyByTaxNumberRequest.Email;
            existingCompanyInformation.Observations = updateCompanyByTaxNumberRequest.Observations;
            existingCompanyInformation.CompanyStatus = updateCompanyByTaxNumberRequest.CompanyStatus;

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Companies.Update(existingCompany);
                _context.CompanyInformation.Update(existingCompanyInformation);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task UpdateCompanyById(int id, CompanyDto companyDto)
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
