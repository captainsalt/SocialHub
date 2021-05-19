﻿using LanguageExt;
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
            var accountOption = await _accountService.GetAccountByUsername(loginRequest.Username);

            return accountOption
                .Some<Either<Error, AuthResult>>(
                    acc =>
                    {
                        var isMatch = _cryptographyService.IsMatch(loginRequest.Password, acc.Password);

                        if (!isMatch)
                            return Errors.InvalidLogin;

                        var token = _jwtService.GenerateJwtToken(acc);
                        return new AuthResult(token, acc);
                    })
                .None(Errors.InvalidLogin);
        }

        public async Task<Either<Error, AuthResult>> RegisterAsync(RegisterRequest request)
        {
            var account = await _accountService.GetAccountByUsername(request.Username);

            if (account.IsSome)
                return Errors.UsernameInUse;

            var newAccount = await _accountService.AddAccountAsync(new Account(request.Email, request.Username, request.Password));
            var token = _jwtService.GenerateJwtToken(newAccount);

            return new AuthResult(token, newAccount);
        }
    }
}
