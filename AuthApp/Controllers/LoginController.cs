using AuthAppCore.Models;
using AuthAppUseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly ILoginUseCase loginUserCase;
        private readonly IRegisterUseCase registerUseCase;

        public LoginController(UserDbContext user, ILoginUseCase loginUserCase, IRegisterUseCase registerUseCase)
        {
            _context = user;
            this.loginUserCase = loginUserCase;
            this.registerUseCase = registerUseCase;
        }

        [HttpGet("get")]
        [Authorize]
        public string Get()
        {
            return "You are logged in";
        }

        [HttpGet]
        public string Login()
        {
            return "Application started";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]User user)
        {
            var a = Request;            

            await registerUseCase.ExecuteAsync(user);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLogin userLogin)
        {
            try
            {
                var token = await loginUserCase.ExecuteAsync(userLogin);

                Response.Headers.Add("Authorization", $"Bearer {token}");

                return Ok(new { Message = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return StatusCode(((int)HttpStatusCode.InternalServerError), "Servico indisponivel, tente novamente");
            }
        }
    }
}
