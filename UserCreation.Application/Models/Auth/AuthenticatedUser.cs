namespace UserCreation.Application.DTOs.Auth;

/// <summary>
/// Representa un usuario autenticado con claims del JWT
/// </summary>
public record AuthenticatedUser(
    Guid UserId,
    string Usuario,
    Guid SessionId
);
