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

namespace VehicleTrafficManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService
        (
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

            User user = await _userService.GetUserByEmail(email);await _userService.GetUserByEmail(email);
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
                    new Claim(ClaimTypes.Role, user.UserType.ToString())
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
        // public async Task<AuthResponse> UpdateFirstAccessPassword(string userId, string Newpassword)
        // {
        //     await await Ok("ok")
        //     return Ok()
        // }
    }
}
