using SocialHub.API.Dtos;
using System;

namespace SocialHub.API.Models.Dtos
{
    public record PostDto(Guid Id, string Content, DateTime CreatedAt, AccountDto Account);
}
