
using Microsoft.AspNetCore.Identity;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Repositories;

namespace VehicleTrafficManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task InsertUser(User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            await _userRepository.InsertUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(string id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
