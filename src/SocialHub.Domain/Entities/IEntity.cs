using System;

namespace SocialHub.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
