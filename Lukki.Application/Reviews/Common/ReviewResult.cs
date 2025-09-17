using Lukki.Domain.ReviewAggregate;

namespace Lukki.Application.Reviews.Common;

public record ReviewResult
(
    Review Review,
    string CustomerName
);