using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sgcae.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialEmpleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EstadoLaboral = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Identificacion",
                table: "Empleados",
                column: "Identificacion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");
        }
    }
}
