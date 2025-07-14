using Lukki.Domain.ProductAggregate.Events;
using MediatR;

namespace Lukki.Application.Products.Events;

public class DummyHandler : INotificationHandler<ProductCreated>
{
    public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}