using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using SocialHub.Application;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Application.Services;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISocialHubDbContext _dbContext;
        private readonly ICryptographyService _cryptographyService;

        public AccountService(
            ISocialHubDbContext dbContext,
            ICryptographyService cryptographyService,
            IJwtService jwtService)
        {
            _dbContext = dbContext;
            _cryptographyService = cryptographyService;
        }

        public Task<Option<Account>> GetUserByIDAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Option<Account>> GetUserByUsernameAsync(string username) =>
            await _dbContext.Accounts.AsNoTracking().SingleOrDefaultAsync(acc => acc.Username == username);

        /// <summary>
        /// Hashes account password then adds it to the database
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<Account> AddAccountAsync(Account account)
        {
            var hashedPassword = _cryptographyService.Hash(account.Password);
            var result = await _dbContext.Accounts.AddAsync(new(account.Email, account.Username, hashedPassword));

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
