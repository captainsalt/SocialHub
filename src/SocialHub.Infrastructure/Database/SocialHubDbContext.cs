using Microsoft.EntityFrameworkCore;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialHub.Infrastructure.Database
{
 
    public class SocialHubDbContext :  DbContext, ISocialHubDbContext
    {
        public SocialHubDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public async Task<int> SaveChangesAsync() =>
            await base.SaveChangesAsync();
    }
}
