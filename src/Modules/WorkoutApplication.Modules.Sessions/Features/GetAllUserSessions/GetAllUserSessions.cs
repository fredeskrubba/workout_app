using Microsoft.EntityFrameworkCore; 
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Data;

namespace WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;

public class GetAllUserSessions
{
    private readonly WorkoutApplicationDBContext _context;

    public GetAllUserSessions(WorkoutApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<GetAllUserSessionsResponse>> Handle(GetAllUserSessionsRequest request)
    {
        int userId = int.Parse(request.LoggedInUserId);
        var workoutSessions = await _context.WorkoutSessions.Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
        
        if (workoutSessions is null || workoutSessions.Count == 0)
        {
            return Result<GetAllUserSessionsResponse>.Failure("No sessions found for user with userid of " + request.LoggedInUserId);
        }
        

        GetAllUserSessionsResponse response = new(workoutSessions);
        return Result<GetAllUserSessionsResponse>.Success(response);
    }
}