using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VehicleTrafficManagement.Data;
using VehicleTrafficManagement.DTOs.Request;
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
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IPasswordHasher<User> passwordHasher,
            IUserService userService,
            ILogger<AuthService> logger
        )
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _userService = userService;
            _logger = logger;
        }

        public async Task<AuthResponse> Authenticate(string email, string password)
        {
            _logger.LogInformation($"Tentando autenticar usuário com email: {email}");

            User user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                _logger.LogWarning($"Usuário com email: {email} não encontrado.");
                throw new Exception("Usuário não encontrado");
            }

            PasswordVerificationResult passwordVerificationResult =
                _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning($"Falha na verificação da senha para o usuário com email: {email}.");
                throw new Exception("Senha incorreta");
            }

            if (user.IsBlocked)
            {
                _logger.LogWarning($"Usuário com email: {email} está bloqueado.");
                throw new Exception("Usuário com acesso bloqueado!");
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

            _logger.LogInformation($"Usuário com email: {email} autenticado com sucesso.");

            return new AuthResponse
            {
                Token = tokenString,
                UserId = user.UserId,
                FullName = user.FullName,
                UserType = user.UserType,
                IsFirstAccess = user.IsFirstAccess,
                IsBlocked = user.IsBlocked,
                CompaniesId = user.CompaniesId,
                Email = user.Email
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

        public async Task UpdateFirstPassword(UpdateFirstPasswordRequestDto updateFirstPasswordRequestDto)
        {
            bool isPasswordValid = Validator.IsPasswordValid(updateFirstPasswordRequestDto.NewPassword);

            if (!isPasswordValid)
            {
                throw new ArgumentException("Senha inválida, utilizar pelo menos 6 caracteres.");
            }

            User user = await _userService.GetUserById(updateFirstPasswordRequestDto.UserId);
            
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            if (user.IsBlocked)
            {
                throw new Exception("Usuário com acesso bloqueado!");
            }

            if (!user.IsFirstAccess)
            {
                throw new Exception("não é o Primeiro acesso.");
            }

            PasswordVerificationResult passwordVerificationResult =
            _passwordHasher.VerifyHashedPassword(user, user.Password, updateFirstPasswordRequestDto.RandomPassword);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Senha incorreta");
            }

            user.Password = _passwordHasher.HashPassword(user, updateFirstPasswordRequestDto.NewPassword);
            user.IsFirstAccess = false;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
