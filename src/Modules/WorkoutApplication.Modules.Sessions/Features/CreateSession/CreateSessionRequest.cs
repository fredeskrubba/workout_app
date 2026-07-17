using WorkoutApplication.Shared.Entities;
namespace WorkoutApplication.Modules.Sessions.Features.CreateSession;

public record CreateSessionRequest(int UserId, DateTime Date, int DurationSeconds, int Rating);