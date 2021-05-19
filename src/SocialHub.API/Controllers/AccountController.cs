using AutoMapper;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using SocialHub.API.Dtos;
using SocialHub.API.Models;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AccountController(
            IAuthenticationService authenticationService,
            IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);

            return result.Match<IActionResult>(
                authResult => Ok(MapAuthResult(authResult)),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);

            return result.Match<IActionResult>(
                authResult => Ok(MapAuthResult(authResult)),
                err => BadRequest(MapError(err))
            );
        }

        private ErrorDto MapError(Error err) =>
            _mapper.Map<ErrorDto>(err);

        private AuthResultDto MapAuthResult(AuthResult result) =>
            _mapper.Map<AuthResultDto>(result);
    }
}
