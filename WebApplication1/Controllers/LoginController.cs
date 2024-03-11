using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Entity;
using WebApplication1.Enums;
using WebApplication1.Interface;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("auth")]
    public class LoginController : ControllerBase
    {

        private readonly IUserRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUserRepository usuarioRepository,
            ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult AuthenticationAsync([FromBody] AuthDTO auth)
        {
            var user = _usuarioRepository.GetUserByNameAndPassword(auth.UserName, auth.Password);

            if (user == null)
            {
                return NotFound(new { msg = "Usuario ou senha inválidos" });
            }

            var token = _tokenService.GetToken(user);

            user.Password = null;

            return Ok(new
            {
                User = user,
                Token = token
            });
        }

    }
}