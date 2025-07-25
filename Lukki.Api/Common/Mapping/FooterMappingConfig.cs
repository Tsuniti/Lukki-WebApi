// using Lukki.Application.Footers.Commands.CreateFooter;
// using Lukki.Contracts.Footers;
// using Lukki.Domain.FooterAggregate;
// using Lukki.Domain.FooterAggregate.ValueObjects;
// using Mapster;
//
// namespace Lukki.Api.Common.Mapping;
//
// public class FooterMappingConfig : IRegister
// {
//     public void Register(TypeAdapterConfig config)
//     {
//         config.NewConfig<CreateFooterRequest, CreateFooterCommand>(); // redundant, but for clarity that this is in use
//
//         config.NewConfig<Footer, FooterResponse>()
//             .Map(dest => dest.Id, src => src.Id.Value);
//         
//         TypeAdapterConfig<FooterId, Guid>.NewConfig().MapWith(id => id.Value);
//
//     }
// }