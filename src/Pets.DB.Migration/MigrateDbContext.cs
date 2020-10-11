using System;

using Microsoft.EntityFrameworkCore;

using Pets.DB.Migrations.Entities;

namespace Pets.DB.Migrations
{
    public class MigrateDbContext : DbContext
    {
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Organisation

            modelBuilder.Entity<Organisation>(builder =>
            {
                builder.ToTable("Organisation");
                builder.HasKey(_ => _.Id);
                builder.Property(_ => _.Id)
                    .ValueGeneratedNever();
            });

            #endregion

            #region OrganisationContact

            modelBuilder.Entity<OrganisationContact>(builder =>
            {
                builder.ToTable("OrganisationContact");
                builder.HasKey(_ => _.Id);
                builder.Property(_ => _.Id)
                    .ValueGeneratedNever();

                builder.Property(_ => _.Type)
                    .HasConversion<String>()
                    .HasMaxLength(64)
                    .IsRequired();
                builder.Property(_ => _.MdBody)
                    .HasMaxLength(2048)
                    .IsRequired(false);
                builder.Property(_ => _.ImgLink)
                    .HasMaxLength(512)
                    .IsRequired(false);

                builder.HasOne(_ => _.Organisation)
                    .WithMany()
                    .HasForeignKey(_ => _.OrganisationId)
                    .HasPrincipalKey(_ => _.Id);

                builder.Property(_ => _.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();
            });

            #endregion

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

                builder.Property(_ => _.OrganisationId)
                    .IsRequired();
                builder.HasOne(_ => _.Organisation)
                    .WithMany()
                    .HasForeignKey(_ => _.OrganisationId)
                    .HasPrincipalKey(_ => _.Id);

                builder.Property(_ => _.UpdateDate)
                    .IsRequired();
                builder.Property(_ => _.CreateDate)
                    .IsRequired();

                builder.Property(_ => _.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();
            });

            #endregion

            #region pages

            modelBuilder.Entity<Page>(builder =>
            {
                builder.ToTable("Page");
                builder.HasKey(_ => new {_.Id, _.OrganisationId});
                builder.Property(_ => _.Id)
                    .HasMaxLength(64)
                    .ValueGeneratedNever();

                builder.Property(_ => _.OrganisationId)
                    .IsRequired();
                builder.HasOne(_ => _.Organisation)
                    .WithMany()
                    .HasForeignKey(_ => _.OrganisationId)
                    .HasPrincipalKey(_ => _.Id);
                
                builder.Property(_ => _.MdBody)
                    .HasMaxLength(10240)
                    .IsRequired(false);
                builder.Property(_ => _.ImgLink)
                    .HasMaxLength(512)
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