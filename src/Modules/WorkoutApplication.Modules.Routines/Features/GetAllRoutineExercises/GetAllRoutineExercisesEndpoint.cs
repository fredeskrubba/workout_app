using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises
{
    public static class GetAllRoutineExercisesEndpoint
    {
        public static void MapGetAllRoutineExercisesEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/routine/{routineId:int}/exercise", async (GetAllRoutineExercises handler, int routineId, ClaimsPrincipal user) =>
            {
                var loggedInUserId = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
                var result = await handler.Handle(new GetAllRoutineExercisesRequest(), routineId, Int32.Parse(loggedInUserId));

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            }).RequireAuthorization();
        }
    }
}
