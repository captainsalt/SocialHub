using System;

namespace SocialHub.API.Dtos
{
    public record AccountDto(Guid Id, string Username, string Email, DateTime CreatedAt);
}
