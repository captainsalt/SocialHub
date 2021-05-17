using LanguageExt;
using Microsoft.EntityFrameworkCore;
using SocialHub.Application.Interfaces;
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
            ICryptographyService cryptographyService)
        {
            _dbContext = dbContext;
            _cryptographyService = cryptographyService;
        }

        public async Task<Option<Account>> GetUserByIDAsync(int id) =>
            await _dbContext.Accounts.FindAsync(id);

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
            var newAccont = new Account(account.Email, account.Username, hashedPassword);

            await _dbContext.Accounts.AddAsync(newAccont);

            await _dbContext.SaveChangesAsync();

            return newAccont;
        }
    }
}
