using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCreation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Crear usuario administrador
            // Usuario: admin
            // Password: Admin123!
            // Hash generado con BCrypt work factor 12
            migrationBuilder.Sql(@"
                INSERT INTO usuarios (identificador, nombre_usuario, pass_hash, fecha_creacion, persona_id)
                VALUES (
                    '00000000-0000-0000-0000-000000000001'::uuid,
                    'admin',
                    '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5GyYIgaT6Z6Wm',
                    NOW(),
                    NULL
                )
                ON CONFLICT (nombre_usuario) DO NOTHING;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM usuarios 
                WHERE nombre_usuario = 'admin' 
                AND identificador = '00000000-0000-0000-0000-000000000001'::uuid;
            ");
        }
    }
}
