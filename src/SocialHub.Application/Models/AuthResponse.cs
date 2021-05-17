using SocialHub.Domain.Models;

namespace SocialHub.Application.Models
{
    public record AuthResponse(string Token, Account Account);
}
