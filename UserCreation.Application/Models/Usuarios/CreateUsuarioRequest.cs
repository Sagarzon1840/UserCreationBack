using System.ComponentModel.DataAnnotations;

namespace UserCreation.Application.DTOs.Usuarios;

/// <summary>
/// DTO para crear un usuario
/// </summary>
public record CreateUsuarioRequest(
    [Required(ErrorMessage = "El usuario es obligatorio")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 100 caracteres")]
    string Usuario,
    
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    string Pass,
    
    Guid? PersonaId
);
