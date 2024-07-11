using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VehicleTrafficManagement.DTOs.Request;
using VehicleTrafficManagement.DTOs.Response;
using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza o login de um usuário e retorna um token JWT válido.
        /// </summary>
        /// <param name="request">Dados de autenticação do usuário.</param>
        /// <returns>Token JWT e detalhes do usuário se a autenticação for bem-sucedida.</returns>
        [HttpPost("login")]
        [SwaggerOperation("Autenticação de Usuário")]
        [SwaggerResponse(200, "Token JWT gerado com sucesso")]
        [SwaggerResponse(401, "Usuário não autorizado")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto authRequestDto)
        {
            AuthResponse authResponse = await _authService.Authenticate(authRequestDto.Email, authRequestDto.Password);
            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

        // /// <summary>
        // /// Realiza o login de um usuário e retorna um token JWT válido.
        // /// </summary>
        // /// <param name="request">Dados de autenticação do usuário.</param>
        // /// <returns>Token JWT e detalhes do usuário se a autenticação for bem-sucedida.</returns>
        // [HttpPost("UpdateFirstAccessPassword")]
        // [SwaggerOperation("Atuliza senha de primeiro acesso Usuário")]
        // [SwaggerResponse(200, "Nova senha atualizada com sucesso")]
        // [SwaggerResponse(401, "Senha não atualizada")]
        // public async Task<IActionResult> UpdateFirstAccessPassword([FromBody]FirstPasswordUpdateRequest firstPasswordUpdateRequest)
        // {
        //     AuthResponse authResponse = await _authService.UpdateFirstAccessPassword(firstPasswordUpdateRequest.UserId, firstPasswordUpdateRequest.NewPassword);
        //     if (authResponse == null)
        //     {
        //         return Unauthorized();
        //     }

        //     return Ok(authResponse);
        // }

    }
}
