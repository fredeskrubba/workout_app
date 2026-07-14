using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace WorkoutApplication.Modules.Users.Features.LoginUser
{
    public static class LoginUserEndpoint
    {
        public static void MapLoginUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginUser handler, LoginUserRequest request) =>
            {
               
                var result = await handler.Handle(request);

                if (result.IsSuccess)
                {
                    
                    return Results.Ok(result.Value);

                } else
                {
                    return Results.BadRequest(result.Error);
                }
          
            });
        }
    }
}
