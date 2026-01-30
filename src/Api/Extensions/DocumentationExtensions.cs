using Scalar.AspNetCore;

namespace Api.Extensions;

public static class DocumentationExtensions
{
    public static IServiceCollection AddDocumentationApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static void UseDocumentationApi(this IEndpointRouteBuilder app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference("/docs",options =>
        {
           options.Title = "Smart Scale Manager API";
           options.Theme = ScalarTheme.Solarized;
        });
    }
}
