using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Data;

namespace WorkoutApplication.Modules.Routines.Features.GetAllUserRoutines;

public class GetAllUserRoutines
{
    private readonly WorkoutApplicationDBContext _context;

    public GetAllUserRoutines(WorkoutApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Result<GetAllUserRoutinesResponse>> Handle(GetAllUserRoutinesRequest request)
    {
        int userId = int.Parse(request.LoggedInUserId);
        var routines = await _context.Routines.Where(x => x.UserId == userId).ToListAsync();

        if (routines is null || routines.Count == 0)
        {
            return Result<GetAllUserRoutinesResponse>.Failure("No routines found for user with userid of " + request.LoggedInUserId);
        }

        var response = new GetAllUserRoutinesResponse(routines);
        return Result<GetAllUserRoutinesResponse>.Success(response);
    }
}
