using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserCreation.Domain.Entities;

namespace UserCreation.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de la entidad Usuario en EF Core
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.Identificador);

        builder.Property(u => u.Identificador)
            .HasColumnName("identificador")
            .IsRequired();

        builder.Property(u => u.NombreUsuario)
            .HasColumnName("nombre_usuario")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.PassHash)
            .HasColumnName("pass_hash")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(u => u.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(u => u.PersonaId)
            .HasColumnName("persona_id")
            .IsRequired(false);

        // Relación con Persona (opcional)
        builder.HasOne(u => u.Persona)
            .WithMany()
            .HasForeignKey(u => u.PersonaId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        // Índices
        builder.HasIndex(u => u.NombreUsuario)
            .IsUnique()
            .HasDatabaseName("ix_usuarios_nombre_usuario");
    }
}
