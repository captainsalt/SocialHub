using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialHub.Domain.Entities;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface ISocialHubDbContext
    {
        DbSet<Account> Accounts { get; set; }

        DbSet<Post> Posts { get; set; }

        Task<Either<Error, T>> AddAsync<T>(T entity) where T : class;

        Task<Either<Error, T>> UpdateAsync<T>(T entity) where T : class;

        EntityEntry Entry<T>(T entity) where T : class;
    }
}
