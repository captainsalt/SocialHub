using System;
using System.ComponentModel.DataAnnotations;

namespace SocialHub.Domain.Models
{
    public class Account
    {
        public Account(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }

        [Key]
        public int ID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
