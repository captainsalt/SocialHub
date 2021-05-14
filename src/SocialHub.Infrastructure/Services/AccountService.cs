using LanguageExt;
using Microsoft.EntityFrameworkCore;
using SocialHub.Application;
using SocialHub.Application.Exceptions;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Application.Services;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure
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

        public Task<Option<Account>> GetUserByIDAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Option<Account>> GetUserByUsernameAsync(string username) =>
            await _dbContext.Accounts.SingleOrDefaultAsync(acc => acc.Username == username);

        public async Task<Either<UsernameInUseException, Account>> RegisterAsync(RegisterRequest request)
        {
            var account = await GetUserByUsernameAsync(request.Username);

            if (account.IsSome)
                return new UsernameInUseException();

            var hashedPassword = _cryptographyService.Hash(request.Password);
            var result = await _dbContext.Accounts.AddAsync(new(request.Email, request.Username, hashedPassword));

            return result.Entity;
        }
    }
}
