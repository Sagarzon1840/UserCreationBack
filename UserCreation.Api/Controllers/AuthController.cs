using Microsoft.AspNetCore.Mvc;
using UserCreation.Application.DTOs.Auth;
using UserCreation.Application.UseCases.Auth;

namespace UserCreation.Api.Controllers;

/// <summary>
/// Controlador para autenticación y login
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUseCase _loginUseCase;

    public AuthController(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    /// <summary>
    /// Endpoint de login que retorna JWT + GUID de sesión
    /// </summary>
    /// <param name="request">Credenciales del usuario</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Token JWT y datos de sesión</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _loginUseCase.ExecuteAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
