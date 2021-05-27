using System;

namespace SocialHub.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public double CreatedAt { get; set; }
    }
}
