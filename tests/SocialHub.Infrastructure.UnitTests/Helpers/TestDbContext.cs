using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Tests.Helpers
{
    public class TestDbContext : DbContext, ISocialHubDbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public Task<Either<Error, T>> AddAsync<T>(T entity) where T : class =>
            Task.FromResult<Either<Error, T>>(entity);

        public Task<Either<Error, T>> UpdateAsync<T>(T entity) where T : class =>
            Task.FromResult<Either<Error, T>>(entity);

        EntityEntry ISocialHubDbContext.Entry<T>(T entity) => throw new System.NotImplementedException();
    }
}
