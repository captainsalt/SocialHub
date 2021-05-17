using LanguageExt;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Services
{
    public interface IAccountService
    {
        Task<Account> AddAccountAsync(Account account);

        Task<Option<Account>> GetUserByIDAsync(int id);

        Task<Option<Account>> GetAccountByUsername(string username);
    }
}
