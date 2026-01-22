using UserCreation.Application.DTOs.Auth;
using UserCreation.Application.Ports.Out;

namespace UserCreation.Application.UseCases.Auth;

/// <summary>
/// Caso de uso para login de usuario
/// </summary>
public class LoginUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUseCase(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        // Buscar usuario por username
        var usuario = await _usuarioRepository.GetByUsernameAsync(request.Usuario, cancellationToken);

        if (usuario == null)
        {
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
        }

        // Verificar contraseña
        if (!_passwordHasher.Verify(request.Pass, usuario.PassHash))
        {
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
        }

        // Generar sessionId
        var sessionId = Guid.NewGuid();

        // Generar JWT
        var token = _jwtTokenService.GenerateToken(usuario, sessionId);
        var expiresAt = _jwtTokenService.GetTokenExpiration();

        return new LoginResponse(
            token,
            expiresAt,
            sessionId,
            usuario.Identificador,
            usuario.NombreUsuario
        );
    }
}
