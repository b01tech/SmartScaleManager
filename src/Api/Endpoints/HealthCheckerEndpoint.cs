namespace Api.Endpoints;

public static class HealthCheckerEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => new { status = "OK" })
            .WithName("HealthChecker")
            .WithTags("Health Checker")
            .WithSummary("Checks the health of the application");
    }
}
