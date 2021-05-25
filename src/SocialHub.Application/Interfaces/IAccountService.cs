using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> AddAccountAsync(Account account);

        Task<Either<Error, Account>> GetAccountByIdAsync(Guid id);

        Task<Either<Error, Account>> GetAccountByUsernameAsync(string username);

        EitherAsync<Error, Unit> FollowAccountAsync(Guid followerId, Guid followeeId);

        EitherAsync<Error, Unit> UnfollowAccountAsync(Guid followerId, Guid followeeId);

        EitherAsync<Error, AccountProfile> GetAccountProfile(string username);

        EitherAsync<Error, bool> IsFollowingAccount(Guid followerId, Guid followeeId);
    }
}
