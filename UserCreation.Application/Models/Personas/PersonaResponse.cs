namespace UserCreation.Application.DTOs.Personas;

/// <summary>
/// DTO de respuesta para Persona
/// </summary>
public record PersonaResponse(
    Guid Identificador,
    string Nombres,
    string Apellidos,
    string NumeroIdentificacion,
    string Email,
    string TipoIdentificacion,
    DateTimeOffset FechaCreacion,
    string IdCompleto,
    string NombreCompleto,
    string NumeroCelular
);
