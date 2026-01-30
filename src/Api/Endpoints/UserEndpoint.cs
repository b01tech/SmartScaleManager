using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.UseCases.User.Interfaces;

namespace Api.Endpoints;

public static class UserEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("users").WithTags("Users");

        group.MapPost("/register", async (RegisterUserRequest request, IRegisterUserUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(request);
                if (result.IsSuccess)
                    return Results.Created(string.Empty, result.Data);

                return (result.ErrorResponse.Status == StatusCodes.Status409Conflict)
                    ? Results.Conflict(result.ErrorResponse)
                    : Results.BadRequest(result.ErrorResponse);

            }).WithName("RegisterUser")
            .WithSummary("Registers a user")
            .Produces<UserResponse>(StatusCodes.Status201Created);
    }
}
