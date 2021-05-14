using LanguageExt;
using SocialHub.Application;
using SocialHub.Application.Interfaces;
using SocialHub.Domain;
using SocialHub.Domain.Models;
using SocialHub.Infrastructure.Database;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure
{
    public class AccountService : IAccountService
    {
        private readonly ISocialHubDbContext _dbContext;

        public AccountService(ISocialHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Option<Account>> GetUserByIDAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Option<Account>> GetUserByUsernameAsync(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
