using Lukki.Application.Customers.Commands.GoogleRegister;
using Lukki.Application.Customers.Commands.Register;
using Lukki.Contracts.Authentication;
using Lukki.Contracts.Customers;
using MapsterMapper;
using MediatR;
using Lukki.Application.Customers.Queries.Login;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;

[Route("customers")]
public class CustomersController : ApiController
{

    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public CustomersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    } 
    
     [HttpPost("register")]
     [ProducesResponseType(typeof(CustomerAuthenticationResponse), StatusCodes.Status200OK)]

     public async Task<IActionResult> CustomerRegister(CustomerRegisterRequest request)
     {
         var command = _mapper.Map<CustomerRegisterCommand>(request);

         var authResult = await _mediator.Send(command);

         return authResult.Match(
             authResult => Ok(_mapper.Map<CustomerAuthenticationResponse>(authResult)),
             errors => Problem(errors));
     }
     [HttpPost("google-auth")]
     [ProducesResponseType(typeof(CustomerAuthenticationResponse), StatusCodes.Status200OK)]
     public async Task<IActionResult> CustomerGoogleAuth(GoogleAuthRequest request)
     {
         var command = _mapper.Map<CustomerGoogleAuthCommand>(request);

         var authResult = await _mediator.Send(command);

         return authResult.Match(
             result => Ok(_mapper.Map<CustomerAuthenticationResponse>(result)),
             errors => Problem(errors)
         );
     }
     
     

     
     
     [HttpPost("login")]
     [ProducesResponseType(typeof(CustomerAuthenticationResponse), StatusCodes.Status200OK)]

     public async Task<IActionResult> Login(LoginRequest request)
     {
         var query = _mapper.Map<CustomerLoginQuery>(request);

         var authResult = await _mediator.Send(query);
         
         return authResult.Match(
             authResult => Ok(_mapper.Map<CustomerAuthenticationResponse>(authResult)),
             errors => Problem(errors));
     }
}