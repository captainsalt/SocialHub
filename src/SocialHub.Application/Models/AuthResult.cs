using SocialHub.Domain.Entities;

namespace SocialHub.Application.Models
{
    public record AuthResult(string Token, Account Account);
}
