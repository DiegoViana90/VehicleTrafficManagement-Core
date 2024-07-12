
using Microsoft.AspNetCore.Identity;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Repositories;
using VehicleTrafficManagement.Util;

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

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserByEmailAndCompanyId(string email, int companyId)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task InsertUser(UserCreationRequest userCreationRequest)
        {
            bool alreadyRegistered = await AlreadyRegistered(userCreationRequest.Email, userCreationRequest.CompanyId);

            if (alreadyRegistered)
            {
                throw new ArgumentException("Usuário já cadastrado na base!");
            }

            bool isPasswordValid = Validator.IsPasswordValid(userCreationRequest.Password);

            if (!isPasswordValid)
            {
                throw new ArgumentException("Senha inválida, utilizar pelo menos 6 caracteres.");
            }

            User user = new User
            {
                FullName = userCreationRequest.FullName,
                Password = userCreationRequest.Password,
                Email = userCreationRequest.Email,
                CompaniesId = userCreationRequest.CompanyId,
                UserType = 0,
                IsFirstAccess = true,
                IsBlocked = false
            };

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            await _userRepository.InsertUser(user);
        }

        private async Task<bool> AlreadyRegistered(string email, int companyId)
        {
            User user = await _userRepository.GetUserByEmailAndCompanyId(email, companyId);

            if (user != null && user.Email == email && user.CompaniesId == companyId)
            {
                return true;
            }

            return false; 
        }


        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
