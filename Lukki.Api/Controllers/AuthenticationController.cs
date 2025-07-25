﻿using ErrorOr;
using Lukki.Application.Authentication.Commands.Register;
using Lukki.Application.Authentication.Common;
using Lukki.Application.Authentication.Queries.Login;
using Lukki.Contracts.Authentication;
using Lukki.Domain.Common.Enums;
using Lukki.Domain.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Lukki.Contracts.Authentication.LoginRequest;
using RegisterRequest = Lukki.Contracts.Authentication.RegisterRequest;

namespace Lukki.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request) with { Role = UserRole.SELLER };

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
    
   
    
    
}