using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;


namespace WorkoutApplication.Modules.Users.Features.UpdateRefreshToken
{
    public static class UpdateRefreshTokenEndpoint
    {
        public static void MapUpdateRefreshTokenEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/refresh-token", async (UpdateRefreshToken handler, UpdateRefreshTokenRequest request, HttpContext httpContext) =>
            {

                var result = await handler.Handle(request, httpContext);

                if (result.IsSuccess)
                {

                    return Results.Ok(result.Value);

                }
                else
                {
                    return Results.BadRequest(result.Error);
                }

            });
        }
    }
}
