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
            app.MapPost("/login", (LoginUser handler, LoginUserRequest request) =>
            {
               
                var result = handler.Handle(request);

                if (result.IsSuccess)
                {
                    
                    return Results.Ok(result.Value.token);

                } else
                {
                    return Results.BadRequest(result.Error);
                }
          
            });
        }
    }
}
