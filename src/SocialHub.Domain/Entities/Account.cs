using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialHub.Domain.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Account : IEntity
    {
        public Account()
        {
        }

        public Account(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public double CreatedAt { get; set; }

        public virtual List<Post> Posts { get; set; } = new();

        public virtual List<Post> Shares { get; set; } = new();

        public virtual List<Post> Likes { get; set; } = new();

        public virtual List<Account> Followers { get; set; } = new();

        public virtual List<Account> Following { get; set; } = new();
    }
}
