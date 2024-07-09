using Microsoft.AspNetCore.Identity;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; } 
    public bool IsFirstAcess { get; set; } = true;
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
