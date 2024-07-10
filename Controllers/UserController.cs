using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VehicleTrafficManagement.Dto;
using VehicleTrafficManagement.Interfaces;
using VehicleTrafficManagement.Models;
using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        [SwaggerOperation(Summary = "Busca todos os usuários.", 
        Description = "Recupera uma lista de todos os usuários.")]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<User>))]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        [SwaggerOperation(Summary = "Busca usuário por ID.", 
        Description = "Recupera um usuário específico pelo ID.")]
        [SwaggerResponse(200, "Success", typeof(User))]
        [SwaggerResponse(404, "User not found")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("GetUserByEmail")]
        [SwaggerOperation(Summary = "Busca usuário por email.", 
        Description = "Recupera um usuário específico pelo email.")]
        [SwaggerResponse(200, "Success", typeof(User))]
        [SwaggerResponse(404, "User not found")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("InsertUser")]
        [SwaggerOperation(Summary = "Adiciona um novo usuário.", 
        Description = "Adiciona um novo usuário ao sistema.")]
        [SwaggerResponse(201, "User created successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        public async Task<ActionResult> InsertUser(UserCreationRequest userCreationRequest)
        {
            User user = new User
            {
                FullName = userCreationRequest.FullName,
                Password = userCreationRequest.Password,
                Email = userCreationRequest.Email,
                UserType = userCreationRequest.UserType,
                IsFirstAcess = true,
                CompanyId = userCreationRequest.CompanyId
            };
            await _userService.InsertUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // [HttpPut("UpdateUser")]
        // [SwaggerOperation(Summary = "Atualiza um usuário por Id.", 
        // Description = "Atualiza um usuário existente no sistema.")]
        // [SwaggerResponse(200, "User updated successfully")]
        // [SwaggerResponse(400, "Invalid request")]
        // [SwaggerResponse(404, "User not found")]
        // public async Task<ActionResult> UpdateUser(string id, User user)
        // {
        //     if (id != user.UserId)
        //     {
        //         return BadRequest();
        //     }

        //     await _userService.UpdateUser(user);
        //     return NoContent();
        // }

        [HttpDelete("DeleteUser")]
        [SwaggerOperation(Summary = "Deleta um usuário.", 
        Description = "Delete um usuário pelo Id.")]
        [SwaggerResponse(200, "User deleted successfully")]
        [SwaggerResponse(404, "User not found")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
