using SocialHub.Domain.Entities;

namespace SocialHub.Application.Models
{
    public record AccountProfile(Account Account, long Followers, long Following, long TotalPosts);
}
