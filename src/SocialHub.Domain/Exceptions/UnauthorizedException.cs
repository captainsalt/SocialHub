using System;

namespace SocialHub.Domain
{
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException() : base("Unauthorized")
        {
        }
    }
}
