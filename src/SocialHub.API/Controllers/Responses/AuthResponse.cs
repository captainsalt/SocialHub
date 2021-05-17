using SocialHub.Infrastructure.Dtos;

namespace SocialHub.API.Controllers
{
    public record AuthResponse(string Token, AccountDto Account);
}
