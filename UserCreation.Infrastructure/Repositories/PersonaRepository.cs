using Microsoft.EntityFrameworkCore;
using UserCreation.Application.Ports.Out;
using UserCreation.Domain.Entities;
using UserCreation.Infrastructure.Persistence;

namespace UserCreation.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio de Persona
/// </summary>
public class PersonaRepository : IPersonaRepository
{
    private readonly AppDbContext _context;

    public PersonaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Persona> InsertAsync(Persona persona, CancellationToken cancellationToken = default)
    {
        await _context.Personas.AddAsync(persona, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Recargar para obtener columnas calculadas
        await _context.Entry(persona).ReloadAsync(cancellationToken);
        
        return persona;
    }

    public async Task<IReadOnlyList<Persona>> GetCreadasAsync(
        DateTimeOffset? desde,
        DateTimeOffset? hasta,
        CancellationToken cancellationToken = default)
    {
        // Usar LINQ con Entity Framework (más limpio y type-safe)
        var query = _context.Personas
            .AsNoTracking()
            .Where(p => 
                (desde == null || p.FechaCreacion >= desde) &&
                (hasta == null || p.FechaCreacion <= hasta))
            .OrderByDescending(p => p.FechaCreacion);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Personas
            .AsNoTracking()
            .AnyAsync(p => p.Email == email, cancellationToken);
    }

    public async Task<bool> NumeroIdentificacionExistsAsync(
        string numeroIdentificacion,
        string tipoIdentificacion,
        CancellationToken cancellationToken = default)
    {
        return await _context.Personas
            .AsNoTracking()
            .AnyAsync(p => p.NumeroIdentificacion == numeroIdentificacion 
                          && p.TipoIdentificacion == tipoIdentificacion, cancellationToken);
    }
}
