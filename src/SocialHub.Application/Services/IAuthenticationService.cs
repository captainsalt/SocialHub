using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Services
{
    public interface IAuthenticationService
    {
        Task<Either<Error, AuthResponse>> LoginAsync(LoginRequest loginRequest);

        Task<Either<Error, AuthResponse>> RegisterAsync(RegisterRequest request);
    }
}
