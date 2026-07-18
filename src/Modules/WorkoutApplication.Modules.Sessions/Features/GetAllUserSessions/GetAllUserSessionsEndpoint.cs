using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
namespace WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;

public static class GetAllUserSessionsEndpoint
{
    public static void MapGetAllUserSessionsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/session", async (GetAllUserSessions handler, ClaimsPrincipal user) =>
        {
            var loggedinUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = new GetAllUserSessionsRequest(loggedinUserId);

            var result = await handler.Handle(query);

            if(result == null)
            {
                return Results.BadRequest("User not found");
            }

            return Results.Ok(result.Value.Sessions);
        }).RequireAuthorization();
    }
}