namespace UserCreation.Application.DTOs.Personas;

/// <summary>
/// Query para obtener personas creadas en un rango de fechas
/// </summary>
public record GetPersonasCreadasQuery(
    DateTimeOffset? Desde,
    DateTimeOffset? Hasta
);
