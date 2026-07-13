using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace WorkoutApplication.Modules.Users.Features.DeleteUser
{
    public static class DeleteUserEndpoint
    {
        public static void MapDeleteUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id:int}", async (DeleteUser handler, int id) =>
            {
                var query = new DeleteUserRequest(id);

                var result = await handler.Handle(query);

                if (result == null)
                {
                    return Results.BadRequest("User not found");
                }

                return Results.Ok(result);
            });
        }
    }
}
