using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System;
using System.Threading.Tasks;

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

        public async Task<Option<Account>> GetAccountByUsername(string username) =>
            await _dbContext.Accounts.SingleOrDefaultAsync(acc => acc.Username == username);

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
    }
}
