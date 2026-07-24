using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Sessions.Features.GetAllSessionExercises
{
    public static class GetAllSessionExercisesEndpoint
    {
        public static void MapGetAllSessionExercisesEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/session/{sessionId}/exercise", async (GetAllSessionExercises handler, int sessionId, ClaimsPrincipal user) =>
            {
                var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                var query = new GetAllSessionExercisesRequest(loggedInUserId, sessionId);

                var result = await handler.Handle(query);

                if (result == null)
                {
                    return Results.BadRequest("Something went wrong");
                }

                return Results.Ok(result.Value.Exercises);
            }).RequireAuthorization();
        }
    }
}
