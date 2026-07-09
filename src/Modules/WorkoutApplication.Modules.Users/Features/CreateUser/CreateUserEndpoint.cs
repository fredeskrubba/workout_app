using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkoutApplication.Modules.Users.Entities;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public static class CreateUserEndpoint
    {
        public static void MapCreateUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/user", async (CreateUser handler, CreateUserRequest request) =>
            {
             
                var result = await handler.Handle(request);

                if(result == null)
                {
                    return Results.BadRequest("Something went wrong");
                }
                return Results.Ok(result);
            });
        }
    }
}
