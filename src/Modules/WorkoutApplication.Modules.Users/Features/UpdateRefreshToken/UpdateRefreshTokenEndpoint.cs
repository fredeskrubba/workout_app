using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Features.LoginUser;

namespace WorkoutApplication.Modules.Users.Features.UpdateRefreshToken
{
    public static class UpdateRefreshTokenEndpoint
    {
        public static void MapUpdateRefreshTokenEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/refresh-token", async (UpdateRefreshToken handler, UpdateRefreshTokenRequest request) =>
            {

                var result = await handler.Handle(request);

                if (result.IsSuccess)
                {

                    return Results.Ok(result.Value.RefreshToken);

                }
                else
                {
                    return Results.BadRequest(result.Error);
                }

            });
        }
    }
}
