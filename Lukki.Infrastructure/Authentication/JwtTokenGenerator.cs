using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Services;
using Lukki.Domain.CustomerAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Lukki.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{

    private readonly JwtSettings _jwtSettings;
    
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Customer customer)
    {
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, customer.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, customer.Email),
            new Claim(JwtSettings.RoleClaimType, AccessRoles.Customer),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        return CreateToken(claims);
    }
    
    // public string GenerateToken(Admin admin)
    // {
    //     
    //     var claims = new[]
    //     {
    //         new Claim(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
    //         new Claim(JwtRegisteredClaimNames.Email, customer.Email),
    //         new Claim(JwtSettings.RoleClaimType, AccessRoles.Admin),
    //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //     };
    //
    //     return CreateToken(claims);
    // }
    
    private string CreateToken(IEnumerable<Claim> claims)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        
        
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}