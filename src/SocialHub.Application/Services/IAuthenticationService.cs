using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Log in and get a JWT token from the server
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>A JWT Token</returns>
        Task<Either<Error, AuthResponse>> LoginAsync(LoginRequest loginRequest);
    }
}
