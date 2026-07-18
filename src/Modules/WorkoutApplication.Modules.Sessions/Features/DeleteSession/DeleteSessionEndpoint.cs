using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WorkoutApplication.Modules.Sessions.Features.DeleteSession;

public static class DeleteSessionEndpoint
{
    public static void MapDeleteSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/session", async (DeleteSession handler, DeleteSessionRequest request) =>
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