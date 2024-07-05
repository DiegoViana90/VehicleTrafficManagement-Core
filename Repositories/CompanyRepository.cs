
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _context.Companies
                .Include(c => c.CompanyInformation)
                .ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _context.Companies
                .Include(c => c.CompanyInformation)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
