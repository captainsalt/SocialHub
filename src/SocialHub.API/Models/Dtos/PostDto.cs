using SocialHub.API.Dtos;

namespace SocialHub.API.Models.Dtos
{
    public record PostDto(string Content, AccountDto Account);
}
