using SocialHub.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.API.Models
{
    public record AccountProfileDto(AccountDto Account, long Followers, long Following, long TotalPosts);
}
