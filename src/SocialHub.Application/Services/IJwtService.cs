using SocialHub.Domain.Models;

namespace SocialHub.Application.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(Account user);
    }
}
