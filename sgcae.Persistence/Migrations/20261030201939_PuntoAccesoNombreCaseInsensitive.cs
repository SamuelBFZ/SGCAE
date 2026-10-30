using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sgcae.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PuntoAccesoNombreCaseInsensitive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PuntosAcceso_Nombre",
                table: "PuntosAcceso");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PuntosAcceso",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.CreateIndex(
                name: "IX_PuntosAcceso_Nombre",
                table: "PuntosAcceso",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PuntosAcceso_Nombre",
                table: "PuntosAcceso");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PuntosAcceso",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.CreateIndex(
                name: "IX_PuntosAcceso_Nombre",
                table: "PuntosAcceso",
                column: "Nombre",
                unique: true);
        }
    }
}
