using SocialHub.Domain;
using System.Threading.Tasks;
using LanguageExt;
using SocialHub.Domain.Models;
using SocialHub.Application.Models;

namespace SocialHub.Application.Services
{
    public interface IAuthenticationService
    {
        // TODO: User Either and make an exception for this
        Task<Option<Account>> Register(RegisterRequest user);

        // TODO: User Either and make an exception for this
        Task<Option<Account>> Login(LoginRequest loginRequest);
    }
}
