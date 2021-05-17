using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Models;
using SocialHub.Application.Services;
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

        public async Task<Either<Error, AuthResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var accountOption = await _accountService.GetUserByUsernameAsync(loginRequest.Username);

            return accountOption
                .Some<Either<Error, AuthResponse>>(
                    acc =>
                    {
                        var isMatch = _cryptographyService.IsMatch(loginRequest.Password, acc.Password);

                        if (!isMatch)
                            return Errors.InvalidLogin;

                        var token = _jwtService.GenerateJwtToken(acc);
                        return new AuthResponse(token, acc);
                    })
                    .None(Errors.InvalidLogin);

        }
    }
}
