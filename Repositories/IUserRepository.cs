using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrafficManagement.Models;

namespace VehicleTrafficManagement.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string id);
    }
}