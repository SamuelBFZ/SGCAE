using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sgcae.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MovimientosAcceso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PuntosAcceso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntosAcceso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosAcceso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoCredencial = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CredencialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmpleadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PuntoAccesoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PuntoAccesoSolicitado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    OcurridoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosAcceso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosAcceso_Credenciales_CredencialId",
                        column: x => x.CredencialId,
                        principalTable: "Credenciales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimientosAcceso_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimientosAcceso_PuntosAcceso_PuntoAccesoId",
                        column: x => x.PuntoAccesoId,
                        principalTable: "PuntosAcceso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAcceso_CredencialId",
                table: "MovimientosAcceso",
                column: "CredencialId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAcceso_EmpleadoId_OcurridoEn",
                table: "MovimientosAcceso",
                columns: new[] { "EmpleadoId", "OcurridoEn" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAcceso_OcurridoEn",
                table: "MovimientosAcceso",
                column: "OcurridoEn");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAcceso_PuntoAccesoId",
                table: "MovimientosAcceso",
                column: "PuntoAccesoId");

            migrationBuilder.CreateIndex(
                name: "IX_PuntosAcceso_Nombre",
                table: "PuntosAcceso",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosAcceso");

            migrationBuilder.DropTable(
                name: "PuntosAcceso");
        }
    }
}
