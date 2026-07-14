using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace WorkoutApplication.Modules.Users.Features.GetUser;

public static class GetUserEndpoint
{
    public static void MapGetUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:int}", async (GetUser handler, int id) =>
        {
            var query = new GetUserRequest(id);

            var result = await handler.Handle(query);

            if(result == null)
            {
                return Results.BadRequest("User not found");
            }

            return Results.Ok(result);
        }).RequireAuthorization();
    }
}
