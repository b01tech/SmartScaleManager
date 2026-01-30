namespace Api.Endpoints;

public static class HealthCheckerEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => new { status = "OK" })
            .WithName("HealthChecker");
    }
}
