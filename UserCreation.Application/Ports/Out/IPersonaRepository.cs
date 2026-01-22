using UserCreation.Domain.Entities;

namespace UserCreation.Application.Ports.Out;

/// <summary>
/// Puerto de salida para operaciones con Persona
/// </summary>
public interface IPersonaRepository
{
    Task<Persona> InsertAsync(Persona persona, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Persona>> GetCreadasAsync(DateTimeOffset? desde, DateTimeOffset? hasta, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> NumeroIdentificacionExistsAsync(string numeroIdentificacion, string tipoIdentificacion, CancellationToken cancellationToken = default);
}
