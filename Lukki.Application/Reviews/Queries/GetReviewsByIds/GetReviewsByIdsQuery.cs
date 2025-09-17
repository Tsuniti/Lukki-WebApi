using MediatR;
using ErrorOr;
using Lukki.Application.Reviews.Common;

namespace Lukki.Application.Reviews.Queries.GetReviewsByIds;

public record GetReviewsByIdsQuery(
    List<string> ReviewIds
    ) : IRequest<ErrorOr<List<ReviewResult>>>;