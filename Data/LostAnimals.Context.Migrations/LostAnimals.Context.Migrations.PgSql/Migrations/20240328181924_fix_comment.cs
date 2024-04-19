using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fix_comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "ParentCommentId",
                table: "comments",
                newName: "ParentCommentID");

            migrationBuilder.RenameIndex(
                name: "IX_comments_ParentCommentId",
                table: "comments",
                newName: "IX_comments_ParentCommentID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 28, 21, 19, 23, 504, DateTimeKind.Local).AddTicks(6881),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 28, 15, 19, 52, 142, DateTimeKind.Local).AddTicks(7163));

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentID",
                table: "comments",
                column: "ParentCommentID",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentID",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "ParentCommentID",
                table: "comments",
                newName: "ParentCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_ParentCommentID",
                table: "comments",
                newName: "IX_comments_ParentCommentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 28, 15, 19, 52, 142, DateTimeKind.Local).AddTicks(7163),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 28, 21, 19, 23, 504, DateTimeKind.Local).AddTicks(6881));

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                table: "comments",
                column: "ParentCommentId",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
