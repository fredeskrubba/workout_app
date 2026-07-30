using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Routines.Features.CreateRoutine;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine
{
    public class AddExerciseToRoutine
    {
        private readonly WorkoutApplicationDBContext _context;

        public AddExerciseToRoutine(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<AddExerciseToRoutineResponse>> Handle(AddExerciseToRoutineRequest request, int routineId, int loggedInUserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == loggedInUserId);

            if (user is null)
            {
                return Result<AddExerciseToRoutineResponse>.Failure("User not found");
            }

            var routine = await _context.Routines
            .FirstOrDefaultAsync(x =>
                x.RoutineId == routineId &&
                x.UserId == loggedInUserId);

            if (routine == null)
            {
                return Result<AddExerciseToRoutineResponse>.Failure("Routine not found");
            }

            var exercise = await _context.Exercises.FindAsync(request.ExerciseId);

            if (exercise == null)
            {
                return Result<AddExerciseToRoutineResponse>.Failure("Exercise not found");
            }

            RoutineExercise exerciseToAdd = new RoutineExercise()
            {
                ExerciseId = exercise.Id,
                RoutineId = routineId,
                Reps = request.Reps,
                Weight = request.Weight,
                Sets = request.Sets,
                SeatSetting = request.SeatSetting
            };

            _context.RoutineExercises.Add(exerciseToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<AddExerciseToRoutineResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            AddExerciseToRoutineResponse response = new();

            return Result<AddExerciseToRoutineResponse>.Success(response);
        }
    }
}
