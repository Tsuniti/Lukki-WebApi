using ErrorOr;
using Lukki.Domain.FooterAggregate;
using MediatR;

namespace Lukki.Application.Footers.Queries.GetFooter;

public record GetFooterQuery(
    string Name) : IRequest<ErrorOr<Footer>>;