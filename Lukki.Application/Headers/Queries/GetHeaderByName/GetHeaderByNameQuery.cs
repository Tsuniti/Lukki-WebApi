using ErrorOr;
using Lukki.Domain.HeaderAggregate;
using MediatR;

namespace Lukki.Application.Headers.Queries.GetHeaderByName;

public record GetHeaderByNameQuery(
    string Name) : IRequest<ErrorOr<MyHeader>>;