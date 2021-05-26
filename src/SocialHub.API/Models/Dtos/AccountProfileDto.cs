using SocialHub.API.Dtos;

namespace SocialHub.API.Models
{
    public record AccountProfileDto(AccountDto Account, long Followers, long Following, long TotalPosts, bool IsFollowing);
}
