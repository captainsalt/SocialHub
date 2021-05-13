using SocialHub.Domain;
using System.Threading.Tasks;
using LanguageExt;
using SocialHub.Domain.Models;

namespace SocialHub.Application.Services
{
    public interface IAuthenticationService
    {
        // TODO: User Either and make an exception for this
        Task<Account> Register(Account user);

        // TODO: User Either and make an exception for this
        Task<Option<Account>> Login(Account user);
    }
}
