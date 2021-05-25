using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using SocialHub.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SocialHub.Application.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Key to get Account from HttpContext
        /// </summary>
        string Key { get; }

        string GenerateJwtToken(Account account);

        Either<Error, Account> GetAccountFromToken(HttpContext context);

        Option<JwtSecurityToken> ValidateToken(string token);
    }
}
