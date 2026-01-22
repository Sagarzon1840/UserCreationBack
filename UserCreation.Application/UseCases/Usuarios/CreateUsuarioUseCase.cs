using UserCreation.Application.DTOs.Usuarios;
using UserCreation.Application.Ports.Out;
using UserCreation.Domain.Entities;

namespace UserCreation.Application.UseCases.Usuarios;

/// <summary>
/// Caso de uso para crear un usuario
/// </summary>
public class CreateUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUsuarioUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioResponse> ExecuteAsync(CreateUsuarioRequest request, CancellationToken cancellationToken = default)
    {
        // Validar username único
        if (await _usuarioRepository.UsernameExistsAsync(request.Usuario, cancellationToken))
        {
            throw new InvalidOperationException($"El usuario {request.Usuario} ya está registrado.");
        }

        // Hash de contraseña
        var passHash = _passwordHasher.Hash(request.Pass);

        var usuario = new Usuario
        {
            Identificador = Guid.NewGuid(),
            NombreUsuario = request.Usuario,
            PassHash = passHash,
            FechaCreacion = DateTimeOffset.UtcNow,
            PersonaId = request.PersonaId
        };

        var createdUsuario = await _usuarioRepository.InsertAsync(usuario, cancellationToken);

        return new UsuarioResponse(
            createdUsuario.Identificador,
            createdUsuario.NombreUsuario,
            createdUsuario.FechaCreacion,
            createdUsuario.PersonaId
        );
    }
}
