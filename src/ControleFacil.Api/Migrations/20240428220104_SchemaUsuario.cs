using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class SchemaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "usuario");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "usuario",
                newSchema: "usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "usuario",
                schema: "usuario",
                newName: "usuario");
        }
    }
}
