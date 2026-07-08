using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace WorkoutApplication.Modules.Users.Features.GetUser;

public static class GetUserEndpoint
{
    public static void MapGetUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", (Guid id) =>
        {
            var query = new GetUser.Query(id);

            var result = GetUser.Handle(query);

            return Results.Ok(result);
        });
    }
}
