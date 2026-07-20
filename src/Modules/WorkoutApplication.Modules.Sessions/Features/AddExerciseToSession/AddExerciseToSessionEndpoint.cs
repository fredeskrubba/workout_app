using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Sessions.Features.AddExerciseToSession;

public static class AddExerciseToSessionEndpoint
{
    public static void MapAddExerciseToSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/session/{sessionId:int}/exercise", async (AddExerciseToSession handler, ClaimsPrincipal user, int sessionId, AddExerciseToSessionRequest request) =>
        {
            var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            
            var result = await handler.Handle(request, sessionId, loggedInUserId);

            if(result == null)
            {
                return Results.BadRequest("Something went wrong");
            }

            return Results.Ok();
        }).RequireAuthorization();
    }
}