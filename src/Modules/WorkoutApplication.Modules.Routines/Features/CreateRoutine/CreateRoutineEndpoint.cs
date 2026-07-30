using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace WorkoutApplication.Modules.Routines.Features.CreateRoutine
{
    public static class CreateroutineEndpoint
    {
        public static void MapCreateRoutineEndpoint(this IEndpointRouteBuilder app)
                {
                    app.MapPost("/routine", async (CreateRoutine handler, CreateRoutineRequest request, ClaimsPrincipal user) =>
                    {
                        var loggedInUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                        var result = await handler.Handle(request, Int32.Parse(loggedInUserId));

                        if (!result.IsSuccess)
                        {
                            return Results.BadRequest(result.Error);
                        }

                        return Results.Ok(result.Value);
                    }).RequireAuthorization();
                }
        }

        }
    
