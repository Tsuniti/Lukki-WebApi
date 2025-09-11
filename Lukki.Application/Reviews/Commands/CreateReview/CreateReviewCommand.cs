using Lukki.Domain.ReviewAggregate;
using MediatR;
using ErrorOr;
using Lukki.Application.Reviews.Commands.Common;

namespace Lukki.Application.Reviews.Commands.CreateReview;

public record CreateReviewCommand
(
    short Rating,
    string Comment,
    string ProductId,
    string CustomerId
) : IRequest<ErrorOr<ReviewResult>>;