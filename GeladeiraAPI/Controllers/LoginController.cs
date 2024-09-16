using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginService _services;

        public LoginController(ILoginService services)
        {
            _services = services;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login userLogin)
        {
            if (userLogin.UserName == "test" && userLogin.Password == "password")
            {
                var token = _services.GenerateJwtToken(userLogin.UserName);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}