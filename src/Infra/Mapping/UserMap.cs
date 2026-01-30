using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Mapping;

public static class UserMap
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Username).IsRequired().HasMaxLength(50);
            entity.Property(x => x.HashPassword).IsRequired().HasMaxLength(255);
            entity.Property(x => x.IsActive).IsRequired();
            entity.Property(x => x.Role).IsRequired();
        });
    }
}
