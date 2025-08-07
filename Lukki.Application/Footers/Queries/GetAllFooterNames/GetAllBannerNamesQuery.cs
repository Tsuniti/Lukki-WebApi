using Lukki.Domain.FooterAggregate;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Footers.Queries.GetAllFooterNames;

public record GetAllFooterNamesQuery : IRequest<ErrorOr<List<string>>>;