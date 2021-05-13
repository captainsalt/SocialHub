using Microsoft.EntityFrameworkCore;
using SocialHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface ISocialHubDbContext
    {
        DbSet<Account> Accounts { get; set; }

        Task<int> SaveChangesAsync();
    }
}
