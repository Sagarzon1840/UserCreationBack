namespace UserCreation.Application.Ports.Out;

/// <summary>
/// Puerto de salida para operaciones de hash de contraseñas
/// </summary>
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
