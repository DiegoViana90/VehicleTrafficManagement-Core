using VehicleTrafficManagement.DTOs.Response;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IAuthService
    {
         Task<AuthResponse> Authenticate(string email, string password);
         Task UpdateFirstPassword(int userId, string newPassword);
         Task <TempPasswordResponseDto> GenerateTemporaryPassword(int userId);
    }
}