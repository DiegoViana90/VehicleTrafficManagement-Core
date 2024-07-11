using VehicleTrafficManagement.DTOs.Response;

namespace VehicleTrafficManagement.Interfaces
{
    public interface IAuthService
    {
         Task<AuthResponse> Authenticate(string email, string password);
        //  Task<AuthResponse> UpdateFirstAccessPassword(int userId, string newPassword);
    }
}