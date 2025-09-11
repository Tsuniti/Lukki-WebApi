// using Lukki.Contracts.Authentication;
// using Lukki.Domain.Common.Enums;
// using Lukki.Domain.Common.ValueObjects;
// using Mapster;
// using LoginRequest = Lukki.Contracts.Authentication.LoginRequest;
// using RegisterRequest = Lukki.Contracts.Authentication.RegisterRequest;
//
// namespace Lukki.Api.Common.Mapping;
//
// public class AuthenticationMappingConfig : IRegister
// {
//     public void Register(TypeAdapterConfig config)
//     {
//         config.NewConfig<RegisterRequest, RegisterCommand>(); // redundant, but for clarity that this is in use
//         config.NewConfig<LoginRequest, LoginQuery>();         // redundant, but for clarity that this is in use
//
//         
//         config.NewConfig<AuthenticationResult, AuthenticationResponse>()
//             .Map(dest => dest, src => src.User);
//         
//         TypeAdapterConfig<UserId, Guid>.NewConfig().MapWith(id => id.Value);
//         TypeAdapterConfig<UserRole, string>.NewConfig().MapWith(role => role.ToString());
//
//     }
// }