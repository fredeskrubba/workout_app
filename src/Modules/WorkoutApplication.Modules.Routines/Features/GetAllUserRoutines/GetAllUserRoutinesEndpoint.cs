using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Routines.Features.GetAllUserRoutines;

public static class GetAllUserRoutinesEndpoint
{
    public static void MapGetAllUserRoutinesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/routine", async (GetAllUserRoutines handler, ClaimsPrincipal user) =>
        {
            var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetAllUserRoutinesRequest(loggedInUserId);

            var result = await handler.Handle(query);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value.Routines);
        }).RequireAuthorization();
    }
}
