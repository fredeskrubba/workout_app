using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Sessions.Features.CreateSession;

public static class CreateSessionEndpoint
{
    public static void MapCreateSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/session", async (CreateSession handler, CreateSessionRequest request, ClaimsPrincipal user) =>
        {

            var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await handler.Handle(request, Int32.Parse(loggedInUserId));

            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        }).RequireAuthorization();
    }
}