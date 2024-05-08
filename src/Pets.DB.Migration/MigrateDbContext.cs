namespace Pets.DB.Migrations;

using System;

using Entities;

using Microsoft.EntityFrameworkCore;

using Types;

public class MigrateDbContext : DbContext
{
    public MigrateDbContext(DbContextOptions<MigrateDbContext> options) : base(options)
    {
    }

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

        #region Needs

        modelBuilder.Entity<Need>(builder =>
        {
            builder.ToTable("Need");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();

            builder.Property(_ => _.MdBody)
                .HasMaxLength(2048)
                .IsRequired(false);
            builder.Property(_ => _.ImgLinks)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(_ => _.NeedState)
                .HasMaxLength(16)
                .IsRequired();

            builder.HasOne(_ => _.Organisation)
                .WithMany()
                .HasForeignKey(_ => _.OrganisationId)
                .HasPrincipalKey(_ => _.Id);

            builder.Property(_ => _.ConcurrencyTokens)
                .IsConcurrencyToken()
                .IsRequired();
        });

        #endregion

        #region Resources

        modelBuilder.Entity<Resource>(builder =>
        {
            builder.ToTable("Resource");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();

            builder.Property(_ => _.State)
                .HasConversion<String>()
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(_ => _.MdBody)
                .HasMaxLength(2048)
                .IsRequired(false);
            builder.Property(_ => _.ImgLink)
                .HasMaxLength(512)
                .IsRequired(false);
            builder.Property(_ => _.Title)
                .HasMaxLength(256)
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
            builder.Property(_ => _.Gender)
                .HasConversion<String>()
                .HasDefaultValue(PetGender.Unset)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(_ => _.AnimalId)
                .IsRequired(false);

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
            builder.HasKey(_ => new { _.Id, _.OrganisationId });
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

        #region News

        modelBuilder.Entity<News>(builder =>
        {
            builder.ToTable("News");
            builder.HasKey(_ => new { _.Id, _.OrganisationId });
            builder.HasIndex(_ => _.Id)
                .IsUnique();
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();

            builder.Property(_ => _.OrganisationId)
                .IsRequired();

            builder.HasOne(_ => _.Organisation)
                .WithMany()
                .HasForeignKey(_ => _.OrganisationId)
                .HasPrincipalKey(_ => _.Id);

            builder.Property(_ => _.Title)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(_ => _.State)
                .HasConversion<String>()
                .HasDefaultValue(NewsState.Active)
                .HasMaxLength(64)
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

            builder.Property(_ => _.ConcurrencyTokens)
                .IsConcurrencyToken()
                .IsRequired();
        });

        #endregion

        #region NewsPets

        modelBuilder.Entity<NewsPets>(b =>
        {
            b.ToTable("NewsPets");

            b.HasIndex(np => new { np.NewsId, np.PetId })
                .IsUnique();
            b.HasKey(np => new { np.NewsId, np.PetId });

            b.Property(np => np.NewsId)
                .ValueGeneratedNever()
                .HasColumnName("NewsId")
                .IsRequired();

            b.Property(np => np.PetId)
                .ValueGeneratedNever()
                .HasColumnName("PetId")
                .IsRequired();

            b.HasOne(np => np.Pet)
                .WithMany(p => p!.PetNews)
                .HasForeignKey(_ => _.PetId)
                .HasPrincipalKey(_ => _!.Id);

            b.HasOne(mr => mr.News)
                .WithMany(n => n!.NewsPets)
                .HasForeignKey(_ => _.NewsId)
                .HasPrincipalKey(_ => _!.Id);
        });

        #endregion

        #region User

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(user => user.Login)
                .HasMaxLength(64)
                .IsRequired();
            builder.HasIndex(user => user.Login)
                .IsUnique();
            builder.Property(user => user.Name)
                .IsRequired(false)
                .HasMaxLength(256);
            builder.Property(user => user.PasswordHash)
                .IsRequired(false)
                .HasMaxLength(512);
            builder.Property(user => user.State)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(user => user.Permissions)
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(_ => _.OrganisationId)
                .HasDefaultValue(Guid.Parse("10000000-0000-4000-0000-000000000000"))
                .IsRequired();
            builder.HasOne(_ => _.Organisation)
                .WithMany()
                .HasForeignKey(_ => _.OrganisationId)
                .HasPrincipalKey(_ => _.Id)
                .IsRequired(false);

            builder.Property(user => user.ConcurrencyToken)
                .IsRequired()
                .IsConcurrencyToken();
        });

        #endregion

        #region RefreshToken

        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(refreshToken => refreshToken.Id);
            builder.Property(refreshToken => refreshToken.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(refreshToken => refreshToken.IpAddress)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(refreshToken => refreshToken.ExpireDate)
                .IsRequired();

            builder.HasIndex(refreshToken => refreshToken.UserId);
            builder.Property(refreshToken => refreshToken.UserId)
                .IsRequired();
        });

        #endregion
    }
}