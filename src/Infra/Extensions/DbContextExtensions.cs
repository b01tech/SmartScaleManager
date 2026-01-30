using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class DbContextExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection") ?? string.Empty;
        services.AddDbContext<ScaleAppDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
}
