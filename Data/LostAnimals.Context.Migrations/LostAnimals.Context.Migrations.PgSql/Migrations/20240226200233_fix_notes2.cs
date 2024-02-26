using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fix_notes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comment_attributes_AttributeID",
                table: "comments");

            migrationBuilder.DropTable(
                name: "comment_attributes");

            migrationBuilder.DropIndex(
                name: "IX_comments_AttributeID",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "AttributeID",
                table: "comments");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "notes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastEditDate",
                table: "notes",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 26, 23, 2, 32, 435, DateTimeKind.Local).AddTicks(905),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 26, 19, 15, 1, 613, DateTimeKind.Local).AddTicks(9086));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "notes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 26, 19, 15, 1, 613, DateTimeKind.Local).AddTicks(9086),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 26, 23, 2, 32, 435, DateTimeKind.Local).AddTicks(905));

            migrationBuilder.AddColumn<int>(
                name: "AttributeID",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "comment_attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttributeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment_attributes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_AttributeID",
                table: "comments",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_comment_attributes_Uid",
                table: "comment_attributes",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comment_attributes_AttributeID",
                table: "comments",
                column: "AttributeID",
                principalTable: "comment_attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
