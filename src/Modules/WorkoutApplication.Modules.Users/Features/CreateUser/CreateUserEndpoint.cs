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
            app.MapPost("/user", (CreateUserRequest request) =>
            {
             
                var result = CreateUser.Handle(request);

                return Results.Ok(result);
            });
        }
    }
}
