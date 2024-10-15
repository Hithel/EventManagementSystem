using Login.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Login.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Username)
            .HasColumnName("username")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Password)
            .HasColumnName("password")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.TwoStepSecret)
            .HasColumnName("twostepsecret");

        builder.Property(p => p.DateCreated)
            .HasColumnName("datecreated")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsRequired();

        builder
            .HasMany(p => p.Rols)
            .WithMany(p => p.Users)
            .UsingEntity<UserRol>
            (
                j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.UsersRols)
                .HasForeignKey(ut => ut.RolId),

                j => j
                .HasOne(et => et.Usuario)
                .WithMany(et => et.UsersRols)
                .HasForeignKey(el => el.UsuarioId),

                j =>
                {
                    j.ToTable("usersRol");
                    j.HasKey(t => new { t.UsuarioId, t.RolId });
                });

        builder.HasMany(p => p.RefreshTokens)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
    }
}

