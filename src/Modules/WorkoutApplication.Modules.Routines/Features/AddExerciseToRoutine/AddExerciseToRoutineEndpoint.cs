using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine
{
    public static class AddExerciseToRoutineEndpoint
    {
        public static void MapAddExerciseToRoutineEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/routine/{routineId:int}/exercise", async (AddExerciseToRoutine handler, AddExerciseToRoutineRequest request, int routineId, ClaimsPrincipal user) =>
            {
                var loggedInUserId = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
                var result = await handler.Handle(request, routineId, Int32.Parse(loggedInUserId));

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            }).RequireAuthorization();
        }
    }
}

