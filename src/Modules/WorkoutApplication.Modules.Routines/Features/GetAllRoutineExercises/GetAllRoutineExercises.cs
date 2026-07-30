using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises
{
    public class GetAllRoutineExercises
    {
        private readonly WorkoutApplicationDBContext _context;

        public GetAllRoutineExercises(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<GetAllRoutineExercisesResponse>> Handle(GetAllRoutineExercisesRequest request, int routineId, int loggedInUserId)
        {
            var routine = await _context.Routines.FirstOrDefaultAsync(x => x.RoutineId == routineId && x.UserId == loggedInUserId);

            if (routine == null)
            {
                return Result<GetAllRoutineExercisesResponse>.Failure("Routine not found");
            }

            var exercises = await _context.RoutineExercises
                .Include(re => re.Exercise)
                .Where(re => re.RoutineId == routineId)
                .Select(re => new RoutineExerciseDto(
                    re.RoutineExerciseId,
                    re.RoutineId,
                    re.Sets,
                    re.Reps,
                    re.SeatSetting,
                    re.Weight,
                    new ExerciseDto(
                        re.Exercise.Id,
                        re.Exercise.Name,
                        re.Exercise.Description,
                        re.Exercise.ExerciseType
                    )
                ))
                .ToListAsync();

            return Result<GetAllRoutineExercisesResponse>.Success(new GetAllRoutineExercisesResponse(exercises));
        }
    }
}
