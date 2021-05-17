using Microsoft.EntityFrameworkCore;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface ISocialHubDbContext
    {
        DbSet<Account> Accounts { get; set; }

        Task<int> SaveChangesAsync();
    }
}
