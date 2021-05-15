using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialHub.Application.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Invalid login")
        {
        }
    }
}
