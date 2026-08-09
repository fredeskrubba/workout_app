using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Users.Features.LogoutUser
{
    public static class LogoutUserEndpoint
    {
        public static void MapLogoutUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/logout", async (LogoutUser handler, HttpContext httpContext) =>
            {
                var refreshToken = httpContext.Request.Cookies["refreshToken"];

                var query = new LogoutUserRequest(refreshToken);
               

                var result = await handler.Handle(query, httpContext);

                if (result == null)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            });
        }
    }
}
