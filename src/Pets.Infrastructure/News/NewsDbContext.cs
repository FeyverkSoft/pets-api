namespace Pets.Infrastructure.News;

using Domain.News.Entity;

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
                .IsRequired();

            builder.HasOne(_ => _.Organisation)
                .WithMany()
                .HasForeignKey(_ => _.Organisation)
                .HasPrincipalKey(_ => _.Id);

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
                .HasDefaultValue("[]")
                .HasMaxLength(1024)
                .IsRequired();
            
            builder.Property(_ => _.MdShortBody)
                .HasMaxLength(512)
                .IsRequired();

            builder
                .HasMany<LinkedPets>()
                .WithMany(_=>_.News)
                .UsingEntity("NewsPets");
            
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

            builder.Property(_ => _.Name);
        });

        #endregion
    }
}