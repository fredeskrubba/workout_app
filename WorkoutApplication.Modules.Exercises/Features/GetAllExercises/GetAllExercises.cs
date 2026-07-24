using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Exercises.Features.GetAllExercises
{
    public class GetAllExercises
    {
        private readonly WorkoutApplicationDBContext _context;

        public GetAllExercises(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<GetAllExercisesResponse>> Handle(GetAllExercisesRequest request)
        {
            
            var exercises = await _context.Exercises.ToListAsync();

            if (exercises is null || exercises.Count == 0)
            {
                return Result<GetAllExercisesResponse>.Failure("No exercises found");
            }


            GetAllExercisesResponse response = new(exercises);
            return Result<GetAllExercisesResponse>.Success(response);
        }
    }
}
