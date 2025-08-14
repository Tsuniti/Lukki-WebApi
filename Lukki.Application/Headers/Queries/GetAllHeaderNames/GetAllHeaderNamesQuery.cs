using ErrorOr;
using MediatR;

namespace Lukki.Application.Headers.Queries.GetAllHeaderNames;

public record GetAllHeaderNamesQuery : IRequest<ErrorOr<List<string>>>;