using System;
using Microsoft.EntityFrameworkCore;
using Pets.DB.Migrations.Entities;

namespace Pets.DB.Migrations
{
    public class MigrateDbContext : DbContext
    {
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Pet
            modelBuilder.Entity<Pet>(builder =>
            {
                builder.ToTable("Pet");
                builder.HasKey(_ => _.Id);
                builder.Property(_ => _.Id)
                    .ValueGeneratedNever();

                builder.HasIndex(_ => _.PetState);

                builder.Property(_ => _.PetState)
                    .HasConversion<String>()
                    .HasMaxLength(64)
                    .IsRequired();
                builder.Property(_ => _.Type)
                    .HasConversion<String>()
                    .HasMaxLength(64)
                    .IsRequired();

                builder.Property(_ => _.Name)
                    .HasMaxLength(512)
                    .IsRequired();
                builder.Property(_ => _.AfterPhotoLink)
                    .HasMaxLength(512)
                    .IsRequired(false);
                builder.Property(_ => _.BeforePhotoLink)
                    .HasMaxLength(512)
                    .IsRequired(false);
                builder.Property(_ => _.MdBody)
                    .HasMaxLength(10240)
                    .IsRequired(false);

                builder.Property(_ => _.UpdateDate)
                    .IsRequired();
                builder.Property(_ => _.CreateDate)
                    .IsRequired();

                builder.Property(_ => _.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();
            });
            #endregion
        }
    }
}