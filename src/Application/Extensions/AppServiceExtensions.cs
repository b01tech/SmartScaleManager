using Application.Services.Encrypter;
using Application.UseCases.User;
using Application.UseCases.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class AppServiceExtensions
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IEncrypter, Encrypter>();
        services.AddScoped<IUserValidator, UserValidator>();
    }
}
