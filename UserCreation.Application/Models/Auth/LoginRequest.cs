using System.ComponentModel.DataAnnotations;

namespace UserCreation.Application.DTOs.Auth;

/// <summary>
/// DTO para solicitar login
/// </summary>
public record LoginRequest(
    [Required(ErrorMessage = "El usuario es obligatorio")]
    string Usuario,

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    string Pass
);
