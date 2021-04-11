using System;

using Microsoft.EntityFrameworkCore;

using Pets.Domain.Pet.Entity;
using Pets.Types;

namespace Pets.Infrastructure.Pet
{
    public sealed class PetDbContext : DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options)
        {
        }

        internal DbSet<Domain.Pet.Entity.Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Pet

            modelBuilder.Entity<Domain.Pet.Entity.Pet>(builder =>
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
                builder.Property(_ => _.Gender)
                    .HasConversion<String>()
                    .HasDefaultValue(PetGender.Unset)
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
                builder.Property(_ => _.MdShortBody)
                    .HasMaxLength(512)
                    .IsRequired(false);

                builder.Property(_ => _.Organisation)
                    .HasColumnName("OrganisationId")
                    .HasConversion(
                        org => org.Id,
                        guid => new Organisation(guid))
                    .IsRequired();

                builder.Property(_ => _.UpdateDate)
                    .IsRequired();
                builder.Property(_ => _.CreateDate)
                    .IsRequired();

                builder.Property(_ => _.ConcurrencyTokens)
                    .IsConcurrencyToken()
                    .IsRequired();
            });

            #endregion }
        }
    }
}