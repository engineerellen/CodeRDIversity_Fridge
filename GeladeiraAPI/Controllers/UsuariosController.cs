using Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _usuarioService.Registro(resource, cancellationToken);
                return Ok($"Regitsro realizado com sucesso - {response}");
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _usuarioService.Login(resource, cancellationToken);
                return Ok($"Login realizado com sucesso - {response}");
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}