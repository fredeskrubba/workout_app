using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;

public record GetAllUserSessionsResponse(IEnumerable<WorkoutSession> Sessions);