using Microsoft.EntityFrameworkCore; 
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Sessions.Features.AddExerciseToSession;

public class AddExerciseToSession
{
    private readonly WorkoutApplicationDBContext _context;

    public AddExerciseToSession(WorkoutApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<AddExerciseToSessionResponse>> Handle(AddExerciseToSessionRequest request, int sessionId, string loggedInUserId)
    {
        int userId = int.Parse(loggedInUserId);
        
        var session = await _context.WorkoutSessions
            .FirstOrDefaultAsync(x => 
                x.SessionId == sessionId &&
                x.UserId == userId);

        if (session == null)
        {
            return Result<AddExerciseToSessionResponse>.Failure("Session not found");
        }

        var exercise = await _context.Exercises.FindAsync(request.ExerciseId);

        if (exercise == null)
        {
            return Result<AddExerciseToSessionResponse>.Failure("Exercise not found");
        }

        SessionExercise exerciseToAdd = new SessionExercise()
        {
            ExerciseId = exercise.Id,
            SessionId = sessionId,
            Reps =  request.Reps,
            Weight = request.Weight,
            Sets = request.Sets
        };
        
        _context.SessionExercises.Add(exerciseToAdd);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return Result<AddExerciseToSessionResponse>.Failure("Something went wrong, see error: " + ex.Message);
        }
        
        AddExerciseToSessionResponse response = new();
        return Result<AddExerciseToSessionResponse>.Success(response);
    }
}