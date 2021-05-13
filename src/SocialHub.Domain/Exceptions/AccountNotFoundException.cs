using System;

namespace SocialHub.Domain
{
    public class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException() : base("Account not found")
        {
        }
    }
}
