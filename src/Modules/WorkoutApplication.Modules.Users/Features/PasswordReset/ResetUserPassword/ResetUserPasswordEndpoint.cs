using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ResetUserPassword
{
    public static class ResetUserPasswordEndpoint
    {
        public static void MapResetUserPasswordEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/password/reset", async (ResetUserPassword handler, ResetUserPasswordRequest request) =>
            {
                var result = await handler.Handle(request);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Value);
                }

                return Results.BadRequest(result.Error);
            });
        }
    }
}
