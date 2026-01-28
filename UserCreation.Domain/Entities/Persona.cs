namespace UserCreation.Domain.Entities;

/// <summary>
/// Entidad Persona del dominio
/// </summary>
public class Persona
{
    public Guid Identificador { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string NumeroIdentificacion { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TipoIdentificacion { get; set; } = string.Empty;
    public DateTimeOffset FechaCreacion { get; set; }
    public string NumeroCelular { get; set; }

    // Columnas calculadas (computed columns en BD)
    public string IdCompleto { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;

}
