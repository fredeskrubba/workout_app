using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using WorkoutApplication.Modules.Users.Features.DeleteUser;

namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public static class UpdateUserPasswordEndpoint
    {
        public static void MapUpdateUserEndpointEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/password", async (UpdateUserPassword handler, UpdateUserPasswordRequest request, ClaimsPrincipal user) =>
            {

                var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await handler.Handle(request, loggedInUserId);
               
                if (result.IsSuccess)
                {

                    return Results.Ok(result.Value.updatedHash);

                }
                else
                {
                    return Results.BadRequest(result.Error);
                }

            }).RequireAuthorization();
        }
    }
}
