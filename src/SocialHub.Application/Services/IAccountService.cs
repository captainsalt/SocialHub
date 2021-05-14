using LanguageExt;
using SocialHub.Domain;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Application
{
    public interface IAccountService
    {
        Task<Option<Account>> GetUserByIDAsync(int id);

        Task<Option<Account>> GetUserByUsernameAsync(string username);
    }
}
