using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class fix_notes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 26, 19, 15, 19, 723, DateTimeKind.Local).AddTicks(5553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 22, 20, 42, 38, 319, DateTimeKind.Local).AddTicks(1831));

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_UserID",
                table: "notes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserID",
                table: "comments",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_User_UserID",
                table: "comments",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_User_UserID",
                table: "notes",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_User_UserID",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_User_UserID",
                table: "notes");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_notes_UserID",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_comments_UserID",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "comments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 22, 20, 42, 38, 319, DateTimeKind.Local).AddTicks(1831),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 26, 19, 15, 19, 723, DateTimeKind.Local).AddTicks(5553));
        }
    }
}
