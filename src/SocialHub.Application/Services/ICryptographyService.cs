using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialHub.Application.Services
{
    public interface ICryptographyService
    {
        string Hash(string input);

        bool IsMatch(string input, string hashedString);
    }
}
