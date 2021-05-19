using LanguageExt;
using LanguageExt.Common;
using SocialHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> AddAccountAsync(Account account);

        Task<Either<Error, Account>> GetAccountByIdAsync(Guid id);

        Task<Option<Account>> GetAccountByUsername(string username);
    }
}
