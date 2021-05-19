using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Either<Error, AuthResult>> LoginAsync(LoginRequest loginRequest);

        Task<Either<Error, AuthResult>> RegisterAsync(RegisterRequest request);
    }
}
