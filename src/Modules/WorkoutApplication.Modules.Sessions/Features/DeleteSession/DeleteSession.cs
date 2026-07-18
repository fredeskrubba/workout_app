using Microsoft.EntityFrameworkCore; 
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Data;


namespace WorkoutApplication.Modules.Sessions.Features.DeleteSession;

public class DeleteSession
{
    private readonly WorkoutApplicationDBContext _context;

    public DeleteSession(WorkoutApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<DeleteSessionResponse>> Handle(DeleteSessionRequest request)
    {
        var workoutSession = await _context.WorkoutSessions.FirstOrDefaultAsync(x => x.SessionId == request.SessionId);

        if (workoutSession is null)
        {
            return Result<DeleteSessionResponse>.Failure("Session not found");
        }

        if (Int32.Parse(request.LoggedInUserId) != workoutSession.UserId)
        {
            return Result<DeleteSessionResponse>.Failure("No session with the supplied id found for the logged in user with id: " + request.LoggedInUserId);
        }
        
        _context.Remove(workoutSession);

        
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return Result<DeleteSessionResponse>.Failure("Something went wrong, see error: " + ex.Message);
        }

        DeleteSessionResponse response = new($"Workout session with id {request.SessionId} was successfully deleted.");
        return Result<DeleteSessionResponse>.Success(response);
    }
}