using Lukki.Domain.CustomerAggregate;

namespace Lukki.Application.Customers.Common;

public record CustomerAuthenticationResult 
(
    Customer Customer,
    string Token
);