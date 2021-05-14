using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialHub.Application.Models
{
    public record RegisterRequest(string Email, string Username, string Password);
}
