namespace UserCreation.Domain.Entities;

/// <summary>
/// Entidad Usuario del dominio
/// </summary>
public class Usuario
{
    public Guid Identificador { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string PassHash { get; set; } = string.Empty;
    public DateTimeOffset FechaCreacion { get; set; }
    public Guid? PersonaId { get; set; }

    // Relación con Persona (opcional)
    public Persona? Persona { get; set; }
}
