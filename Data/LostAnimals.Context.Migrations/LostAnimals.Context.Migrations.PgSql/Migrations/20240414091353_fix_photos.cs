using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fix_photos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "notes");

            migrationBuilder.RenameColumn(
                name: "PhotoFullName",
                table: "photo_storage",
                newName: "PhotoName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 14, 12, 13, 51, 392, DateTimeKind.Local).AddTicks(1774),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 28, 21, 19, 23, 504, DateTimeKind.Local).AddTicks(6881));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "photo_storage",
                newName: "PhotoFullName");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 28, 21, 19, 23, 504, DateTimeKind.Local).AddTicks(6881),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 4, 14, 12, 13, 51, 392, DateTimeKind.Local).AddTicks(1774));
        }
    }
}
