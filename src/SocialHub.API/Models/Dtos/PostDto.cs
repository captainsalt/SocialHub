using System;

namespace SocialHub.API.Dtos
{
    public record PostDto(Guid Id, string Content, DateTime CreatedAt, AccountDto Account, bool IsLiked = false, bool IsShared = false);
}
