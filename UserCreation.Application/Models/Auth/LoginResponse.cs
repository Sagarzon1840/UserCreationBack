namespace UserCreation.Application.DTOs.Auth;

/// <summary>
/// DTO de respuesta para login exitoso
/// </summary>
public record LoginResponse(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    Guid SessionId,
    Guid UserId,
    string Usuario
);
