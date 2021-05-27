using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialHub.Domain.Entities
{
    public class Post : IEntity
    {
        public Post()
        {
        }

        public Post(Guid accountId, string content)
        {
            AccountId = accountId;
            Content = content;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public bool IsLiked { get; set; }

        [NotMapped]
        public bool IsShared { get; set; }

        /// <summary>
        /// Post Author Id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Post Author
        /// </summary>
        public virtual Account Account { get; }

        public virtual List<Account> LikedBy { get; set; } = new();

        public virtual List<Account> SharedBy { get; set; } = new();
    }
}
