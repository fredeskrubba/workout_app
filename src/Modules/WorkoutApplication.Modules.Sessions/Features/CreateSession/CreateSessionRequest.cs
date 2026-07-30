using WorkoutApplication.Shared.Entities;
namespace WorkoutApplication.Modules.Sessions.Features.CreateSession;

public record CreateSessionRequest( DateTime Date, int DurationSeconds, int Rating);