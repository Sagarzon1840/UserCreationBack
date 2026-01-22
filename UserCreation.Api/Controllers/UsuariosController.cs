using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserCreation.Application.DTOs.Usuarios;
using UserCreation.Application.UseCases.Usuarios;

namespace UserCreation.Api.Controllers;

/// <summary>
/// Controlador para operaciones con Usuarios
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly CreateUsuarioUseCase _createUsuarioUseCase;

    public UsuariosController(CreateUsuarioUseCase createUsuarioUseCase)
    {
        _createUsuarioUseCase = createUsuarioUseCase;
    }

    /// <summary>
    /// Crea un nuevo usuario (requiere JWT)
    /// </summary>
    /// <param name="request">Datos del usuario</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Usuario creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UsuarioResponse>> CreateUsuario(
        [FromBody] CreateUsuarioRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _createUsuarioUseCase.ExecuteAsync(request, cancellationToken);
            return CreatedAtAction(nameof(CreateUsuario), new { id = response.Identificador }, response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
