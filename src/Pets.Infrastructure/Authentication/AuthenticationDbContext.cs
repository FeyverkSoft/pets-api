using System;

using Microsoft.EntityFrameworkCore;

using Pets.Domain.Authentication;
using Pets.Helpers;

namespace Pets.Infrastructure.Authentication
{
    public sealed class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }

        internal DbSet<User> Users { get; set; }
        internal DbSet<RefreshToken> RefreshTokens { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    .HasMaxLength(2048)
                    .HasConversion<String>(
                        converterTo => converterTo == null ? (new ScopeAction()).ToJson() : converterTo.ToJson(),
                        converterForm => converterForm == null ? new ScopeAction() : converterForm.ParseJson<ScopeAction>()
                    );

                builder.Property(user => user.ConcurrencyToken)
                    .IsRequired()
                    .IsConcurrencyToken();
            });
            
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
                
                builder.HasIndex(refreshToken => refreshToken.UserId);
                builder.Property(refreshToken => refreshToken.UserId)
                    .IsRequired();
            });

            #endregion
        }
    }
}