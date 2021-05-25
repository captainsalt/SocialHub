using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace SocialHub.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISocialHubDbContext _dbContext;
        private readonly ICryptographyService _cryptographyService;

        public AccountService(
            ISocialHubDbContext dbContext,
            ICryptographyService cryptographyService)
        {
            _dbContext = dbContext;
            _cryptographyService = cryptographyService;
        }

        public async Task<Either<Error, Account>> GetAccountByIdAsync(Guid id)
        {
            var user = await _dbContext.Accounts.FindAsync(id);

            if (user is null)
                return Errors.UserDoesNotExist;

            return user;
        }

        public async Task<Either<Error, Account>> GetAccountByUsernameAsync(string username)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(acc => acc.Username == username);

            if (user is null)
                return Errors.UserDoesNotExist;

            return user;
        }

        /// <summary>
        /// Hashes account password then adds it to the database
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<Account> AddAccountAsync(Account account)
        {
            var hashedPassword = _cryptographyService.Hash(account.Password);
            var newAccont = new Account(account.Email, account.Username, hashedPassword);

            await _dbContext.AddAsync(newAccont);

            return newAccont;
        }

        public EitherAsync<Error, Unit> FollowAccountAsync(Guid followerId, Guid followeeId)
        {
            if (followerId == followeeId)
                return Errors.CannotFollowSelf;

            var followeeContext = GetAccountByIdAsync(followeeId).ToAsync();
            var followerContext = GetAccountByIdAsync(followerId).ToAsync();

            return
                followeeContext.Bind(followee =>
                followerContext.Bind(follower => 
                IsFollowingUser(followerId, followeeId).BindAsync<Unit>(async isFollowing => 
                {
                    if (isFollowing)
                        return Errors.AlreadyFollowing;

                    await _dbContext.Entry(followee)
                        .Collection(nameof(followee.Followers))
                        .LoadAsync();

                    followee.Followers.Add(follower);

                    await _dbContext.UpdateAsync(followee);

                    return unit;
                })));
        }

        public EitherAsync<Error, Unit> UnfollowAccountAsync(Guid followerId, Guid followeeId)
        {
            var followeeContext = GetAccountByIdAsync(followeeId).ToAsync();
            var followerContext = GetAccountByIdAsync(followerId).ToAsync();

            return
                followeeContext.Bind(followee =>
                followerContext.BindAsync<Unit>(async follower =>
                {
                    await _dbContext.Entry(followee)
                        .Collection(nameof(followee.Followers))
                        .LoadAsync();

                    followee.Followers.Remove(follower);
                    await _dbContext.UpdateAsync(followee);

                    return unit;
                }));
        }

        public EitherAsync<Error, AccountProfile> GetAccountProfile(string username)
        {
            return GetAccountByUsernameAsync(username).ToAsync()
                .BindAsync<AccountProfile>(async account =>
                {
                    var accountEntry = _dbContext.Entry(account);

                    var followerCount = await accountEntry.Collection(nameof(account.Followers))
                        .Query()
                        .Cast<Account>()
                        .LongCountAsync();

                    var followingCount = await accountEntry.Collection(nameof(account.Following))
                        .Query()
                        .Cast<Account>()
                        .LongCountAsync();

                    var totalPosts = await accountEntry.Collection(nameof(account.Posts))
                        .Query()
                        .Cast<Post>()
                        .LongCountAsync();

                    return new AccountProfile(account, followerCount, followingCount, totalPosts);
                });
        }

        public EitherAsync<Error, bool> IsFollowingUser(Guid followerId, Guid followeeId)
        {
            var followeeContext = GetAccountByIdAsync(followeeId).ToAsync();
            var followerContext = GetAccountByIdAsync(followerId).ToAsync();

            return 
                followeeContext.Bind(followee => 
                followerContext.BindAsync<bool>(async follower=>
                {
                    var exists = await _dbContext.Entry(followee)
                        .Collection(nameof(followee.Followers))
                        .Query()
                        .Cast<Account>()
                        .AnyAsync(acc => acc.Id == follower.Id);

                    return exists;
                }));
        }
    }
}
