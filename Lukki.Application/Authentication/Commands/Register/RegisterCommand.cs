// using ErrorOr;
// using Lukki.Application.Authentication.Common;
// using Lukki.Domain.Common.Enums;
// using MediatR;
//
// namespace Lukki.Application.Authentication.Commands.Register;
//
// public record RegisterCommand(
//     string? BrandName,          // Optional for Seller role
//     string? FirstName,          // null if seller don't want to provide it
//     string? LastName,           // null if seller don't want to provide it
//     string Email,
//     string? Password,           // null if registering with Google
//     UserRole Role,              // "Customer" or "Seller"
//     string? PhoneNumber         // Optional for Customer role
//     ) : IRequest<ErrorOr<AuthenticationResult>>;