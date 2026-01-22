using UserCreation.Domain.Entities;

namespace UserCreation.Application.Ports.Out;

/// <summary>
/// Puerto de salida para operaciones con JWT
/// </summary>
public interface IJwtTokenService
{
    string GenerateToken(Usuario usuario, Guid sessionId);
    DateTimeOffset GetTokenExpiration();
}
