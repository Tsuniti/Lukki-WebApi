using ErrorOr;
using Lukki.Domain.FooterAggregate;
using MediatR;

namespace Lukki.Application.Footers.Queries.GetFooterByName;

public record GetFooterByNameQuery(
    string Name) : IRequest<ErrorOr<Footer>>;