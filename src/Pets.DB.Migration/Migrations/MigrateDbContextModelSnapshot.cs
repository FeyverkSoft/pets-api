﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pets.DB.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    [DbContext(typeof(MigrateDbContext))]
    partial class MigrateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Need", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImgLinks")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(2048);

                    b.Property<string>("MdBody")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(2048);

                    b.Property<string>("NeedState")
                        .IsRequired()
                        .HasColumnType("varchar(16) CHARACTER SET utf8mb4")
                        .HasMaxLength(16);

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Need");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.News", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImgLink")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("MdBody")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(10240);

                    b.Property<string>("MdShortBody")
                        .IsRequired()
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id", "OrganisationId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("OrganisationId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.NewsPets", b =>
                {
                    b.Property<Guid>("NewsId")
                        .HasColumnName("NewsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PetId")
                        .HasColumnName("PetId")
                        .HasColumnType("char(36)");

                    b.HasKey("NewsId", "PetId");

                    b.HasIndex("PetId");

                    b.HasIndex("NewsId", "PetId")
                        .IsUnique();

                    b.ToTable("NewsPets");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Organisation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Organisation");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.OrganisationContact", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImgLink")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("MdBody")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(2048);

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("OrganisationContact");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Page", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImgLink")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("MdBody")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(10240);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id", "OrganisationId");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("AfterPhotoLink")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("BeforePhotoLink")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MdBody")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(10240);

                    b.Property<string>("MdShortBody")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PetState")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("PetState");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Resource", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<Guid>("ConcurrencyTokens")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImgLink")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                        .HasMaxLength(512);

                    b.Property<string>("MdBody")
                        .HasColumnType("mediumtext CHARACTER SET utf8mb4")
                        .HasMaxLength(2048);

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("Title")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Need", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.News", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.NewsPets", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.News", "News")
                        .WithMany("NewsPets")
                        .HasForeignKey("NewsId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pets.DB.Migrations.Entities.Pet", "Pet")
                        .WithMany("PetNews")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.OrganisationContact", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Page", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Pet", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pets.DB.Migrations.Entities.Resource", b =>
                {
                    b.HasOne("Pets.DB.Migrations.Entities.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
