using Lukki.Application.Customers.Commands.Register;
using Lukki.Application.Customers.Common;
using Lukki.Application.Customers.Queries.Login;
using Lukki.Contracts.Customers;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class CustomerMappingConfig : IRegister
{
         public void Register(TypeAdapterConfig config)
     {
         config.NewConfig<CustomerRegisterRequest, CustomerRegisterCommand>(); // redundant, but for clarity that this is in use
         config.NewConfig<CustomerLoginRequest, CustomerLoginQuery>();         // redundant, but for clarity that this is in use

         
         config.NewConfig<CustomerAuthenticationResult, CustomerAuthenticationResponse>()
             .Map(dest => dest, src => src.Customer);
         
         TypeAdapterConfig<CustomerId, string>.NewConfig().MapWith(id => id.Value.ToString());

     }
}