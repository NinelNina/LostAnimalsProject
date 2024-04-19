using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class fix_notes_configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_photo_gallery_PhotoGalleryID",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_photo_gallery_PhotoGalleryID",
                table: "notes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 15, 21, 21, 22, 174, DateTimeKind.Local).AddTicks(837),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 4, 14, 12, 13, 51, 392, DateTimeKind.Local).AddTicks(1774));

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
                name: "FK_comments_photo_gallery_PhotoGalleryID",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_photo_gallery_PhotoGalleryID",
                table: "notes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 14, 12, 13, 51, 392, DateTimeKind.Local).AddTicks(1774),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 4, 15, 21, 21, 22, 174, DateTimeKind.Local).AddTicks(837));

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
