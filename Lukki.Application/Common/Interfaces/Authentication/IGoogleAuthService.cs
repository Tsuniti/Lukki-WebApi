using Google.Apis.Auth;
namespace Lukki.Application.Common.Interfaces.Authentication;

public interface IGoogleAuthService
{
    Task<GoogleJsonWebSignature.Payload> ValidateIdTokenAsync(string idToken);
}