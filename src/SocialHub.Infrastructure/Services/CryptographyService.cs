using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using System;
using System.Security.Cryptography;

namespace SocialHub.Infrastructure.Services
{
    public class CryptographyService : ICryptographyService
    {
        private readonly CryptographyServiceConfiguration _config;

        public CryptographyService(CryptographyServiceConfiguration config)
        {
            _config = config;
        }

        public string Hash(string input)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, _config.Iterations);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var hashed = Convert.ToBase64String(hashBytes);
            return hashed;
        }

        public bool IsMatch(string input, string hashedString)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedString);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, _config.Iterations);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
