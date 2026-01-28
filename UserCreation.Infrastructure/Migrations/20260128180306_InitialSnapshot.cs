using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCreation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Las tablas ya existen en la base de datos
            // Esta migración solo registra el estado actual
            // No se ejecuta ningún comando SQL
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No hacer nada en el rollback
        }
    }
}
