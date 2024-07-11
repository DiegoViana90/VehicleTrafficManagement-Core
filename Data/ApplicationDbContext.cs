using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyInformation> CompanyInformation { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TrafficLog> TrafficLogs { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<FuelLog> FuelLogs { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany()
                .HasForeignKey(u => u.CompaniesId);
        }
    }
}
