using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.DTOs.Response;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IAuthService
    {
         Task<AuthResponse> Authenticate(string email, string password);
         Task UpdateFirstPassword(UpdateFirstPasswordRequestDto updateFirstPasswordRequestDto);
         Task <TempPasswordResponseDto> GenerateTemporaryPassword(int userId);
    }
}