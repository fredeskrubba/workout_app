using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Sessions.Features.GetAllSessionExercises
{

    public class GetAllSessionExercises
    {
        private readonly WorkoutApplicationDBContext _context;

        public GetAllSessionExercises(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<GetAllSessionExercisesResponse>> Handle(GetAllSessionExercisesRequest request)
        {
            int userId = int.Parse(request.LoggedInUserId);
            var exercises = await _context.SessionExercises.Include(se => se.Exercise).Where(x => x.SessionId == request.SessionId).Select(se => new SessionExerciseDto(
                se.SessionExerciseId,
                se.SessionId,
                se.Sets,
                se.Reps,
                se.Weight,
                new ExerciseDto(
                    se.Exercise.Id,
                    se.Exercise.Name,
                    se.Exercise.Description,
                    se.Exercise.ExerciseType
                ))).ToListAsync();

            if (exercises is null || exercises.Count == 0)
            {
                return Result<GetAllSessionExercisesResponse>.Failure("No exercises found for session with id of " + request);
            }


            GetAllSessionExercisesResponse response = new(exercises);
            return Result<GetAllSessionExercisesResponse>.Success(response);
        }
    }
}
