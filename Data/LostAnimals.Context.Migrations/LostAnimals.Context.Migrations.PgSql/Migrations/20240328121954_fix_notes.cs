using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fix_notes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSeenDate",
                table: "notes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEditDate",
                table: "notes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PhotoGalleryID",
                table: "comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 28, 15, 19, 52, 142, DateTimeKind.Local).AddTicks(7163),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 26, 16, 15, 52, 242, DateTimeKind.Local).AddTicks(9360));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastSeenDate",
                table: "notes",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastEditDate",
                table: "notes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PhotoGalleryID",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 26, 16, 15, 52, 242, DateTimeKind.Local).AddTicks(9360),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 28, 15, 19, 52, 142, DateTimeKind.Local).AddTicks(7163));
        }
    }
}
