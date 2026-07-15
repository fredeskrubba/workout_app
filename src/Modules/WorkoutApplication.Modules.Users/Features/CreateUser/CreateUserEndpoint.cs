using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public static class CreateUserEndpoint
    {
        public static void MapCreateUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/user", async (CreateUser handler, CreateUserRequest request) =>
            {
             
                var result = await handler.Handle(request);

                if(!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            });
        }
    }
}
