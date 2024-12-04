using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurguerManiaAPI.Interfaces.User;
using BurguerManiaAPI.Dto.User;

namespace BurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserInterface _userService;

        public UsersController(UserInterface userService)
        {
            _userService = userService;
        }

        // Obter todos os usuários
        [HttpGet("GetUsers")]
        public async Task<IActionResult> FetchAllUsers()
        {
            var result = await _userService.GetUsers();
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Obter um usuário pelo ID
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> FetchUserById(int id)
        {
            var result = await _userService.GetUser(id);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Atualizar um usuário existente
        [HttpPut("PutUsers/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
        {
            var result = await _userService.PutUsers(id, request);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Criar um novo usuário
        [HttpPost("PostUsers")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var result = await _userService.PostUsers(request);
            return Ok(result);
        }

        // Deletar um usuário pelo ID
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var result = await _userService.DeleteUsers(id);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }
    }
}
