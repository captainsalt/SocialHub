using LanguageExt;
using SocialHub.Application.Exceptions;
using SocialHub.Application.Models;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Services
{
    public interface IAccountService
    {
        Task<Option<Account>> GetUserByIDAsync(int id);

        Task<Option<Account>> GetUserByUsernameAsync(string username);

        Task<Either<UsernameInUseException, Account>> RegisterAsync(RegisterRequest user);
    }
}
