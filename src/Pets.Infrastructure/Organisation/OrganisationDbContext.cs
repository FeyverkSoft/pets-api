namespace Pets.Infrastructure.Organisation;

using Domain.ValueTypes;

using Microsoft.EntityFrameworkCore;

using Rabbita.Entity;

public sealed class OrganisationDbContext : PersistentMessagingDbContext
{
    public OrganisationDbContext(DbContextOptions<OrganisationDbContext> options) : base(options)
    {
    }

    internal DbSet<Organisation> Organisations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Organisation

        modelBuilder.Entity<Organisation>(builder =>
        {
            builder.ToTable("Organisation");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();

            builder.HasIndex(_ => _.Id);
        });

        #endregion }
    }
}