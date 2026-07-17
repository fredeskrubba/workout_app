using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Sessions.Features.CreateSession;

public class CreateSession
{
    private readonly WorkoutApplicationDBContext _context;

    public CreateSession(WorkoutApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateSessionResponse>> Handle(CreateSessionRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (user is null)
        {
            return Result<CreateSessionResponse>.Failure("User not found");
        }
        
        if (request.DurationSeconds <= 0)
        {
            return Result<CreateSessionResponse>.Failure("Duration must be greater than zero");
        }

        if (request.Rating is < 1 or > 5)
        {
            return Result<CreateSessionResponse>.Failure("Rating must be between 1 and 5");
        }

        WorkoutSession session = new()
        {
            UserId = user.UserId,
            Date =  request.Date,
            DurationSeconds = request.DurationSeconds,
            Rating = request.Rating,
            
        };

        _context.WorkoutSessions.Add(session);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return Result<CreateSessionResponse>.Failure("Something went wrong, see error: " + ex.Message);
        }

        CreateSessionResponse response = new(session.Date, user.UserId);

        return Result<CreateSessionResponse>.Success(response);
    }
}