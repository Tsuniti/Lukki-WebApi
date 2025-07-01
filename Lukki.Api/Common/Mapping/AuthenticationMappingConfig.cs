using Lukki.Application.Authentication.Commands.Register;
using Lukki.Application.Authentication.Common;
using Lukki.Application.Authentication.Queries.Login;
using Lukki.Contracts.Authentication;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>(); // redundant, but for clarity that this is in use
        config.NewConfig<LoginRequest, LoginQuery>();         // redundant, but for clarity that this is in use

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}