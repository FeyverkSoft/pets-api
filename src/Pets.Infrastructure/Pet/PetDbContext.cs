namespace Pets.Infrastructure.Pet;

using Domain.Pet.Entity;
using Domain.ValueTypes;

using Microsoft.EntityFrameworkCore;

using Rabbita.Entity;
using Rabbita.Entity.FluentExtensions;

using Types;

public sealed class PetDbContext : PersistentMessagingDbContext
{
    public PetDbContext(DbContextOptions<PetDbContext> options) : base(options)
    {
    }

    internal DbSet<Pet> Pets { get; set; }

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
            builder.Property(_ => _.AnimalId)
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

            builder.IsEvents(_ => _.Events);

            builder.Property(_ => _.ConcurrencyTokens)
                .IsConcurrencyToken()
                .IsRequired();
        });

        #endregion }
    }
}