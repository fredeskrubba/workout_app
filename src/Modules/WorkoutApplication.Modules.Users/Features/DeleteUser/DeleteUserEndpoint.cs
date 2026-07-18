using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Users.Features.DeleteUser
{
    public static class DeleteUserEndpoint
    {
        public static void MapDeleteUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id:int}", async (DeleteUser handler, int id, ClaimsPrincipal user) =>
            {
                var loggedinUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new DeleteUserRequest(id, loggedinUserId);

                var result = await handler.Handle(query);

                if (result == null)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            }).RequireAuthorization();
        }
    }
}
