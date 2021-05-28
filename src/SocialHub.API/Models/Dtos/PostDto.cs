using System;

namespace SocialHub.API.Dtos
{
    public record PostDto(Guid Id, string Content, long CreatedAt, AccountDto Account, bool IsLiked, bool IsShared);
}
