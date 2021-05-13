using LanguageExt;
using Microsoft.Extensions.Configuration;
using SocialHub.Application.Services;
using SocialHub.Domain;
using SocialHub.Domain.Models;
using SocialHub.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SocialHubDbContext _dbContext;

        public AuthenticationService(SocialHubDbContext dbContext, IConfiguration config) 
        {
            _dbContext = dbContext;
        }

        public Task<Option<Account>> Login(Account user)
        {
            throw new NotImplementedException();
        }

        public Task<Account> Register(Account user)
        {
            throw new NotImplementedException();
        }
    }
}
