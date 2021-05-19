using SocialHub.API.Dtos;

namespace SocialHub.API.Models
{
    public record AuthResultDto(string Token, AccountDto Account);
}
