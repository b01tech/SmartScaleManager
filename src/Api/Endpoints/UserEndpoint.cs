using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.UseCases.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class UserEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("users").WithTags("Users");

        group.MapPost("/register",
                async ([FromBody] RegisterUserRequest request, [FromServices] IRegisterUserUseCase useCase) =>
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

        group.MapGet("/{id:long}", async ([FromRoute] long id, [FromServices] IGetUserUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                if (result.IsSuccess)
                    return Results.Ok(result.Data);
                return (result.ErrorResponse.Status == StatusCodes.Status404NotFound)
                    ? Results.NotFound(result.ErrorResponse)
                    : Results.BadRequest(result.ErrorResponse);
            }).WithName("GetUserDetails")
            .WithSummary("Retrieved a user by Id")
            .Produces<UserDetailsResponse>(StatusCodes.Status200OK);

        group.MapPost("/active/{id:long}", async ([FromRoute] long id, [FromServices] IActiveUserUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.ErrorResponse);
            }).WithName("ActivateUser")
            .WithSummary("Activates a user")
            .Produces(StatusCodes.Status204NoContent);

        group.MapDelete("/remove/{id:long}", async ([FromRoute] long id, [FromServices] IRemoveUserUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.ErrorResponse);
            }).WithName("RemoveUser")
            .WithSummary("Removes a user")
            .Produces(StatusCodes.Status204NoContent);
    }
}
