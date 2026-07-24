using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ForgotUserPassword
{
    public static class ForgotUserPasswordEndpoint
    {
        public static void MapForgotUserPasswordEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/password/forgot", async (ForgotUserPassword handler, ForgotUserPasswordRequest request) =>
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
