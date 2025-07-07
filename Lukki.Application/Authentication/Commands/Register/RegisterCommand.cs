using ErrorOr;
using Lukki.Application.Authentication.Common;
using Lukki.Domain.Common.Enums;
using MediatR;

namespace Lukki.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string? FirstName,
    string? LastName,
    string Email,
    string Password,
    UserRole Role,           // "Customer" or "Seller"
    string PhoneNumber, // Optional for Customer role
    string? BrandName // Optional for Seller role
    ) : IRequest<ErrorOr<AuthenticationResult>>;