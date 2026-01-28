using System.ComponentModel.DataAnnotations;

namespace UserCreation.Application.DTOs.Personas;

/// <summary>
/// DTO para crear una persona
/// </summary>
public record CreatePersonaRequest(
    [Required(ErrorMessage = "Los nombres son obligatorios")]
    [StringLength(100, ErrorMessage = "Los nombres no pueden exceder 100 caracteres")]
    string Nombres,

    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder 100 caracteres")]
    string Apellidos,

    [Required(ErrorMessage = "El número de identificación es obligatorio")]
    [StringLength(50, ErrorMessage = "El número de identificación no puede exceder 50 caracteres")]
    string NumeroIdentificacion,

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(200, ErrorMessage = "El email no puede exceder 200 caracteres")]
    string Email,

    [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
    [StringLength(50, ErrorMessage = "El tipo de identificación no puede exceder 50 caracteres")]
    string TipoIdentificacion,

    [Required(ErrorMessage = "El número de celular es obligatorio")]
    [StringLength(16, ErrorMessage = "El número de celular no puede exceder 16 caracteres")]
    string NumeroCelular
);
