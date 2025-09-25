using Google.Apis.Auth;
using Lukki.Application.Common.Interfaces.Authentication;

namespace Lukki.Infrastructure.Authentication;

public class GoogleAuthService : IGoogleAuthService
{
    public async Task<GoogleJsonWebSignature.Payload> ValidateIdTokenAsync(string idToken)
    {
        return await GoogleJsonWebSignature.ValidateAsync(idToken);
    }
}