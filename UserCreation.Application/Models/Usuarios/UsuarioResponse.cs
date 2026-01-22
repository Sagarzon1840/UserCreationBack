namespace UserCreation.Application.DTOs.Usuarios;

/// <summary>
/// DTO de respuesta para Usuario
/// </summary>
public record UsuarioResponse(
    Guid Identificador,
    string Usuario,
    DateTimeOffset FechaCreacion,
    Guid? PersonaId
);
