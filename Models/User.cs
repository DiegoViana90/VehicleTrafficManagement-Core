using Microsoft.AspNetCore.Identity;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public UserType UserType { get; set; } 
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
