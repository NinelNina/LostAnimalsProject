using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class fix_notes_configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_photo_gallery_PhotoGalleryID",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_photo_gallery_PhotoGalleryID",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_comments_ParentCommentId",
                table: "comments");

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

            migrationBuilder.RenameColumn(
                name: "ParentCommentId",
                table: "comments",
                newName: "ParentCommentID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 15, 21, 21, 55, 714, DateTimeKind.Local).AddTicks(196),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 28, 15, 21, 25, 311, DateTimeKind.Local).AddTicks(1782));

            migrationBuilder.CreateIndex(
                name: "IX_comments_ParentCommentID",
                table: "comments",
                column: "ParentCommentID",
                unique: true,
                filter: "[ParentCommentID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentID",
                table: "comments",
                column: "ParentCommentID",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_photo_gallery_PhotoGalleryID",
                table: "comments",
                column: "PhotoGalleryID",
                principalTable: "photo_gallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_photo_gallery_PhotoGalleryID",
                table: "notes",
                column: "PhotoGalleryID",
                principalTable: "photo_gallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentID",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_photo_gallery_PhotoGalleryID",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_photo_gallery_PhotoGalleryID",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_comments_ParentCommentID",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "photo_storage",
                newName: "PhotoFullName");

            migrationBuilder.RenameColumn(
                name: "ParentCommentID",
                table: "comments",
                newName: "ParentCommentId");

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
                defaultValue: new DateTime(2024, 3, 28, 15, 21, 25, 311, DateTimeKind.Local).AddTicks(1782),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 15, 21, 21, 55, 714, DateTimeKind.Local).AddTicks(196));

            migrationBuilder.CreateIndex(
                name: "IX_comments_ParentCommentId",
                table: "comments",
                column: "ParentCommentId",
                unique: true,
                filter: "[ParentCommentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                table: "comments",
                column: "ParentCommentId",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_photo_gallery_PhotoGalleryID",
                table: "comments",
                column: "PhotoGalleryID",
                principalTable: "photo_gallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_photo_gallery_PhotoGalleryID",
                table: "notes",
                column: "PhotoGalleryID",
                principalTable: "photo_gallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
