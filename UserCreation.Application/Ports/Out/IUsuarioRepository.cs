using UserCreation.Domain.Entities;

namespace UserCreation.Application.Ports.Out;

/// <summary>
/// Puerto de salida para operaciones con Usuario
/// </summary>
public interface IUsuarioRepository
{
    Task<Usuario> InsertAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = default);
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
