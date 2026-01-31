using Application.UseCases.User;
using Application.UseCases.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class UseCasesExtensions
{
    internal static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IActiveUserUseCase, ActiveUserUseCase>();
        services.AddScoped<IRemoveUserUseCase, RemoveUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();

        return services;
    }
}
