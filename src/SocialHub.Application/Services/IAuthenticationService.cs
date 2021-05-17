using SocialHub.Domain;
using System.Threading.Tasks;
using LanguageExt;
using SocialHub.Domain.Models;
using SocialHub.Application.Models;
using LanguageExt.Common;

namespace SocialHub.Application.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Log in and get a JWT token from the server
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>A JWT Token</returns>
        Task<Either<Error, (string token, Account account)>> LoginAsync(LoginRequest loginRequest);
    }
}
