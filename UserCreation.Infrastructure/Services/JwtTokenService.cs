using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserCreation.Application.Ports.Out;
using UserCreation.Domain.Entities;

namespace UserCreation.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de generación de JWT
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationMinutes;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        var jwtSection = configuration.GetSection("Jwt");
        
        _key = jwtSection["Key"] ?? throw new InvalidOperationException("JWT Key not configured");
        _issuer = jwtSection["Issuer"] ?? "UserCreation";
        _audience = jwtSection["Audience"] ?? "UserCreation";
        _expirationMinutes = int.TryParse(jwtSection["ExpirationMinutes"], out var exp) ? exp : 60;
    }

    public string GenerateToken(Usuario usuario, Guid sessionId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Identificador.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, sessionId.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public DateTimeOffset GetTokenExpiration()
    {
        return DateTimeOffset.UtcNow.AddMinutes(_expirationMinutes);
    }
}
