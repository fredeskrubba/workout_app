using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Routines.Features.DeleteRoutine;

public static class DeleteRoutineEndpoint
{
    public static void MapDeleteRoutineEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/routine/{routineId}", async (DeleteRoutine handler, int routineId, ClaimsPrincipal user) =>
        {
            var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var request = new DeleteRoutineRequest(routineId, loggedInUserId);
            var result = await handler.Handle(request);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        }).RequireAuthorization();
    }
}
