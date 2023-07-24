using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaguicVilla.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordenadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyaceso = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordenadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GpsTrasabilidads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GpsId = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Longitud = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsTrasabilidads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpsTrasabilidads_Coordenadas_GpsId",
                        column: x => x.GpsId,
                        principalTable: "Coordenadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 24, 14, 2, 58, 834, DateTimeKind.Local).AddTicks(2590), new DateTime(2023, 7, 24, 14, 2, 58, 834, DateTimeKind.Local).AddTicks(2603) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 24, 14, 2, 58, 834, DateTimeKind.Local).AddTicks(2605), new DateTime(2023, 7, 24, 14, 2, 58, 834, DateTimeKind.Local).AddTicks(2606) });

            migrationBuilder.CreateIndex(
                name: "IX_GpsTrasabilidads_GpsId",
                table: "GpsTrasabilidads",
                column: "GpsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GpsTrasabilidads");

            migrationBuilder.DropTable(
                name: "Coordenadas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4785), new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4797) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4800), new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4801) });
        }
    }
}
