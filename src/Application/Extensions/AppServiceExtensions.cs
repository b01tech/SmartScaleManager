using Application.Services.Encrypter;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class AppServiceExtensions
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IEncrypter, Encrypter>();
    }
}
