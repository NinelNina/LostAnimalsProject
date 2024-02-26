﻿// <auto-generated />
using System;
using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LostAnimals.Context.Migrations.PgSql.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240226200233_fix_notes2")]
    partial class fix_notes2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LostAnimals.Context.Entities.AnimalKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AnimalKindName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("animal_kinds", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalKindID")
                        .HasColumnType("integer");

                    b.Property<string>("BreedName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AnimalKindID");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 2, 26, 23, 2, 32, 435, DateTimeKind.Local).AddTicks(905));

                    b.Property<int>("NoteID")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("integer");

                    b.Property<int>("PhotoGalleryID")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NoteID");

                    b.HasIndex("ParentCommentId")
                        .IsUnique();

                    b.HasIndex("PhotoGalleryID");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AnimalName")
                        .HasColumnType("text");

                    b.Property<int?>("BreedID")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateOnly?>("LastEditDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("LastSeenDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longtitude")
                        .HasColumnType("double precision");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PhotoGalleryID")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BreedID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("PhotoGalleryID");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("notes", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.NoteCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("note_categories", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.PhotoGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("photo_gallery", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.PhotoStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PhotoFullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhotoGalleryID")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PhotoGalleryID");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("photo_storage", (string)null);
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Breed", b =>
                {
                    b.HasOne("LostAnimals.Context.Entities.AnimalKind", "AnimalKind")
                        .WithMany("Breeds")
                        .HasForeignKey("AnimalKindID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalKind");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Comment", b =>
                {
                    b.HasOne("LostAnimals.Context.Entities.Note", "Note")
                        .WithMany("Comments")
                        .HasForeignKey("NoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LostAnimals.Context.Entities.Comment", "ParentComment")
                        .WithOne()
                        .HasForeignKey("LostAnimals.Context.Entities.Comment", "ParentCommentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LostAnimals.Context.Entities.PhotoGallery", "PhotoGallery")
                        .WithMany("Comments")
                        .HasForeignKey("PhotoGalleryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LostAnimals.Context.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("ParentComment");

                    b.Navigation("PhotoGallery");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Note", b =>
                {
                    b.HasOne("LostAnimals.Context.Entities.Breed", "Breed")
                        .WithMany("Notes")
                        .HasForeignKey("BreedID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LostAnimals.Context.Entities.NoteCategory", "Category")
                        .WithMany("Notes")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LostAnimals.Context.Entities.PhotoGallery", "PhotoGallery")
                        .WithMany("Notes")
                        .HasForeignKey("PhotoGalleryID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LostAnimals.Context.Entities.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Category");

                    b.Navigation("PhotoGallery");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.PhotoStorage", b =>
                {
                    b.HasOne("LostAnimals.Context.Entities.PhotoGallery", "PhotoGallery")
                        .WithMany("PhotoStorages")
                        .HasForeignKey("PhotoGalleryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhotoGallery");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.AnimalKind", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Breed", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.Note", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.NoteCategory", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.PhotoGallery", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Notes");

                    b.Navigation("PhotoStorages");
                });

            modelBuilder.Entity("LostAnimals.Context.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
