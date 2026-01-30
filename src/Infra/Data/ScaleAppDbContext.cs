using Core.Entities;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class ScaleAppDbContext(DbContextOptions<ScaleAppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UserMap.Configure(modelBuilder);
    }
}
