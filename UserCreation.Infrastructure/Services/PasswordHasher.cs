using UserCreation.Application.Ports.Out;

namespace UserCreation.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de hash de contraseñas usando BCrypt
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    public bool Verify(string password, string hash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch
        {
            return false;
        }
    }
}
