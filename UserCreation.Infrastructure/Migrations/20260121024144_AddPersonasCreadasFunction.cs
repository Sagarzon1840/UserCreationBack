using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCreation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonasCreadasFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION fn_personas_creadas(
                    p_desde TIMESTAMP WITH TIME ZONE DEFAULT NULL,
                    p_hasta TIMESTAMP WITH TIME ZONE DEFAULT NULL
                )
                RETURNS TABLE (
                    identificador UUID,
                    nombres VARCHAR(100),
                    apellidos VARCHAR(100),
                    numero_identificacion VARCHAR(50),
                    email VARCHAR(200),
                    tipo_identificacion VARCHAR(50),
                    fecha_creacion TIMESTAMP WITH TIME ZONE,
                    id_completo TEXT,
                    nombre_completo TEXT
                )
                LANGUAGE plpgsql
                AS $$
                BEGIN
                    RETURN QUERY
                    SELECT 
                        p.identificador,
                        p.nombres,
                        p.apellidos,
                        p.numero_identificacion,
                        p.email,
                        p.tipo_identificacion,
                        p.fecha_creacion,
                        p.id_completo,
                        p.nombre_completo
                    FROM personas p
                    WHERE 
                        (p_desde IS NULL OR p.fecha_creacion >= p_desde)
                        AND (p_hasta IS NULL OR p.fecha_creacion <= p_hasta)
                    ORDER BY p.fecha_creacion DESC;
                END;
                $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fn_personas_creadas(TIMESTAMP WITH TIME ZONE, TIMESTAMP WITH TIME ZONE);");
        }
    }
}
