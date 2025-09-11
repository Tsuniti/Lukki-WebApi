using Lukki.Domain.ReviewAggregate;

namespace Lukki.Application.Reviews.Commands.Common;

public record ReviewResult
(
    Review Review,
    string CustomerName
);