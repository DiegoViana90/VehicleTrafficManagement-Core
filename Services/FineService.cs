using System.Threading.Tasks;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;

public class FineService : IFineService
{
    private readonly ApplicationDbContext _context;

    public FineService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<FineDto>> GetAllFines()
    {
        // Implementar lógica
        throw new System.NotImplementedException();
    }

    public Task<FineDto> GetFineById(int id)
    {
        // Implementar lógica
        throw new System.NotImplementedException();
    }

    public async Task InsertFine(FineDto fineDto)
    {
        Fine fine = new Fine
        {
            FineNumber = fineDto.FineNumber,
            FineDateTime = fineDto.FineDateTime,
            FineDueDate = fineDto.FineDueDate,
            EnforcingAgency = fineDto.EnforcingAgency,
            FineLocation = fineDto.FineLocation,
            FineAmount = fineDto.FineAmount,
            DiscountedFineAmount = fineDto.DiscountedFineAmount,
            InterestFineAmount = fineDto.InterestFineAmount,
            FinalFineAmount = fineDto.FinalFineAmount,
            FineStatus = fineDto.FineStatus,
            Description = fineDto.Description,
            RegistrationDate = fineDto.RegistrationDate,
            VehicleId = fineDto.VehicleId
        };

        _context.Fines.Add(fine);
        await _context.SaveChangesAsync();
    }

    public Task UpdateFineById(int id, FineDto fineDto)
    {
        // Implementar lógica
        throw new System.NotImplementedException();
    }

    public Task DeleteFineById(int id)
    {
        // Implementar lógica
        throw new System.NotImplementedException();
    }
}
