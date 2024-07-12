using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.DTOs.Response;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Util;

namespace VehicleTrafficManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IPasswordHasher<User> passwordHasher,
            IUserService userService
        )
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<AuthResponse> Authenticate(string email, string password)
        {
            Console.WriteLine($"Tentando autenticar usuário com email: {email}");

            User user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            PasswordVerificationResult passwordVerificationResult =
                _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Senha incorreta");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserType.ToString()),
        }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthResponse
            {
                Token = tokenString,
                UserId = user.UserId,
                FullName = user.FullName,
                UserType = user.UserType,
                IsFirstAccess = user.IsFirstAccess,
                IsBlocked = user.IsBlocked,
                CompaniesId = user.CompaniesId
            };
        }

        public async Task<TempPasswordResponseDto> GenerateTemporaryPassword(int userId)
        {
            User user = await _userService.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            Random randomPassword = new Random();
            string randonGeneratedPassword = randomPassword.Next(100000, 999999).ToString();

            user.Password = _passwordHasher.HashPassword(user, randonGeneratedPassword);

            user.IsFirstAccess = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new TempPasswordResponseDto
            {
                Message = "Nova senha gerada com sucesso.",
                RandomPassword = randonGeneratedPassword
            };
        }

        public async Task UpdateFirstPassword(int userId, string newPassword)
        {

            bool isPasswordValid = Validator.IsPasswordValid(newPassword);

            if (!isPasswordValid)
            {
                throw new ArgumentException("Senha inválida, utilizar pelo menos 6 caracteres.");
            }

            User user = await _userService.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            if (user.IsFirstAccess == true)
            {
                user.Password = _passwordHasher.HashPassword(user, newPassword);
                user.IsFirstAccess = false;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
