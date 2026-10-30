using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sgcae.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Credenciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credenciales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmpleadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credenciales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credenciales_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credenciales_Codigo",
                table: "Credenciales",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credenciales_EmpleadoId",
                table: "Credenciales",
                column: "EmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credenciales");
        }
    }
}
