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
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from profile in _accountService.GetAccountProfile(username)
                from isFollowing in _accountService.IsFollowingAccount(tokenAccount.Id, profile.Account.Id)
                select profile with { IsFollowing = isFollowing };

            return await result.Match<IActionResult>(
                result => Ok(Map<AccountProfileDto>(result)),
                err => BadRequest(MapError(err))
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
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from followeeAcc in _accountService.GetAccountByUsernameAsync(request.FolloweeUsername).ToAsync()
                from unit in _accountService.FollowAccountAsync(tokenAccount.Id, followeeAcc.Id)
                select unit;

            return await result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        [HttpDelete("unfollow")]
        public async Task<IActionResult> Unfollow([FromQuery] FollowRequest request)
        {
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from followeeAcc in _accountService.GetAccountByUsernameAsync(request.FolloweeUsername).ToAsync()
                from unit in _accountService.UnfollowAccountAsync(tokenAccount.Id, followeeAcc.Id)
                select unit;

            return await result.Match<IActionResult>(
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
