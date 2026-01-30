using Api.Endpoints;

namespace Api.Extensions;

public static class EndpointExtensions
{
    public static void UseAppEndpoints(this IEndpointRouteBuilder app)
    {
        HealthCheckerEndpoint.Map(app);
    }
}
