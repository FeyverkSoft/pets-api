namespace Pets.Infrastructure.News;

using System.Collections.Generic;

using Domain.News.Entity;
using Domain.ValueTypes;

using Helpers;

using Microsoft.EntityFrameworkCore;

using Rabbita.Entity;
using Rabbita.Entity.FluentExtensions;

public sealed class NewsDbContext : PersistentMessagingDbContext
{
    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {
    }

    internal DbSet<News> News { get; set; }
    internal DbSet<Pet> Pets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region News

        modelBuilder.Entity<News>(builder =>
        {
            builder.ToTable("News");
            builder.HasKey(_ => new { _.Id, _.Organisation });
            builder.HasIndex(_ => _.Id)
                .IsUnique();
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();

            builder.Property(_ => _.Organisation)
                .HasColumnName("OrganisationId")
                .HasConversion(
                    organisation => organisation.Id,
                    guid => (Organisation)guid)
                .IsRequired();

            builder.Property(_ => _.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(_ => _.MdBody)
                .HasMaxLength(10240)
                .IsRequired(false);
            builder.Property(_ => _.ImgLink)
                .HasMaxLength(512)
                .IsRequired(false);
            builder.Property(_ => _.MdShortBody)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(_ => _.UpdateDate)
                .IsRequired();
            builder.Property(_ => _.CreateDate)
                .IsRequired();

            builder.Property(_ => _.Tags)
                .HasConversion(
                    converterTo => converterTo == null ? new List<String>().ToJson() : converterTo.ToJson(),
                    converterForm =>
                        converterForm == null ? new List<String>() : converterForm.ParseJson<List<String>>()
                )
                .HasDefaultValue(new List<String>())
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(_ => _.MdShortBody)
                .HasMaxLength(512)
                .IsRequired();

            builder.HasMany(_ => _.LinkedPets)
                .WithMany()
                .UsingEntity("NewsPets",
                    r => r.HasOne(typeof(Pet)).WithMany().HasForeignKey("PetId").HasPrincipalKey("Id"),
                    l => l.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey("Id"),
                    j => j.HasKey("NewsId", "PetId"));

            builder.Property(_ => _.ConcurrencyTokens)
                .IsConcurrencyToken()
                .IsRequired();

            builder.IsEvents(_ => _.Events);
        });

        #endregion

        #region Pet

        modelBuilder.Entity<Pet>(builder =>
        {
            builder.ToTable("Pet");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();

            builder.HasIndex(_ => _.Id);
            builder.Property(_ => _.Organisation)
                .HasColumnName("OrganisationId")
                .HasConversion(
                    organisation => organisation.Id,
                    guid => (Organisation)guid)
                .IsRequired();

            builder.Property(_ => _.Name);

            builder.HasMany(_ => _.News)
                .WithMany()
                .UsingEntity("NewsPets");
        });

        #endregion
    }
}