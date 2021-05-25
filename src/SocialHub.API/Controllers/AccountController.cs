using AutoMapper;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using SocialHub.API.Dtos;
using SocialHub.API.Models;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using System.Threading.Tasks;

namespace SocialHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(
            IAuthenticationService authenticationService,
            IAccountService accountService,
            IJwtService jwtService,
            IMapper mapper)
        {
            _authenticationService = authenticationService;
            _accountService = accountService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpGet("profile/{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var result = _accountService.GetAccountProfile(username);

            return await result.Match<IActionResult>(
                result => Ok(Map<AccountProfileDto>(result)),
                err => BadRequest(err)
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);

            return result.Match<IActionResult>(
                authResult => Ok(Map<AuthResultDto>(authResult)),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);

            return result.Match<IActionResult>(
                authResult => Created(HttpContext.Request.Path.Value, Map<AuthResultDto>(authResult)),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromQuery] FollowRequest request)
        {
            var tokenAccount = _jwtService.GetAccountFromToken(HttpContext);

            return await _accountService.FollowAccountAsync(tokenAccount.Id, request.followeeId)
                .Match<IActionResult>(
                    unit => Ok(),
                    err => BadRequest(MapError(err))
            );
        }

        [HttpDelete("unfollow")]
        public async Task<IActionResult> Unfollow([FromQuery] FollowRequest request)
        {
            var tokenAccount = _jwtService.GetAccountFromToken(HttpContext);

            return await _accountService.UnfollowAccountAsync(tokenAccount.Id, request.followeeId)
                .Match<IActionResult>(
                    unit => Ok(),
                    err => BadRequest(MapError(err))
            );
        }

        private ErrorDto MapError(Error err) =>
            _mapper.Map<ErrorDto>(err);

        private T Map<T>(object source) =>
            _mapper.Map<T>(source);
    }
}
