namespace Lukki.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastname, string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(), 
            firstName,
            lastname, 
            email, 
            "token");
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(), 
            "firstName",
            "lastname", 
            email, 
            "token");
    }
}