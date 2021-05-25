using SocialHub.Domain.Entities;
using System;

namespace SocialHub.Application.Models
{
    public record AccountProfile(Account Account, long Followers, long Following, long TotalPosts);
}
