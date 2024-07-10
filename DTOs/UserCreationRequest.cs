using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Dto
{
    public class UserCreationRequest
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; } 
        public int CompanyId { get; set; }
    }
}
