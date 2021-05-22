using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IJwtService _jwtService;

        public AuthenticationService(
            IAccountService accountService,
            ICryptographyService cryptographyService,
            IJwtService jwtService)
        {
            _accountService = accountService;
            _cryptographyService = cryptographyService;
            _jwtService = jwtService;
        }

        public async Task<Either<Error, AuthResult>> LoginAsync(LoginRequest loginRequest)
        {
            var accountEither = await _accountService.GetAccountByUsernameAsync(loginRequest.Username);

            return accountEither.Match<Either<Error, AuthResult>>(
                acc =>
                {
                    var isMatch = _cryptographyService.IsMatch(loginRequest.Password, acc.Password);

                    if (!isMatch)
                        return Errors.InvalidLogin;

                    var token = _jwtService.GenerateJwtToken(acc);
                    return new AuthResult(token, acc);
                },
                err => Errors.InvalidLogin
            );
        }

        public async Task<Either<Error, AuthResult>> RegisterAsync(RegisterRequest request)
        {
            var accountEither = _accountService.GetAccountByUsernameAsync(request.Username).ToAsync();

            return await accountEither.MatchAsync<Either<Error, AuthResult>>(
                acc => Errors.UsernameInUse,
                async err =>
                {
                    var newAccount = await _accountService.AddAccountAsync(new Account(request.Email, request.Username, request.Password));
                    var token = _jwtService.GenerateJwtToken(newAccount);

                    return new AuthResult(token, newAccount);
                }
            );
        }
    }
}
