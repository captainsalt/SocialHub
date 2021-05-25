using System;

namespace SocialHub.Application.Models
{
    public record AccountProfile(Guid Id, long Followers, long Following, long TotalPosts);
}
