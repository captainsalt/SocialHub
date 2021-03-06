using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Database
{

    public class SocialHubDbContext : DbContext, ISocialHubDbContext
    {
        public SocialHubDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(acc => acc.Posts)
                .WithOne(post => post.Account);

            // TODO: Use entities for join
            modelBuilder.Entity<Account>()
                .HasMany(acc => acc.Followers)
                .WithMany(acc => acc.Following)
                .UsingEntity(joinTable => joinTable.ToTable("AccountAccountFollowing"));

            // TODO: Combine likes with shares
            modelBuilder.Entity<Account>()
                .HasMany(acc => acc.Likes)
                .WithMany(post => post.LikedBy)
                .UsingEntity(joinTable => joinTable.ToTable("AccountPostLike"));

            modelBuilder.Entity<Account>()
                .HasMany(acc => acc.Shares)
                .WithMany(post => post.SharedBy)
                .UsingEntity(joinTable => joinTable.ToTable("AccountPostShare"));

            base.OnModelCreating(modelBuilder);
        }

        public new EntityEntry Entry<T>(T entity) where T : class =>
            base.Entry(entity);

        // TODO: Capture errors
        public async Task<Either<Error, T>> AddAsync<T>(T entity) where T : class
        {
            try
            {
                var doesNotExist = !Entry(entity).IsKeySet;

                if (entity is IEntity x && doesNotExist)
                {
                    // Use javascript inital date
                    x.CreatedAt = DateTime.UtcNow
                        .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds;
                }

                var added = await base.AddAsync(entity);
                await SaveChangesAsync();

                return added.Entity;
            }
            catch (Exception ex)
            {
                return Error.New(ex);
            }
        }

        // TODO: Capture errors
        public async Task<Either<Error, T>> UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                var updated = base.Update(entity);
                await SaveChangesAsync();

                return updated.Entity;
            }
            catch (Exception ex)
            {
                return Error.New(ex);
            }
        }
    }
}
