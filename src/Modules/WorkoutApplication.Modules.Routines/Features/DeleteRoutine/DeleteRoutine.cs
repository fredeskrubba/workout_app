using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Routines.Features.DeleteRoutine;

public class DeleteRoutine
{
    private readonly WorkoutApplicationDBContext _context;

    public DeleteRoutine(WorkoutApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Result<DeleteRoutineResponse>> Handle(DeleteRoutineRequest request)
    {
        var routine = await _context.Routines.FirstOrDefaultAsync(x => x.RoutineId == request.RoutineId);

        if (routine is null)
        {
            return Result<DeleteRoutineResponse>.Failure("Routine not found");
        }

        if (Int32.Parse(request.LoggedInUserId) != routine.UserId)
        {
            return Result<DeleteRoutineResponse>.Failure("No routine with the supplied id found for the logged in user with id: " + request.LoggedInUserId);
        }

        _context.Routines.Remove(routine);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return Result<DeleteRoutineResponse>.Failure("Something went wrong, see error: " + ex.Message);
        }

        var response = new DeleteRoutineResponse($"Routine with id {request.RoutineId} was successfully deleted.");
        return Result<DeleteRoutineResponse>.Success(response);
    }
}
