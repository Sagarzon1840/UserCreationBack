using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserCreation.Domain.Entities;

namespace UserCreation.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de la entidad Persona en EF Core
/// </summary>
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("personas");

        builder.HasKey(p => p.Identificador);

        builder.Property(p => p.Identificador)
            .HasColumnName("identificador")
            .IsRequired();

        builder.Property(p => p.Nombres)
            .HasColumnName("nombres")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Apellidos)
            .HasColumnName("apellidos")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.NumeroIdentificacion)
            .HasColumnName("numero_identificacion")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.TipoIdentificacion)
            .HasColumnName("tipo_identificacion")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        // Columnas calculadas
        builder.Property(p => p.IdCompleto)
            .HasColumnName("id_completo")
            .HasComputedColumnSql("tipo_identificacion || '-' || numero_identificacion", stored: true);

        builder.Property(p => p.NombreCompleto)
            .HasColumnName("nombre_completo")
            .HasComputedColumnSql("nombres || ' ' || apellidos", stored: true);

        // Índices
        builder.HasIndex(p => p.Email)
            .IsUnique()
            .HasDatabaseName("ix_personas_email");

        builder.HasIndex(p => new { p.TipoIdentificacion, p.NumeroIdentificacion })
            .IsUnique()
            .HasDatabaseName("ix_personas_tipo_numero");
    }
}
