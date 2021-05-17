﻿using LanguageExt;
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
        private readonly IJwtService _jwtService;

        public AccountService(
            ISocialHubDbContext dbContext,
            ICryptographyService cryptographyService,
            IJwtService jwtService)
        {
            _dbContext = dbContext;
            _cryptographyService = cryptographyService;
            _jwtService = jwtService;
        }

        public Task<Option<Account>> GetUserByIDAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Option<Account>> GetUserByUsernameAsync(string username) => 
            await _dbContext.Accounts.AsNoTracking().SingleOrDefaultAsync(acc => acc.Username == username);

        public async Task<Either<Error, AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            var account = await GetUserByUsernameAsync(request.Username);

            if (account.IsSome)
                return Errors.UsernameInUse;

            var hashedPassword = _cryptographyService.Hash(request.Password);
            var result = await _dbContext.Accounts.AddAsync(new(request.Email, request.Username, hashedPassword));

            await _dbContext.SaveChangesAsync();

            var token = _jwtService.GenerateJwtToken(result.Entity);

            return new AuthResponse(token, result.Entity);
        }
    }
}
