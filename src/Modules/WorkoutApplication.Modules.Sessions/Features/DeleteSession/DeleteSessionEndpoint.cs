using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
namespace WorkoutApplication.Modules.Sessions.Features.DeleteSession;

public static class DeleteSessionEndpoint
{
    public static void MapDeleteSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/session/{sessionId}", async (DeleteSession handler, int sessionId, ClaimsPrincipal user) =>
        {
            var loggedinUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            
            DeleteSessionRequest request = new(sessionId,  loggedinUserId);
            var result = await handler.Handle(request);
            
            if(!result.IsSuccess)
            {
                return Results.BadRequest(result.Error);
            }
                
            return Results.Ok(result.Value);
        }).RequireAuthorization();
    }
}