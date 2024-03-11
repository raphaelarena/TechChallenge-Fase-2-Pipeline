using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication1.DTO;
using WebApplication1.Entity;
using WebApplication1.Enums;
using WebApplication1.Interface;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _usuarioRepository;

        public UserController(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        [Authorize]
        [Authorize(Roles = $"{Permissions.Admin}")]
        [HttpGet()]
        public IActionResult ObterTodosUsuario()
        {
            var usuario = _usuarioRepository.GetAll();
            return Ok(usuario);
        }

        [Authorize]
        [Authorize(Roles = $"{Permissions.Admin}")]
        [HttpGet("{id}")]
        public IActionResult ObterUsuarioPorId([FromRoute] Guid id)
        {
            var usuario = _usuarioRepository.GetById(id);
            return Ok(usuario);
        }

        [HttpPost()]
        public IActionResult CriarUsuario([FromBody] AddUserDTO usuario)
        {
            _usuarioRepository.Add(new User(usuario));
            return Ok("Usuário criado com sucesso");
        }

        [Authorize]
        [Authorize(Roles = $"{Permissions.Admin}")]
        [HttpPut()]
        public IActionResult AlterarUsuario([FromBody] EditUserDTO usuario)
        {
            _usuarioRepository.Edit(new User(usuario));
            return Ok("Usuario alterado com sucesso");
        }

        [Authorize]
        [Authorize(Roles = $"{Permissions.Admin}")]
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario([FromRoute] Guid id)
        {
            _usuarioRepository.Delete(id);
            return Ok("Usuario deletado com sucesso");
        }

    }
}