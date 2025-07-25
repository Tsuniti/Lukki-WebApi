// using System.Security.Claims;
// using Lukki.Application.Footer.Commands.CreateFooter;
// using Lukki.Contracts.Footer;
// using Lukki.Domain.Common.Enums;
// using MapsterMapper;
// using MediatR;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Lukki.Api.Controllers;
//
//
// [Route("footer")]
// public class FooterController : ApiController
// {
//
//     private readonly IMapper _mapper;
//     private readonly ISender _mediator;
//
//     public FooterController(IMapper mapper, ISender mediator)
//     {
//         _mapper = mapper;
//         _mediator = mediator;
//     }
//     
//     [HttpPost]
//     [Authorize(Roles = nameof(UserRole.SELLER))] // hack: Temporary, until we have an admin
//     public async Task<IActionResult> CreateFooter(CreateFooterRequest request)
//     {
//         var command = _mapper.Map<CreateFooterCommand>(request);
//         
//         return createFooterResult.Match(
//             footer => Ok(_mapper.Map<FooterResponse>(footer)),
//             errors => Problem(errors) 
//         );
//     }
// }