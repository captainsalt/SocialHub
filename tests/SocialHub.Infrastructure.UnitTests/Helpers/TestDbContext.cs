using Microsoft.EntityFrameworkCore;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Tests.Helpers
{
    public class TestDbContext : DbContext, ISocialHubDbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public async Task<int> SaveChangesAsync() =>
            await base.SaveChangesAsync();
    }
}
