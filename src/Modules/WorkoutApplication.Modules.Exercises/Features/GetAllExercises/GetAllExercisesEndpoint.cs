using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace WorkoutApplication.Modules.Exercises.Features.GetAllExercises
{
    public static class GetAllExercisesEndpoint
    {
        public static void MapGetAllExercisesEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise", async (GetAllExercises handler) =>
            {
               
                var query = new GetAllExercisesRequest();

                var result = await handler.Handle(query);

                if (result == null)
                {
                    return Results.BadRequest("Something went wrong");
                }

                return Results.Ok(result.Value.Exercises);
            });
        }
    }
}
