using UserCreation.Application.DTOs.Personas;
using UserCreation.Application.Ports.Out;

namespace UserCreation.Application.UseCases.Personas;

/// <summary>
/// Caso de uso para obtener personas creadas en un rango de fechas
/// </summary>
public class GetPersonasCreadasUseCase
{
    private readonly IPersonaRepository _personaRepository;

    public GetPersonasCreadasUseCase(IPersonaRepository personaRepository)
    {
        _personaRepository = personaRepository;
    }

    public async Task<IReadOnlyList<PersonaResponse>> ExecuteAsync(GetPersonasCreadasQuery query, CancellationToken cancellationToken = default)
    {
        var personas = await _personaRepository.GetCreadasAsync(query.Desde, query.Hasta, cancellationToken);

        return personas.Select(p => new PersonaResponse(
            p.Identificador,
            p.Nombres,
            p.Apellidos,
            p.NumeroIdentificacion,
            p.Email,
            p.TipoIdentificacion,
            p.FechaCreacion,
            p.IdCompleto,
            p.NombreCompleto
        )).ToList();
    }
}
