using ErrorOr;

namespace Lukki.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string lastname, string email, string password);
    ErrorOr<AuthenticationResult> Login(string email, string password);
}