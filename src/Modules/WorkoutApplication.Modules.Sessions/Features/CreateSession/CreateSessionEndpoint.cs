using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WorkoutApplication.Modules.Sessions.Features.CreateSession;

public static class CreateSessionEndpoint
{
    public static void MapCreateSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/session", async (CreateSession handler, CreateSessionRequest request) =>
        {
             
            var result = await handler.Handle(request);

            if(!result.IsSuccess)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        }).RequireAuthorization();
    }
}