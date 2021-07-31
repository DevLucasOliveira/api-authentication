using System;

namespace Authentication.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

    }
}