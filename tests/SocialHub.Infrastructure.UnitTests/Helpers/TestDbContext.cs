using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace SocialHub.Infrastructure.Tests.Helpers
{
    public class TestDbContext : DbContext, ISocialHubDbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public Task<Either<Error, Unit>> AddAsync<T>(T entity) where T : class =>
            Task.FromResult<Either<Error, Unit>>(unit);

        public Task InsertOrUpdateAsync<T>(T entity) where T : class =>
            Task.CompletedTask;

        public Task<Either<Error, Unit>> UpdateAsync<T>(T entity) where T : class =>
            Task.FromResult<Either<Error, Unit>>(unit);

        EntityEntry ISocialHubDbContext.Entry<T>(T entity) => throw new System.NotImplementedException();
    }
}
