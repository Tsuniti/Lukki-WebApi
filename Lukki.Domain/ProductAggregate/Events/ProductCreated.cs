using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.Events;

public record ProductCreated(Product Product) : IDomainEvent;