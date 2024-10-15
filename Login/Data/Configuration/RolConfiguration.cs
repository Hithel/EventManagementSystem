using Login.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Data.Configuration;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("rol");
        builder.Property(p => p.Id)
            .IsRequired();
        builder.Property(p => p.Nombre)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
    }
}

