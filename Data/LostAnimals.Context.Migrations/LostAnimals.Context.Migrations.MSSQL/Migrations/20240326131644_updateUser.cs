using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 26, 16, 16, 43, 775, DateTimeKind.Local).AddTicks(1213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 27, 22, 36, 37, 808, DateTimeKind.Local).AddTicks(7096));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "notes");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 27, 22, 36, 37, 808, DateTimeKind.Local).AddTicks(7096),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 26, 16, 16, 43, 775, DateTimeKind.Local).AddTicks(1213));
        }
    }
}
