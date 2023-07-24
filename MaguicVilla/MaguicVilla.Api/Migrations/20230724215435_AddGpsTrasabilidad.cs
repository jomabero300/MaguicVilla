using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaguicVilla.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGpsTrasabilidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GpsTrasabilidads_Coordenadas_GpsId",
                table: "GpsTrasabilidads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coordenadas",
                table: "Coordenadas");

            migrationBuilder.RenameTable(
                name: "Coordenadas",
                newName: "Gps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gps",
                table: "Gps",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 24, 16, 54, 35, 650, DateTimeKind.Local).AddTicks(4242), new DateTime(2023, 7, 24, 16, 54, 35, 650, DateTimeKind.Local).AddTicks(4252) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 24, 16, 54, 35, 650, DateTimeKind.Local).AddTicks(4254), new DateTime(2023, 7, 24, 16, 54, 35, 650, DateTimeKind.Local).AddTicks(4255) });

            migrationBuilder.AddForeignKey(
                name: "FK_GpsTrasabilidads_Gps_GpsId",
                table: "GpsTrasabilidads",
                column: "GpsId",
                principalTable: "Gps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GpsTrasabilidads_Gps_GpsId",
                table: "GpsTrasabilidads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gps",
                table: "Gps");

            migrationBuilder.RenameTable(
                name: "Gps",
                newName: "Coordenadas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coordenadas",
                table: "Coordenadas",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GpsTrasabilidads_Coordenadas_GpsId",
                table: "GpsTrasabilidads",
                column: "GpsId",
                principalTable: "Coordenadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
