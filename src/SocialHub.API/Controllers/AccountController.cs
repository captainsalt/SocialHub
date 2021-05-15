using Microsoft.AspNetCore.Mvc;
using SocialHub.API.Attributes;
using SocialHub.Application.Models;
using SocialHub.Application.Services;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAccountService accountService, IAuthenticationService authenticationService)
        {
            _accountService = accountService;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [Authorize]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);

            return result.Match<IActionResult>(
                token => Ok(token),
                ex => BadRequest(ex)
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            var result = await _accountService.RegisterAsync(request);

            return result.Match<IActionResult>(
                acc => Ok(),
                ex => BadRequest(ex)
            );
        }
    }
}
