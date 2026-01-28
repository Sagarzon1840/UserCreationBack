using UserCreation.Application.DTOs.Personas;
using UserCreation.Application.Ports.Out;
using UserCreation.Domain.Entities;

namespace UserCreation.Application.UseCases.Personas;

/// <summary>
/// Caso de uso para crear una persona
/// </summary>
public class CreatePersonaUseCase
{
    private readonly IPersonaRepository _personaRepository;

    public CreatePersonaUseCase(IPersonaRepository personaRepository)
    {
        _personaRepository = personaRepository;
    }

    public async Task<PersonaResponse> ExecuteAsync(CreatePersonaRequest request, CancellationToken cancellationToken = default)
    {
        // Validar email único
        if (await _personaRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            throw new InvalidOperationException($"El email {request.Email} ya está registrado.");
        }

        // Validar numero de identificacion unico
        if (await _personaRepository.NumeroIdentificacionExistsAsync(request.NumeroIdentificacion, request.TipoIdentificacion, cancellationToken))
        {
            throw new InvalidOperationException($"El numero de identificacion {request.NumeroIdentificacion} de tipo {request.TipoIdentificacion} ya esta registrado.");
        }

        var persona = new Persona
        {
            Identificador = Guid.NewGuid(),
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            NumeroIdentificacion = request.NumeroIdentificacion,
            Email = request.Email,
            TipoIdentificacion = request.TipoIdentificacion,
            FechaCreacion = DateTimeOffset.UtcNow,
            NumeroCelular = request.NumeroCelular
        };

        var createdPersona = await _personaRepository.InsertAsync(persona, cancellationToken);

        return new PersonaResponse(
            createdPersona.Identificador,
            createdPersona.Nombres,
            createdPersona.Apellidos,
            createdPersona.NumeroIdentificacion,
            createdPersona.Email,
            createdPersona.TipoIdentificacion,
            createdPersona.FechaCreacion,
            createdPersona.IdCompleto,
            createdPersona.NombreCompleto,
            createdPersona.NumeroCelular
        );
    }
}
