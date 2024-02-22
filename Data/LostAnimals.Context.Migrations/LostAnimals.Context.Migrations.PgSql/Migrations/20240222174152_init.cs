using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "animal_kinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnimalKindName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal_kinds", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "note_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "photo_gallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photo_gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BreedName = table.Column<string>(type: "text", nullable: false),
                    AnimalKindID = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_breeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_breeds_animal_kinds_AnimalKindID",
                        column: x => x.AnimalKindID,
                        principalTable: "animal_kinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "photo_storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoFullName = table.Column<string>(type: "text", nullable: false),
                    PhotoGalleryID = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photo_storage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photo_storage_photo_gallery_PhotoGalleryID",
                        column: x => x.PhotoGalleryID,
                        principalTable: "photo_gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryID = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    AnimalName = table.Column<string>(type: "text", nullable: true),
                    BreedID = table.Column<int>(type: "integer", nullable: false),
                    PhotoGalleryID = table.Column<int>(type: "integer", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longtitude = table.Column<double>(type: "double precision", nullable: true),
                    LastSeenDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notes_breeds_BreedID",
                        column: x => x.BreedID,
                        principalTable: "breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notes_note_categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "note_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notes_photo_gallery_PhotoGalleryID",
                        column: x => x.PhotoGalleryID,
                        principalTable: "photo_gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NoteID = table.Column<int>(type: "integer", nullable: false),
                    AttributeID = table.Column<int>(type: "integer", nullable: false),
                    ParentCommentId = table.Column<int>(type: "integer", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 2, 22, 20, 41, 52, 546, DateTimeKind.Local).AddTicks(731)),
                    PhotoGalleryID = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_comment_attributes_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "comment_attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comments_comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comments_notes_NoteID",
                        column: x => x.NoteID,
                        principalTable: "notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_photo_gallery_PhotoGalleryID",
                        column: x => x.PhotoGalleryID,
                        principalTable: "photo_gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_animal_kinds_Uid",
                table: "animal_kinds",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_breeds_AnimalKindID",
                table: "breeds",
                column: "AnimalKindID");

            migrationBuilder.CreateIndex(
                name: "IX_breeds_Uid",
                table: "breeds",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comment_attributes_Uid",
                table: "comment_attributes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_AttributeID",
                table: "comments",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_NoteID",
                table: "comments",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_ParentCommentId",
                table: "comments",
                column: "ParentCommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_PhotoGalleryID",
                table: "comments",
                column: "PhotoGalleryID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_Uid",
                table: "comments",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_note_categories_Uid",
                table: "note_categories",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notes_BreedID",
                table: "notes",
                column: "BreedID");

            migrationBuilder.CreateIndex(
                name: "IX_notes_CategoryID",
                table: "notes",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_notes_PhotoGalleryID",
                table: "notes",
                column: "PhotoGalleryID");

            migrationBuilder.CreateIndex(
                name: "IX_notes_Uid",
                table: "notes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_photo_gallery_Uid",
                table: "photo_gallery",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_photo_storage_PhotoGalleryID",
                table: "photo_storage",
                column: "PhotoGalleryID");

            migrationBuilder.CreateIndex(
                name: "IX_photo_storage_Uid",
                table: "photo_storage",
                column: "Uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "photo_storage");

            migrationBuilder.DropTable(
                name: "comment_attributes");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "note_categories");

            migrationBuilder.DropTable(
                name: "photo_gallery");

            migrationBuilder.DropTable(
                name: "animal_kinds");
        }
    }
}
