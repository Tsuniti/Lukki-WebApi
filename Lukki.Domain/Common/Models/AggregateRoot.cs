namespace Lukki.Domain.Common.Models;

public class AggregateRoot<TId> : Entity<TId> 
where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
    
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected AggregateRoot() : base(default!)
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
    
}