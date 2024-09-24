using Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        ITokenService _services;
        IUsuarioService _usuarioService;

        public TokenController(ITokenService services, IUsuarioService usuarioService)
        {
            _services = services;
            _usuarioService = usuarioService;
        }

        [HttpPost("GerarToken")]
        public IActionResult GerarToken([FromBody] LoginResource resource, CancellationToken cancellationToken)
        {
            var login = _usuarioService.Login(resource, cancellationToken);

            if (login is not null)
            {
                var token = _services.GenerateJwtToken(resource.Nome);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}