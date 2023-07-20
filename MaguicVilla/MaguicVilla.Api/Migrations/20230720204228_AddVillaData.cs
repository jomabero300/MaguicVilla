using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaguicVilla.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenida", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "detalle de la villa", new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4785), new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4797), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "detalle de la vista a la piscina", new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4800), new DateTime(2023, 7, 20, 15, 42, 28, 495, DateTimeKind.Local).AddTicks(4801), "", 40, "Premio Vista a la piscina", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Detalle",
                table: "Villas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
