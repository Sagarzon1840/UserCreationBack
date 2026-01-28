using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCreation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroCelularColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "numero_celular",
                table: "personas",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero_celular",
                table: "personas");
        }
    }
}
