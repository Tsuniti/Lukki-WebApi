﻿using ErrorOr;
using Lukki.Application.Authentication.Common;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.Interfaces;
using MediatR;

namespace Lukki.Application.Authentication.Queries.Login;

public class LoginQuerydHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQuerydHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        
        // 1. Validate the user exists
        if (await _userRepository.GetUserByEmailAsync(query.Email) is not IUser user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        // 2. Validate the password is correct
        if (user.PasswordHash != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
}