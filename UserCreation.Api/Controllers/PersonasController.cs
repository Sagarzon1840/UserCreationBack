using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserCreation.Application.DTOs.Personas;
using UserCreation.Application.UseCases.Personas;

namespace UserCreation.Api.Controllers;

/// <summary>
/// Controlador para operaciones con Personas
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly CreatePersonaUseCase _createPersonaUseCase;
    private readonly GetPersonasCreadasUseCase _getPersonasCreadasUseCase;

    public PersonasController(
        CreatePersonaUseCase createPersonaUseCase,
        GetPersonasCreadasUseCase getPersonasCreadasUseCase)
    {
        _createPersonaUseCase = createPersonaUseCase;
        _getPersonasCreadasUseCase = getPersonasCreadasUseCase;
    }

    /// <summary>
    /// Crea una nueva persona (requiere JWT)
    /// </summary>
    /// <param name="request">Datos de la persona</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Persona creada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PersonaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PersonaResponse>> CreatePersona(
        [FromBody] CreatePersonaRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _createPersonaUseCase.ExecuteAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetPersonasCreadas), new { }, response);
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

    /// <summary>
    /// Consulta personas creadas en un rango de fechas (requiere JWT)
    /// </summary>
    /// <param name="desde">Fecha desde (opcional)</param>
    /// <param name="hasta">Fecha hasta (opcional)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de personas creadas</returns>
    [HttpGet("creadas")]
    [ProducesResponseType(typeof(IReadOnlyList<PersonaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IReadOnlyList<PersonaResponse>>> GetPersonasCreadas(
        [FromQuery] DateTimeOffset? desde,
        [FromQuery] DateTimeOffset? hasta,
        CancellationToken cancellationToken)
    {
        var query = new GetPersonasCreadasQuery(desde, hasta);
        var response = await _getPersonasCreadasUseCase.ExecuteAsync(query, cancellationToken);
        return Ok(response);
    }
}
