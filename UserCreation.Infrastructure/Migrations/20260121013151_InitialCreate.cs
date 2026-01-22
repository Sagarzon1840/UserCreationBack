using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCreation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    identificador = table.Column<Guid>(type: "uuid", nullable: false),
                    nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    numero_identificacion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    tipo_identificacion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha_creacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    id_completo = table.Column<string>(type: "text", nullable: false, computedColumnSql: "tipo_identificacion || '-' || numero_identificacion", stored: true),
                    nombre_completo = table.Column<string>(type: "text", nullable: false, computedColumnSql: "nombres || ' ' || apellidos", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.identificador);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    identificador = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_usuario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pass_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    fecha_creacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    persona_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.identificador);
                    table.ForeignKey(
                        name: "FK_usuarios_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_personas_email",
                table: "personas",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_personas_tipo_numero",
                table: "personas",
                columns: new[] { "tipo_identificacion", "numero_identificacion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_nombre_usuario",
                table: "usuarios",
                column: "nombre_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_persona_id",
                table: "usuarios",
                column: "persona_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "personas");
        }
    }
}
