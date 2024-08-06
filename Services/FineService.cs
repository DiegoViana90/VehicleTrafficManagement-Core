using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using Microsoft.EntityFrameworkCore;
using VehicleTrafficManagement.DTOs.Request;
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

    public async Task<FineDto> GetFineByFineNumberAndVehicleId
    (FineNumberAndVehicleIdRequest fineNumberAndVehicleIdRequest)
    {
        var fine = await _context.Fines
            .Where(f => f.FineNumber == fineNumberAndVehicleIdRequest.FineNumber
            && f.VehicleId == fineNumberAndVehicleIdRequest.VehicleId)
            .Select(f => new FineDto
            {
                FineNumber = f.FineNumber,
                FineDateTime = f.FineDateTime,
                FineDueDate = f.FineDueDate,
                EnforcingAgency = f.EnforcingAgency,
                FineLocation = f.FineLocation,
                FineAmount = f.FineAmount,
                DiscountedFineAmount = f.DiscountedFineAmount,
                InterestFineAmount = f.InterestFineAmount,
                FinalFineAmount = f.FinalFineAmount,
                FineStatus = f.FineStatus,
                Description = f.Description,
                RegistrationDate = f.RegistrationDate,
                VehicleId = f.VehicleId
            })
            .FirstOrDefaultAsync();

        return fine;
    }

    private async Task<bool> FineNumberExists(string fineNumber, int vehicleId)
    {
        return await _context.Fines.AnyAsync(f => f.FineNumber == fineNumber && f.VehicleId == vehicleId);
    }

    public async Task InsertFine(FineDto fineDto)
    {
        if (await FineNumberExists(fineDto.FineNumber, fineDto.VehicleId))
        {
            throw new Exception("Multa com este número já existe para este veículo.");
        }

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

    public async Task UpdateFine(FineDto fineDto)
    {
        var fine = await _context.Fines.FirstOrDefaultAsync(f => f.FineNumber == fineDto.FineNumber && f.VehicleId == fineDto.VehicleId);
        if (fine != null)
        {
            fine.FineDateTime = fineDto.FineDateTime;
            fine.FineDueDate = fineDto.FineDueDate;
            fine.EnforcingAgency = fineDto.EnforcingAgency;
            fine.FineLocation = fineDto.FineLocation;
            fine.FineAmount = fineDto.FineAmount;
            fine.DiscountedFineAmount = fineDto.DiscountedFineAmount;
            fine.InterestFineAmount = fineDto.InterestFineAmount;
            fine.FinalFineAmount = fineDto.FinalFineAmount;
            fine.FineStatus = fineDto.FineStatus;
            fine.Description = fineDto.Description;
            fine.RegistrationDate = fineDto.RegistrationDate;

            _context.Fines.Update(fine);
            await _context.SaveChangesAsync();
        }
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
