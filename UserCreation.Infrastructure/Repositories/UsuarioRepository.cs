using Microsoft.EntityFrameworkCore;
using UserCreation.Application.Ports.Out;
using UserCreation.Domain.Entities;
using UserCreation.Infrastructure.Persistence;

namespace UserCreation.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio de Usuario
/// </summary>
public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> InsertAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        await _context.Usuarios.AddAsync(usuario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return usuario;
    }

    public async Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.NombreUsuario == username, cancellationToken);
    }

    public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .AnyAsync(u => u.NombreUsuario == username, cancellationToken);
    }

    public async Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Identificador == id, cancellationToken);
    }
}
