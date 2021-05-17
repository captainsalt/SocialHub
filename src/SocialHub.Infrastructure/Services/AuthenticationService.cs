using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application;
using SocialHub.Application.Models;
using SocialHub.Application.Services;
using SocialHub.Domain.Models;
using System;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Services
{
    // TODO: Combine with AccountService
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

        public async Task<Either<Error, (string token, Account account)>> LoginAsync(LoginRequest loginRequest)
        {
            var accountOption = await _accountService.GetUserByUsernameAsync(loginRequest.Username);

            return accountOption.Some<Either<Error, (string token, Account account)>>(
                acc =>
                {
                    var isMatch = _cryptographyService.IsMatch(loginRequest.Password, acc.Password);

                    if (isMatch)
                        return (_jwtService.GenerateJwtToken(acc), acc);
                    else
                        return Errors.InvalidLogin;
                })
                .None(() => Errors.InvalidLogin);
        }
    }
}
