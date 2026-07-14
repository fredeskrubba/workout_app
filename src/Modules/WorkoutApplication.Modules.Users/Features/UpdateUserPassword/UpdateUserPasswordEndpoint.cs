using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Features.LoginUser;

namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public static class UpdateUserPasswordEndpoint
    {
        public static void MapUpdateUserEndpointEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/password", async (UpdateUserPassword handler, UpdateUserPasswordRequest request) =>
            {

                var result = await handler.Handle(request);

                if (result.IsSuccess)
                {

                    return Results.Ok(result.Value.updatedHash);

                }
                else
                {
                    return Results.BadRequest(result.Error);
                }

            });
        }
    }
}
