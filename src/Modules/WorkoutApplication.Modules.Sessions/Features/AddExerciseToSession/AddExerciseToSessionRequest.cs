namespace WorkoutApplication.Modules.Sessions.Features.AddExerciseToSession;

public record AddExerciseToSessionRequest(int ExerciseId, int Reps, int Sets, double Weight);