using SocialHub.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.API.Controllers
{
    public record AuthResponse(string Token, AccountDto Account);
}
