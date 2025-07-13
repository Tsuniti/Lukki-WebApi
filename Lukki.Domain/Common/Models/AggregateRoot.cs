namespace Lukki.Domain.Common.Models;

public class AggregateRoot<TId, TIdType> : Entity<TId>
where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set; }
    protected AggregateRoot(TId id)
    {
        Id = id;
    }
    
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected AggregateRoot() : base(default!)
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
    
}