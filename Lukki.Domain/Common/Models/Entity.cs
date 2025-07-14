namespace Lukki.Domain.Common.Models;

    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
    {
        
        private readonly List<IDomainEvent> _domainEvents = new();
        public TId Id { get; protected set; }

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected Entity(TId id)
        {
            Id = id;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Entity<TId>);
        }
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        public bool Equals(Entity<TId>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
        
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        
        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Entity()
        {
            // EF Core requires a parameterless constructor for materialization
        }
#pragma warning restore CS8618
        
    }
